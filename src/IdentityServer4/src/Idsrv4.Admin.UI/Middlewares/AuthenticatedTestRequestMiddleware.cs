﻿using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Idsrv4.Admin.UI.Middlewares;

public class AuthenticatedTestRequestMiddleware
{
    public static readonly string TestAuthorizationHeader = "FakeAuthorization";
    private readonly RequestDelegate _next;

    public AuthenticatedTestRequestMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Headers.Keys.Contains(TestAuthorizationHeader))
        {
            var token = context.Request.Headers[TestAuthorizationHeader].FirstOrDefault();
            var jwt = new JwtSecurityToken(token);
            var claimsIdentity = new ClaimsIdentity(jwt.Claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            context.User = claimsPrincipal;
        }

        await _next(context);
    }
}