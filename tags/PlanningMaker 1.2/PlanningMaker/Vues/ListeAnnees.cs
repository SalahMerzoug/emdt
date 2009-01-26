using System;
using System.Collections.ObjectModel;
using PlanningMaker.Modele;

namespace PlanningMaker.Vues
{
    public class ListeAnnees : ObservableCollection<string>
    {
        public ListeAnnees()
        {
            foreach (String s in Enum.GetNames(typeof(EAnnees)))
                Add(s);
        }
    }
}
