using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedOptimizationApp.Data.Models;

public class FeedTranslation
{
    [Required]
    public Guid FeedId { get; set; } // Reference to Feed.Id

    [Required]
    [StringLength(2)]
    public string LanguageCode { get; set; } // NOT NULL

    [Required]
    [StringLength(255)]
    public string TranslatedDescription { get; set; } // NOT NULL
}