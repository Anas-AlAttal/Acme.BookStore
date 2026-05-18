// src/Acme.BookStore.Domain.Shared/Settings/BookStoreSettings.cs
namespace Acme.BookStore.Settings;

public static class BookStoreSettings
{
    private const string Prefix = "BookStore";

    public const string SmtpHost = Prefix + ".SmtpHost";
    public const string MaxBooksCount = Prefix + ".MaxBooksCount";
    public const string MaxUploadSizeMb = Prefix + ".MaxUploadSizeMb";
    public const string MaxAuthorsCount = Prefix + ".MaxAuthorsCount";
}