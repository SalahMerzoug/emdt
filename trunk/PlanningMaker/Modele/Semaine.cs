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

        private Jour lundi;
        private Jour mardi;
        private Jour mercredi;
        private Jour jeudi;
        private Jour vendredi;

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
                Collection<Jour> jours = new Collection<Jour>();
                jours.Add(lundi);
                jours.Add(mardi);
                jours.Add(mercredi);
                jours.Add(jeudi);
                jours.Add(vendredi);
                return jours;
            }
        }

        public Jour Lundi
        {
            get
            {
                return lundi;
            }
            set
            {
                lundi = value;
                Lundi.Nom = EJours.Lundi;
                ObjectChanged("Lundi");
            }
        }

        public Jour Mardi
        {
            get
            {
                return mardi;
            }
            set
            {
                mardi = value;
                mardi.Nom = EJours.Mardi;
                ObjectChanged("Mardi");
            }
        }

        public Jour Mercredi
        {
            get
            {
                return mercredi;
            }
            set
            {
                mercredi = value;
                mercredi.Nom = EJours.Mercredi;
                ObjectChanged("Mercredi");
            }
        }

        public Jour Jeudi
        {
            get
            {
                return jeudi;
            }
            set
            {
                jeudi = value;
                jeudi.Nom = EJours.Jeudi;
                ObjectChanged("Jeudi");
            }
        }

        public Jour Vendredi
        {
            get
            {
                return vendredi;
            }
            set
            {
                vendredi = value;
                vendredi.Nom = EJours.Vendredi;
                ObjectChanged("Vendredi");
            }
        }

        public Semaine()
        {
            numero = 0;
            date = "01/01/1900";
            
            lundi = new Jour();
            mardi = new Jour();
            mercredi = new Jour();
            jeudi = new Jour();
            vendredi = new Jour();

            lundi.Nom = EJours.Lundi;
            mardi.Nom = EJours.Mardi;
            mercredi.Nom = EJours.Mercredi;
            jeudi.Nom = EJours.Jeudi;
            vendredi.Nom = EJours.Vendredi;
        }

        public Semaine(int numero, String date)
        {;
            this.numero = numero;
            this.date = date;

            lundi = new Jour();
            mardi = new Jour();
            mercredi = new Jour();
            jeudi = new Jour();
            vendredi = new Jour();

            lundi.Nom = EJours.Lundi;
            mardi.Nom = EJours.Mardi;
            mercredi.Nom = EJours.Mercredi;
            jeudi.Nom = EJours.Jeudi;
            vendredi.Nom = EJours.Vendredi;
        }

        public override string ToString()
        {
            return "Semaine " + numero + " du " + date;
        }
    }
}
