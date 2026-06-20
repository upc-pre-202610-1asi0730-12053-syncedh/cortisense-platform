using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.AuditCompliance.Application.CommandServices;
using SyncedHealth.Center.Platform.AuditCompliance.Application.Internal.CommandServices;
using SyncedHealth.Center.Platform.AuditCompliance.Application.Internal.QueryServices;
using SyncedHealth.Center.Platform.AuditCompliance.Application.QueryServices;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Repositories;
using SyncedHealth.Center.Platform.AuditCompliance.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

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

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Audit Compliance Bounded Context Injection Configuration
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddScoped<IAuditLogCommandService, AuditLogCommandService>();
builder.Services.AddScoped<IAuditLogQueryService, AuditLogQueryService>();

var app = builder.Build();

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
