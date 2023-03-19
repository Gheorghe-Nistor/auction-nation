using Cegeka.Auction.Application.Common.Services.Identity;
using Cegeka.Auction.WebUI.Shared.AccessControl;

namespace Cegeka.Auction.Application.Roles.Commands;

public record CreateRoleCommand(RoleDto Role) : IRequest;

public class CreateRoleCommandHandler : AsyncRequestHandler<CreateRoleCommand>
{
    private readonly IIdentityService _identityService;

    public CreateRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    protected override async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        await _identityService.CreateRoleAsync(request.Role);
    }
}
