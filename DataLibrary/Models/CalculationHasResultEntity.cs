namespace DataLibrary.Models;

public class CalculationHasResultEntity : EntityBase
{
    public CalculationHasResultEntity()
    {
    }

    public CalculationHasResultEntity(int calculationHasFeedId, decimal gFresh, decimal percentFresh, decimal percentDryMatter, decimal totalRation)
    {
        CalculationHasFeedId = calculationHasFeedId;
        GFresh = gFresh;
        PercentFresh = percentFresh;
        PercentDryMatter = percentDryMatter;
        TotalRation = totalRation;
    }

    public void Set(int calculationHasFeedId, decimal gFresh, decimal percentFresh, decimal percentDryMatter, decimal totalRation)
    {
        CalculationHasFeedId = calculationHasFeedId;
        GFresh = gFresh;
        PercentFresh = percentFresh;
        PercentDryMatter = percentDryMatter;
        TotalRation = totalRation;
    }

    public int CalculationHasFeedId { get; set; } // List of references to CalculationHasFeedEntity.Id

    public decimal GFresh { get; set; } // NOT NULL

    public decimal PercentFresh { get; set; } // NOT NULL

    public decimal PercentDryMatter { get; set; } // NOT NULL

    public decimal TotalRation { get; set; } // NOT NULL

    public CalculationHasFeedEntity CalculationHasFeed { get; set; }
}