// src/Acme.BookStore.Application.Contracts/Settings/BookStoreSettingsDto.cs
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Settings;

public class BookStoreSettingsDto
{
    [MaxLength(256)]
    public string SmtpHost { get; set; }

    [Range(1, 10000)]
    public int MaxBooksCount { get; set; }

    [Range(1, 500)]
    public int MaxUploadSizeMb { get; set; }

    [Range(1, 5000)]
    public int MaxAuthorsCount { get; set; }
}