using Acme.BookStore.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Acme.BookStore.Settings;

public class BookStoreSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
        new SettingDefinition(
                  BookStoreSettingNames.MaxBooksPerPage,
                  defaultValue: "10",
                  displayName: LocalizableString.Create<BookStoreResource>("MaxBooksPerPage"),
                    description: LocalizableString.Create<BookStoreResource>("MaxBooksPerPageDescription")
                  ),


                 new SettingDefinition(
                  BookStoreSettingNames.EnableFavorites,
                  defaultValue: "true",
                    displayName: LocalizableString.Create<BookStoreResource>("EnableFavorites")
                    ),

                 new SettingDefinition(
                  BookStoreSettingNames.WelcomeMessage,
                  defaultValue: "Welcome to Acme Book Store!",
                    displayName: LocalizableString.Create<BookStoreResource>("WelcomeMessage")
                    )
                );
    }
}
