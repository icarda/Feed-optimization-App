using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models;

public class RefSpeciesEntity
{
    public RefSpeciesEntity()
    {
    }

    public RefSpeciesEntity(string id, string name)
    {
        _id = id;
        _name = name;
    }

    private string _id;
    public string Id => _id; // Primary key

    private string _name;
    public string Name => _name; // NOT NULL
}