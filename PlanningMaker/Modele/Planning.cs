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
                string id = elementEnseignant.SelectSingleNode("@id").Value;
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
                string id = elementMatiere.SelectSingleNode("@id").Value;
                string titreMatiere = elementMatiere.SelectSingleNode("titre/text()").Value;
                Matiere matiere = new Matiere(titreMatiere);
                foreach (XmlNode elementEnseignant in elementMatiere.SelectNodes("enseignant"))
                {
                    string idEnseignant = elementEnseignant.SelectSingleNode("@ref").Value;
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
                string id = elementHoraire.SelectSingleNode("@id").Value;
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
                string id = elementSalle.SelectSingleNode("@id").Value;
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
                        string idEnseignant = elementEnseignement.SelectSingleNode("enseignant/@ref").Value;
                        enseignement.Enseignant = dicoEnseignants[idEnseignant];
                        string idMatiere = elementEnseignement.SelectSingleNode("matiere/@ref").Value;
                        enseignement.Matiere = dicoMatieres[idMatiere];
                        string idHoraire1 = elementEnseignement.SelectSingleNode("horaire[1]/@ref").Value;
                        enseignement.Horaire1 = dicoHoraires[idHoraire1];
                        string idHoraire2 = "";
                        if(elementEnseignement.SelectSingleNode("horaire[2]/@ref") != null){
                            idHoraire2 = elementEnseignement.SelectSingleNode("horaire[2]/@ref").Value;
                            enseignement.Horaire2 = dicoHoraires[idHoraire2];
                        }else{
                            enseignement.Horaire2 = null;
                        }
                        string idSalle = elementEnseignement.SelectSingleNode("salle/@ref").Value;
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
            //structure générale
            XmlElement planning = document.CreateElement("emploi-du-temps");
            XmlElement enseignantsPlanning = document.CreateElement("enseignants");
            XmlElement matieresPlanning = document.CreateElement("matieres");
            XmlElement horairesPlanning = document.CreateElement("horaires");
            XmlElement sallesPlanning = document.CreateElement("salles");
            XmlElement semainesPlanning = document.CreateElement("semaines");
            XmlElement anneePlanning = document.CreateElement("annee");
            XmlElement divisionPlanning = document.CreateElement("division");
            XmlElement promotionPlanning = document.CreateElement("promotion");
            document.AppendChild(planning);
            planning.AppendChild(enseignantsPlanning);
            planning.AppendChild(matieresPlanning);
            planning.AppendChild(horairesPlanning);
            planning.AppendChild(sallesPlanning);
            planning.AppendChild(semainesPlanning);
            planning.AppendChild(anneePlanning);
            planning.AppendChild(divisionPlanning);
            planning.AppendChild(promotionPlanning);
            anneePlanning.AppendChild(document.CreateTextNode(Enum.Format(typeof(EAnnees), annee, "G")));
            divisionPlanning.AppendChild(document.CreateTextNode(Enum.Format(typeof(EDivisions), division, "G")));
            promotionPlanning.AppendChild(document.CreateTextNode(promotion));
            //enseignants
            int compteurIdEnseignant = 0;
            foreach (Enseignant e in enseignants)
            {
                XmlElement enseignant = document.CreateElement("enseignant");
                AjouterEnseignant(e, compteurIdEnseignant, document, enseignantsPlanning, enseignant);
                compteurIdEnseignant++;
            }
            //matieres
            int compteurIdMatiere = 0;
            foreach (Matiere m in matieres)
            {
                XmlElement matiere = document.CreateElement("matiere");
                AjouterMatiere(m, compteurIdMatiere, document, matieresPlanning, matiere);
                compteurIdMatiere++;
            }
            //horaires
            int compteurIdHoraire = 0;
            foreach (Horaire h in horaires)
            {
                XmlElement horaire = document.CreateElement("horaire");
                AjouterHoraire(h, compteurIdHoraire, document, horairesPlanning, horaire);
                compteurIdHoraire++;
            }
            //salles
            int compteurIdSalle = 0;
            foreach (Salle s in salles)
            {
                XmlElement salle = document.CreateElement("salle");
                AjouterSalle(s, compteurIdSalle, document, sallesPlanning, salle);
                compteurIdSalle++;
            }
            //semaines
            foreach (Semaine sem in semaines)
            {
                XmlElement semaine = document.CreateElement("semaine");
                AjouterSemaine(sem, document, semainesPlanning, semaine);
            }
            document.Save(nomFichier);
        }

        private void AjouterEnseignant(Enseignant e, int compteurId, XmlDocument document, XmlElement enseignants, XmlElement enseignant)
        {
            XmlAttribute id = document.CreateAttribute("id");
            XmlElement prenom = document.CreateElement("prenom");
            XmlElement nom = document.CreateElement("nom");
            enseignants.AppendChild(enseignant);
            enseignant.Attributes.Append(id);
            enseignant.AppendChild(prenom);
            enseignant.AppendChild(nom);
            id.Value = "id" + compteurId.ToString();
            prenom.AppendChild(document.CreateTextNode(e.Prenom));
            nom.AppendChild(document.CreateTextNode(e.Nom));
        }

        private void AjouterMatiere(Matiere m, int compteurId, XmlDocument document, XmlElement matieres, XmlElement matiere)
        {
            XmlAttribute id = document.CreateAttribute("id");
            XmlElement titre = document.CreateElement("titre");
            foreach (Enseignant e in m.Enseignants)
            {
                XmlElement enseignant = document.CreateElement("enseignant");
                matiere.AppendChild(enseignant);
                XmlAttribute refEnseignant = document.CreateAttribute("ref");
                matiere.Attributes.Append(refEnseignant);
                refEnseignant.Value = "id" + enseignants.IndexOf(e).ToString();
            }
            XmlElement nom = document.CreateElement("nom");
            matieres.AppendChild(matiere);
            matiere.Attributes.Append(id);
            matiere.AppendChild(titre);
            id.Value = "id" + compteurId.ToString();
            titre.AppendChild(document.CreateTextNode(m.Titre));
        }

        private void AjouterHoraire(Horaire h, int compteurId, XmlDocument document, XmlElement horaires, XmlElement horaire)
        {
            XmlAttribute id = document.CreateAttribute("id");
            XmlElement debut = document.CreateElement("debut");
            XmlElement fin = document.CreateElement("fin");
            horaires.AppendChild(horaire);
            horaire.Attributes.Append(id);
            horaire.AppendChild(debut);
            horaire.AppendChild(fin);
            id.Value = "id" + compteurId.ToString();
            debut.AppendChild(document.CreateTextNode(h.Debut));
            fin.AppendChild(document.CreateTextNode(h.Fin));
        }

        private void AjouterSalle(Salle s, int compteurId, XmlDocument document, XmlElement salles, XmlElement salle)
        {
            XmlAttribute id = document.CreateAttribute("id");
            XmlElement nom = document.CreateElement("nom");
            XmlElement typeSalle = document.CreateElement("typeSalle");
            salles.AppendChild(salle);
            salle.Attributes.Append(id);
            salle.AppendChild(nom);
            salle.AppendChild(typeSalle);
            id.Value = "id" + compteurId.ToString();
            nom.AppendChild(document.CreateTextNode(s.Nom));
            typeSalle.AppendChild(document.CreateTextNode(s.GetType().Name)); // A vérifier
        }

        private void AjouterSemaine(Semaine s, XmlDocument document, XmlElement semaines, XmlElement semaine)
        {
            XmlElement numero = document.CreateElement("numero");
            XmlElement date = document.CreateElement("date");
            semaines.AppendChild(semaine);
            semaine.AppendChild(numero);
            semaine.AppendChild(date);
            numero.AppendChild(document.CreateTextNode(s.Numero.ToString()));
            date.AppendChild(document.CreateTextNode(s.Date));
            foreach (Jour j in s.Jours)
            {
                XmlElement jour = document.CreateElement("jour");
                XmlElement nom = document.CreateElement("nom");
                XmlElement enseignements = document.CreateElement("enseignements");
                semaine.AppendChild(jour);
                jour.AppendChild(nom);
                jour.AppendChild(enseignements);
                nom.AppendChild(document.CreateTextNode(Enum.Format(typeof(EJours), j.Nom, "G").ToLower()));
                foreach (Enseignement e in j.Enseignements)
	            {
                    XmlElement enseignement = document.CreateElement("enseignement");
                    AjouterEnseignement(e, document, enseignements, enseignement);
	            }
            }
        }

        private void AjouterEnseignement(Enseignement e, XmlDocument document, XmlElement enseignements, XmlElement enseignement)
        {
            XmlElement type = document.CreateElement("type");
            XmlElement enseignant = document.CreateElement("enseignant");
            XmlAttribute refEnseignant = document.CreateAttribute("ref");
            XmlElement matiere = document.CreateElement("matiere");
            XmlAttribute refMatiere = document.CreateAttribute("ref");
            XmlElement horaire1 = document.CreateElement("horaire");
            XmlAttribute refHoraire1 = document.CreateAttribute("ref");
            XmlElement horaire2 = document.CreateElement("horaire");
            XmlAttribute refHoraire2 = document.CreateAttribute("ref");
            XmlElement salle = document.CreateElement("salle");
            XmlAttribute refSalle = document.CreateAttribute("ref");
            enseignements.AppendChild(enseignement);
            enseignement.AppendChild(type);
            enseignement.AppendChild(enseignant);
            enseignant.Attributes.Append(refEnseignant);
            enseignement.AppendChild(matiere);
            matiere.Attributes.Append(refMatiere);
            enseignement.AppendChild(horaire1);
            horaire1.Attributes.Append(refHoraire1);
            if (e.Horaire2 != null)
            {
                enseignement.AppendChild(horaire2);
                horaire2.Attributes.Append(refHoraire2);
            }
            enseignement.AppendChild(salle);
            salle.Attributes.Append(refSalle);
            type.AppendChild(document.CreateTextNode(e.GetType().Name.ToUpper()));
            refEnseignant.Value = "id" + enseignants.IndexOf(e.Enseignant).ToString();
            refMatiere.Value = "id" + matieres.IndexOf(e.Matiere).ToString();
            refHoraire1.Value = "id" + horaires.IndexOf(e.Horaire1).ToString();
            if (e.Horaire2 != null)
            {
                refHoraire2.Value = "id" + horaires.IndexOf(e.Horaire2).ToString();
            }
            refSalle.Value = "id" + salles.IndexOf(e.Salle).ToString();
        }
    }
}
