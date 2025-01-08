using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedOptimizationApp.Data.Models;

public class CalculationHasFeed
{
    [Required]
    public Guid CalculationId { get; set; } // Reference to Calculations.Id

    [Required]
    public Guid FeedId { get; set; } // Reference to Feed.Id

    [Required]
    public int DM { get; set; } // NOT NULL

    [Required]
    public int CPDM { get; set; } // NOT NULL

    [Required]
    public int MEMJKGDM { get; set; } // NOT NULL

    [Required]
    public decimal Price { get; set; } // NOT NULL

    [Required]
    public decimal Intake { get; set; } // NOT NULL

    [Required]
    public decimal MinLimit { get; set; } // NOT NULL

    [Required]
    public decimal MaxLimit { get; set; } // NOT NULL
}