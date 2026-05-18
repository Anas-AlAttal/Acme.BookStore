using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.BookStore.Web.Components.TenantSettingsButton;

public class TenantSettingsButtonViewComponent : AbpViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("~/Components/TenantSettingsButton/Default.cshtml");
    }
}