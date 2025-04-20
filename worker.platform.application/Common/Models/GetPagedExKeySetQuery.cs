using System.Linq.Expressions;
using System.Security.AccessControl;

namespace worker.platform.application.Common.Models;

public class GetPagedExKeySetQuery<TKey, T>

{
    public TKey LastKey { get; set; }
    public int PageSize { get; set; } = 20;
    public Expression<Func<T, TKey>> KeySelector { get; set; } = default!;
}

