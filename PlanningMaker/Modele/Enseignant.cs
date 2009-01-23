using System;

namespace PlanningMaker.Modele
{
    public class Enseignant : ObservableObject
    {

        private String id;
        private String nom;
        private String prenom;

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

        public String Prenom
        {
            get
            {
                return prenom;
            }
            set
            {
                prenom = value;
                ObjectChanged("Prenom");
            }
        }

        public Enseignant()
        {
            this.nom = "Undefined";
            this.prenom = "Undefined";
        }

        public Enseignant(String nom, String prenom)
        {
            this.nom = nom;
            this.prenom = prenom;
        }

        public override string ToString()
        {
            return "Enseignant : " + nom + " " + prenom;
        }
    }
}
