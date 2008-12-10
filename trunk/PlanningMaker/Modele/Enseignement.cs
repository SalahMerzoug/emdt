using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanningMaker.Modele
{
    public abstract class Enseignement : ObservableObject
    {
        private int numeroGroupe;

        private Enseignant enseignant;
        private Matiere matiere;

        private Horaire plage1;
        private Horaire plage2;

        private Salle salle;

        public int Groupe
        {
            get
            {
                return numeroGroupe;
            }
            set
            {
                numeroGroupe = value;
                ObjectChanged("Groupe");
            }
        }

        public Enseignant Enseignant
        {
            get
            {
                return enseignant;
            }
            set
            {
                enseignant = value;
                ObjectChanged("Enseignant");
            }
        }

        public Matiere Matiere
        {
            get
            {
                return matiere;
            }
            set
            {
                matiere = value;
                ObjectChanged("Matiere");
            }
        }

        public Horaire Horaire1
        {
            get
            {
                return plage1;
            }
            set
            {
                plage1 = value;
                ObjectChanged("Horraire1");
            }
        }

        public Horaire Horaire2
        {
            get
            {
                return plage2;
            }
            set
            {
                plage2 = value;
                ObjectChanged("Horaire2");
            }
        }

        public Salle Salle
        {
            get
            {
                return salle;
            }
            set
            {
                salle = value;
                ObjectChanged("Salle");
            }
        }

        public Enseignement()
        {
            numeroGroupe = 0;
        }

        public Enseignement(int numGroupe)
        {
            numeroGroupe = numGroupe;
        }
    }
}
