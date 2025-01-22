using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models;

public class RefSpeciesEntity
{
    public RefSpeciesEntity()
    {
    }

    public RefSpeciesEntity(int id, string name)
    {
        _id = id;
        _name = name;
    }

    private int _id;
    public int Id => _id; // Primary key

    private string _name;
    public string Name => _name; // NOT NULL
}