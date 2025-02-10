using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace PeopleHub.Application.Dtos.Person;

public class UploadPersonPhotoDto
{
    private string _identifier = string.Empty;

    public string Identifier
    {
        get => _identifier;
        set => _identifier = string.IsNullOrWhiteSpace(value) ? string.Empty : Regex.Replace(value, @"[^\d]", "");
    }
    public IFormFile Photo { get; set; } = null!;
}