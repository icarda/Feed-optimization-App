namespace DataLibrary.Models;

public class RefLanguageEntity
{
    public RefLanguageEntity()
    {
    }

    public RefLanguageEntity(string languageCode, string name)
    {
        LanguageCode = languageCode;
        Name = name;
    }

    public int Id { get; set; } // Primary key

    public string LanguageCode { get; set; } // NOT NULL

    public string Name { get; set; } // NOT NULL
}