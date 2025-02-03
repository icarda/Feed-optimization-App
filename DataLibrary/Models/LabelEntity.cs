namespace DataLibrary.Models;

public class LabelEntity : EntityBase
{
    public LabelEntity()
    {
    }

    public LabelEntity(string labelKey)
    {
        LabelKey = labelKey;
    }

    public string LabelKey { get; set; } // NOT NULL
}