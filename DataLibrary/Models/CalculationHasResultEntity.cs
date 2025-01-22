namespace DataLibrary.Models;

public class CalculationHasResultEntity
{
    public CalculationHasResultEntity()
    {
    }

    public CalculationHasResultEntity(int id, int calculationId, decimal gFresh, decimal percentFresh, decimal percentDryMatter, decimal totalRation)
    {
        Id = id;
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

    public int Id { get; private set; } // Primary key

    public int CalculationId { get; private set; } // Reference to Calculations.Id

    public decimal GFresh { get; private set; } // NOT NULL

    public decimal PercentFresh { get; private set; } // NOT NULL

    public decimal PercentDryMatter { get; private set; } // NOT NULL

    public decimal TotalRation { get; private set; } // NOT NULL

    public CalculationEntity Calculation { get; set; }
}