using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryMS.Services.ServiceModels
{
    internal sealed class SecuritySchemeDocumentTransformer : IOpenApiDocumentTransformer
    {
       
        public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            document.Components ??= new OpenApiComponents();
            document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

            document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            };

            document.Components.SecuritySchemes["Cookie"] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Cookie,
                Name = "Authorization.Cookies",
                Description = "Cookie-based authentication."
            };

            return Task.CompletedTask;
        }
    }

    internal sealed class SecurityRequirementOperationTransformer : IOpenApiOperationTransformer
    {
        public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
        {
            var hasAuthorize = context.Description.ActionDescriptor.EndpointMetadata
                .OfType<Microsoft.AspNetCore.Authorization.IAuthorizeData>()
                .Any();

            if (hasAuthorize)
            {
                operation.Security ??= new List<OpenApiSecurityRequirement>();
                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", context.Document)] = new List<string>()
                });
                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Cookie", context.Document)] = new List<string>()
                });
            }

            return Task.CompletedTask;
        }
    }
}
