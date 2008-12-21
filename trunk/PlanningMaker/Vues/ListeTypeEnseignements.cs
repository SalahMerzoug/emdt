using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
