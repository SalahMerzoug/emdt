using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Globalization;

namespace PlanningMaker.Modele
{
    class GroupeValidationRule : ValidationRule
    {
        public GroupeValidationRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int groupe = 0;

            try
            {
                if (((string)value).Length > 0)
                    groupe = Int32.Parse((String)value);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Le groupe doit être un chiffre (0, 1 ou 2).");
            }

            if ((groupe < 0) || (groupe > 2))
            {
                return new ValidationResult(false,
                    "Le groupe de cet enseignement n'est pas correct :\n"+
                    "0 pour un enseignement en classe entière,\n"+
                    "1 ou 2 pour un enseignement en groupe");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
