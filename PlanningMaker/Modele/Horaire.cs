using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PlanningMaker.Modele
{
    public class Horaire : ObservableObject
    {
        private String debut;
        private String fin;

        public string Debut
        {
            get
            {
                return debut;
            }
            set
            {
                debut = value;
                ObjectChanged("Debut");
            }
        }

        public string Fin
        {
            get
            {
                return fin;
            }
            set
            {
                fin = value;
                ObjectChanged("Fin");
            }
        }

        public Horaire()
        {
            debut = "00h00";
            fin = "00h00";
        }

        public Horaire(String debut, String fin)
        {
            this.debut = debut;
            this.fin = fin;
        }

        public override string ToString()
        {
            return "Horaire : " + debut + " - " + fin;
        }

        public static bool IsHeureValide(string heure)
        {
            Regex rx = new Regex(@"^(\d|[1-2]\d|[0]{2})[h][0-5]\d$");
            if (rx.IsMatch(heure))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Méthode qui teste si un horaire se suit cet horraire (Importance de l'ordre !).
        /// Deux horraires se suivent si l'heure de fin  du premier horaire
        /// est avant l'heure de début du second horaire avec au plus 15 minutes de battement.
        /// </summary>
        /// <param name="h">Horraire à tester</param>
        /// <returns>Renvoie true si les horraires se suivent</returns>
        public bool EstSuiviPar(Horaire h)
        {
            bool result = false;
            string[] h1 = this.fin.Split('h');
            string[] h2 = h.debut.Split('h');

            if (h1.Length == 2 && h2.Length == 2)
            {
                try
                {
                    int heure1 = Int32.Parse(h1[0]);
                    int minutes1 = Int32.Parse(h1[1]);
                    int heure2 = Int32.Parse(h2[0]);
                    int minutes2 = Int32.Parse(h2[1]);

                    //Convertion en minutes
                    minutes1 += heure1 * 60;
                    minutes2 += heure2 * 60;

                    if ((minutes1 <= minutes2)&&(minutes2 - minutes1) < 16)
                        result = true;
                }
                catch (Exception)
                {
                }
            }

            return result;
        }
    }
}
