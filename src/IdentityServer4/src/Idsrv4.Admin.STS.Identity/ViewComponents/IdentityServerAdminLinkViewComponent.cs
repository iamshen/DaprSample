﻿using Microsoft.AspNetCore.Mvc;

namespace Idsrv4.Admin.STS.Identity.ViewComponents;

public class IdentityServerAdminLinkViewComponent : ViewComponent
{
    private readonly IRootConfiguration _configuration;

    public IdentityServerAdminLinkViewComponent(IRootConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IViewComponentResult Invoke()
    {
        return View(model: _configuration.AdminConfiguration.IdentityAdminBaseUrl);
    }
}