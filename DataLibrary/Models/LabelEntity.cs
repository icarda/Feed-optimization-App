using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models;

public class LabelEntity
{
    public LabelEntity()
    {
    }

    public LabelEntity(int id, string labelKey)
    {
        _id = id;
        _labelKey = labelKey;
    }

    private int _id;
    public int Id => _id; // Primary key

    private string _labelKey;
    public string LabelKey => _labelKey; // NOT NULL
}