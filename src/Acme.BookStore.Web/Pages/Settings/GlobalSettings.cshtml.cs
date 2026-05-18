// src/Acme.BookStore.Web/Pages/Settings/GlobalSettings.cshtml.cs
using Acme.BookStore.Permissions;
using Acme.BookStore.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Acme.BookStore.Web.Pages.Settings;

[Authorize(BookStorePermissions.Settings.ManageGlobalSettings)]
public class GlobalSettingsModel : BookStorePageModel
{
    [BindProperty]
    public UpdateBookStoreSettingsDto Settings { get; set; }

    private readonly IBookStoreSettingsAppService _settingsAppService;

    public GlobalSettingsModel(IBookStoreSettingsAppService settingsAppService)
    {
        _settingsAppService = settingsAppService;
    }

    public async Task OnGetAsync()
    {
        var current = await _settingsAppService.GetGlobalSettingsAsync();
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
        await _settingsAppService.UpdateGlobalSettingsAsync(Settings);
        Alerts.Success(L["SavedSuccessfully"]);
        return RedirectToPage();
    }
}