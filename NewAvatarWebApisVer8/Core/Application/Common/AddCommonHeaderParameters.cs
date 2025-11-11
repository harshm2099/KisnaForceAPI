using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NewAvatarWebApis.Core.Application.Common
{
    public class AddCommonHeaderParameters : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            // Remove any auto-generated parameters for CommonHeader (usually as query)
            var toRemove = operation.Parameters
                .Where(p => p.Name.Equals("header", StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var p in toRemove)
            {
                operation.Parameters.Remove(p);
            }

            // Add CommonHeader properties explicitly as headers
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "devicetype",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema { Type = "string" }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "devicename",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema { Type = "string" }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "appversion",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema { Type = "string" }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "orgtype",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema { Type = "string" }
            });
            // Add Authorization header for JWT
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-Authentication",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema { Type = "string" }
            });
        }
    }
}
