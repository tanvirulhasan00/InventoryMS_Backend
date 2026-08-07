using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using InventoryMS.Database.Data;
using InventoryMS.Models.Entities.ApplicationUserModel;
using InventoryMS.Services.ServiceModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHealthChecks();
builder.Services.AddAuthorization();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


// DbContext
builder.Services.AddDbContext<InventoryMSDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("LocalConnectionString"))

);

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<InventoryMSDbContext>()
    .AddDefaultTokenProviders();

// scopes

// OpenAPI (default .NET 10) - JSON only
builder.Services.AddEndpointsApiExplorer(); // gnerates /openapi/v1.json


//api versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
builder.Services.AddApiVersioning()
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
// Register one OpenAPI document per discovered API version
var apiVersionDescriptionProvider = builder.Services.BuildServiceProvider()
    .GetRequiredService<IApiVersionDescriptionProvider>();

foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
{
    builder.Services.AddOpenApi(description.GroupName, options =>
    {
        options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_1;
        options.AddDocumentTransformer<SecuritySchemeDocumentTransformer>();
        options.AddOperationTransformer<SecurityRequirementOperationTransformer>();
    });
} 




// ===== JWT Authentication =====

var key = builder.Configuration.GetValue<string>("TokenSetting:SecretKey") ?? "";
var tokenValidationParams = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
    ValidateIssuer = false,
    ValidateAudience = false,
    ClockSkew = TimeSpan.Zero
};

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = tokenValidationParams;
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // Read toke   
                var accessToken = context.HttpContext.Request.Cookies["access_token"];
                if (!string.IsNullOrEmpty(accessToken))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

// ===== CORS =====
const string allowedOrigin = "http://localhost:7186";

builder.Services.AddCors(options =>
{

    options.AddPolicy("AllowCors", policy =>
    {
        policy.WithOrigins(
                allowedOrigin
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        //options.SwaggerEndpoint("/openapi/v1.json", "InventoryMS v1");
        //options.SwaggerEndpoint("/openapi/v2.json", "InventoryMS v2");
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/openapi/{description.GroupName}.json", description.GroupName);
        }
        //options.OAuthUsePkce();
    });

}
// Redirect root URL to Swagger UI
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
