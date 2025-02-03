namespace DataLibrary.Models;

public class RefSpeciesEntity : EntityBase
{
    public RefSpeciesEntity()
    {
    }

    public RefSpeciesEntity(string name)
    {
        Name = name;
    }

    public string Name { get; set; } // NOT NULL
}