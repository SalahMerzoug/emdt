using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace PlanningMaker.Modele
{
    public class Matiere : ObservableObject
    {
        private String id;
        private String titre;
        private ObservableNotifiableCollection<Enseignant> enseignants;

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
            this.titre = "undefined";
            enseignants = new ObservableNotifiableCollection<Enseignant>();
            enseignants.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
            enseignants.ItemPropertyChanged += OnItemPropertyChanged;
        }

        
        public Matiere(String titre)
        {
            this.titre = titre;
            enseignants = new ObservableNotifiableCollection<Enseignant>();
            enseignants.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
            enseignants.ItemPropertyChanged += OnItemPropertyChanged;

        }

        public void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ObjectChanged("Enseignants");
        }

        private void OnItemPropertyChanged(object sender, ItemPropertyChangedEventArgs args)
        {
            ObjectChanged("Enseignants");
        }

        public override string ToString()
        {
            return "Matière : " + titre;
        }

    }
}
