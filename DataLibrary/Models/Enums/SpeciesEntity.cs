using DataLibrary.Exceptions;
using DataLibrary.Seedwork;

namespace DataLibrary.Models.Enums
{
    public class SpeciesEntity : Enumeration
    {
        public static SpeciesEntity SHEEP = new SpeciesEntity(1, "Sheep");
        public static SpeciesEntity GOAT = new SpeciesEntity(2, "Goat");

        public SpeciesEntity()
        {
        }

        public SpeciesEntity(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<SpeciesEntity> List()
        {
            return new[] { SHEEP, GOAT };
        }

        public static SpeciesEntity FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new GeneralDomainException($"Possible values for AssetEntity Condition: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static SpeciesEntity From(int id)
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