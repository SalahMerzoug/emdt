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
    }
}
