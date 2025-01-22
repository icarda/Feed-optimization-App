using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models;

public class LabelEntity
{
    public LabelEntity()
    {
    }

    public LabelEntity(int id, string labelKey)
    {
        Id = id;
        LabelKey = labelKey;
    }

    public int Id { get; private set; } // Primary key

    public string LabelKey { get; private set; } // NOT NULL
}