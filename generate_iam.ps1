$basePath = "C:\Users\marsr\cortisense-platform\SyncedHealth.Center.Platform"

# Directorios IAM
$dirs = @(
    "$basePath\IAM\Domain\Model\Aggregates",
    "$basePath\IAM\Domain\Model\Entities",
    "$basePath\IAM\Domain\Model\ValueObjects",
    "$basePath\IAM\Domain\Repositories",
    "$basePath\IAM\Domain\Services",
    "$basePath\IAM\Application\Internal\CommandServices",
    "$basePath\IAM\Application\Internal\QueryServices",
    "$basePath\IAM\Application\Internal\OutboundServices",
    "$basePath\IAM\Infrastructure\Persistence\EntityFrameworkCore\Repositories",
    "$basePath\IAM\Infrastructure\Persistence\EntityFrameworkCore\Configuration",
    "$basePath\IAM\Infrastructure\Hashing\BCrypt\Services",
    "$basePath\IAM\Infrastructure\Tokens\JWT\Services",
    "$basePath\IAM\Infrastructure\Tokens\JWT\Configuration",
    "$basePath\IAM\Interfaces\REST",
    "$basePath\IAM\Interfaces\REST\Resources",
    "$basePath\IAM\Interfaces\REST\Transform"
)

foreach ($dir in $dirs) {
    if (-not (Test-Path $dir)) {
        New-Item -ItemType Directory -Force -Path $dir | Out-Null
    }
}

# Archivos iniciales
Set-Content -Path "$basePath\IAM\Domain\Model\Aggregates\User.cs" -Value @"
using System.Text.Json.Serialization;

namespace SyncedHealth.Center.Platform.IAM.Domain.Model.Aggregates;

public class User
{
    public User()
    {
        Username = string.Empty;
        PasswordHash = string.Empty;
    }

    public User(string username, string passwordHash)
    {
        Username = username;
        PasswordHash = passwordHash;
    }

    public int Id { get; set; }
    public string Username { get; private set; }
    
    [JsonIgnore]
    public string PasswordHash { get; private set; }

    public User UpdateUsername(string username)
    {
        Username = username;
        return this;
    }

    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
}
"@

Set-Content -Path "$basePath\IAM\Domain\Repositories\IUserRepository.cs" -Value @"
using SyncedHealth.Center.Platform.IAM.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;

namespace SyncedHealth.Center.Platform.IAM.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByUsernameAsync(string username);
    bool ExistsByUsername(string username);
}
"@

Set-Content -Path "$basePath\IAM\Application\Internal\OutboundServices\IHashingService.cs" -Value @"
namespace SyncedHealth.Center.Platform.IAM.Application.Internal.OutboundServices;

public interface IHashingService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}
"@

Set-Content -Path "$basePath\IAM\Application\Internal\OutboundServices\ITokenService.cs" -Value @"
using SyncedHealth.Center.Platform.IAM.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.IAM.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    Task<int?> ValidateToken(string token);
}
"@

Set-Content -Path "$basePath\IAM\Domain\Services\IUserCommandService.cs" -Value @"
using SyncedHealth.Center.Platform.IAM.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.IAM.Domain.Services;

public interface IUserCommandService
{
    Task Handle(User user);
}
"@

Set-Content -Path "$basePath\IAM\Infrastructure\Hashing\BCrypt\Services\HashingService.cs" -Value @"
using SyncedHealth.Center.Platform.IAM.Application.Internal.OutboundServices;
using BCryptNet = BCrypt.Net.BCrypt;

namespace SyncedHealth.Center.Platform.IAM.Infrastructure.Hashing.BCrypt.Services;

public class HashingService : IHashingService
{
    public string HashPassword(string password)
    {
        return BCryptNet.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCryptNet.Verify(password, passwordHash);
    }
}
"@

Set-Content -Path "$basePath\IAM\Infrastructure\Persistence\EntityFrameworkCore\Configuration\IamModelBuilderExtensions.cs" -Value @"
using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.IAM.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.IAM.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public static class IamModelBuilderExtensions
{
    public static void ConfigureIamContext(this ModelBuilder builder)
    {
        builder.Entity<User>().ToTable(`"Users`");
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired().HasMaxLength(50);
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
    }
}
"@

Write-Host "IAM Structure generated."
