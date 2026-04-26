using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.Settings
{
    public static class BookStoreSettingNames
    {
        private const string Prefix = "Acme.BookStore";

        // كم كتاب نعرض في الصفحة (الافتراضي 10)
        public const string MaxBooksPerPage = Prefix + ".MaxBooksPerPage";

        // هل ميزة المفضلة مفعّلة (افتراضي true)
        public const string EnableFavorites = Prefix + ".EnableFavorites";

        public const string WelcomeMessage = Prefix + ".WelcomeMessage";   
    }
}