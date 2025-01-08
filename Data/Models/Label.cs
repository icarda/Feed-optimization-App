using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedOptimizationApp.Data.Models;

public class Label
{
    [PrimaryKey]
    public int Id { get; set; } // Primary key

    [Required]
    [StringLength(255)]
    public string LabelKey { get; set; } // NOT NULL
}