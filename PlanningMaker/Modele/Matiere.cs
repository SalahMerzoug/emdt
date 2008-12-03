using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PlanningMaker.Modele
{
    public class Matiere : ObservableObject
    {
        private String titre;
        private ObservableCollection<Enseignant> enseignants;

        public String Titre
        {
            get
            {
                return titre;
            }
            set
            {
                titre = value;
                ObjectChanged("Titre");
            }
        }

        public ICollection<Enseignant> Enseignants
        {
            get
            {
                return enseignants;
            }
        }

        public Matiere()
        {
            titre = "undefined";
            enseignants = new ObservableCollection<Enseignant>();
        }

        public Matiere(String titre)
        {
            this.titre = titre;
            enseignants = new ObservableCollection<Enseignant>();
        }

        public override string ToString()
        {
            return "Matière : " + titre;
        }

    }
}
