using Acme.BookStore.Web.Components.TenantSettingsButton;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;

namespace Acme.BookStore.Web.Pages.Settings;

public class BookStoreSettingsToolbarContributor : IPageToolbarContributor
{
    public Task ContributeAsync(PageToolbarContributionContext context)
    {
        context.Items.Insert(
            0,
            new PageToolbarItem(typeof(TenantSettingsButtonViewComponent))
        );
        return Task.CompletedTask;
    }
}