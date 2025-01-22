using DataLibrary.Models.Enums;

namespace DataLibrary.Models;

public class CalculationEntity
{
    public CalculationEntity()
    {
    }

    public CalculationEntity(int id, int speciesId, string name, string description, string type, int grazingId, int bodyWeightId, decimal? adg, bool gestation, decimal? milkYield, decimal? fatContent, int dietQualityEstimateId, int kidsLambsId)
    {
        _id = id;
        _speciesId = speciesId;
        _name = name;
        _description = description;
        _type = type;
        _grazingId = grazingId;
        _bodyWeightId = bodyWeightId;
        _adg = adg;
        _gestation = gestation;
        _milkYield = milkYield;
        _fatContent = fatContent;
        _dietQualityEstimateId = dietQualityEstimateId;
        _kidsLambsId = kidsLambsId;
    }

    public void Set(int speciesId, string name, string description, string type, int grazingId, int bodyWeightId, decimal? adg, bool gestation, decimal? milkYield, decimal? fatContent, int dietQualityEstimateId, int kidsLambsId)
    {
        _speciesId = speciesId;
        _name = name;
        _description = description;
        _type = type;
        _grazingId = grazingId;
        _bodyWeightId = bodyWeightId;
        _adg = adg;
        _gestation = gestation;
        _milkYield = milkYield;
        _fatContent = fatContent;
        _dietQualityEstimateId = dietQualityEstimateId;
        _kidsLambsId = kidsLambsId;
    }

    private int _id;
    public int Id => _id;

    private int _speciesId;
    public int SpeciesId => _speciesId; // Reference to Ref_Species.Id

    private string _name;
    public string Name => _name; // NOT NULL

    private string _description;
    public string Description => _description; // NULL

    private string _type;
    public string Type => _type; // NULL

    private int _grazingId;
    public int GrazingId => _grazingId; // NOT NULL

    private int _bodyWeightId;
    public int BodyWeightId => _bodyWeightId; // NOT NULL

    private decimal? _adg;
    public decimal? ADG => _adg; // NOT NULL
    private bool _gestation;
    public bool Gestation => _gestation; // NOT NULL

    private decimal? _milkYield;
    public decimal? MilkYield => _milkYield; // NULL

    private decimal? _fatContent;
    public decimal? FatContent => _fatContent; // NULL

    private int _dietQualityEstimateId;
    public int DietQualityEstimateId => _dietQualityEstimateId; // NOT NULL

    private int _kidsLambsId;
    public int KidsLambsId => _kidsLambsId; // NOT NULL
    public SpeciesEntity SpeciesEntity { get; set; } //
    public GrazingEntity GrazingEntity { get; set; } // NOT NULL
    public BodyWeightEntity BodyWeightEntity { get; set; } // NOT NULL
    public DietQualityEstimateEntity DietQualityEstimateEntity { get; set; } // NOT NULL
    public KidsLambsEntity KidsLambsEntity { get; set; } // NOT NULL
}