using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedOptimizationApp.Data.Models;

public class RefCountry
{
    [PrimaryKey]
    public Guid Id { get; set; } // Primary key

    [Required]
    public string Country { get; set; } // NOT NULL

    [Required]
    public string DateFormat { get; set; } // NOT NULL

    [Required]
    public string CurrencyValue { get; set; } // NOT NULL
}