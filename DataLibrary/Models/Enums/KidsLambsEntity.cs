using DataLibrary.Exceptions;
using DataLibrary.Seedwork;

namespace DataLibrary.Models
{
    public class KidsLambsEntity : Enumeration
    {
        public static KidsLambsEntity ZERO = new KidsLambsEntity(1, "0");
        public static KidsLambsEntity ONE = new KidsLambsEntity(2, "1");
        public static KidsLambsEntity TWO = new KidsLambsEntity(3, "2");
        public static KidsLambsEntity THREE = new KidsLambsEntity(4, "3");
        public static KidsLambsEntity FOUR = new KidsLambsEntity(5, "4");

        public KidsLambsEntity()
        {
        }

        public KidsLambsEntity(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<KidsLambsEntity> List()
        {
            return new[] { ZERO, ONE, TWO, THREE, FOUR };
        }

        public static KidsLambsEntity FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new GeneralDomainException($"Possible values for AssetEntity Condition: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static KidsLambsEntity From(int id)
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