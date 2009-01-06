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
        private static System.Windows.Input.RoutedUICommand nextWeek;
        private static System.Windows.Input.RoutedUICommand previousWeek;
        private static System.Windows.Input.RoutedUICommand ajouterProf;
        private static System.Windows.Input.RoutedUICommand supprimerProf;

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
            previousWeek = new System.Windows.Input.RoutedUICommand(
                "Precedent", "Semaine Précédente", typeof(PlanningCommands));
            nextWeek = new System.Windows.Input.RoutedUICommand(
                "Suivante", "Semaine Suivante", typeof(PlanningCommands));
            ajouterProf = new System.Windows.Input.RoutedUICommand(
                "Ajouter Professeur", "Ajouter Professeur", typeof(PlanningCommands));
            supprimerProf = new System.Windows.Input.RoutedUICommand(
                "Supprimer Professeur", "Supprimer Professeur", typeof(PlanningCommands));
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

        public static System.Windows.Input.RoutedUICommand NextWeek
        {
            get
            {
                return nextWeek;
            }
        }

        public static System.Windows.Input.RoutedUICommand PreviousWeek
        {
            get
            {
                return previousWeek;
            }
        }

        public static System.Windows.Input.RoutedUICommand AjouterProf
        {
            get
            {
                return ajouterProf;
            }
        }

        public static System.Windows.Input.RoutedUICommand SupprimerProf
        {
            get
            {
                return supprimerProf;
            }
        }
    }
}
