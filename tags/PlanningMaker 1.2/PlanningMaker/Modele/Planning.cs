using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Text;
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
                ObjectChanged("HasChanged");
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

        public Collection<Enseignant> Enseignants
        {
            get
            {
                return enseignants;
            }
        }

        public Collection<Matiere> Matieres
        {
            get
            {
                return matieres;
            }
        }

        public Collection<Horaire> Horaires
        {
            get
            {
                return horaires;
            }
        }

        public Collection<Salle> Salles
        {
            get
            {
                return salles;
            }
        }

        public Collection<Semaine> Semaines
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
            ObjectChanged("CollectionChanged : " + sender.ToString());
        }

        private void OnItemPropertyChanged(object sender, ItemPropertyChangedEventArgs args)
        {
            this.HasChanged = true;
            ObjectChanged("ItemPropertyChanged : " + sender.ToString());
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
                        else if (e.Horaire2 == horaire)
                        {
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

        public void ExporterICalendar(string nomFichier)
        {
            StreamWriter writer = new StreamWriter(nomFichier, false, Encoding.UTF8);
            // TODO : définir type MIME 'text/calendar'
            writer.NewLine = "\r\n";
            try
            {
                // ecriture de l'entête du iCalendar
                writer.WriteLine("BEGIN:VCALENDAR");
                writer.WriteLine("PRODID:-//Viactisoft//PlanningMaker 2008//FR");
                writer.WriteLine("VERSION:2.0");
                // description du planning dans une entrée VJOURNAL
                writer.WriteLine("BEGIN:VJOURNAL");
                writer.WriteLine("CATEGORY:Planning");
                writer.WriteLine("DESCRIPTION:" + this.promotion + "-" + this.annee + "-" + this.division);
                writer.WriteLine("END:VJOURNAL");
                // ecriture des enseignements
                foreach (Semaine semaine in semaines)
                {
                    string dateSemaine = semaine.Date;
                    // signalement d'un début de semaine par une entrée VJOURNAL
                    writer.WriteLine("BEGIN:VJOURNAL");
                    writer.WriteLine("CATEGORY:Semaine");
                    writer.WriteLine("DESCRIPTION:" + semaine.Numero.ToString());
                    writer.WriteLine("DTSTAMP:" + AjoutJour(semaine.Date, 0) + "T000000Z");
                    writer.WriteLine("END:VJOURNAL");
                    // signalement du lundi par une entrée VJOURNAL
                    writer.WriteLine("BEGIN:VJOURNAL");
                    writer.WriteLine("CATEGORY:Jour");
                    writer.WriteLine("DESCRIPTION:Lundi");
                    writer.WriteLine("END:VJOURNAL");
                    foreach (Enseignement enseignement in semaine.Lundi.Enseignements)
                    {
                        EnseignementToICalendar(writer, semaine, EJours.Lundi, enseignement);
                    }
                    // signalement du mardi par une entrée VJOURNAL
                    writer.WriteLine("BEGIN:VJOURNAL");
                    writer.WriteLine("CATEGORY:Jour");
                    writer.WriteLine("DESCRIPTION:Mardi");
                    writer.WriteLine("END:VJOURNAL");
                    foreach (Enseignement enseignement in semaine.Mardi.Enseignements)
                    {
                        EnseignementToICalendar(writer, semaine, EJours.Mardi, enseignement);
                    }
                    // signalement du mercredi par une entrée VJOURNAL
                    writer.WriteLine("BEGIN:VJOURNAL");
                    writer.WriteLine("CATEGORY:Jour");
                    writer.WriteLine("DESCRIPTION:Mercredi");
                    writer.WriteLine("END:VJOURNAL");
                    foreach (Enseignement enseignement in semaine.Mercredi.Enseignements)
                    {
                        EnseignementToICalendar(writer, semaine, EJours.Mercredi, enseignement);
                    }
                    // signalement du jeudi par une entrée VJOURNAL
                    writer.WriteLine("BEGIN:VJOURNAL");
                    writer.WriteLine("CATEGORY:Jour");
                    writer.WriteLine("DESCRIPTION:Jeudi");
                    writer.WriteLine("END:VJOURNAL");
                    foreach (Enseignement enseignement in semaine.Jeudi.Enseignements)
                    {
                        EnseignementToICalendar(writer, semaine, EJours.Jeudi, enseignement);
                    }
                    // signalement du vendredi par une entrée VJOURNAL
                    writer.WriteLine("BEGIN:VJOURNAL");
                    writer.WriteLine("CATEGORY:Jour");
                    writer.WriteLine("DESCRIPTION:Vendredi");
                    writer.WriteLine("END:VJOURNAL");
                    foreach (Enseignement enseignement in semaine.Vendredi.Enseignements)
                    {
                        EnseignementToICalendar(writer, semaine, EJours.Vendredi, enseignement);
                    }
                }
                // ecriture de la fin du iCalendar
                writer.WriteLine("END:VCALENDAR");
            }
            finally
            {
                writer.Close();
            }

        }

        private void EnseignementToICalendar(StreamWriter writer, Semaine semaine, EJours jour, Enseignement enseignement)
        {
            writer.WriteLine("BEGIN:VEVENT");
            writer.WriteLine("DTSTART:" + DateJour(semaine.Date, jour) + HeureEnseignementExp(enseignement.Horaire1, true));
            Horaire horaireFin = null;
            if (enseignement.Horaire2 == null)
                horaireFin = enseignement.Horaire1;
            else
                horaireFin = enseignement.Horaire2;
            writer.WriteLine("DTEND:" + DateJour(semaine.Date, jour) + HeureEnseignementExp(horaireFin, false));
            writer.WriteLine("SUMMARY:" + enseignement.Matiere.Titre);
            writer.WriteLine("LOCATION:" + enseignement.Salle.Type + " " + enseignement.Salle.Nom);
            writer.WriteLine("CATEGORIES:" + enseignement.Type + " " + enseignement.Groupe.ToString());
            writer.WriteLine("DESCRIPTION:" + enseignement.Enseignant.Prenom + " " + enseignement.Enseignant.Nom);
            writer.WriteLine("END:VEVENT");
        }

        private string DateJour(string dateDebutSemaine, EJours jourSemaine)
        {
            int ajoutJour = 0;
            if (jourSemaine.Equals(EJours.Lundi))
                ajoutJour = 0;
            else if (jourSemaine.Equals(EJours.Mardi))
                ajoutJour = 1;
            else if (jourSemaine.Equals(EJours.Mercredi))
                ajoutJour = 2;
            else if (jourSemaine.Equals(EJours.Jeudi))
                ajoutJour = 3;
            else if (jourSemaine.Equals(EJours.Vendredi))
                ajoutJour = 4;
            string dateJour = AjoutJour(dateDebutSemaine, ajoutJour);
            dateJour += "T";
            return dateJour;
        }

        private string HeureEnseignementExp(Horaire horaire, bool horaireDebut)
        {
            string[] strHoraire = horaireDebut ? horaire.Debut.Split('h') : horaire.Fin.Split('h');
            return strHoraire[0] + strHoraire[1] + "00Z";
        }

        private string AjoutJour(string dateDebutSemaine, int ajout)
        {
            string[] strDate = dateDebutSemaine.Split('-');
            int annee = Int32.Parse(strDate[0]);
            int mois = Int32.Parse(strDate[1]);
            int jour = Int32.Parse(strDate[2]);
            DateTime date = new DateTime(annee, mois, jour);
            date = date.AddDays(ajout);
            annee = date.Year;
            mois = date.Month;
            jour = date.Day;
            return annee.ToString() + (mois < 10 ? "0" + mois.ToString() : mois.ToString()) +
                    (jour < 10 ? "0" + jour.ToString() : jour.ToString());
        }

        public bool ImporterICalendar(string nomFichier)
        {
            StreamReader reader = new StreamReader(nomFichier, Encoding.UTF8);
            bool inJournal = false;
            bool inEvent = false;
            string currentCategorie = "";
            Semaine currentSemaine = null;
            Enseignement currentEnseignement = null;
            Jour currentJour = null;
            Horaire currentHoraire = null;
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("PRODID:"))
                {
                    string strProdId = line.Substring(line.IndexOf(':')+1);
                    if (!strProdId.Equals("-//Viactisoft//PlanningMaker 2008//FR"))
                    {
                        return false;
                    }
                }
                else if (line.Equals("BEGIN:VCALENDAR")) { }
                else if (line.Equals("END:VCALENDAR")) { }
                else if (line.Equals("BEGIN:VJOURNAL"))
                {
                    inJournal = true;
                }
                else if (line.Equals("END:VJOURNAL"))
                {
                    inJournal = false;
                }
                else if (line.Equals("BEGIN:VEVENT"))
                {
                    inEvent = true;
                    currentEnseignement = new Enseignement();
                }
                else if (line.Equals("END:VEVENT"))
                {
                    inEvent = false;
                    currentJour.Enseignements.Add(currentEnseignement);
                }
                else
                {
                    if (inJournal)
                    {
                        if (line.Equals("CATEGORY:Planning"))
                            currentCategorie = "Planning";
                        else if (line.Equals("CATEGORY:Semaine"))
                            currentCategorie = "Semaine";
                        else if (line.Equals("CATEGORY:Jour"))
                            currentCategorie = "Jour";
                        else if (line.StartsWith("DESCRIPTION:"))
                        {
                            if (currentCategorie == "Planning")
                            {
                                string strPlanning = line.Substring(line.IndexOf(':') + 1);
                                string[] infos = strPlanning.Split('-');
                                promotion = infos[0];
                                switch (infos[1])
                                {
                                    case "P1":
                                        annee = EAnnees.P1;
                                        break;
                                    case "P2":
                                        annee = EAnnees.P2;
                                        break;
                                    case "I1":
                                        annee = EAnnees.I1;
                                        break;
                                    case "I2":
                                        annee = EAnnees.I2;
                                        break;
                                    case "I3":
                                        annee = EAnnees.I3;
                                        break;
                                }
                                switch (infos[2])
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
                            }
                            else if (currentCategorie == "Semaine")
                            {
                                string strSemaine = line.Substring(line.IndexOf(':') + 1);
                                int numero = Int32.Parse(strSemaine);
                                currentSemaine = new Semaine();
                                currentSemaine.Numero = numero;
                                semaines.Add(currentSemaine);
                            }
                            else if (currentCategorie == "Jour")
                            {
                                string strJour = line.Substring(line.IndexOf(':') + 1);
                                currentJour = new Jour();
                                switch (strJour)
                                {
                                    case "Lundi":
                                        currentSemaine.Lundi = currentJour;
                                        currentSemaine.Jours.Add(currentSemaine.Lundi);
                                        break;
                                    case "Mardi":
                                        currentSemaine.Mardi = currentJour;
                                        currentSemaine.Jours.Add(currentSemaine.Mardi);
                                        break;
                                    case "Mercredi":
                                        currentSemaine.Mercredi = currentJour;
                                        currentSemaine.Jours.Add(currentSemaine.Mercredi);
                                        break;
                                    case "Jeudi":
                                        currentSemaine.Jeudi = currentJour;
                                        currentSemaine.Jours.Add(currentSemaine.Jeudi);
                                        break;
                                    case "Vendredi":
                                        currentSemaine.Vendredi = currentJour;
                                        currentSemaine.Jours.Add(currentSemaine.Vendredi);
                                        break;
                                }
                            }
                        }
                        else if (line.StartsWith("DTSTAMP:"))
                        {
                            string strSemaineDate = line.Substring(line.IndexOf(':') + 1);
                            string strAnnee = strSemaineDate.Substring(0, 4);
                            string strMois = strSemaineDate.Substring(4, 2);
                            string strJour = strSemaineDate.Substring(6, 2);
                            currentSemaine.Date = strAnnee + "-" + strMois + "-" + strJour;
                        }
                    }
                    else if (inEvent)
                    {
                        if (line.StartsWith("DTSTART:"))
                        {
                            string strHoraireDebut = line.Substring(line.IndexOf(':') + 1);
                            string horaireDebut = strHoraireDebut.Substring(strHoraireDebut.IndexOf('T') + 1);
                            string heure = horaireDebut.Substring(0, 2);
                            string minutes = horaireDebut.Substring(2, 2);
                            currentHoraire = new Horaire();
                            currentHoraire.Debut = heure + "h" + minutes;
                        }
                        else if (line.StartsWith("DTEND:"))
                        {
                            string strHoraireFin = line.Substring(line.IndexOf(':'));
                            string horaireFin = strHoraireFin.Substring(strHoraireFin.IndexOf('T') + 1, 4);
                            string heure = horaireFin.Substring(0, 2);
                            string minutes = horaireFin.Substring(2, 2);
                            currentHoraire.Fin = heure + "h" + minutes;
                            Horaire horaireExiste = null;
                            foreach (Horaire horaire in horaires)
                                if (horaire.Debut.Equals(currentHoraire.Debut) &&
                                        horaire.Fin.Equals(currentHoraire.Fin))
                                {
                                    horaireExiste = horaire;
                                    break;
                                }
                            if (horaireExiste == null)
                            {
                                horaireExiste = currentHoraire;
                                horaires.Add(horaireExiste);
                            }
                            currentEnseignement.Horaire1 = currentHoraire;
                        }
                        else if (line.StartsWith("SUMMARY:"))
                        {
                            string strMatiere = line.Substring(line.IndexOf(':') + 1);
                            Matiere matiereExiste = null;
                            foreach (Matiere matiere in matieres)
                                if (matiere.Titre.Equals(strMatiere))
                                {
                                    matiereExiste = matiere;
                                    break;
                                }
                            if (matiereExiste == null)
                            {
                                matiereExiste = new Matiere(strMatiere);
                                matieres.Add(matiereExiste);
                            }
                            currentEnseignement.Matiere = matiereExiste;
                        }
                        else if (line.StartsWith("LOCATION:"))
                        {
                            string strSalle = line.Substring(line.IndexOf(':') + 1);
                            string[] infosSalle = strSalle.Split(' ');
                            ETypeSalles type = ETypeSalles.Amphi;
                            switch (infosSalle[0])
                            {
                                case "Labo":
                                    type = ETypeSalles.Labo;
                                    break;
                                case "Amphi":
                                    type = ETypeSalles.Amphi;
                                    break;
                            }
                            Salle salleExiste = null;
                            foreach (Salle salle in salles)
                                if (salle.Nom.Equals(infosSalle[1]) &&
                                        salle.Type == type)
                                {
                                    salleExiste = salle;
                                    break;
                                }
                            if (salleExiste == null)
                            {
                                salleExiste = new Salle(infosSalle[1]);
                                salleExiste.Type = type;
                                salles.Add(salleExiste);
                            }
                            currentEnseignement.Salle = salleExiste;
                        }
                        else if (line.StartsWith("CATEGORIES:"))
                        {
                            string strEnseignement = line.Substring(line.IndexOf(':') + 1);
                            string[] infosEnseignement = strEnseignement.Split(' ');
                            switch (infosEnseignement[0])
                            {
                                case "COURS":
                                    currentEnseignement.Type = ETypeEnseignements.Cours;
                                    break;
                                case "TD":
                                    currentEnseignement.Type = ETypeEnseignements.TD;
                                    break;
                                case "TP":
                                    currentEnseignement.Type = ETypeEnseignements.TP;
                                    break;
                            }
                            currentEnseignement.Groupe = Int32.Parse(infosEnseignement[1]);
                        }
                        else if (line.StartsWith("DESCRIPTION:"))
                        {
                            string strEnseignant = line.Substring(line.IndexOf(':') + 1);
                            string[] infosEnseignant = strEnseignant.Split(' ');
                            Enseignant enseignantExiste = null;
                            foreach (Enseignant enseignant in enseignants)
                                if (enseignant.Nom.Equals(infosEnseignant[1]) &&
                                        enseignant.Prenom.Equals(infosEnseignant[0]))
                                {
                                    enseignantExiste = enseignant;
                                    break;
                                }
                            if (enseignantExiste == null)
                            {
                                enseignantExiste = new Enseignant(infosEnseignant[1], infosEnseignant[0]);
                                enseignants.Add(enseignantExiste);
                            }
                            currentEnseignement.Enseignant = enseignantExiste;
                            Enseignant enseignantMatiereExiste = null;
                            foreach (Enseignant enseignant in currentEnseignement.Matiere.Enseignants)
                                if (enseignant.Nom.Equals(infosEnseignant[1]) &&
                                        enseignant.Prenom.Equals(infosEnseignant[0]))
                                {
                                    enseignantMatiereExiste = enseignant;
                                    break;
                                }
                            if (enseignantMatiereExiste == null)
                            {
                                currentEnseignement.Matiere.Enseignants.Add(currentEnseignement.Enseignant);
                            }
                        }
                    }
                }
            }
            return true;
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
                enseignant.Id = id;
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
                matiere.Id = id;
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
                if (debutHoraire.Length == 4) debutHoraire = "0" + debutHoraire;
                if (finHoraire.Length == 4) finHoraire = "0" + finHoraire;
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
                salle.Id = id;
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
                if (h.Debut.StartsWith("0")) h.Debut = h.Debut.Substring(1);
                if (h.Fin.StartsWith("0")) h.Fin = h.Fin.Substring(1);
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
                if (j.Enseignements.Count > 0)
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
