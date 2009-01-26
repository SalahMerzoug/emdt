using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace PlanningMaker.Modele
{
    public class Jour : ObservableObject
    {

        private EJours nom;
        private ObservableNotifiableCollection<Enseignement> enseignements;

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

        public Collection<Enseignement> Enseignements
        {
            get
            {
                return enseignements;
            }
        }

        public Jour()
        {
            enseignements = new ObservableNotifiableCollection<Enseignement>();
            enseignements.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
            enseignements.ItemPropertyChanged += OnItemPropertyChanged;
        }

        public void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ObjectChanged("Enseignements");
        }

        private void OnItemPropertyChanged(object sender, ItemPropertyChangedEventArgs args)
        {
            ObjectChanged("Enseignements");
        }

        public override string ToString()
        {
            return "Jour : " + nom;
        }

    }
}
