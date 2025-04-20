using System.Text.Json;
using System.Text.Json.Serialization;
using worker.platform.domain.Entities;

namespace worker.platform.domain.Converters;

public class JobParamsConverter: JsonConverter<IJobCategoryParams>
{
    public override IJobCategoryParams? Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var document = JsonDocument.ParseValue(ref reader);

        var root = document.RootElement;

        // Determine the type based on a discriminator (e.g., JobCategoryId)
        if (root.TryGetProperty(nameof(IJobCategoryParams.JobCategoryId), out var jobCategoryIdElement))
        {
            var jobCategoryId = jobCategoryIdElement.GetInt32();

            // Deserialize based on JobCategoryId
            return jobCategoryId switch
            {
                _ => throw new NotSupportedException($"JobCategoryId {jobCategoryId} is not supported.")
            };
        }

        throw new JsonException("JobCategoryId is missing.");
    }

    public override void Write(Utf8JsonWriter writer, IJobCategoryParams value, JsonSerializerOptions options) => JsonSerializer.Serialize(writer, value, value.GetType(), options);
}
