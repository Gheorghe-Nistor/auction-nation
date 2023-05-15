using Cegeka.Auction.Application.Common.Services.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Application.Users.Queries;

public record GetUserIdByUsernameQuery(string UserName) : IRequest<string>;

public class GetUserIdByUsernameQueryHandler : IRequestHandler<GetUserIdByUsernameQuery, string>
{
    private readonly IIdentityService _identityService;

    public GetUserIdByUsernameQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(GetUserIdByUsernameQuery request, CancellationToken cancellationToken)
    {
        var userId = await _identityService.GetUserIdByNameAsync(request.UserName);

        if (userId == null)
        {
            throw new UserNotFoundException($"User with username {request.UserName} not found.");
        }

        return userId;
    }
}

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string message) : base(message)
    {
    }
}
