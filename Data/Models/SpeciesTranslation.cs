using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedOptimizationApp.Data.Models;

public class SpeciesTranslation
{
    [Required]
    public Guid SpeciesId { get; set; } // Reference to Ref_Species.Id

    [Required]
    public string Name { get; set; } // NOT NULL

    [Required]
    [StringLength(2)]
    public string LanguageCode { get; set; } // NOT NULL

    [Required]
    [StringLength(255)]
    public string TranslatedDescription { get; set; } // NOT NULL
}