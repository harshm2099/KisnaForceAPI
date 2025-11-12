using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NewAvatarWebApis.Common;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.Common.OtpApi.Models;
using NewAvatarWebApis.Infrastructure.Services;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Presentation.Middlewares;
using OtpApi.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// To make routing lowercase
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
              .WithExposedHeaders("Authentication"));
});

// Adding Dependency Injection
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<ICustomerMarginService, CustomerMarginService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartBulkUploadService, CartBulkUploadService>();
builder.Services.AddScoped<ICommonService, CommonService>();
builder.Services.AddScoped<IInformationService, InformationService>();
builder.Services.AddScoped<IItemListingService, ItemListingService>();
builder.Services.AddScoped<IItemsService, ItemsService>();
builder.Services.AddScoped<IItemViewService, ItemViewService>();
builder.Services.AddScoped<IGoldService, GoldService>();
builder.Services.AddScoped<IHomeListService, HomeListService>();
builder.Services.AddScoped<IOrderCartService, OrderCartService>();
builder.Services.AddScoped<IOrderTrackListService, OrderTrackListService>();
builder.Services.AddScoped<IScreenSaverService, ScreenSaverService>();
builder.Services.AddScoped<IOthersService, OthersService>();
builder.Services.AddScoped<ISolicatListService, SolicatListService>();
builder.Services.AddScoped<IWishListService, WishListService>();
builder.Services.AddScoped<ISolitaireService, SolitaireService>();
builder.Services.AddScoped<ITeamsService, TeamsService>();
builder.Services.AddScoped<IWatchService, WatchService>();
builder.Services.AddScoped<IMasterService, MasterService>();

// Add services to the container.
builder.Services.AddControllers();

// Add API versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var apiVersionDescriptionProvider = builder.Services.BuildServiceProvider()
        .GetRequiredService<IApiVersionDescriptionProvider>();

    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
    {
        options.SwaggerDoc(description.GroupName, new OpenApiInfo
        {
            Title = $"New Admin Avatar Web APIs - {description.GroupName.ToUpperInvariant()}",
            Version = description.ApiVersion.ToString(),
            Description = description.IsDeprecated ? "This API version has been deprecated." : null,
        });
    }

    options.OperationFilter<AddCommonHeaderParameters>();
});

// Add TokenService and CommonHelpers
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<CommonHelpers>();
builder.Configuration.AddEnvironmentVariables();

// Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    // Secure token handling
    options.RequireHttpsMetadata = builder.Environment.IsDevelopment() ? false : true; // Secure in production
    options.SaveToken = true;

    // Fetch the JWT Key from environment variable
    var jwtKey = builder.Configuration["Jwt:Key"]
                     ?? Environment.GetEnvironmentVariable("JWT_SECRET_KEY")
                     ?? throw new InvalidOperationException("Jwt:Key is not found in configuration or environment variables!");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // You can fetch Issuer from configuration if needed
        ValidAudience = builder.Configuration["Jwt:Audience"], // Audience from config
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.Zero // No clock skew for exact expiration check
    };
});


var app = builder.Build();

// Swagger UI and HTTPS redirection
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");
app.UseMiddleware<JwtAndHeaderMiddleware>();

app.MapControllers();

app.Run();
