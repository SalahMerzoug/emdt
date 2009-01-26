using System;
using System.Globalization;
using System.Windows.Controls;

namespace PlanningMaker.Modele
{
    class NumeroValidationRule : ValidationRule
    {
        public NumeroValidationRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try{

                int numero = Int32.Parse(value as string);
                if (numero>0 && numero<53)
                {
                    return new ValidationResult(true, null);
                }
                else
                {
                    return new ValidationResult(false,
                        "Le numéro entré est incorrect.\n"+
                        "Le numéro doit être un entier compris entre 1 et 52");
                }

            }
            catch (FormatException)
            {
                return new ValidationResult(false,
                    "Le numéro entré est incorrect.\n"+
                        "Le numéro doit être un entier compris entre 1 et 52");
            }
        }
    }
}
