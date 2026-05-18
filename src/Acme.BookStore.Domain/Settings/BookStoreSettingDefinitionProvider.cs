using Acme.BookStore.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Acme.BookStore.Settings;

public class BookStoreSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(BookStoreSettings.MySetting1));
          context.Add(
               new SettingDefinition(
               BookStoreSettings.SmtpHost,
               defaultValue: "localhost",
               displayName: L("Setting:SmtpHost"),
               isVisibleToClients: false,
               isEncrypted: false
               //todo:revejion is visible to clients and is encrypted values
               ));

        context.Add(new SettingDefinition(
           BookStoreSettings.MaxBooksCount,
           defaultValue: "10",
           displayName: L("Setting:MaxBooksCount"),
           isVisibleToClients: true,
           isEncrypted: false
        ));

        context.Add(new SettingDefinition(
           BookStoreSettings.MaxUploadSizeMb,
           defaultValue: "5",
           displayName: L("Setting:MaxUploadSizeMb"),
           isVisibleToClients: true,
           isEncrypted: false
        ));

        context.Add(new SettingDefinition(
           BookStoreSettings.MaxAuthorsCount,
           defaultValue: "10",
           displayName: L("Setting:MaxAuthorsCount"),
           isVisibleToClients: true,
           isEncrypted: false
        ));
    }
    private static LocalizableString L(string name) => LocalizableString.Create<BookStoreResource>(name);

}
