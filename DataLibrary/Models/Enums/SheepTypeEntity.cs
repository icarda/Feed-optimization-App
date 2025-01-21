using DataLibrary.Exceptions;
using DataLibrary.Seedwork;

namespace DataLibrary.Models
{
    public class SheepTypeEntity : Enumeration
    {
        public static SheepTypeEntity EWES = new SheepTypeEntity(1, "Ewes");
        public static SheepTypeEntity WEANED_LAMBS = new SheepTypeEntity(2, "Weaned lambs");
        public static SheepTypeEntity RAMS = new SheepTypeEntity(3, "Rams");

        public SheepTypeEntity()
        {
        }

        public SheepTypeEntity(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<SheepTypeEntity> List()
        {
            return new[] { EWES, WEANED_LAMBS, RAMS };
        }

        public static SheepTypeEntity FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new GeneralDomainException($"Possible values for AssetEntity Condition: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static SheepTypeEntity From(int id)
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