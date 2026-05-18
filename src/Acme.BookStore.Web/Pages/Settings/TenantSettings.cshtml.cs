// src/Acme.BookStore.Web/Pages/Settings/TenantSettings.cshtml.cs
using Acme.BookStore.Permissions;
using Acme.BookStore.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Saas.Host;

namespace Acme.BookStore.Web.Pages.Settings;

[Authorize(BookStorePermissions.Settings.ManageTenantSettings)]
public class TenantSettingsModel : BookStorePageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid TenantId { get; set; }

    [BindProperty]
    public UpdateBookStoreSettingsDto Settings { get; set; }

    public string TenantName { get; set; }
    public BookStoreSettingsDto GlobalSettings { get; set; }

    private readonly IBookStoreSettingsAppService _settingsAppService;
    private readonly ITenantAppService _tenantAppService;

    public TenantSettingsModel(
        IBookStoreSettingsAppService settingsAppService,
        ITenantAppService tenantAppService)
    {
        _settingsAppService = settingsAppService;
        _tenantAppService = tenantAppService;
    }

    public async Task OnGetAsync()
    {
        var tenant = await _tenantAppService.GetAsync(TenantId);
        TenantName = tenant.Name;

        GlobalSettings = await _settingsAppService.GetGlobalSettingsAsync();

        var current = await _settingsAppService.GetTenantSettingsAsync(TenantId);
        Settings = new UpdateBookStoreSettingsDto
        {
            SmtpHost = current.SmtpHost,
            MaxBooksCount = current.MaxBooksCount,
            MaxUploadSizeMb = current.MaxUploadSizeMb,
            MaxAuthorsCount = current.MaxAuthorsCount
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _settingsAppService.UpdateTenantSettingsAsync(TenantId, Settings);
        Alerts.Success(L["SavedSuccessfully"]);
        return RedirectToPage(new { TenantId });
    }
}