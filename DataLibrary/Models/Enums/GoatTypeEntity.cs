using DataLibrary.Exceptions;
using DataLibrary.Seedwork;

namespace DataLibrary.Models
{
    public class GoatTypeEntity : Enumeration
    {
        public static GoatTypeEntity DOES = new GoatTypeEntity(1, "Does");
        public static GoatTypeEntity KIDS = new GoatTypeEntity(2, "Kids");
        public static GoatTypeEntity BUCKS = new GoatTypeEntity(3, "Bucks");

        public GoatTypeEntity()
        {
        }

        public GoatTypeEntity(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<GoatTypeEntity> List()
        {
            return new[] { DOES, KIDS, BUCKS };
        }

        public static GoatTypeEntity FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new GeneralDomainException($"Possible values for AssetEntity Condition: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static GoatTypeEntity From(int id)
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