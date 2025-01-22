namespace DataLibrary.Models
{
    public class CalculationHasFeedEntity
    {
        public CalculationHasFeedEntity()
        { }

        public CalculationHasFeedEntity(int calculationId, int feedId, decimal dm, decimal cpdm, decimal memjkgdm, decimal price, decimal intake, decimal minLimit, decimal maxLimit)
        {
            CalculationId = calculationId;
            FeedId = feedId;
            DM = dm;
            CPDM = cpdm;
            MEMJKGDM = memjkgdm;
            Price = price;
            Intake = intake;
            MinLimit = minLimit;
            MaxLimit = maxLimit;
        }

        public void Set(int calculationId, int feedId, decimal dm, decimal cpdm, decimal memjkgdm, decimal price, decimal intake, decimal minLimit, decimal maxLimit)
        {
            CalculationId = calculationId;
            FeedId = feedId;
            DM = dm;
            CPDM = cpdm;
            MEMJKGDM = memjkgdm;
            Price = price;
            Intake = intake;
            MinLimit = minLimit;
            MaxLimit = maxLimit;
        }

        public int CalculationId { get; private set; } // Reference to Calculations.Id
        public int FeedId { get; private set; } // Reference to Feed.Id
        public decimal DM { get; private set; } // NOT NULL
        public decimal CPDM { get; private set; } // NOT NULL
        public decimal MEMJKGDM { get; private set; } // NOT NULL
        public decimal Price { get; private set; } // NOT NULL
        public decimal Intake { get; private set; } // NOT NULL
        public decimal MinLimit { get; private set; } // NOT NULL
        public decimal MaxLimit { get; private set; } // NOT NULL

        public CalculationEntity Calculation { get; set; }
        public FeedEntity Feed { get; set; }
    }
}