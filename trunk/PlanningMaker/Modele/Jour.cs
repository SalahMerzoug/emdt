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
            enseignements.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CollectionChanged);
        }

        public void CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ObjectChanged("Enseignements");
        }

        public override string ToString()
        {
            return "Jour : " + nom;
        }

    }
}
