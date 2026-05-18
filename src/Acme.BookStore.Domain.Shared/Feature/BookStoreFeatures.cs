using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.BookStore.Feature
{
    public class BookStoreFeatures
    {
        public const string GroupName = "BookStore";

        public const string AuthorManagement = GroupName + ".AuthorManagement";
        public const string ExportingFiles = GroupName + ".ExportingFiles";
        public const string PdfExporting = GroupName + ".PdfExporting";
        public const string ExcelExporting = GroupName + ".ExcelExporting";
        public const string MaxBookCount = GroupName + ".MaxBookCount";
    }
}
