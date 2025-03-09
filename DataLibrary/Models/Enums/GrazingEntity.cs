using DataLibrary.Exceptions;
using DataLibrary.Seedwork;

namespace DataLibrary.Models
{
    public class GrazingEntity : Enumeration
    {
        public static GrazingEntity NONE = new GrazingEntity(1, "None");
        public static GrazingEntity GRAZING_CLOSE_BY = new GrazingEntity(2, "Grazing close-by");
        public static GrazingEntity OPEN_RANGE = new GrazingEntity(3, "Open range");
        public static GrazingEntity ROUGH_MOUNTAIN_TERRAIN = new GrazingEntity(4, "Rough mountain terrain");

        public GrazingEntity()
        {
        }

        public GrazingEntity(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<GrazingEntity> List()
        {
            return new[] { NONE, GRAZING_CLOSE_BY, OPEN_RANGE, ROUGH_MOUNTAIN_TERRAIN };
        }

        public static GrazingEntity FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new GeneralDomainException($"Possible values for AssetEntity Condition: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static GrazingEntity From(int id)
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