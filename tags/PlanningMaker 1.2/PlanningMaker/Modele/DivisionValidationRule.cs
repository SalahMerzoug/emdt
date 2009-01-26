using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace PlanningMaker.Modele
{
    class DivisionValidationRule : ValidationRule
    {
        public DivisionValidationRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string lettreDivision = value as string;
            Regex reg = new Regex("^[A-E]$");

            if (lettreDivision == null)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                if (reg.IsMatch(lettreDivision))
                {
                    return new ValidationResult(true, null);
                }
                else
                {
                    return new ValidationResult(false, "Une division doit valoir A, B, C, D ou E.");
                }
            }
        }
    }
}
