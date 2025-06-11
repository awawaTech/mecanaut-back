using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Configuration;

// ───── AssetManagement usings ─────
using AwawaTech.Mecanaut.API.AssetManagement.Infrastructure.Persistence.EFC.Repositories;
using AwawaTech.Mecanaut.API.AssetManagement.Application.Internal.CommandServices;
using AwawaTech.Mecanaut.API.AssetManagement.Application.Internal.QueryServices;
using AwawaTech.Mecanaut.API.AssetManagement.Application.ACL;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Repositories;
using AwawaTech.Mecanaut.API.AssetManagement.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using AwawaTech.Mecanaut.API.AssetManagement.Interfaces.ACL;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Tenancy;


var builder = WebApplication.CreateBuilder(args);

// ───────────── Controllers & routing ─────────────
builder.Services.AddRouting(o => o.LowercaseUrls = true);
builder.Services.AddControllers(o => o.Conventions.Add(new KebabCaseRouteNamingConvention()));

// ───────────── CORS ─────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        p => p.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// ───────────── DbContext (MySQL) ─────────────
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
               .LogTo(Console.WriteLine, LogLevel.Information)
               .EnableSensitiveDataLogging()
               .EnableDetailedErrors();
    else
        options.UseMySQL(connectionString)
               .LogTo(Console.WriteLine, LogLevel.Error);
});

// ───────────── Swagger / OpenAPI ─────────────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AwawaTech.Mecanaut.API",
        Version = "v1",
        Description = "AwawaTech Mecanaut API",
        TermsOfService = new Uri("https://awawatech.com/tos"),
        Contact = new OpenApiContact
        {
            Name = "AwawaTech",
            Email = "contact@awawatech.com"
        },
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme }
            },
            Array.Empty<string>()
        }
    });
    options.EnableAnnotations();
});

// ───────────── Dependency Injection ─────────────

// HttpContext accessor (for TenantProvider)
builder.Services.AddHttpContextAccessor();

// Tenant Provider
builder.Services.AddScoped<ITenantProvider, HttpTenantProvider>(); // implement HttpTenantProvider elsewhere

// Repositories
builder.Services.AddScoped<IPlantRepository, PlantRepository>();
builder.Services.AddScoped<IProductionLineRepository, ProductionLineRepository>();
builder.Services.AddScoped<IMachineryRepository, MachineryRepository>();

// Unit-of-Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Application Services
builder.Services.AddScoped<IPlantCommandService, PlantCommandService>();
builder.Services.AddScoped<IPlantQueryService, PlantQueryService>();
builder.Services.AddScoped<IProductionLineCommandService, ProductionLineCommandService>();
builder.Services.AddScoped<IProductionLineQueryService, ProductionLineQueryService>();
builder.Services.AddScoped<IMachineryCommandService, MachineryCommandService>();
builder.Services.AddScoped<IMachineryQueryService, MachineryQueryService>();

// ACL Facade
builder.Services.AddScoped<IAssetManagementContextFacade, AssetManagementContextFacade>();

// ───── (Other BC dependency registrations go here) ─────

// ───────────── Build & DB ensure ─────────────
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// ───────────── HTTP pipeline ─────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
