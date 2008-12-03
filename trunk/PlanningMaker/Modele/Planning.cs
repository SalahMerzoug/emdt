using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanningMaker.Modele
{
    public class Planning : ObservableObject
    {
        private EAnnees annee;
        private EDivisions division;
        private String promotion;

        public EAnnees Annee
        {
            get
            {
                return annee;
            }
            set
            {
                annee = value;
                ObjectChanged("Annee");
            }
        }

        public EDivisions Division
        {
            get
            {
                return division;
            }
            set
            {
                division = value;
                ObjectChanged("Division");
            }
        }

        public String Promotion
        {
            get
            {
                return promotion;
            }
            set
            {
                promotion = value;
                ObjectChanged("Promotion");
            }
        }
    }
}
