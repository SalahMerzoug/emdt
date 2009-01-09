using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanningMaker.Vues
{
    public class NewWeekCommands
    {
        private static System.Windows.Input.RoutedUICommand ok;
        private static System.Windows.Input.RoutedUICommand cancel;

        static NewWeekCommands()
        {
            ok = new System.Windows.Input.RoutedUICommand(
                "OK", "Créer une nouvelle semaine", typeof(NewWeekCommands));
            cancel = new System.Windows.Input.RoutedUICommand(
                "Cancel", "Annuler la création", typeof(NewWeekCommands));
        }

        public static System.Windows.Input.RoutedUICommand OK
        {
            get
            {
                return ok;
            }
        }

        public static System.Windows.Input.RoutedUICommand Cancel
        {
            get
            {
                return cancel;
            }
        }
    }
}
