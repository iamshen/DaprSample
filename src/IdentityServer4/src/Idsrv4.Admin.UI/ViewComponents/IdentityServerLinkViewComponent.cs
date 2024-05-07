using Microsoft.AspNetCore.Mvc;
using Idsrv4.Admin.UI.Configuration;

namespace Idsrv4.Admin.UI.ViewComponents;

public class IdentityServerLinkViewComponent : ViewComponent
{
    private readonly AdminConfiguration _configuration;

    public IdentityServerLinkViewComponent(AdminConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IViewComponentResult Invoke()
    {
        var identityServerUrl = _configuration.IdentityServerBaseUrl;

        return View(model: identityServerUrl);
    }
}