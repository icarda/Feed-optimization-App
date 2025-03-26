namespace DataLibrary.Models;

public class CalculationHasResultEntity : EntityBase
{
    public CalculationHasResultEntity()
    {
    }

    public CalculationHasResultEntity(int calculationId, decimal gFresh, decimal percentFresh, decimal percentDryMatter, decimal totalRation, decimal dmi, decimal cpi, decimal mei, decimal cost, decimal dmiRequirement, decimal cpiRequirement, decimal meiRequirement)
    {
        CalculationId = calculationId;
        GFresh = gFresh;
        PercentFresh = percentFresh;
        PercentDryMatter = percentDryMatter;
        TotalRation = totalRation;
        DMi = dmi;
        CPi = cpi;
        MEi = mei;
        Cost = cost;
        DMiRequirement = dmiRequirement;
        CPiRequirement = cpiRequirement;
        MEiRequirement = meiRequirement;
    }

    public void Set(int calculationId, decimal gFresh, decimal percentFresh, decimal percentDryMatter, decimal totalRation, decimal dmi, decimal cpi, decimal mei, decimal cost, decimal dmiRequirement, decimal cpiRequirement, decimal meiRequirement)
    {
        CalculationId = calculationId;
        GFresh = gFresh;
        PercentFresh = percentFresh;
        PercentDryMatter = percentDryMatter;
        TotalRation = totalRation;
        DMi = dmi;
        CPi = cpi;
        MEi = mei;
        Cost = cost;
        DMiRequirement = dmiRequirement;
        CPiRequirement = cpiRequirement;
        MEiRequirement = meiRequirement;
    }

    public int CalculationId { get; set; } // List of references to CalculationEntity.Id

    public decimal GFresh { get; set; } // NOT NULL

    public decimal PercentFresh { get; set; } // NOT NULL

    public decimal PercentDryMatter { get; set; } // NOT NULL

    public decimal TotalRation { get; set; } // NOT NULL

    public decimal DMi { get; set; } // NOT NULL

    public decimal CPi { get; set; } // NOT NULL

    public decimal MEi { get; set; } // NOT NULL

    public decimal DMiRequirement { get; set; } // NOT NULL

    public decimal CPiRequirement { get; set; } // NOT NULL

    public decimal MEiRequirement { get; set; } // NOT NULL

    public decimal Cost { get; set; } // NOT NULL

    public CalculationEntity Calculation { get; set; }
}