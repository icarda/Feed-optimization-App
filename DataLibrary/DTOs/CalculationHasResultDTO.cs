namespace DataLibrary.DTOs;

public class CalculationHasResultDTO
{
    public int Id { get; set; }
    public int CalculationId { get; set; }
    public decimal GFresh { get; set; }
    public decimal PercentFresh { get; set; }
    public decimal PercentDryMatter { get; set; }
    public decimal TotalRation { get; set; }
}