using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.CommandServices;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.Internal.CommandServices;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.Internal.QueryServices;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.QueryServices;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Repositories;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Resources;
using SyncedHealth.Center.Platform.Iam.Application.Acl;
using SyncedHealth.Center.Platform.Iam.Application.CommandServices;
using SyncedHealth.Center.Platform.Iam.Application.Internal.CommandServices;
using SyncedHealth.Center.Platform.Iam.Application.Internal.OutboundServices;
using SyncedHealth.Center.Platform.Iam.Application.Internal.QueryServices;
using SyncedHealth.Center.Platform.Iam.Application.QueryServices;
using SyncedHealth.Center.Platform.Iam.Domain.Repositories;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Hashing.BCrypt.Services;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Tokens.Jwt.Services;
using SyncedHealth.Center.Platform.Iam.Interfaces.Acl;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using ProblemDetailsFactory = SyncedHealth.Center.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddDataAnnotationsLocalization();

builder.Services.AddProblemDetails();

// Add localization
builder.Services.AddLocalization();

// Add Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null)
{
    throw new InvalidOperationException("Connection string not found.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Configuration.GetValue<bool>("UseInMemoryDatabase"))
    {
        options.UseInMemoryDatabase("CortisenseDb");
    }
    else
    {
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
});

// Problem Details Factory
builder.Services.AddSingleton<ProblemDetailsFactory>();

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// IAM Bounded Context Injection Configuration

// Token Settings Configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

// Clinical Risk Assessment Bounded Context Injection Configuration
builder.Services.AddSingleton<IStringLocalizer<ClinicalRiskAssessmentMessages>, StringLocalizer<ClinicalRiskAssessmentMessages>>();

builder.Services.AddScoped<IRiskAssessmentRepository, RiskAssessmentRepository>();
builder.Services.AddScoped<IClinicalAlertRepository, ClinicalAlertRepository>();
builder.Services.AddScoped<IVitalSignAnomalyRepository, VitalSignAnomalyRepository>();
builder.Services.AddScoped<IVitalSignReadingRepository, VitalSignReadingRepository>();

builder.Services.AddScoped<IRiskAssessmentQueryService, RiskAssessmentQueryService>();
builder.Services.AddScoped<IClinicalAlertQueryService, ClinicalAlertQueryService>();
builder.Services.AddScoped<IVitalSignAnomalyQueryService, VitalSignAnomalyQueryService>();
builder.Services.AddScoped<IVitalSignReadingQueryService, VitalSignReadingQueryService>();

builder.Services.AddScoped<IRiskAssessmentCommandService, RiskAssessmentCommandService>();
builder.Services.AddScoped<IClinicalAlertCommandService, ClinicalAlertCommandService>();
builder.Services.AddScoped<IVitalSignAnomalyCommandService, VitalSignAnomalyCommandService>();
builder.Services.AddScoped<IVitalSignReadingCommandService, VitalSignReadingCommandService>();

var app = builder.Build();

// Configure localization
var supportedCultures = new[] { "en", "es" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();