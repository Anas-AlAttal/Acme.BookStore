// src/Acme.BookStore.Application/Settings/BookStoreSettingsAppService.cs
using Acme.BookStore.Settings;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.SettingManagement;

namespace Acme.BookStore.Application.Settings;

public class BookStoreSettingsAppService
    : ApplicationService, IBookStoreSettingsAppService
{
    private readonly ISettingManager _settingManager;

    public BookStoreSettingsAppService(ISettingManager settingManager)
    {
        _settingManager = settingManager;
    }

    public async Task<BookStoreSettingsDto> GetGlobalSettingsAsync()
    {
        return new BookStoreSettingsDto
        {
            SmtpHost = await _settingManager
                .GetOrNullGlobalAsync(BookStoreSettings.SmtpHost),

            MaxBooksCount = int.Parse(
                await _settingManager.GetOrNullGlobalAsync(
                    BookStoreSettings.MaxBooksCount) ?? "100"),

            MaxUploadSizeMb = int.Parse(
                await _settingManager.GetOrNullGlobalAsync(
                    BookStoreSettings.MaxUploadSizeMb) ?? "10"),

            MaxAuthorsCount = int.Parse(
                await _settingManager.GetOrNullGlobalAsync(
                    BookStoreSettings.MaxAuthorsCount) ?? "50"),
        };
    }

    public async Task<BookStoreSettingsDto> GetTenantSettingsAsync(Guid tenantId)
    {
        return new BookStoreSettingsDto
        {
            SmtpHost = await _settingManager
                .GetOrNullForTenantAsync(BookStoreSettings.SmtpHost, tenantId),

            MaxBooksCount = int.Parse(
                await _settingManager.GetOrNullForTenantAsync(
                    BookStoreSettings.MaxBooksCount, tenantId) ?? "100"),

            MaxUploadSizeMb = int.Parse(
                await _settingManager.GetOrNullForTenantAsync(
                    BookStoreSettings.MaxUploadSizeMb, tenantId) ?? "10"),

            MaxAuthorsCount = int.Parse(
                await _settingManager.GetOrNullForTenantAsync(
                    BookStoreSettings.MaxAuthorsCount, tenantId) ?? "50"),
        };
    }

    public async Task UpdateGlobalSettingsAsync(UpdateBookStoreSettingsDto updatedSettings)
    {
        await _settingManager.SetGlobalAsync(
            BookStoreSettings.SmtpHost,
            updatedSettings.SmtpHost);

        await _settingManager.SetGlobalAsync(
            BookStoreSettings.MaxBooksCount,
            updatedSettings.MaxBooksCount.ToString());

        await _settingManager.SetGlobalAsync(
            BookStoreSettings.MaxUploadSizeMb,
            updatedSettings.MaxUploadSizeMb.ToString());

        await _settingManager.SetGlobalAsync(
            BookStoreSettings.MaxAuthorsCount,
            updatedSettings.MaxAuthorsCount.ToString());
    }

    public async Task UpdateTenantSettingsAsync(
        Guid tenantId,
        UpdateBookStoreSettingsDto updatedSettings)
    {
        await _settingManager.SetForTenantAsync(
            tenantId,
            BookStoreSettings.SmtpHost,
            updatedSettings.SmtpHost);

        await _settingManager.SetForTenantAsync(
            tenantId,
            BookStoreSettings.MaxBooksCount,
            updatedSettings.MaxBooksCount.ToString());

        await _settingManager.SetForTenantAsync(
            tenantId,
            BookStoreSettings.MaxUploadSizeMb,
            updatedSettings.MaxUploadSizeMb.ToString());

        await _settingManager.SetForTenantAsync(
            tenantId,
            BookStoreSettings.MaxAuthorsCount,
            updatedSettings.MaxAuthorsCount.ToString());
    }
}