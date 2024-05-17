using Microsoft.AspNetCore.Mvc;

namespace Idsrv4.Admin.STS.Identity.ViewComponents;

public class WebAdminLinkViewComponent : ViewComponent
{
    private readonly IRootConfiguration _configuration;

    public WebAdminLinkViewComponent(IRootConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IViewComponentResult Invoke()
    {
        return View(model: _configuration.AdminConfiguration.WebAdminBaseUrl);
    }
}