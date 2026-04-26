using Volo.Abp.Features;
using Volo.Abp.Localization;
using Volo.Abp.Validation.StringValues;
using Acme.BookStore.Localization;

namespace Acme.BookStore.Features;

public class BookStoreFeatureDefinitionProvider : FeatureDefinitionProvider
{
    public override void Define(IFeatureDefinitionContext context)
    {
        var group = context.AddGroup("Acme.BookStore", LocalizableString.Create<BookStoreResource>("BookStoreFeatures"));

        group.AddFeature(
            name: "Acme.BookStore.Reporting",
            defaultValue: "false",
            displayName: LocalizableString.Create<BookStoreResource>("Reporting"),
            description: LocalizableString.Create<BookStoreResource>("ReportingDesc"),
            valueType: new ToggleStringValueType()   // Checkbox في الـ UI
        );

        group.AddFeature(
            name: "Acme.BookStore.MaxBookCount",
            defaultValue: "100",
            displayName: LocalizableString.Create<BookStoreResource>("MaxBookCount"),
            valueType: new FreeTextStringValueType(
                new NumericValueValidator(0, 1000000)
            )
        );

        group.AddFeature(
            name: "Acme.BookStore.Lending",
            defaultValue: "false",
            displayName: LocalizableString.Create<BookStoreResource>("Lending"),
            valueType: new ToggleStringValueType()
        );
    }
}