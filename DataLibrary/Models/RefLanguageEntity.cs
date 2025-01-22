using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models;

public class RefLanguageEntity
{
    public RefLanguageEntity()
    {
    }

    public RefLanguageEntity(int id, string languageCode, string name)
    {
        _id = id;
        _languageCode = languageCode;
        _name = name;
    }

    private int _id;
    public int Id => _id; // Primary key

    private string _languageCode;
    public string LanguageCode => _languageCode; // NOT NULL

    private string _name;
    public string Name => _name; // NOT NULL
}