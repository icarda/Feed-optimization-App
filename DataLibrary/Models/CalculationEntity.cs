using DataLibrary.Models.Enums;

namespace DataLibrary.Models;

public class CalculationEntity
{
    public CalculationEntity()
    {
    }

    public CalculationEntity(string id, SpeciesEntity species, string name, string description, SheepTypeEntity sheepType, GoatTypeEntity goatType, GrazingEntity grazing, BodyWeightEntity bodyWeight, decimal? adg, bool gestation, decimal? milkYield, decimal? fatContent, DietQualityEstimateEntity dietQualityEstimate, KidsLambsEntity kidsLambs)
    {
        _id = id;
        _speciesId = species.Id.ToString();
        _name = name;
        _description = description;
        _sheepTypeId = sheepType.Id.ToString();
        _goatTypeId = goatType.Id.ToString();
        _grazingId = grazing.Id.ToString();
        _bodyWeightId = bodyWeight.Id.ToString();
        _adg = adg;
        _gestation = gestation;
        _milkYield = milkYield;
        _fatContent = fatContent;
        _dietQualityEstimateId = dietQualityEstimate.Id.ToString();
        _kidsLambsId = kidsLambs.Id.ToString();
    }

    public void Set(SpeciesEntity species, string name, string description, SheepTypeEntity sheepType, GoatTypeEntity goatType, GrazingEntity grazing, BodyWeightEntity bodyWeight, decimal? adg, bool gestation, decimal? milkYield, decimal? fatContent, DietQualityEstimateEntity dietQualityEstimate, KidsLambsEntity kidsLambs)
    {
        _speciesId = species.Id.ToString();
        _name = name;
        _description = description;
        _sheepTypeId = sheepType.Id.ToString();
        _goatTypeId = goatType.Id.ToString();
        _grazingId = grazing.Id.ToString();
        _bodyWeightId = bodyWeight.Id.ToString();
        _adg = adg;
        _gestation = gestation;
        _milkYield = milkYield;
        _fatContent = fatContent;
        _dietQualityEstimateId = dietQualityEstimate.Id.ToString();
        _kidsLambsId = kidsLambs.Id.ToString();
    }

    private string _id;
    public string Id => _id;

    private string _speciesId;
    public string SpeciesId => _speciesId; // Reference to Ref_Species.Id

    private string _name;
    public string Name => _name; // NOT NULL

    private string _description;
    public string Description => _description; // NULL

    private string _sheepTypeId;
    public string SheepTypeId => _sheepTypeId; // NOT NULL

    private string _goatTypeId;
    public string GoatTypeId => _goatTypeId; // NOT NULL

    private string _grazingId;
    public string GrazingId => _grazingId; // NOT NULL

    private string _bodyWeightId;
    public string BodyWeightId => _bodyWeightId; // NOT NULL

    private decimal? _adg;
    public decimal? ADG => _adg; // NOT NULL
    private bool _gestation;
    public bool Gestation => _gestation; // NOT NULL

    private decimal? _milkYield;
    public decimal? MilkYield => _milkYield; // NULL

    private decimal? _fatContent;
    public decimal? FatContent => _fatContent; // NULL

    private string _dietQualityEstimateId;
    public string DietQualityEstimateId => _dietQualityEstimateId; // NOT NULL

    private string _kidsLambsId;
    public string KidsLambsId => _kidsLambsId; // NOT NULL
    public SpeciesEntity SpeciesEntity { get; set; } //
    public SheepTypeEntity SheepTypeEntity { get; set; } // NOT NULL
    public GoatTypeEntity GoatTypeEntity { get; set; } // NOT NULL
    public GrazingEntity GrazingEntity { get; set; } // NOT NULL
    public BodyWeightEntity BodyWeightEntity { get; set; } // NOT NULL
    public DietQualityEstimateEntity DietQualityEstimateEntity { get; set; } // NOT NULL
    public KidsLambsEntity KidsLambsEntity { get; set; } // NOT NULL
}