using DataLibrary.Exceptions;
using DataLibrary.Seedwork;

namespace DataLibrary.Models.Enums
{
    public class LanguageEntity : Enumeration
    {
        public static LanguageEntity ENGLISH = new LanguageEntity(1, "English / Anglais");
        public static LanguageEntity FRENCH = new LanguageEntity(2, "French / Français");

        public LanguageEntity()
        {
        }

        public LanguageEntity(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<LanguageEntity> List()
        {
            return new[] { ENGLISH, FRENCH };
        }

        public static LanguageEntity FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new GeneralDomainException($"Possible values for AssetEntity Condition: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static LanguageEntity From(int id)
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