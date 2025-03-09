namespace DataLibrary.Models;

public class RefLanguageEntity : EntityBase
{
    public RefLanguageEntity()
    {
    }

    public RefLanguageEntity(string languageCode, string name)
    {
        LanguageCode = languageCode;
        Name = name;
    }

    public string LanguageCode { get; set; } // NOT NULL

    public string Name { get; set; } // NOT NULL
}