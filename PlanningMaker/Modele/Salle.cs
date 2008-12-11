using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanningMaker.Modele
{
    public abstract class Salle : ObservableObject
    {

        private String nom;

        public String Nom
        {
            get
            {
                return nom;
            }
            set
            {
                nom = value;
                ObjectChanged("Nom");
            }
        }

        public Salle()
        {
            nom = "undefined";
        }

        public Salle(String nom)
        {
            this.nom = nom;
        }

        public override string ToString()
        {
            return "Salle : " + nom;
        }

        public virtual String TypeOfSalle
        {
            get
            {
                return "Salle";
            }
        }
    }
}
