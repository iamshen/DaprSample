﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Idsrv4.Admin.Shared.Configuration.Configuration.Identity;

namespace Idsrv4.Admin.STS.Identity.Helpers;

public class UserResolver<TUser> where TUser : class
{
    private readonly LoginResolutionPolicy _policy;
    private readonly UserManager<TUser> _userManager;

    public UserResolver(UserManager<TUser> userManager, LoginConfiguration configuration)
    {
        _userManager = userManager;
        _policy = configuration.ResolutionPolicy;
    }

    public async Task<TUser> GetUserAsync(string login)
    {
        switch (_policy)
        {
            case LoginResolutionPolicy.Username:
                return await _userManager.FindByNameAsync(login);
            case LoginResolutionPolicy.Email:
                return await _userManager.FindByEmailAsync(login);
            default:
                return null;
        }
    }
}