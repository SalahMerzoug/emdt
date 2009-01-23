using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace PlanningMaker.Modele
{
    class StringValidationRule : ValidationRule
    {
        public StringValidationRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string chaine = value as string;
            //  Ll = Letter, Lowercase (accents inclus)
            Regex reg = new Regex(@"^([A-Z]\p{Ll}+\.?)((-|\s)[A-Z]\p{Ll}+\.?)?$");

            if (chaine == null)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                if (reg.IsMatch(chaine))
                {
                    return new ValidationResult(true, null);
                }
                else
                {
                    return new ValidationResult(false,
                                    "Ce champ doit contenir un nom simple ou composé.\n" +
                                    "* Chaque mot commence par une majuscule et possède au minimum 2 caractères.\n" +
                                    "* Un point peut servir à abréger et le séparateur est soit '-', soit 'espace'.");
                }
            }
        }
    }
}
