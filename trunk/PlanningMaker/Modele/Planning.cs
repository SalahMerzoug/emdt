using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml;

namespace PlanningMaker.Modele
{
    public class Planning : ObservableObject
    {
        private EAnnees annee;
        private EDivisions division;
        private String promotion;

        private ObservableCollection<Enseignant> enseignants;
        private ObservableCollection<Matiere> matieres;
        private ObservableCollection<Horaire> horaires;
        private ObservableCollection<Salle> salles;
        private ObservableCollection<Semaine> semaines;


        public EAnnees Annee
        {
            get
            {
                return annee;
            }
            set
            {
                annee = value;
                ObjectChanged("Annee");
            }
        }

        public EDivisions Division
        {
            get
            {
                return division;
            }
            set
            {
                division = value;
                ObjectChanged("Division");
            }
        }

        public String Promotion
        {
            get
            {
                return promotion;
            }
            set
            {
                promotion = value;
                ObjectChanged("Promotion");
            }
        }

        public ICollection<Enseignant> Enseignants
        {
            get
            {
                return enseignants;
            }
        }

        public ICollection<Matiere> Matieres
        {
            get
            {
                return matieres;
            }
        }

        public ICollection<Horaire> Horaires
        {
            get
            {
                return horaires;
            }
        }

        public ICollection<Salle> Salles
        {
            get
            {
                return salles;
            }
        }

        public ICollection<Semaine> Semaines
        {
            get
            {
                return semaines;
            }
        }

        public Planning()
        {
            promotion = "undefined";
            enseignants = new ObservableCollection<Enseignant>();
            matieres = new ObservableCollection<Matiere>();
            horaires = new ObservableCollection<Horaire>();
            salles = new ObservableCollection<Salle>();
            semaines = new ObservableCollection<Semaine>();
        }

        public Planning(String promotion)
        {
            this.promotion = promotion;
            enseignants = new ObservableCollection<Enseignant>();
            matieres = new ObservableCollection<Matiere>();
            horaires = new ObservableCollection<Horaire>();
            salles = new ObservableCollection<Salle>();
            semaines = new ObservableCollection<Semaine>();
        }

