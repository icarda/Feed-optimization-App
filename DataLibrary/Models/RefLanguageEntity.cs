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

    public int Id { get; private set; } // Primary key

    public string LanguageCode { get; private set; } // NOT NULL

    public string Name { get; private set; } // NOT NULL
}