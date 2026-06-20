$basePath = "C:\Users\marsr\cortisense-platform\SyncedHealth.Center.Platform"

# --- IAM Application & Interfaces ---

# Resources
Set-Content -Path "$basePath\IAM\Interfaces\REST\Resources\SignUpResource.cs" -Value @"
namespace SyncedHealth.Center.Platform.IAM.Interfaces.REST.Resources;

public record SignUpResource(string Username, string Password);
"@

Set-Content -Path "$basePath\IAM\Interfaces\REST\Resources\SignInResource.cs" -Value @"
namespace SyncedHealth.Center.Platform.IAM.Interfaces.REST.Resources;

public record SignInResource(string Username, string Password);
"@

Set-Content -Path "$basePath\IAM\Interfaces\REST\Resources\AuthenticatedUserResource.cs" -Value @"
namespace SyncedHealth.Center.Platform.IAM.Interfaces.REST.Resources;

public record AuthenticatedUserResource(int Id, string Username, string Token);
"@

# Commands
Set-Content -Path "$basePath\IAM\Application\Internal\CommandServices\SignUpCommand.cs" -Value @"
namespace SyncedHealth.Center.Platform.IAM.Application.Internal.CommandServices;

public record SignUpCommand(string Username, string Password);
"@

Set-Content -Path "$basePath\IAM\Application\Internal\CommandServices\SignInCommand.cs" -Value @"
namespace SyncedHealth.Center.Platform.IAM.Application.Internal.CommandServices;

public record SignInCommand(string Username, string Password);
"@

# Services Interfaces
Set-Content -Path "$basePath\IAM\Domain\Services\IUserCommandService.cs" -Value @"
using SyncedHealth.Center.Platform.IAM.Application.Internal.CommandServices;

namespace SyncedHealth.Center.Platform.IAM.Domain.Services;

public interface IUserCommandService
{
    Task Handle(SignUpCommand command);
    Task<(int UserId, string Token)?> Handle(SignInCommand command);
}
"@

# Services Implementation
Set-Content -Path "$basePath\IAM\Application\Internal\CommandServices\UserCommandService.cs" -Value @"
using SyncedHealth.Center.Platform.IAM.Application.Internal.OutboundServices;
using SyncedHealth.Center.Platform.IAM.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.IAM.Domain.Repositories;
using SyncedHealth.Center.Platform.IAM.Domain.Services;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;

namespace SyncedHealth.Center.Platform.IAM.Application.Internal.CommandServices;

public class UserCommandService : IUserCommandService
{
    private readonly IUserRepository _userRepository;
    private readonly IHashingService _hashingService;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public UserCommandService(IUserRepository userRepository, IHashingService hashingService, ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _hashingService = hashingService;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(SignUpCommand command)
    {
        if (_userRepository.ExistsByUsername(command.Username))
            throw new Exception(`"Username is already taken.`");

        var hashedPassword = _hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashedPassword);

        await _userRepository.AddAsync(user);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<(int UserId, string Token)?> Handle(SignInCommand command)
    {
        var user = await _userRepository.FindByUsernameAsync(command.Username);
        if (user == null || !_hashingService.VerifyPassword(command.Password, user.PasswordHash))
            return null;

        var token = _tokenService.GenerateToken(user);
        return (user.Id, token);
    }
}
"@

# Controllers
Set-Content -Path "$basePath\IAM\Interfaces\REST\AuthenticationController.cs" -Value @"
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncedHealth.Center.Platform.IAM.Application.Internal.CommandServices;
using SyncedHealth.Center.Platform.IAM.Domain.Services;
using SyncedHealth.Center.Platform.IAM.Interfaces.REST.Resources;

namespace SyncedHealth.Center.Platform.IAM.Interfaces.REST;

[ApiController]
[Route(`"api/v1/[controller]`")]
public class AuthenticationController : ControllerBase
{
    private readonly IUserCommandService _userCommandService;

    public AuthenticationController(IUserCommandService userCommandService)
    {
        _userCommandService = userCommandService;
    }

    [AllowAnonymous]
    [HttpPost(`"sign-in`")]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
    {
        var command = new SignInCommand(resource.Username, resource.Password);
        var result = await _userCommandService.Handle(command);

        if (result == null)
            return Unauthorized(new { Message = `"Invalid credentials`" });

        var authenticatedUserResource = new AuthenticatedUserResource(result.Value.UserId, resource.Username, result.Value.Token);
        return Ok(authenticatedUserResource);
    }

    [AllowAnonymous]
    [HttpPost(`"sign-up`")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
    {
        var command = new SignUpCommand(resource.Username, resource.Password);
        await _userCommandService.Handle(command);

        return Ok(new { Message = `"User created successfully`" });
    }
}
"@

Write-Host "IAM App and REST layer generated."
