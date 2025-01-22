using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models;

public class RefSpeciesEntity
{
    public RefSpeciesEntity()
    {
    }

    public RefSpeciesEntity(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; private set; } // Primary key

    public string Name { get; private set; } // NOT NULL
}