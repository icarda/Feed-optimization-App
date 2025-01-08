using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedOptimizationApp.Data.Models;

public class Calculation
{
    [PrimaryKey]
    public int Id { get; set; } // Primary key

    [Required]
    public Guid SpeciesId { get; set; } // Reference to Ref_Species.Id

    [Required]
    [StringLength(50)]
    public string Name { get; set; } // NOT NULL

    [StringLength(255)]
    public string Description { get; set; } // NULL

    [Required]
    [StringLength(50)]
    public string Type { get; set; } // NOT NULL

    [Required]
    [StringLength(50)]
    public string Grazing { get; set; } // NOT NULL

    [Required]
    public int BodyWeight { get; set; } // NOT NULL

    [Required]
    public int ADG { get; set; } // NOT NULL

    [Required]
    [StringLength(50)]
    public string DietQualityEstimate { get; set; } // NOT NULL

    [Required]
    public bool Gestation { get; set; } // NOT NULL

    public int? MilkYield { get; set; } // NULL

    public int? FatContent { get; set; } // NULL
}