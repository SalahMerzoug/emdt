using System;

namespace PlanningMaker.Modele
{
    public class Salle : ObservableObject
    {
        private String id;
        private ETypeSalles type;
        private String nom;

        public String Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

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
            this.nom = "Undefined";
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
