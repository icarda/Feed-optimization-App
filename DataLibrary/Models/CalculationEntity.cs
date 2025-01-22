using DataLibrary.Models.Enums;

namespace DataLibrary.Models;

public class CalculationEntity
{
    public CalculationEntity()
    {
    }

    public CalculationEntity(int id, SpeciesEntity species, string name, string description, SheepTypeEntity sheepType, GoatTypeEntity goatType, GrazingEntity grazing, BodyWeightEntity bodyWeight, decimal? adg, bool gestation, decimal? milkYield, decimal? fatContent, DietQualityEstimateEntity dietQualityEstimate, KidsLambsEntity kidsLambs)
    {
        _id = id;
        _speciesId = species.Id;
        _name = name;
        _description = description;
        _sheepTypeId = sheepType.Id;
        _goatTypeId = goatType.Id;
        _grazingId = grazing.Id;
        _bodyWeightId = bodyWeight.Id;
        _adg = adg;
        _gestation = gestation;
        _milkYield = milkYield;
        _fatContent = fatContent;
        _dietQualityEstimateId = dietQualityEstimate.Id;
        _kidsLambsId = kidsLambs.Id;
    }

    public void Set(SpeciesEntity species, string name, string description, SheepTypeEntity sheepType, GoatTypeEntity goatType, GrazingEntity grazing, BodyWeightEntity bodyWeight, decimal? adg, bool gestation, decimal? milkYield, decimal? fatContent, DietQualityEstimateEntity dietQualityEstimate, KidsLambsEntity kidsLambs)
    {
        _speciesId = species.Id;
        _name = name;
        _description = description;
        _sheepTypeId = sheepType.Id;
        _goatTypeId = goatType.Id;
        _grazingId = grazing.Id;
        _bodyWeightId = bodyWeight.Id;
        _adg = adg;
        _gestation = gestation;
        _milkYield = milkYield;
        _fatContent = fatContent;
        _dietQualityEstimateId = dietQualityEstimate.Id;
        _kidsLambsId = kidsLambs.Id;
    }

    private int _id;
    public int Id => _id;

    private int _speciesId;
    public int SpeciesId => _speciesId; // Reference to Ref_Species.Id

    private string _name;
    public string Name => _name; // NOT NULL

    private string _description;
    public string Description => _description; // NULL

    private int _sheepTypeId;
    public int SheepTypeId => _sheepTypeId; // NOT NULL

    private int _goatTypeId;
    public int GoatTypeId => _goatTypeId; // NOT NULL

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
    public SheepTypeEntity SheepTypeEntity { get; set; } // NOT NULL
    public GoatTypeEntity GoatTypeEntity { get; set; } // NOT NULL
    public GrazingEntity GrazingEntity { get; set; } // NOT NULL
    public BodyWeightEntity BodyWeightEntity { get; set; } // NOT NULL
    public DietQualityEstimateEntity DietQualityEstimateEntity { get; set; } // NOT NULL
    public KidsLambsEntity KidsLambsEntity { get; set; } // NOT NULL
}