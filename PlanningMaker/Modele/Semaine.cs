﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace PlanningMaker.Modele
{
    public class Semaine : ObservableObject
    {
        private int numero;
        private String date;

        private ObservableNotifiableCollection<Jour> jours;
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

        public Collection<Jour> Jours
        {
            get
            {
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
                lundi.Nom = EJours.Lundi;
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
            numero = 1;
            date = "1990-12-01";
            
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

            jours = new ObservableNotifiableCollection<Jour>();
            jours.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
            jours.ItemPropertyChanged += OnItemPropertyChanged;
            jours.Add(lundi);
            jours.Add(mardi);
            jours.Add(mercredi);
            jours.Add(jeudi);
            jours.Add(vendredi);
        }

        public Semaine(int numero, String date)
        {
            //Si le numéro est inférieur à un, le numéro de la semaine sera automatiquement assigné à un
            this.numero = (numero>0) ? numero : 1; 
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

            jours = new ObservableNotifiableCollection<Jour>();
            jours.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
            jours.ItemPropertyChanged += OnItemPropertyChanged;
            jours.Add(lundi);
            jours.Add(mardi);
            jours.Add(mercredi);
            jours.Add(jeudi);
            jours.Add(vendredi);
        }

        public void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ObjectChanged("Jours");
        }

        private void OnItemPropertyChanged(object sender, ItemPropertyChangedEventArgs args)
        {
            ObjectChanged("Jours");
        }

        public override string ToString()
        {
            return "Semaine " + numero + " du " + date;
        }
    }
}
