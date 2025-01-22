namespace DataLibrary.Models;

public class CalculationHasResultEntity
{
    public CalculationHasResultEntity()
    {
    }

    public CalculationHasResultEntity(int id, CalculationEntity calculation, decimal gFresh, decimal percentFresh, decimal percentDryMatter, decimal totalRation)
    {
        _id = id;
        _calculationId = calculation.Id;
        _gFresh = gFresh;
        _percentFresh = percentFresh;
        _percentDryMatter = percentDryMatter;
        _totalRation = totalRation;
    }

    public void Set(CalculationEntity calculation, decimal gFresh, decimal percentFresh, decimal percentDryMatter, decimal totalRation)
    {
        _calculationId = calculation.Id;
        _gFresh = gFresh;
        _percentFresh = percentFresh;
        _percentDryMatter = percentDryMatter;
        _totalRation = totalRation;
    }

    private int _id;
    public int Id => _id; // Primary key

    private int _calculationId;
    public int CalculationId => _calculationId; // Reference to Calculations.Id

    private decimal _gFresh;
    public decimal GFresh => _gFresh; // NOT NULL

    private decimal _percentFresh;
    public decimal PercentFresh => _percentFresh; // NOT NULL

    private decimal _percentDryMatter;
    public decimal PercentDryMatter => _percentDryMatter; // NOT NULL

    private decimal _totalRation;
    public decimal TotalRation => _totalRation; // NOT NULL

    public CalculationEntity Calculation { get; set; }
}