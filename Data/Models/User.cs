using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedOptimizationApp.Data.Models;

public class User
{
    [PrimaryKey]
    public Guid Id { get; set; } // Primary key

    [Required]
    public Guid RefCountryId { get; set; } // Reference to Ref_Country.Id

    [Required]
    public Guid RefLanguageId { get; set; } // Reference to Ref_Language.Id

    [Required]
    public Guid RefSpeciesId { get; set; } // Reference to Ref_Species.Id

    [Required]
    public bool TermsAndConditions { get; set; } // NOT NULL

    [Required]
    public DateTime CreatedAt { get; set; } // NOT NULL
}