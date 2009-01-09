using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Globalization;

namespace PlanningMaker.Modele
{
    class DateValidationRule : ValidationRule
    {
        public DateValidationRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string dateTest = value as string;
            string[] elementsDateTest = dateTest.Split('-');
            if (elementsDateTest.Length == 3){
                try
                {
                    int annee = Int32.Parse(elementsDateTest[0]);
                    int mois = Int32.Parse(elementsDateTest[1]);
                    int jour = Int32.Parse(elementsDateTest[2]);
                    if (annee < 999 || annee > 3000)
                    {
                        return new ValidationResult(false,
                            "Le format de la date est incorrect.\n" +
                            "L'année doit être comprise entre 1000 et 3000");
                    }
                    if (mois > 12 || mois < 1)
                    {
                        return new ValidationResult(false,
                            "Le format de la date est incorrect.\n" +
                            "Le mois doit être compris entre 1 et 12");
                    }
                    if (jour > 31 || jour < 1)
                    {
                        return new ValidationResult(false,
                            "Le format de la date est incorrect.\n" +
                            "Le jour doit être compris entre 1 et 31");
                    }
                    return new ValidationResult(true, null);
                }
                catch (FormatException)
                {
                    return new ValidationResult(false,
                        "Le format de la date est incorrect.\n" +
                        "Le format de la date doit être le suivant :\n" +
                        "AAAA-MM-JJ");
                }
            }else{
                return new ValidationResult(false,
                    "Le format de la date est incorrect.\n"+
                    "Le format de la date doit être le suivant :\n" +
                    "AAAA-MM-JJ");
            }
        }
    }
}
