using System.Reflection;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi;

using SyncedHealth.Center.Platform.AuditCompliance.Application.CommandServices;
using SyncedHealth.Center.Platform.AuditCompliance.Application.Internal.CommandServices;
using SyncedHealth.Center.Platform.AuditCompliance.Application.Internal.QueryServices;
using SyncedHealth.Center.Platform.AuditCompliance.Application.QueryServices;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Repositories;
using SyncedHealth.Center.Platform.AuditCompliance.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.CommandServices;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.Internal.CommandServices;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.Internal.QueryServices;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.QueryServices;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Repositories;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Resources;

using SyncedHealth.Center.Platform.Iam.Application.Acl;
using SyncedHealth.Center.Platform.Iam.Interfaces.Acl;
using SyncedHealth.Center.Platform.Iam.Application.CommandServices;
using SyncedHealth.Center.Platform.Iam.Application.Internal.CommandServices;
using SyncedHealth.Center.Platform.Iam.Application.Internal.OutboundServices;
using SyncedHealth.Center.Platform.Iam.Application.Internal.QueryServices;
using SyncedHealth.Center.Platform.Iam.Application.OutboundServices;
using SyncedHealth.Center.Platform.Iam.Application.QueryServices;
using SyncedHealth.Center.Platform.Iam.Domain.Repositories;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Email.Resend.Configuration;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Email.Resend.Services;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Hashing.BCrypt.Services;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Tokens.Jwt.Services;
using SyncedHealth.Center.Platform.Iam.Resources;

using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Mediator.Cortex.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;
using SyncedHealth.Center.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SyncedHealth.Center.Platform.Shared.Resources;
using SyncedHealth.Center.Platform.Shared.Resources.Errors;

using SyncedHealth.Center.Platform.ShiftCoordination.Application.CommandServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.Internal.CommandServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.Internal.QueryServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.QueryServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;
using SyncedHealth.Center.Platform.ShiftCoordination.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.ShiftCoordination.Resources;

using SyncedHealth.Center.Platform.StaffRecovery.Application.CommandServices;
using SyncedHealth.Center.Platform.StaffRecovery.Application.Internal.CommandServices;
using SyncedHealth.Center.Platform.StaffRecovery.Application.Internal.QueryServices;
using SyncedHealth.Center.Platform.StaffRecovery.Application.QueryServices;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Repositories;
using SyncedHealth.Center.Platform.StaffRecovery.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.StaffRecovery.Resources;

using SyncedHealth.Center.Platform.Subscription.Application.CommandServices;
using SyncedHealth.Center.Platform.Subscription.Application.Internal.CommandServices;
using SyncedHealth.Center.Platform.Subscription.Application.Internal.OutboundServices;
using SyncedHealth.Center.Platform.Subscription.Application.Internal.QueryServices;
using SyncedHealth.Center.Platform.Subscription.Application.OutboundServices;
using SyncedHealth.Center.Platform.Subscription.Application.QueryServices;
using SyncedHealth.Center.Platform.Subscription.Domain.Repositories;
using SyncedHealth.Center.Platform.Subscription.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.Subscription.Infrastructure.Stripe.Configuration;
using SyncedHealth.Center.Platform.Subscription.Resources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

// Add ProblemDetails services
builder.Services.AddProblemDetails();

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Configure Database Context and route EF logs through the app logger pipeline.
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");

    if (string.IsNullOrWhiteSpace(connectionStringTemplate))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);

    if (connectionString.Contains('%'))
    {
        var missingVariables = new List<string>();

        if (connectionString.Contains("%DATABASE_URL%"))
            missingVariables.Add("DATABASE_URL");

        if (connectionString.Contains("%DATABASE_PORT%"))
            missingVariables.Add("DATABASE_PORT");

        if (connectionString.Contains("%DATABASE_SCHEMA%"))
            missingVariables.Add("DATABASE_SCHEMA");

        if (connectionString.Contains("%DATABASE_USER%"))
            missingVariables.Add("DATABASE_USER");

        if (connectionString.Contains("%DATABASE_PASSWORD%"))
            missingVariables.Add("DATABASE_PASSWORD");

        throw new InvalidOperationException(
            $"Database connection string contains unresolved environment variables: {string.Join(", ", missingVariables)}");
    }

    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    options.UseMySQL(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

// Localization
builder.Services.AddLocalization();

// Explicitly register IStringLocalizer
builder.Services.AddSingleton<IStringLocalizer<ErrorMessages>, StringLocalizer<ErrorMessages>>();
builder.Services.AddSingleton<IStringLocalizer<CommonMessages>, StringLocalizer<CommonMessages>>();
builder.Services.AddSingleton<IStringLocalizer<IamMessages>, StringLocalizer<IamMessages>>();
builder.Services.AddSingleton<IStringLocalizer<ClinicalRiskAssessmentMessages>, StringLocalizer<ClinicalRiskAssessmentMessages>>();
builder.Services.AddSingleton<IStringLocalizer<ShiftCoordinationMessages>, StringLocalizer<ShiftCoordinationMessages>>();
builder.Services.AddSingleton<IStringLocalizer<StaffRecoveryMessages>, StringLocalizer<StaffRecoveryMessages>>();
builder.Services.AddSingleton<IStringLocalizer<SubscriptionMessages>, StringLocalizer<SubscriptionMessages>>();

// Register the custom ProblemDetailsFactory
builder.Services.AddSingleton<ProblemDetailsFactory>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "SyncedHealth.Center.Platform",
            Version = "v1",
            Description = "SyncedHealth CortiSense Platform API",
            TermsOfService = new Uri("https://acme-learning.com/tos"),
            Contact = new OpenApiContact
            {
                Name = "SyncedHealth",
                Email = "contact@syncedhealth.com"
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

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });

    options.EnableAnnotations();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// Dependency Injection

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// IAM Bounded Context Injection Configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.Configure<ResendSettings>(builder.Configuration.GetSection("Resend"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IInvitationRepository, InvitationRepository>();

builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();

builder.Services.AddScoped<IOrganizationCommandService, OrganizationCommandService>();
builder.Services.AddScoped<IOrganizationQueryService, OrganizationQueryService>();

builder.Services.AddScoped<IInvitationCommandService, InvitationCommandService>();
builder.Services.AddScoped<IInvitationQueryService, InvitationQueryService>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

builder.Services.AddHttpClient<IInvitationEmailService, ResendInvitationEmailService>();

// Clinical Risk Assessment Bounded Context Injection Configuration
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

// Shift Coordination Bounded Context Injection Configuration
builder.Services.AddScoped<IShiftRecordRepository, ShiftRecordRepository>();
builder.Services.AddScoped<IWorkAreaRepository, WorkAreaRepository>();
builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
builder.Services.AddScoped<ICareTeamRepository, CareTeamRepository>();
builder.Services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();

builder.Services.AddScoped<IShiftRecordQueryService, ShiftRecordQueryService>();
builder.Services.AddScoped<IWorkAreaQueryService, WorkAreaQueryService>();
builder.Services.AddScoped<ISpecialtyQueryService, SpecialtyQueryService>();
builder.Services.AddScoped<ICareTeamQueryService, CareTeamQueryService>();
builder.Services.AddScoped<ITeamMemberQueryService, TeamMemberQueryService>();

builder.Services.AddScoped<IShiftRecordCommandService, ShiftRecordCommandService>();
builder.Services.AddScoped<IWorkAreaCommandService, WorkAreaCommandService>();
builder.Services.AddScoped<ISpecialtyCommandService, SpecialtyCommandService>();
builder.Services.AddScoped<ICareTeamCommandService, CareTeamCommandService>();
builder.Services.AddScoped<ITeamMemberCommandService, TeamMemberCommandService>();

// Staff Recovery Bounded Context Injection Configuration
builder.Services.AddScoped<IRecoveryPlanRepository, RecoveryPlanRepository>();

builder.Services.AddScoped<IRecoveryPlanQueryService, RecoveryPlanQueryService>();

builder.Services.AddScoped<IRecoveryPlanCommandService, RecoveryPlanCommandService>();

// Subscription Bounded Context Injection Configuration
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.PostConfigure<StripeSettings>(settings =>
{
    var envKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");
    if (!string.IsNullOrWhiteSpace(envKey))
        settings.SecretKey = envKey;
});

builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<ICheckoutSessionRepository, CheckoutSessionRepository>();

builder.Services.AddScoped<IPlanQueryService, PlanQueryService>();
builder.Services.AddScoped<ISubscriptionQueryService, SubscriptionQueryService>();
builder.Services.AddScoped<ICheckoutSessionQueryService, CheckoutSessionQueryService>();

builder.Services.AddScoped<ISubscriptionCommandService, SubscriptionCommandService>();
builder.Services.AddScoped<ICheckoutSessionCommandService, CheckoutSessionCommandService>();

builder.Services.AddScoped<IStripeBillingService, StripeBillingService>();

// Audit Compliance Bounded Context Injection Configuration
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();

builder.Services.AddScoped<IAuditLogQueryService, AuditLogQueryService>();
builder.Services.AddScoped<IAuditLogCommandService, AuditLogCommandService>();

// Mediator Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

builder.Services.AddCortexMediator(
    [typeof(Program)]);

var app = builder.Build();

// Automatic migrations are disabled.
// Database schema changes should be applied manually with:
// dotnet ef database update
//
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.AppDbContext>();
    context.Database.Migrate();
    
    // Purge spammy audit logs on startup
    var logsToPurge = context.Set<SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates.AuditLog>().Where(l => l.Type == SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditLogType.RiskAssessmentEvaluated);
    context.Set<SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates.AuditLog>().RemoveRange(logsToPurge);
    
    // Check if there are any logs left, if not, create a seed one
    if (!context.Set<SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates.AuditLog>().Any())
    {
        var dummyLog = new SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates.AuditLog(
            new SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands.CreateAuditLogCommand(
                1, 1, 
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditLogType.SystemAccessed, 
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditSeverity.Info, 
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditResourceType.System, 
                1, 
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditActionSource.Iam, 
                "El sistema fue actualizado para registrar acciones administrativas."
            )
        );
        context.Set<SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates.AuditLog>().Add(dummyLog);
    }

    context.SaveChanges();
}

// Configure the HTTP request pipeline.
app.UseGlobalExceptionHandler();

var supportedCultures = new[] { "en", "es" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

// Swagger enabled for current hosted environment.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/", () => Results.Redirect("/swagger"));
// }

app.UseHttpsRedirection();

// UseRouting makes the endpoint available via context.GetEndpoint()
app.UseRouting();

// Apply CORS Policy
app.UseCors("AllowAllPolicy");

// UseAuthorization must come before the custom middleware
app.UseAuthorization();

// Custom middleware runs AFTER UseRouting so GetEndpoint() is populated
// and [AllowAnonymous] attributes are correctly detected
app.UseRequestAuthorization();

// Map controllers last — they are the terminal handler
app.MapControllers();

app.Run();