        public void Charger(string nomFichier)
        {
            XmlDocument document = new XmlDocument();
            document.Load(nomFichier);
            XmlNode elementPlanning = document.SelectSingleNode("/emploi-du-temps");
            string valueAnnee = elementPlanning.SelectSingleNode("annee/text()").Value;
            switch (valueAnnee)
            {
                case "P1" :
                    annee = EAnnees.P1;
                    break;
                case "P2" :
                    annee = EAnnees.P2;
                    break;
                case "I1" :
                    annee = EAnnees.I1;
                    break;
                case "I2" :
                    annee = EAnnees.I2;
                    break;
                case "I3" :
                    annee = EAnnees.I3;
                    break;
            }
            string valueDivision = elementPlanning.SelectSingleNode("division/text()").Value;
            switch (valueDivision)
            {
                case "A":
                    division = EDivisions.A;
                    break;
                case "B":
                    division = EDivisions.B;
                    break;
                case "C":
                    division = EDivisions.C;
                    break;
                case "D":
                    division = EDivisions.D;
                    break;
            }
            promotion = elementPlanning.SelectSingleNode("promotion/text()").Value;
            //enseignants
            Dictionary<string, Enseignant> dicoEnseignants = new Dictionary<string, Enseignant>();
            foreach (XmlNode elementEnseignant in document.SelectNodes("/emploi-du-temps/enseignants/enseignant"))
            {
                string id = elementEnseignant.SelectSingleNode("@id/text()").Value;
                string prenomEnseignant = elementEnseignant.SelectSingleNode("prenom/text()").Value;
                string nomEnseignant = elementEnseignant.SelectSingleNode("nom/text()").Value;
                Enseignant enseignant = new Enseignant(nomEnseignant, prenomEnseignant);
                dicoEnseignants.Add(id, enseignant);
                enseignants.Add(enseignant);
            }
            //matieres
            Dictionary<string, Matiere> dicoMatieres = new Dictionary<string, Matiere>();
            foreach (XmlNode elementMatiere in document.SelectNodes("/emploi-du-temps/matieres/matiere"))
            {
                string id = elementMatiere.SelectSingleNode("@id/text()").Value;
                string titreMatiere = elementMatiere.SelectSingleNode("titre/text()").Value;
                Matiere matiere = new Matiere(titreMatiere);
                foreach (XmlNode elementEnseignant in elementMatiere.SelectNodes("enseignant"))
                {
                    string idEnseignant = elementEnseignant.SelectSingleNode("@ref/text()").Value;
                    Enseignant enseignant = dicoEnseignants[idEnseignant];
                    matiere.Enseignants.Add(enseignant);
                }
                dicoMatieres.Add(id, matiere);
                matieres.Add(matiere);
            }
            //horaires
            Dictionary<string, Horaire> dicoHoraires = new Dictionary<string, Horaire>();
            foreach (XmlNode elementHoraire in document.SelectNodes("/emploi-du-temps/horaires/horaire"))
            {
                string id = elementHoraire.SelectSingleNode("@id/text()").Value;
                string debutHoraire = elementHoraire.SelectSingleNode("debut/text()").Value;
                string finHoraire = elementHoraire.SelectSingleNode("fin/text()").Value;
                Horaire horaire = new Horaire(debutHoraire, finHoraire);
                dicoHoraires.Add(id, horaire);
                horaires.Add(horaire);
            }
            //salles
            Dictionary<string, Salle> dicoSalles = new Dictionary<string, Salle>();
            foreach (XmlNode elementSalle in document.SelectNodes("/emploi-du-temps/salles/salle"))
            {
                string id = elementSalle.SelectSingleNode("@id/text()").Value;
                string nomSalle = elementSalle.SelectSingleNode("nom/text()").Value;
                string typeSalle = elementSalle.SelectSingleNode("typeSalle/text()").Value;
                switch (typeSalle)
                {
                    case "Labo" :
                        Labo labo = new Labo(nomSalle);
                        dicoSalles.Add(id, labo);
                        salles.Add(labo);
                        break;
                    case "Amphi" :
                        Amphi amphi = new Amphi(nomSalle);
                        dicoSalles.Add(id, amphi);
                        salles.Add(amphi);
                        break;
                }
            }
            //semaines
            foreach (XmlNode elementSemaine in document.SelectNodes("/emploi-du-temps/semaines/semaine"))
            {
                int numeroSemaine = System.Int32.Parse(elementSemaine.SelectSingleNode("numero/text()").Value);
                string dateSemaine = elementSemaine.SelectSingleNode("date/text()").Value;
                Semaine semaine = new Semaine(numeroSemaine, dateSemaine);
                foreach (XmlNode elementJour in elementSemaine.SelectNodes("jour"))
                {
                    string nomJour = elementJour.SelectSingleNode("nom/text()").Value;
                    Jour jour = new Jour();
                    switch (nomJour)
                    {
                        case "lundi" :
                            jour.Nom= EJours.Lundi;
                            break;
                        case "mardi" :
                            jour.Nom = EJours.Mardi;
                            break;
                        case "mercredi" :
                            jour.Nom = EJours.Mercredi;
                            break;
                        case "jeudi" :
                            jour.Nom = EJours.Jeudi;
                            break;
                        case "vendredi" :
                            jour.Nom = EJours.Vendredi;
                            break;
                    }
                    foreach (XmlNode elementEnseignement in elementJour.SelectNodes("enseignements/enseignement"))
                    {
                        int numeroGroupe = 0;
                        XmlNode nodeNumeroGroupe = elementEnseignement.SelectSingleNode("numeroGroupe/text()");
                        if (nodeNumeroGroupe != null)
                        {
                            numeroGroupe = System.Int32.Parse(nodeNumeroGroupe.Value);
                        }
                        string typeEnseignement = elementEnseignement.SelectSingleNode("type/text()").Value;
                        Enseignement enseignement = null;
                        switch(typeEnseignement){
                            case "COURS" :
                                enseignement = new Cours(numeroGroupe);
                                break;
                            case "TD" :
                                enseignement = new TD(numeroGroupe);
                                break;
                            case "TP" :
                                enseignement = new TP(numeroGroupe);
                                break;
                        }
                        string idEnseignant = elementEnseignement.SelectSingleNode("enseignant/@idref").Value;
                        enseignement.Enseignant = dicoEnseignants[idEnseignant];
                        string idMatiere = elementEnseignement.SelectSingleNode("matiere/@idref").Value;
                        enseignement.Matiere = dicoMatieres[idMatiere];
                        string idHoraire1 = elementEnseignement.SelectSingleNode("horaire[1]/@idref").Value;
                        enseignement.Horaire1 = dicoHoraires[idHoraire1];
                        string idHoraire2 = "";
                        if(elementEnseignement.SelectSingleNode("horaire[2]/@idref") != null){
                            idHoraire2 = elementEnseignement.SelectSingleNode("horaire[2]/@idref").Value;
                            enseignement.Horaire2 = dicoHoraires[idHoraire2];
                        }else{
                            enseignement.Horaire2 = null;
                        }
                        string idSalle = elementEnseignement.SelectSingleNode("matiere/@idref").Value;
                        enseignement.Salle = dicoSalles[idSalle];
                        jour.Enseignements.Add(enseignement);
                    }
                    semaine.Jours.Add(jour);
                }
                semaines.Add(semaine);
            }
        }

        public void Sauver(string nomFichier)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "ISO-8859-15", null);
            document.AppendChild(declaration);
            // TODO
            document.Save(nomFichier);
        }
    }
}
