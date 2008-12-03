using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanningMaker.Vues
{
    public class PlanningCommands
    {

        private static System.Windows.Input.RoutedUICommand exit;

        static PlanningCommands()
        {
            exit = new System.Windows.Input.RoutedUICommand(
                "Exit", "Quitter PlanningMaker 2008", typeof(PlanningCommands));
        }

        public static System.Windows.Input.RoutedUICommand Exit
        {
            get
            {
                return exit;
            }
        }
    }
}
