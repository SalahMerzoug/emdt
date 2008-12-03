using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace PlanningMaker.Modele
{
    public class Jour : ObservableObject
    {

        private EJours nom;
        private ObservableCollection<Enseignement> enseignements;

        public EJours Nom
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

        public ICollection<Enseignement> Enseignements
        {
            get
            {
                return enseignements;
            }
        }

        public Jour()
        {
            enseignements = new ObservableCollection<Enseignement>();
        }

        public Jour(String nom)
        {
            enseignements = new ObservableCollection<Enseignement>();
        }

        public override string ToString()
        {
            return "Jour : " + nom;
        }

    }
}
