using DataLibrary.Exceptions;
using DataLibrary.Seedwork;

namespace DataLibrary.Models
{
    public class DietQualityEstimateEntity : Enumeration
    {
        public static DietQualityEstimateEntity LOW = new DietQualityEstimateEntity(1, "Low (< 6.5 MJ/kg DM)");
        public static DietQualityEstimateEntity MEDIUM = new DietQualityEstimateEntity(2, "Medium (~7.5 MJ/kg DM)");
        public static DietQualityEstimateEntity HIGH = new DietQualityEstimateEntity(3, "High (>8.5 MJ/kg DM)");

        public DietQualityEstimateEntity()
        {
        }

        public DietQualityEstimateEntity(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<DietQualityEstimateEntity> List()
        {
            return new[] { LOW, MEDIUM, HIGH };
        }

        public static DietQualityEstimateEntity FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new GeneralDomainException($"Possible values for AssetEntity Condition: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static DietQualityEstimateEntity From(int id)
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