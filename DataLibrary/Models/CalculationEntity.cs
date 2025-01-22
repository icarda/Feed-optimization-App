using DataLibrary.Models.Enums;

namespace DataLibrary.Models;

public class CalculationEntity
{
    public CalculationEntity()
    {
    }

    public CalculationEntity(int id, int speciesId, string name, string description, string type, int grazingId, int bodyWeightId, decimal? adg, bool gestation, decimal? milkYield, decimal? fatContent, int dietQualityEstimateId, int kidsLambsId)
    {
        Id = id;
        SpeciesId = speciesId;
        Name = name;
        Description = description;
        Type = type;
        GrazingId = grazingId;
        BodyWeightId = bodyWeightId;
        ADG = adg;
        Gestation = gestation;
        MilkYield = milkYield;
        FatContent = fatContent;
        DietQualityEstimateId = dietQualityEstimateId;
        KidsLambsId = kidsLambsId;
    }

    public void Set(int speciesId, string name, string description, string type, int grazingId, int bodyWeightId, decimal? adg, bool gestation, decimal? milkYield, decimal? fatContent, int dietQualityEstimateId, int kidsLambsId)
    {
        SpeciesId = speciesId;
        Name = name;
        Description = description;
        Type = type;
        GrazingId = grazingId;
        BodyWeightId = bodyWeightId;
        ADG = adg;
        Gestation = gestation;
        MilkYield = milkYield;
        FatContent = fatContent;
        DietQualityEstimateId = dietQualityEstimateId;
        KidsLambsId = kidsLambsId;
    }

    public int Id { get; private set; }

    public int SpeciesId { get; private set; } // Reference to Ref_Species.Id

    public string Name { get; private set; } // NOT NULL

    public string Description { get; private set; } // NULL

    public string Type { get; private set; } // NULL

    public int GrazingId { get; private set; } // NOT NULL

    public int BodyWeightId { get; private set; } // NOT NULL

    public decimal? ADG { get; private set; } // NOT NULL

    public bool Gestation { get; private set; } // NOT NULL

    public decimal? MilkYield { get; private set; } // NULL

    public decimal? FatContent { get; private set; } // NULL

    public int DietQualityEstimateId { get; private set; } // NOT NULL

    public int KidsLambsId { get; private set; } // NOT NULL

    public SpeciesEntity SpeciesEntity { get; set; } // NOT NULL
    public GrazingEntity GrazingEntity { get; set; } // NOT NULL
    public BodyWeightEntity BodyWeightEntity { get; set; } // NOT NULL
    public DietQualityEstimateEntity DietQualityEstimateEntity { get; set; } // NOT NULL
    public KidsLambsEntity KidsLambsEntity { get; set; } // NOT NULL
}