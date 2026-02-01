using System.Globalization;
using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using worker.platform.application.Common;
using worker.platform.application.Common.Models;

namespace worker.platform.infrastructure.Common;

public abstract class RepositoryBase<T, TKey>(ApplicationDbContext applicationDbContext) : IRepositoryBase<T, TKey> where T : class
{
    protected DbSet<T> DbSet { get; } = applicationDbContext.Set<T>();

    public virtual Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => DbSet.ToListAsync(cancellationToken).ContinueWith(task => task.Result.AsEnumerable(), cancellationToken);

    public virtual ValueTask<T> FindAsync(TKey key, CancellationToken cancellationToken = default)
        => DbSet.FindAsync([key], cancellationToken);

public virtual async Task<IEnumerable<T>> GetPagedCursor(IQueryable<T> source,
    GetPagedKeySetQuery pageAndSortQuery,
    CancellationToken cancellationToken = default)
{
    var sortField = string.IsNullOrWhiteSpace(pageAndSortQuery.SortField) ? "Id" : pageAndSortQuery.SortField;

    var parameter = Expression.Parameter(typeof(T), "x");
    var sortProperty = Expression.PropertyOrField(parameter, sortField);
    var sortLambda = Expression.Lambda(sortProperty, parameter);

    var idProperty = Expression.PropertyOrField(parameter, "Id");
    var idLambda = Expression.Lambda(idProperty, parameter);

    if (!string.IsNullOrEmpty(pageAndSortQuery.LastKeyValue.ToString()) &&
        pageAndSortQuery.LastKeyValue.ValueKind == JsonValueKind.Object &&
        pageAndSortQuery.LastKeyValue.EnumerateObject().Any())
    {
        Expression filterExpression = null;

        if (pageAndSortQuery.LastKeyValue.TryGetProperty(sortField, out var lastSortValue))
        {
            var sortConstant = Expression.Constant(Convert.ChangeType(lastSortValue.ToString(), sortProperty.Type));

            // Create the comparison for the sort field
            if (pageAndSortQuery.SortDirection == SortDirection.Ascending)
            {
                filterExpression = Expression.GreaterThan(sortProperty, sortConstant);

                // Check if we also have an Id value for tie-breaking
                if (pageAndSortQuery.LastKeyValue.TryGetProperty("Id", out var lastIdValue))
                {
                    var idConstant = Expression.Constant(Convert.ChangeType(lastIdValue.ToString(), idProperty.Type, CultureInfo.InvariantCulture));

                    // If sort values are equal, use ID to break the tie
                    var equalExpr = Expression.Equal(sortProperty, sortConstant);
                    var idCompare = Expression.GreaterThan(idProperty, idConstant);
                    var combinedExpr = Expression.OrElse(
                        filterExpression,
                        Expression.AndAlso(equalExpr, idCompare)
                    );

                    filterExpression = combinedExpr;
                }
            }
            else // Descending
            {
                filterExpression = Expression.LessThan(sortProperty, sortConstant);

                // Check if we also have an Id value for tie-breaking
                if (pageAndSortQuery.LastKeyValue.TryGetProperty("Id", out var lastIdValue))
                {
                    var idConstant = Expression.Constant(Convert.ChangeType(lastIdValue.ToString(), idProperty.Type));

                    // If sort values are equal, use ID to break the tie
                    var equalExpr = Expression.Equal(sortProperty, sortConstant);
                    var idCompare = Expression.GreaterThan(idProperty, idConstant);
                    var combinedExpr = Expression.OrElse(
                        filterExpression,
                        Expression.AndAlso(equalExpr, idCompare)
                    );

                    filterExpression = combinedExpr;
                }
            }

            // Apply the filter
            if (filterExpression != null)
            {
                var filterLambda = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
                source = source.Where(filterLambda);
            }
        }
        else if (pageAndSortQuery.LastKeyValue.TryGetProperty("Id", out var lastIdValue))
        {
            var idConstant = Expression.Constant(Convert.ChangeType(lastIdValue.ToString(), idProperty.Type));
            var idCompare = Expression.GreaterThan(idProperty, idConstant);
            var filterLambda = Expression.Lambda<Func<T, bool>>(idCompare, parameter);
            source = source.Where(filterLambda);
        }
    }

    IOrderedEnumerable<T> sortedSource;
    if (pageAndSortQuery.SortDirection == SortDirection.Ascending)
    {
        sortedSource = source.OrderBy(Expression.Lambda<Func<T, object>>(
            Expression.Convert(sortProperty, typeof(object)), parameter).Compile());
    }
    else
    {
        sortedSource = source.OrderByDescending(Expression.Lambda<Func<T, object>>(
            Expression.Convert(sortProperty, typeof(object)), parameter).Compile());
    }

    // Apply secondary sorting
    var result = sortedSource.ThenBy(Expression.Lambda<Func<T, object>>(
        Expression.Convert(idProperty, typeof(object)), parameter).Compile());

    // Use the non-dynamic Take method
    return  result.Take(pageAndSortQuery.PageSize).ToList();
}



    public virtual void Add(T newEntity) => DbSet.Add(newEntity);

    public virtual void Update(T newEntity) => DbSet.Update(newEntity);

    public virtual void Remove(T newEntity) => DbSet.Remove(newEntity);

    public virtual Task AddRangeAsync(IEnumerable<T> newEntities, CancellationToken cancellationToken = default)
        => DbSet.AddRangeAsync(newEntities, cancellationToken);
}
