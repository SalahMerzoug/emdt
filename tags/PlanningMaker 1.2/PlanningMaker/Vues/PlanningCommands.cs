using System.Windows.Input;

namespace PlanningMaker.Vues
{
    public class PlanningCommands
    {
        private static RoutedUICommand importer;
        private static RoutedUICommand exporter;
        private static RoutedUICommand exit;
        private static RoutedUICommand ajouterElement;
        private static RoutedUICommand supprimerElement;
        private static RoutedUICommand newWeek;
        private static RoutedUICommand delWeek;
        private static RoutedUICommand nextWeek;
        private static RoutedUICommand previousWeek;
        private static RoutedUICommand ajouterProf;
        private static RoutedUICommand supprimerProf;

        private static RoutedUICommand ok;
        private static RoutedUICommand cancel;

        static PlanningCommands()
        {
            importer = new RoutedUICommand("Importer", "Importer", typeof(PlanningCommands));
            exporter = new RoutedUICommand("Exporter", "Exporter", typeof(PlanningCommands));
            exit = new RoutedUICommand("Exit", "Quitter PlanningMaker 2008", typeof(PlanningCommands));
            ajouterElement = new RoutedUICommand("Ajouter", "Ajouter une ressource", typeof(PlanningCommands));
            supprimerElement = new RoutedUICommand("Supprimer", "Supprimer une ressource", typeof(PlanningCommands));
            newWeek = new RoutedUICommand("Nouvelle semaine", "Ajouter une nouvelle semaine", typeof(PlanningCommands));
            delWeek = new RoutedUICommand("Supprimer semaine", "Ajouter une nouvelle semaine", typeof(PlanningCommands));
            previousWeek = new RoutedUICommand("Precedent", "Semaine précédente", typeof(PlanningCommands));
            nextWeek = new RoutedUICommand("Suivante", "Semaine suivante", typeof(PlanningCommands));
            ajouterProf = new RoutedUICommand("Ajouter professeur", "Ajouter professeur", typeof(PlanningCommands));
            supprimerProf = new RoutedUICommand("Supprimer professeur", "Supprimer professeur", typeof(PlanningCommands));

            ok = new RoutedUICommand("OK", "Créer une nouvelle semaine", typeof(PlanningCommands));
            cancel = new RoutedUICommand("Cancel", "Annuler la création", typeof(PlanningCommands));
        }

        public static RoutedUICommand Importer
        {
            get
            {
                return importer;
            }
        }

        public static RoutedUICommand Exporter
        {
            get
            {
                return exporter;
            }
        }

        public static RoutedUICommand Exit
        {
            get
            {
                return exit;
            }
        }

        public static RoutedUICommand AjouterElement
        {
            get
            {
                return ajouterElement;
            }
        }

        public static RoutedUICommand SupprimerElement
        {
            get
            {
                return supprimerElement;
            }
        }

        public static RoutedUICommand NewWeek
        {
            get
            {
                return newWeek;
            }
        }

        public static RoutedUICommand DelWeek
        {
            get
            {
                return delWeek;
            }
        }

        public static RoutedUICommand NextWeek
        {
            get
            {
                return nextWeek;
            }
        }

        public static RoutedUICommand PreviousWeek
        {
            get
            {
                return previousWeek;
            }
        }

        public static RoutedUICommand AjouterProf
        {
            get
            {
                return ajouterProf;
            }
        }

        public static RoutedUICommand SupprimerProf
        {
            get
            {
                return supprimerProf;
            }
        }


        public static RoutedUICommand OK
        {
            get
            {
                return ok;
            }
        }

        public static RoutedUICommand Cancel
        {
            get
            {
                return cancel;
            }
        }
    }
}
