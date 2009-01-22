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
            this.nom = "undefined";
            this.type = ETypeSalles.Amphi;
        }

        public Salle(String nom)
        {
            this.nom = nom;
            this.type = ETypeSalles.Amphi;
        }

        public override string ToString()
        {
            return "Salle : " + nom;
        }

    }
}
