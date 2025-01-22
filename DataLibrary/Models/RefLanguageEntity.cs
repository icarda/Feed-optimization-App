using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models;

public class RefLanguageEntity
{
    public RefLanguageEntity()
    {
    }

    public RefLanguageEntity(int id, string languageCode, string name)
    {
        Id = id;
        LanguageCode = languageCode;
        Name = name;
    }

    public int Id { get; private set; } // Primary key

    public string LanguageCode { get; private set; } // NOT NULL

    public string Name { get; private set; } // NOT NULL
}