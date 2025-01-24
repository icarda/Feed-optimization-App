namespace DataLibrary.Models;

public class LabelEntity
{
    public LabelEntity()
    {
    }

    public LabelEntity(string labelKey)
    {
        LabelKey = labelKey;
    }

    public int Id { get; private set; } // Primary key

    public string LabelKey { get; private set; } // NOT NULL
}