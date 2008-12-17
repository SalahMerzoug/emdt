using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanningMaker.Vues
{
    public class PlanningCommands
    {
        private static System.Windows.Input.RoutedUICommand importer;
        private static System.Windows.Input.RoutedUICommand exporter;
        private static System.Windows.Input.RoutedUICommand exit;
        private static System.Windows.Input.RoutedUICommand ajouterElement;
        private static System.Windows.Input.RoutedUICommand supprimerElement;

        static PlanningCommands()
        {
            importer = new System.Windows.Input.RoutedUICommand(
                "Importer", "Importer", typeof(PlanningCommands));
            exporter = new System.Windows.Input.RoutedUICommand(
                "Exporter", "Exporter", typeof(PlanningCommands));
            exit = new System.Windows.Input.RoutedUICommand(
                "Exit", "Quitter PlanningMaker 2008", typeof(PlanningCommands));
            ajouterElement = new System.Windows.Input.RoutedUICommand(
                "Ajouter", "Ajouter une ressource", typeof(PlanningCommands));
            supprimerElement = new System.Windows.Input.RoutedUICommand(
                "Supprimer", "Supprimer une ressource", typeof(PlanningCommands));
        }

        public static System.Windows.Input.RoutedUICommand Importer
        {
            get
            {
                return importer;
            }
        }

        public static System.Windows.Input.RoutedUICommand Exporter
        {
            get
            {
                return exporter;
            }
        }

        public static System.Windows.Input.RoutedUICommand Exit
        {
            get
            {
                return exit;
            }
        }

        public static System.Windows.Input.RoutedUICommand AjouterElement
        {
            get
            {
                return ajouterElement;
            }
        }

        public static System.Windows.Input.RoutedUICommand SupprimerElement
        {
            get
            {
                return supprimerElement;
            }
        }
    }
}
