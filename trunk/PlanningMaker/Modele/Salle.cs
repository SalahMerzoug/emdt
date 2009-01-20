using System;

namespace PlanningMaker.Modele
{
    public class Salle : ObservableObject
    {
        private ETypeSalles type;
        private String nom;

        public virtual String Nom
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

        public ETypeSalles Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                ObjectChanged("Type");
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

    }
}
