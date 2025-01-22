namespace DataLibrary.Models;

public class CalculationHasFeedEntity
{
    public CalculationHasFeedEntity()
    {
    }

    public CalculationHasFeedEntity(CalculationEntity calculation, FeedEntity feed, decimal dm, decimal cpdm, decimal memjkgdm, decimal price, decimal intake, decimal minLimit, decimal maxLimit)
    {
        _calculationId = calculation.Id;
        _feedId = feed.Id;
        _dm = dm;
        _cpdm = cpdm;
        _memjkgdm = memjkgdm;
        _price = price;
        _intake = intake;
        _minLimit = minLimit;
        _maxLimit = maxLimit;
    }

    public void Set(CalculationEntity calculation, FeedEntity feed, decimal dm, decimal cpdm, decimal memjkgdm, decimal price, decimal intake, decimal minLimit, decimal maxLimit)
    {
        _calculationId = calculation.Id;
        _feedId = feed.Id;
        _dm = dm;
        _cpdm = cpdm;
        _memjkgdm = memjkgdm;
        _price = price;
        _intake = intake;
        _minLimit = minLimit;
        _maxLimit = maxLimit;
    }

    private int _calculationId;
    public int CalculationId => _calculationId; // Reference to Calculations.Id

    private int _feedId;
    public int FeedId => _feedId; // Reference to Feed.Id

    private decimal _dm;
    public decimal DM => _dm; // NOT NULL

    private decimal _cpdm;
    public decimal CPDM => _cpdm; // NOT NULL

    private decimal _memjkgdm;
    public decimal MEMJKGDM => _memjkgdm; // NOT NULL

    private decimal _price;
    public decimal Price => _price; // NOT NULL

    private decimal _intake;
    public decimal Intake => _intake; // NOT NULL

    private decimal _minLimit;
    public decimal MinLimit => _minLimit; // NOT NULL

    private decimal _maxLimit;
    public decimal MaxLimit => _maxLimit; // NOT NULL

    public CalculationEntity Calculation { get; set; }
    public FeedEntity Feed { get; set; }
}