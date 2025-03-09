using DataLibrary.Exceptions;
using DataLibrary.Seedwork;

namespace DataLibrary.Models.Enums
{
    public class CountryEntity : Enumeration
    {
        public static CountryEntity ETHIOPIA = new CountryEntity(1, "Ethiopia");
        public static CountryEntity TUNISIA = new CountryEntity(2, "Tunisia");

        public CountryEntity()
        {
        }

        public CountryEntity(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<CountryEntity> List()
        {
            return new[] { ETHIOPIA, TUNISIA };
        }

        public static CountryEntity FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new GeneralDomainException($"Possible values for AssetEntity Condition: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static CountryEntity From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new GeneralDomainException($"Possible values for AssetEntity Condition: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}