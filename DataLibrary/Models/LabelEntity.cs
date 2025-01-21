using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models;

public class LabelEntity
{
    public LabelEntity()
    {
    }

    public LabelEntity(string id, string labelKey)
    {
        _id = id;
        _labelKey = labelKey;
    }

    private string _id;
    public string Id => _id; // Primary key

    private string _labelKey;
    public string LabelKey => _labelKey; // NOT NULL
}