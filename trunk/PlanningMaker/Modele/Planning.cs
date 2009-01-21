using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace PlanningMaker.Modele
{
    public class Planning : ObservableObject
    {
        private EAnnees annee;
        private EDivisions division;
        private String promotion;

        private ObservableNotifiableCollection<Enseignant> enseignants;
        private ObservableNotifiableCollection<Matiere> matieres;
        private ObservableNotifiableCollection<Horaire> horaires;
        private ObservableNotifiableCollection<Salle> salles;
        private ObservableNotifiableCollection<Semaine> semaines;

        private bool hasChanged;

        public bool HasChanged
        {
            get
            {
                return hasChanged;
            }
            set
            {
                hasChanged = value;
            }
        }

        public EAnnees Annee
        {
            get
            {
                return annee;
            }
            set
            {
                annee = value;
                this.HasChanged = true;
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
                this.HasChanged = true;
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
                this.HasChanged = true;
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
            promotion = "Undefined";

            enseignants = new ObservableNotifiableCollection<Enseignant>();
            enseignants.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
            enseignants.ItemPropertyChanged += OnItemPropertyChanged;

            matieres = new ObservableNotifiableCollection<Matiere>();
            matieres.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
            matieres.ItemPropertyChanged += OnItemPropertyChanged;

            horaires = new ObservableNotifiableCollection<Horaire>();
            horaires.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
            horaires.ItemPropertyChanged += OnItemPropertyChanged;

            salles = new ObservableNotifiableCollection<Salle>();
            salles.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
            salles.ItemPropertyChanged += OnItemPropertyChanged;

            semaines = new ObservableNotifiableCollection<Semaine>();
            semaines.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
            semaines.ItemPropertyChanged += OnItemPropertyChanged;
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.HasChanged = true;
            ObjectChanged("");
        }

        private void OnItemPropertyChanged(object sender, ItemPropertyChangedEventArgs args)
        {
            this.HasChanged = true;
            ObjectChanged("");
        }

        public void SupprimerEnseignant(Enseignant enseignant)
        {
            Enseignants.Remove(enseignant);

            foreach (Matiere m in Matieres)
            {
                m.Enseignants.Remove(enseignant);
            }

            foreach (Semaine s in Semaines)
            {
                foreach (Jour j in s.Jours)
                {
                    ArrayList enseignementsASupprimer = new ArrayList();
                    foreach (Enseignement e in j.Enseignements)
                    {
                        if (e.Enseignant == enseignant)
                            enseignementsASupprimer.Add(e);
                    }
                    foreach (Enseignement e in enseignementsASupprimer)
                    {
                        j.Enseignements.Remove(e);
                    }
                }
            }
        }

        public void SupprimerMatiere(Matiere matiere)
        {
            Matieres.Remove(matiere);

            foreach (Semaine s in Semaines)
            {
                foreach (Jour j in s.Jours)
                {
                    ArrayList enseignementsASupprimer = new ArrayList();
                    foreach (Enseignement e in j.Enseignements)
                    {
                        if (e.Matiere == matiere)
                            enseignementsASupprimer.Add(e);
                    }
                    foreach (Enseignement e in enseignementsASupprimer)
                    {
                        j.Enseignements.Remove(e);
                    }
                }
            }
        }

        public void SupprimerHoraire(Horaire horaire)
        {
            Horaires.Remove(horaire);

            foreach (Semaine s in Semaines)
            {
                foreach (Jour j in s.Jours)
                {
                    ArrayList enseignementsASupprimer = new ArrayList();
                    foreach (Enseignement e in j.Enseignements)
                    {
                        if (e.Horaire1 == horaire && e.Horaire2 == null)
                            enseignementsASupprimer.Add(e);
                        else if (e.Horaire1 == horaire)
                        {
                            e.Horaire1 = e.Horaire2;
                            e.Horaire2 = null;
                        }
                    }
                    foreach (Enseignement e in enseignementsASupprimer)
                    {
                        j.Enseignements.Remove(e);
                    }
                }
            }
        }

        public void SupprimerSalle(Salle salle)
        {
            Salles.Remove(salle);
            foreach (Semaine s in Semaines)
            {
                foreach (Jour j in s.Jours)
                {
                    foreach (Enseignement e in j.Enseignements)
                    {
                        if (e.Salle == salle)
                            e.Salle = null;
                    }
                }
            }
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
            Promotion = elementPlanning.SelectSingleNode("promotion/text()").Value;
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
                Salle salle = new Salle(nomSalle);
                switch (typeSalle)
                {
                    case "Labo" :
                        salle.Type = ETypeSalles.Labo;
                        break;
                    case "Amphi" :
                        salle.Type = ETypeSalles.Amphi;
                        break;
                }
                dicoSalles.Add(id, salle);
                salles.Add(salle);
            }
            //semaines
            foreach (XmlNode elementSemaine in document.SelectNodes("/emploi-du-temps/semaines/semaine"))
            {
                Semaine semaine = new Semaine();
                semaine.Numero = System.Int32.Parse(elementSemaine.SelectSingleNode("numero/text()").Value);
                try
                {
                    semaine.Date = elementSemaine.SelectSingleNode("date/text()").Value;
                }
                catch (NullReferenceException)
                {
                    semaine.Date = "";
                }
                finally
                {
                    foreach (XmlNode elementJour in elementSemaine.SelectNodes("jour"))
                    {
                        string nomJour = elementJour.SelectSingleNode("nom/text()").Value;
                        Jour jour = new Jour();
                        switch (nomJour)
                        {
                            case "lundi":
                                semaine.Lundi = jour;
                                semaine.Jours.Add(semaine.Lundi);
                                break;
                            case "mardi":
                                semaine.Mardi = jour;
                                semaine.Jours.Add(semaine.Mardi);
                                break;
                            case "mercredi":
                                semaine.Mercredi = jour;
                                semaine.Jours.Add(semaine.Mercredi);
                                break;
                            case "jeudi":
                                semaine.Jeudi = jour;
                                semaine.Jours.Add(semaine.Jeudi);
                                break;
                            case "vendredi":
                                semaine.Vendredi = jour;
                                semaine.Jours.Add(semaine.Vendredi);
                                break;
                        }
                        foreach (XmlNode elementEnseignement in elementJour.SelectNodes("enseignements/enseignement"))
                        {
                            string typeEnseignement = elementEnseignement.SelectSingleNode("type/text()").Value;
                            int numeroGroupe = 0;
                            XmlNode nodeNumeroGroupe = elementEnseignement.SelectSingleNode("numeroGroupe/text()");
                            if (nodeNumeroGroupe != null)
                            {
                                numeroGroupe = System.Int32.Parse(nodeNumeroGroupe.Value);
                            }
                            Enseignement enseignement = new Enseignement(numeroGroupe);
                            switch (typeEnseignement)
                            {
                                case "COURS":
                                    enseignement.Type = ETypeEnseignements.Cours;
                                    break;
                                case "TD":
                                    enseignement.Type = ETypeEnseignements.TD;
                                    break;
                                case "TP":
                                    enseignement.Type = ETypeEnseignements.TP;
                                    break;
                            }
                            string idEnseignant = elementEnseignement.SelectSingleNode("enseignant/@ref").Value;
                            enseignement.Enseignant = dicoEnseignants[idEnseignant];
                            string idMatiere = elementEnseignement.SelectSingleNode("matiere/@ref").Value;
                            enseignement.Matiere = dicoMatieres[idMatiere];
                            string idHoraire1 = elementEnseignement.SelectSingleNode("horaire[1]/@ref").Value;
                            enseignement.Horaire1 = dicoHoraires[idHoraire1];
                            string idHoraire2 = "";
                            if (elementEnseignement.SelectSingleNode("horaire[2]/@ref") != null)
                            {
                                idHoraire2 = elementEnseignement.SelectSingleNode("horaire[2]/@ref").Value;
                                enseignement.Horaire2 = dicoHoraires[idHoraire2];
                            }
                            else
                            {
                                enseignement.Horaire2 = null;
                            }
                            string idSalle = elementEnseignement.SelectSingleNode("salle/@ref").Value;
                            enseignement.Salle = dicoSalles[idSalle];
                            jour.Enseignements.Add(enseignement);
                        }
                    }
                    semaines.Add(semaine);
                }
            }
        }

        public void Sauver(string nomFichier)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "ISO-8859-15", null);
            document.AppendChild(declaration);
            //structure générale
            XmlElement planning = document.CreateElement("emploi-du-temps");
            XmlElement anneePlanning = document.CreateElement("annee");
            XmlElement divisionPlanning = document.CreateElement("division");
            XmlElement promotionPlanning = document.CreateElement("promotion");
            XmlElement enseignantsPlanning = document.CreateElement("enseignants");
            XmlElement matieresPlanning = document.CreateElement("matieres");
            XmlElement horairesPlanning = document.CreateElement("horaires");
            XmlElement sallesPlanning = document.CreateElement("salles");
            XmlElement semainesPlanning = document.CreateElement("semaines");
            
            document.AppendChild(planning);
            planning.AppendChild(anneePlanning);
            planning.AppendChild(divisionPlanning);
            planning.AppendChild(promotionPlanning);
            planning.AppendChild(enseignantsPlanning);
            planning.AppendChild(matieresPlanning);
            planning.AppendChild(horairesPlanning);
            planning.AppendChild(sallesPlanning);
            planning.AppendChild(semainesPlanning);
            
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
            id.Value = "idp" + compteurId.ToString();
            prenom.AppendChild(document.CreateTextNode(e.Prenom));
            nom.AppendChild(document.CreateTextNode(e.Nom));
        }

        private void AjouterMatiere(Matiere m, int compteurId, XmlDocument document, XmlElement matieres, XmlElement matiere)
        {
            XmlAttribute id = document.CreateAttribute("id");
            XmlElement titre = document.CreateElement("titre");
            matieres.AppendChild(matiere);
            matiere.Attributes.Append(id);
            matiere.AppendChild(titre);
            id.Value = "idm" + compteurId.ToString();
            titre.AppendChild(document.CreateTextNode(m.Titre));
            foreach (Enseignant e in m.Enseignants)
            {
                XmlElement enseignant = document.CreateElement("enseignant");
                matiere.AppendChild(enseignant);
                XmlAttribute refEnseignant = document.CreateAttribute("ref");
                enseignant.Attributes.Append(refEnseignant);
                refEnseignant.Value = "idp" + enseignants.IndexOf(e).ToString();
            }         
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
            id.Value = "idh" + compteurId.ToString();
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
            id.Value = "ids" + compteurId.ToString();
            nom.AppendChild(document.CreateTextNode(s.Nom));
            typeSalle.AppendChild(document.CreateTextNode(Enum.Format(typeof(ETypeSalles), s.Type, "G")));
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
            XmlElement numeroGroupe = document.CreateElement("numeroGroupe");
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
            if (e.Groupe != 0)
            {
                enseignement.AppendChild(numeroGroupe);
            }
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
            type.AppendChild(document.CreateTextNode(Enum.Format(typeof(ETypeEnseignements), e.Type, "G").ToUpper()));
            if (e.Groupe != 0)
            {
                numeroGroupe.AppendChild(document.CreateTextNode(e.Groupe.ToString()));
            }
            refEnseignant.Value = "idp" + enseignants.IndexOf(e.Enseignant).ToString();
            refMatiere.Value = "idm" + matieres.IndexOf(e.Matiere).ToString();
            refHoraire1.Value = "idh" + horaires.IndexOf(e.Horaire1).ToString();
            if (e.Horaire2 != null)
            {
                refHoraire2.Value = "idh" + horaires.IndexOf(e.Horaire2).ToString();
            }
            refSalle.Value = "ids" + salles.IndexOf(e.Salle).ToString();
        }

    }
}
