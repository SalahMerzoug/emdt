using System;
using System.Collections.ObjectModel;
using PlanningMaker.Modele;

namespace PlanningMaker.Vues
{
    public class ListeTypeSalles : ObservableCollection<string>
    {
        public ListeTypeSalles(){
            foreach(String s in Enum.GetNames(typeof(ETypeSalles)))
                Add(s);
        }
    }
}
