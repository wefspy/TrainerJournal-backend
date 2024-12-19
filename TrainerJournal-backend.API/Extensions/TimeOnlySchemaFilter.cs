using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TrainerJournal_backend.API.Extensions;

public class TimeOnlySchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(TimeOnly))
        {
            schema.Type = "string";
            schema.Format = "time";
            schema.Example = new OpenApiString("14:30:00"); // Пример значения
        }
    }
}