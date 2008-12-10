﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanningMaker.Modele
{
    public class Enseignant : ObservableObject
    {

        private String nom;
        private String prenom;

        public String Nom
        {
            get
            {
                return nom;
            }
            set
            {
                nom = value;
                ObjectChanged("Nom");
            }
        }

        public String Prenom
        {
            get
            {
                return prenom;
            }
            set
            {
                prenom = value;
                ObjectChanged("Prenom");
            }
        }

        public Enseignant()
        {
            this.nom = "undefined";
            this.prenom = "undefined";
        }

        public Enseignant(String nom, String prenom)
        {
            this.nom = nom;
            this.prenom = prenom;
        }

        public override string ToString()
        {
            return "Enseignant : " + nom + " " + prenom;
        }

    }
}