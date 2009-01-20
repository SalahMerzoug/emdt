using System;
using System.Collections.ObjectModel;
using PlanningMaker.Modele;

namespace PlanningMaker.Vues
{
    public class ListeTypeEnseignements : ObservableCollection<string>
    {
        public ListeTypeEnseignements()
        {
            foreach(String s in Enum.GetNames(typeof(ETypeEnseignements)))
                Add(s);
        }
    }
}
