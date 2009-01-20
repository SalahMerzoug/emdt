using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace PlanningMaker.Modele
{
    class PromotionValidationRule : ValidationRule
    {
        public PromotionValidationRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string nomPromotion = value as string;
            Regex reg = new Regex("^[A-Z][a-z]+$");

            if (nomPromotion == null)
            {
                return new ValidationResult(true, null);
            }

            if (reg.IsMatch(nomPromotion))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false,
                                "Le nom d'une promotion doit s'écrire\n" +
                                "- en un seul mot de 2 caractères minimum,\n" +
                                "- sans chiffre ni caractère spécial.");
            }
        }
    }
}
