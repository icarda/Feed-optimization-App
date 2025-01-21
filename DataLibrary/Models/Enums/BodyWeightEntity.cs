using DataLibrary.Exceptions;
using DataLibrary.Seedwork;

namespace DataLibrary.Models
{
    public class BodyWeightEntity : Enumeration
    {
        public static BodyWeightEntity FIVE = new BodyWeightEntity(1, "5");
        public static BodyWeightEntity TEN = new BodyWeightEntity(2, "10");
        public static BodyWeightEntity FIFTEEN = new BodyWeightEntity(3, "15");
        public static BodyWeightEntity TWENTY = new BodyWeightEntity(4, "20");
        public static BodyWeightEntity TWENTY_FIVE = new BodyWeightEntity(5, "25");
        public static BodyWeightEntity THIRTY = new BodyWeightEntity(6, "30");
        public static BodyWeightEntity THIRTY_FIVE = new BodyWeightEntity(7, "35");
        public static BodyWeightEntity FORTY = new BodyWeightEntity(8, "40");
        public static BodyWeightEntity FORTY_FIVE = new BodyWeightEntity(9, "45");
        public static BodyWeightEntity FIFTY = new BodyWeightEntity(10, "50");
        public static BodyWeightEntity FIFTY_FIVE = new BodyWeightEntity(11, "55");
        public static BodyWeightEntity SIXTY = new BodyWeightEntity(12, "60");
        public static BodyWeightEntity SIXTY_FIVE = new BodyWeightEntity(13, "65");
        public static BodyWeightEntity SEVENTY = new BodyWeightEntity(14, "70");
        public static BodyWeightEntity SEVENTY_FIVE = new BodyWeightEntity(15, "75");
        public static BodyWeightEntity EIGHTY = new BodyWeightEntity(16, "80");
        public static BodyWeightEntity EIGHTY_FIVE = new BodyWeightEntity(17, "85");
        public static BodyWeightEntity NINETY = new BodyWeightEntity(18, "90");

        public BodyWeightEntity()
        {
        }

        public BodyWeightEntity(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<BodyWeightEntity> List()
        {
            return new[] { FIVE, TEN, FIFTEEN, TWENTY, TWENTY_FIVE, THIRTY, THIRTY_FIVE, FORTY, FORTY_FIVE, FIFTY, FIFTY_FIVE, SIXTY, SIXTY_FIVE, SEVENTY, SEVENTY_FIVE, EIGHTY, EIGHTY_FIVE, NINETY };
        }

        public static BodyWeightEntity FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new GeneralDomainException($"Possible values for AssetEntity Condition: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static BodyWeightEntity From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new GeneralDomainException($"Possible values for AssetEntity Condition: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}