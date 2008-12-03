using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace PlanningMaker.Modele
{
    public class Planning : ObservableObject
    {
        private EAnnees annee;
        private EDivisions division;
        private String promotion;

        private ObservableCollection<Enseignant> enseignants;
        private ObservableCollection<Matiere> matieres;
        private ObservableCollection<Horaire> horaires;
        private ObservableCollection<Salle> salles;
        private ObservableCollection<Semaine> semaines;


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

        public ICollection<Enseignant> Enseignants
        {
            get
            {
                return enseignants;
            }
        }

        public ICollection<Matiere> Matieres
        {
            get
            {
                return matieres;
            }
        }

        public ICollection<Horaire> Horaires
        {
            get
            {
                return horaires;
            }
        }

        public ICollection<Salle> Salles
        {
            get
            {
                return salles;
            }
        }

        public ICollection<Semaine> Semaines
        {
            get
            {
                return semaines;
            }
        }

        public Planning()
        {
            promotion = "undefined";
            enseignants = new ObservableCollection<Enseignant>();
            matieres = new ObservableCollection<Matiere>();
            horaires = new ObservableCollection<Horaire>();
            salles = new ObservableCollection<Salle>();
            semaines = new ObservableCollection<Semaine>();
        }

        public Planning(String promotion)
        {
            this.promotion = promotion;
            enseignants = new ObservableCollection<Enseignant>();
            matieres = new ObservableCollection<Matiere>();
            horaires = new ObservableCollection<Horaire>();
            salles = new ObservableCollection<Salle>();
            semaines = new ObservableCollection<Semaine>();
        }
    }
}
