namespace DataLibrary.Models;

public class CalculationHasResultEntity : EntityBase
{
    public CalculationHasResultEntity()
    {
    }

    public CalculationHasResultEntity(int calculationId, List<int> calculationHasFeedIds, decimal gFresh, decimal percentFresh, decimal percentDryMatter, decimal totalRation)
    {
        CalculationId = calculationId;
        CalculationHasFeedIds = calculationHasFeedIds;
        GFresh = gFresh;
        PercentFresh = percentFresh;
        PercentDryMatter = percentDryMatter;
        TotalRation = totalRation;
    }

    public void Set(int calculationId, List<int> calculationHasFeedIds, decimal gFresh, decimal percentFresh, decimal percentDryMatter, decimal totalRation)
    {
        CalculationId = calculationId;
        CalculationHasFeedIds = calculationHasFeedIds;
        GFresh = gFresh;
        PercentFresh = percentFresh;
        PercentDryMatter = percentDryMatter;
        TotalRation = totalRation;
    }

    public void AddCalculationHasFeed(CalculationHasFeedEntity calculationHasFeed)
    {
        CalculationHasFeedList.Add(calculationHasFeed);
    }

    public void RemoveCalculationHasFeed(CalculationHasFeedEntity calculationHasFeed)
    {
        CalculationHasFeedList.Remove(calculationHasFeed);
    }

    public void UpdateCalculationHasFeed(CalculationHasFeedEntity calculationHasFeed)
    {
        CalculationHasFeedList[CalculationHasFeedList.FindIndex(x => x.FeedId == calculationHasFeed.FeedId)] = calculationHasFeed;
    }

    public int Id { get; set; } // Primary key

    public int CalculationId { get; set; } // Reference to Calculations.Id
    
    public List<int> CalculationHasFeedIds { get; set; } // List of references to CalculationHasFeedEntity.Id

    public decimal GFresh { get; set; } // NOT NULL

    public decimal PercentFresh { get; set; } // NOT NULL

    public decimal PercentDryMatter { get; set; } // NOT NULL

    public decimal TotalRation { get; set; } // NOT NULL

    public CalculationEntity Calculation { get; set; }

    public List<CalculationHasFeedEntity> CalculationHasFeedList { get; set; }
}