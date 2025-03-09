namespace DataLibrary.Models
{
    public class CalculationHasFeedEntity : EntityBase
    {
        public CalculationHasFeedEntity()
        { }

        public CalculationHasFeedEntity(int feedId, int calculationId, decimal dm, decimal cpdm, decimal memjkgdm, decimal price, decimal intake, decimal minLimit, decimal maxLimit)
        {
            FeedId = feedId;
            CalculationId = calculationId;
            DM = dm;
            CPDM = cpdm;
            MEMJKGDM = memjkgdm;
            Price = price;
            Intake = intake;
            MinLimit = minLimit;
            MaxLimit = maxLimit;
        }

        public void Set(int feedId, int calculationId, decimal dm, decimal cpdm, decimal memjkgdm, decimal price, decimal intake, decimal minLimit, decimal maxLimit)
        {
            FeedId = feedId;
            CalculationId = calculationId;
            DM = dm;
            CPDM = cpdm;
            MEMJKGDM = memjkgdm;
            Price = price;
            Intake = intake;
            MinLimit = minLimit;
            MaxLimit = maxLimit;
        }

        public int FeedId { get; set; } // Reference to Feed.Id
        public int CalculationId { get; set; } // Reference to Calculation.Id
        public decimal DM { get; set; } // NOT NULL
        public decimal CPDM { get; set; } // NOT NULL
        public decimal MEMJKGDM { get; set; } // NOT NULL
        public decimal Price { get; set; } // NOT NULL
        public decimal Intake { get; set; } // NOT NULL
        public decimal MinLimit { get; set; } // NOT NULL
        public decimal MaxLimit { get; set; } // NOT NULL
        public FeedEntity Feed { get; set; }
        public CalculationEntity Calculation { get; set; }
    }
}