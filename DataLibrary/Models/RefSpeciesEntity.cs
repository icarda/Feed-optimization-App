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

    public int Id { get; private set; } // Primary key

    public string Name { get; private set; } // NOT NULL
}