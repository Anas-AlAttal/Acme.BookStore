using Acme.BookStore.Localization;
using Microsoft.IdentityModel.Tokens.Experimental;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Features;
using Volo.Abp.Localization;
using Volo.Abp.Validation.StringValues;

namespace Acme.BookStore.Feature
{
    public class BookStoreFeatureDefinitionProvider : FeatureDefinitionProvider
    {
        public override void Define(IFeatureDefinitionContext context)
        {
            var bookStoreGroup = context.AddGroup(BookStoreFeatures.GroupName, L("Feature:BookStore"));

            bookStoreGroup.AddFeature(
                BookStoreFeatures.AuthorManagement,
                defaultValue: "false",
                displayName: L("Feature:AuthorManagement"),
                valueType: new ToggleStringValueType()
            );

          var exportingFiles =  bookStoreGroup.AddFeature(
                BookStoreFeatures.ExportingFiles,
                defaultValue: "false",
                displayName: L("Feature:ExportingFiles"),
                valueType: new ToggleStringValueType()
            );
            exportingFiles.CreateChild(
                BookStoreFeatures.PdfExporting,
                defaultValue: "false",
                displayName: L("Feature:PdfExporting"),
                valueType: new ToggleStringValueType()
            );
            exportingFiles.CreateChild(
                BookStoreFeatures.ExcelExporting,
                defaultValue: "false",
                displayName: L("Feature:ExcelExporting"),
                valueType: new ToggleStringValueType()
            );
            bookStoreGroup.AddFeature(
                BookStoreFeatures.MaxBookCount,
                defaultValue: "10",
                displayName: L("Feature:MaxBookCount"),
                valueType: new FreeTextStringValueType(new NumericValueValidator(1, 9999))
            );

        }

        private static ILocalizableString? L(string n) => LocalizableString.Create<BookStoreResource>(n);

    }
}
