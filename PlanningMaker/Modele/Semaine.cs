using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace PlanningMaker.Modele
{
    public class Semaine : ObservableObject
    {

        private int numero;
        private String date;

        private ObservableCollection<Jour> jours;

        public int Numero
        {
            get
            {
                return numero;
            }
            set
            {
                numero = value;
                ObjectChanged("Numero");
            }
        }

        public String Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                ObjectChanged("Date");
            }
        }

        public ICollection<Jour> Jours
        {
            get
            {
                return jours;
            }
        }

        public Semaine()
        {
            promotion = "undefined";
            numero = 0;
            date = "01/01/1900";
            jours = new ObservableCollection<Jour>();
        }

        public Semaine(EAnnees annee, EDivisions division, String promotion, int numero, String date)
        {
            this.annee = annee;
            this.division = division;
            this.promotion = promotion;
            this.numero = numero;
            this.date = date;
            this.jours = new ObservableCollection<Jour>();
        }

        public override string ToString()
        {
            return "Semaine " + numero + " du " + date + " : " + annee + " " + division + " - " + promotion;
        }
    }
}
