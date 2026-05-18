// src/Acme.BookStore.Application.Contracts/Settings/IBookStoreSettingsAppService.cs
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Settings;

public interface IBookStoreSettingsAppService : IApplicationService
{
    Task<BookStoreSettingsDto> GetGlobalSettingsAsync();
    Task<BookStoreSettingsDto> GetTenantSettingsAsync(Guid tenantId);
    Task UpdateGlobalSettingsAsync(UpdateBookStoreSettingsDto updatedSettings);
    Task UpdateTenantSettingsAsync(Guid tenantId, UpdateBookStoreSettingsDto updatedSettings);
}