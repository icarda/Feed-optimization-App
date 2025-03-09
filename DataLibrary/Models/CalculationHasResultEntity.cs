namespace DataLibrary.Models;

public class CalculationHasResultEntity : EntityBase
{
    public CalculationHasResultEntity()
    {
    }

    public CalculationHasResultEntity(int calculationId, decimal gFresh, decimal percentFresh, decimal percentDryMatter, decimal totalRation)
    {
        CalculationId = calculationId;
        GFresh = gFresh;
        PercentFresh = percentFresh;
        PercentDryMatter = percentDryMatter;
        TotalRation = totalRation;
    }

    public void Set(int calculationId, decimal gFresh, decimal percentFresh, decimal percentDryMatter, decimal totalRation)
    {
        CalculationId = calculationId;
        GFresh = gFresh;
        PercentFresh = percentFresh;
        PercentDryMatter = percentDryMatter;
        TotalRation = totalRation;
    }

    public int CalculationId { get; set; } // List of references to CalculationEntity.Id

    public decimal GFresh { get; set; } // NOT NULL

    public decimal PercentFresh { get; set; } // NOT NULL

    public decimal PercentDryMatter { get; set; } // NOT NULL

    public decimal TotalRation { get; set; } // NOT NULL

    public CalculationEntity Calculation { get; set; }
}