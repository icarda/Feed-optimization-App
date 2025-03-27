using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    /// <summary>
    /// Represents the stored results for a calculation including feed details and ration values.
    /// </summary>
    public class StoredResults
    {
        /// <summary>
        /// Gets or sets the feed entity.
        /// </summary>
        public FeedEntity? Feed { get; set; }

        /// <summary>
        /// Gets or sets the Calculation ID.
        /// </summary>
        public int? CalculationId { get; set; }

        /// <summary>
        /// Gets or sets the fresh feed value (GFresh).
        /// </summary>
        public decimal GFresh { get; set; }

        /// <summary>
        /// Gets or sets the percentage of fresh feed.
        /// </summary>
        public decimal PercentFresh { get; set; }

        /// <summary>
        /// Gets or sets the percentage of dry matter.
        /// </summary>
        public decimal PercentDryMatter { get; set; }

        /// <summary>
        /// Gets or sets the total ration value.
        /// </summary>
        public decimal TotalRation { get; set; }

        public decimal DMi { get; set; }

        public decimal CPi { get; set; }

        public decimal MEi { get; set; }

        public decimal Cost { get; set; }

        public decimal DMiRequirement { get; set; }

        public decimal CPiRequirement { get; set; }

        public decimal MEiRequirement { get; set; }
    }
}