using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedOptimizationApp.Data.Models;

public class CalculationHasResult
{
    [PrimaryKey]
    public Guid Id { get; set; } // Primary key

    [Required]
    public Guid CalculationId { get; set; } // Reference to Calculations.Id

    [Required]
    public int GFresh { get; set; } // NOT NULL

    [Required]
    public int PercentFresh { get; set; } // NOT NULL

    [Required]
    public int PercentDryMatter { get; set; } // NOT NULL

    [Required]
    public decimal TotalRation { get; set; } // NOT NULL
}