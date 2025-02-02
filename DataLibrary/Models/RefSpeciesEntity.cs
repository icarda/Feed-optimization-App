namespace DataLibrary.Models;

public class RefSpeciesEntity
{
    public RefSpeciesEntity()
    {
    }

    public RefSpeciesEntity(string name)
    {
        Name = name;
    }

    public int Id { get; set; } // Primary key

    public string Name { get; set; } // NOT NULL
}