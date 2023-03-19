using Microsoft.AspNetCore.Authorization;

namespace Cegeka.Auction.WebUI.Shared.Authorization;

public class PermissionAuthorizationRequirement : IAuthorizationRequirement
{
    public PermissionAuthorizationRequirement(Permissions permission)
    {
        Permissions = permission;
    }

    public Permissions Permissions { get; }
}
