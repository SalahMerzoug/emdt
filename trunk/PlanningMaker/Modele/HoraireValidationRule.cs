using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Globalization;

namespace PlanningMaker.Modele
{
    class HoraireValidationRule : ValidationRule
    {
        public HoraireValidationRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!Horaire.IsHeureValide((string)value))
            {
                return new ValidationResult(false,
                    "Le format de l'heure n'est pas correct ou bien l'heure est saisie n'est pas valide.\n"+
                    "Le format de l'heure doit être le suivant :\n" +
                    "-si l'heure est inférieure à 10h00 : HhMM (exemple : 7h45)\n"+
                    "-sinon : HHhMM (exemple 13h30)");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
