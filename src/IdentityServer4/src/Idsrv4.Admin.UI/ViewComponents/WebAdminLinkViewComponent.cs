using Microsoft.AspNetCore.Mvc;
using Idsrv4.Admin.UI.Configuration;

namespace Idsrv4.Admin.UI.ViewComponents;

public class WebAdminLinkViewComponent : ViewComponent
{
    private readonly AdminConfiguration _configuration;

    public WebAdminLinkViewComponent(AdminConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IViewComponentResult Invoke()
    {
        return View(model: _configuration.WebAdminBaseUrl);
    }
}