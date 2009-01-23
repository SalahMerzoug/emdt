using System.Globalization;
using System.Windows.Controls;

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

            if (chaine.Length > 0)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Ce champ ne doit pas rester vide.");
            }
        }
    }
}
