using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using PlanningMaker.Modele;
using PlanningMaker.Vues;

namespace PlanningMaker
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
    public partial class MainWindow : Window
	{
        private Planning planning;
        private string nomFichier;
        private string appName;

        private GridViewColumnHeader colonneCouranteTri = null;
        private SortAdorner adornerCourantTri = null;

		public MainWindow()
		{
			this.InitializeComponent();
            this.getAppName();
            this.SetTitle();
            DataContext = planning;

            vueMatiere.SuppressionEnseignantEvent += new RoutedEventHandler(VueMatiere_SuppressionEnseignantEvent);

			// Insert code required on object creation below this point.
            /*MenuItem_Annuler.IsEnabled = false;
            MenuItem_Rétablir.IsEnabled = false;
            MenuItem_Couper.IsEnabled = false;
            MenuItem_Copier.IsEnabled = false;
            MenuItem_Coller.IsEnabled = false;
            MenuItem_Supprimer.IsEnabled = false;*/
            MenuItem_Aide.IsEnabled = false;
        }

        private void VueMatiere_SuppressionEnseignantEvent(object sender, RoutedEventArgs e)
        {
            foreach (Semaine s in planning.Semaines)
            {
                foreach (Jour j in s.Jours)
                {
                    foreach (Enseignement enseignement in j.Enseignements)
                    {
                        Matiere matiere = enseignement.Matiere;
                        if (!matiere.Enseignants.Contains(enseignement.Enseignant))
                            enseignement.Enseignant = null;
                    }
                }
            }
        }

        public Planning Planning
        { 
            get
            {
                return Planning;
            }
        }

        private void getAppName()
        {
            object[] attrs1 = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            object[] attrs2 = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            object[] attrs3 = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            string applicationName = ((AssemblyCompanyAttribute)attrs1[0]).Company + " " +
                ((AssemblyTitleAttribute)attrs2[0]).Title + " " +
                ((AssemblyDescriptionAttribute)attrs3[0]).Description;

            appName = applicationName;
        }

        public static string getNumeroVersion()
        {
            Version vrs = Assembly.GetExecutingAssembly().GetName().Version;
            string stringVersion = String.Format("{0}.{1}.{2}", vrs.Major, vrs.Minor, vrs.Build);

            return stringVersion;
        }

        public void SetTitle()
        {
            string nomFichierCourt = (nomFichier == null) ? null : nomFichier.Substring(nomFichier.LastIndexOf(Path.DirectorySeparatorChar)+1);
            string fileName = (planningExiste() ? ((nomFichier == null) ? "Untitled" : nomFichierCourt) : "");
            string pChanged = (planningExiste() ? (planning.HasChanged ? "* - " : " - ") : "");
            
            Window.Title = fileName + pChanged + appName;
        }

        private void ProposerEnregistrement(object sender, RoutedEventArgs e, string action)
        {
            if (planningExiste() && planning.HasChanged)
            {
                MessageBoxResult result = MessageBox.Show("Voulez-vous enregistrer les modifications effectuées avant de " + action + " ?", "PlanningMaker",
                        MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    if (EnrPossible()) Save(sender, e);
                    else
                    {
                        if (EnrSousPossible()) SaveAs(sender, e);
                    }
                }
            }
        }

        private void New(object sender, RoutedEventArgs e)
        {
            Close(sender,e);

            planning = new Planning();
            planning.PropertyChanged += new PropertyChangedEventHandler(Planning_PropertyChanged);
            
            DataContext = planning;
            this.SetTitle();

            TabItem_Emploi_du_temps.IsSelected = true;
            TabPanel.IsEnabled = true;
            vueEnseignement.IsEnabled = false;

            vueEnseignement.SetPlanningContext(planning);
            vueRequetesXPath.SetPlanningContext(planning);

            ICollectionView vueSemaines = CollectionViewSource.GetDefaultView(planning.Semaines);
            vueSemaines.SortDescriptions.Add(new SortDescription("Numero", ListSortDirection.Ascending));
            vueSemaines.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Ascending));
            ComboBox_NumSemaine.ItemsSource = planning.Semaines;

            ICollectionView vueEnseignants = CollectionViewSource.GetDefaultView(planning.Enseignants);
            vueEnseignants.SortDescriptions.Add(new SortDescription("Nom", ListSortDirection.Ascending));
            vueEnseignants.SortDescriptions.Add(new SortDescription("Prenom", ListSortDirection.Ascending));
            listeEnseignants.ItemsSource = planning.Enseignants;

            ICollectionView vueSalles = CollectionViewSource.GetDefaultView(planning.Salles);
            vueSalles.SortDescriptions.Add(new SortDescription("Nom", ListSortDirection.Ascending));
            vueSalles.SortDescriptions.Add(new SortDescription("Type", ListSortDirection.Ascending));
            listeSalles.ItemsSource = planning.Salles;

            ICollectionView vueHoraires = CollectionViewSource.GetDefaultView(planning.Horaires);
            vueHoraires.SortDescriptions.Add(new SortDescription("Debut", ListSortDirection.Ascending));
            vueHoraires.SortDescriptions.Add(new SortDescription("Fin", ListSortDirection.Ascending));
            listeHoraires.ItemsSource = planning.Horaires;

            ICollectionView vueMatieres = CollectionViewSource.GetDefaultView(planning.Matieres);
            vueMatieres.SortDescriptions.Add(new SortDescription("Titre", ListSortDirection.Ascending));
            vueMatiere.SetEnseignantsContext(planning.Enseignants);
            listeMatieres.ItemsSource = planning.Matieres;
        }

        private void Planning_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetTitle();
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            string directory = Environment.CurrentDirectory;
            System.Windows.Forms.OpenFileDialog dialogueO = new System.Windows.Forms.OpenFileDialog();
            dialogueO.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogueO.Filter = "Fichier XML (*.xml)|*.xml";

            if (dialogueO.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                New(sender, e);
                nomFichier = dialogueO.FileName;
                try
                {
                    planning.Charger(nomFichier);
                    planning.HasChanged = false;
                    MessageBox.Show("Fichier chargé avec succès dans l'application !", "PlanningMaker",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    // Mise à jour du fichier source pour les requêtes XPath
                    RequetesXPath.NomFichierSemaine = nomFichier;

                    SetDefaultSemaine();

                    IEnumerator<Semaine> enumSemaine = planning.Semaines.GetEnumerator();
                    if (enumSemaine.MoveNext())
                    {
                        Semaine firstSemaine = enumSemaine.Current as Semaine;
                        ComboBox_NumSemaine.Text = firstSemaine.Numero.ToString();
                        listeEnseignements.ItemsSource = firstSemaine.Lundi.Enseignements;
                    }
                }
                catch (Exception)
                {
                    planning.HasChanged = false;
                    Close(sender, e);
                    MessageBox.Show("Fichier invalide !", "PlanningMaker",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            Environment.CurrentDirectory = directory;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            ProposerEnregistrement(sender, e, "fermer");

            planning = null;
            nomFichier = null;

            ComboBox_NumSemaine.ItemsSource = null;
            listeEnseignements.ItemsSource = null;
            ComboBox_Annee.Text = null;
            TextBox_Division.Text = null;
            TextBox_Promotion.Text = null;

            listeSalles.ItemsSource = null;
            listeMatieres.ItemsSource = null;
            listeHoraires.ItemsSource = null;
            listeEnseignants.ItemsSource = null;
            listeEnseignements.ItemsSource = null;

            vueSalle.ClearView();
            vueHoraire.ClearView();
            vueMatiere.ClearView();
            vueEnseignant.ClearView();
            vueRequetesXPath.ClearView();

            ClearSemaine();
            TabItem_Emploi_du_temps.IsSelected = true;
            TabPanel.IsEnabled = false;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            planning.Sauver(nomFichier);
            planning.HasChanged = false;
            MessageBox.Show("Planning sauvegardé avec succès !", "PlanningMaker",
                    MessageBoxButton.OK, MessageBoxImage.Information);

            // Mise à jour du fichier source pour les requêtes XPath
            RequetesXPath.NomFichierSemaine = nomFichier;
        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {
            string directory = Environment.CurrentDirectory;
            System.Windows.Forms.SaveFileDialog dialogueS = new System.Windows.Forms.SaveFileDialog();
            dialogueS.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogueS.Filter = "Fichier XML (*.xml)|*.xml";
            dialogueS.DefaultExt = "*.xml";

            if (dialogueS.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                nomFichier = dialogueS.FileName;
                Save(sender, e);
            }

            // Mise à jour du fichier source pour les requêtes XPath
            RequetesXPath.NomFichierSemaine = nomFichier;
            Environment.CurrentDirectory = directory;
        }

        private void FermeturePossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = planningExiste();
        }

        private void EnregistrementPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = EnrPossible();
        }

        private void EnregistrementSousPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = EnrSousPossible();
        }

        private bool EnrPossible()
        {
            return planningExiste() && (nomFichier != null) && planning.HasChanged;
        }

        private bool EnrSousPossible()
        {
            return planningExiste();
        }

        private bool planningExiste()
        {
            return (planning != null);
        }

        private void Importer(object sender, RoutedEventArgs e)
        {
            string directory = Environment.CurrentDirectory;
            System.Windows.Forms.OpenFileDialog dialogueO = new System.Windows.Forms.OpenFileDialog();
            dialogueO.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogueO.Filter = "iCalendar (*.ics)|*.ics|Valeurs séparées par des virgules Outlook (*.csv)|*.csv";

            Environment.CurrentDirectory = directory;

            if (dialogueO.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                nomFichier = dialogueO.FileName;
                // TODO : traitements en fonction de l'import choisi
            }
        }

        private void Exporter(object sender, RoutedEventArgs e)
        {
            string directory = Environment.CurrentDirectory;
            System.Windows.Forms.SaveFileDialog dialogueS = new System.Windows.Forms.SaveFileDialog();
            dialogueS.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogueS.Filter = "iCalendar (*.ics)|*.ics|Valeurs séparées par des virgules Outlook (*.csv)|*.csv";

            if (dialogueS.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                nomFichier = dialogueS.FileName;
                // TODO : traitements en fonction de l'export choisi
            }
            Environment.CurrentDirectory = directory;
        }

        private void ExporterPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = planningExiste();
        }

        private void PrintPreview(object sender, RoutedEventArgs e)
        {
            // TODO : choisir quoi voir : image, texte, multi-semaines ?

            ProposerEnregistrement(sender, e, "lancer l'aperçu");

            Print print = new Print("ooo");
            System.Windows.Forms.PrintPreviewDialog printPreviewD = new System.Windows.Forms.PrintPreviewDialog();
            printPreviewD.Document = print.Document;
            printPreviewD.ShowDialog();
        }

        private void Print(object sender, RoutedEventArgs e)
        {
            // TODO : choisir quoi imprimer : image, texte, multi-semaines ?

            ProposerEnregistrement(sender, e, "lancer l'impression");

            Print print = new Print("ooo");
            System.Windows.Forms.PrintDialog printD = new System.Windows.Forms.PrintDialog();
            printD.Document = print.Document;
            
            if (printD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                print.Document.Print();
            }
        }

        private void ApercuPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = planningExiste() && (nomFichier != null);
        }

        private void ImprimerPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = planningExiste() && (nomFichier != null);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /*private void Undo(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        private void Cut(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            // TODO
        }*/

        private void Help(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        private void MenuItemValiderXML_Click(object sender, RoutedEventArgs e)
        {
            ProposerEnregistrement(sender, e, "lancer la validation");

            if (OperationSurFichierXMLPossible())
            {
                ValidationXmlXsd validation = new ValidationXmlXsd();
                MessageBox.Show(this, validation.ValiderFichierXml(nomFichier), "Validation XMLSchema");
            }
            else
            {
                MessageBox.Show(this, "Validation impossible : aucun planning enregistré.", "Validation XMLSchema");
            }
        }

        private void MenuItemTransfoXSLT_Click(object sender, RoutedEventArgs e)
        {
            string messageTransformation = null;
            string nomFichierSVG = null;

            ProposerEnregistrement(sender, e, "lancer la transformation");

            if (OperationSurFichierXMLPossible())
            {
                nomFichierSVG = SaveFileSVG();

                if (nomFichierSVG != null)
                {
                    string directory = Environment.CurrentDirectory;
                    TransformationXslt transformation = new TransformationXslt();
                    messageTransformation = transformation.TransformerXslt(ComboBox_NumSemaine.Text, "EdTversSVG-FF.xsl", nomFichier, nomFichierSVG);
                    Environment.CurrentDirectory = directory;
                }
                else
                {
                    messageTransformation = "Transformation annulée.";
                }
            }
            else
            {
                messageTransformation = "Transformation impossible : aucun planning enregistré.";
            }

            MessageBox.Show(this, messageTransformation, "Transformation XSLT vers SVG");
            if (messageTransformation.Equals("Transfomation OK."))
            {
                StartExternWebBrowser(nomFichierSVG);
            }
        }

        private void MenuItemRequetesXPath_Click(object sender, RoutedEventArgs e)
        {
            ProposerEnregistrement(sender, e, "lancer les requêtes XPath");

            if (OperationSurFichierXMLPossible())
            {
                RequetesXPath requetesXPath = new RequetesXPath();
                requetesXPath.ExecRequetesXPath("RequetesXPath.xsl", "Semaine37.xml");
            }
            else
            {
                MessageBox.Show(this, "Exécution des requêtes impossible : aucun planning enregistré.", "Requêtes XPath");
            }
        }

        private void MenuItemMAJ_Click(object sender, RoutedEventArgs e)
        {
            Vues.VueMiseAJour vueMAJ = new Vues.VueMiseAJour();
            vueMAJ.Owner = this;
            vueMAJ.ShowDialog();
        }

        private void MenuItemAPropos_Click(object sender, RoutedEventArgs e)
        {
            Vues.VueAPropos fAPropos = new Vues.VueAPropos();
            fAPropos.Owner = this;
            this.Opacity = 0.8;
            fAPropos.ShowDialog();
            this.Opacity = 1;
        }

        private void AjouterElement(object sender, RoutedEventArgs e)
        {
            if (TabItem_Emploi_du_temps.IsSelected)
            {
                Enseignement nouvelEnseignement = new Enseignement();
                
                Semaine semaineEnCours = null;
                int nrSemaine = -1;
                
                try
                {
                    nrSemaine = Int32.Parse(ComboBox_NumSemaine.Text);
                }
                catch (FormatException) { }

                if (nrSemaine != -1)
                {
                    foreach (Semaine s in planning.Semaines)
                    {
                        if (s.Numero == nrSemaine)
                        {
                            semaineEnCours = s;
                            break;
                        }
                    }
                }

                if (semaineEnCours != null)
                {
                    if (RadioButton_Lundi.IsChecked == true)
                    {
                        semaineEnCours.Lundi.Enseignements.Add(nouvelEnseignement);
                    }
                    else if (RadioButton_Mardi.IsChecked == true)
                    {
                        semaineEnCours.Mardi.Enseignements.Add(nouvelEnseignement);
                    }
                    else if (RadioButton_Mercredi.IsChecked == true)
                    {
                        semaineEnCours.Mercredi.Enseignements.Add(nouvelEnseignement);
                    }
                    else if (RadioButton_Jeudi.IsChecked == true)
                    {
                        semaineEnCours.Jeudi.Enseignements.Add(nouvelEnseignement);
                    }
                    else if (RadioButton_Vendredi.IsChecked == true)
                    {
                        semaineEnCours.Vendredi.Enseignements.Add(nouvelEnseignement);
                    }
                }

                listeEnseignements.SelectedItem = nouvelEnseignement;
            }
            else if (TabItem_Horaires.IsSelected)
            {
                Horaire nouvelHoraire = new Horaire();
                planning.Horaires.Add(nouvelHoraire);
                listeHoraires.SelectedItem = nouvelHoraire;
            }
            else if (TabItem_Enseignants.IsSelected)
            {
                Enseignant nouvelEnseignant = new Enseignant();
                planning.Enseignants.Add(nouvelEnseignant);
                listeEnseignants.SelectedItem = nouvelEnseignant;
            }
            else if (TabItem_Matieres.IsSelected)
            {
                Matiere nouvelleMatiere = new Matiere();
                foreach (Enseignant enseignant in vueMatiere.EnseignantsAssocies)
                    nouvelleMatiere.Enseignants.Add(enseignant);

                planning.Matieres.Add(nouvelleMatiere);
                listeMatieres.SelectedItem = nouvelleMatiere;
            }
            else if (TabItem_Salles.IsSelected)
            {
                Salle nouvelleSalle = new Salle();
                planning.Salles.Add(nouvelleSalle);
                listeSalles.SelectedItem = nouvelleSalle;
            }
        }

        private void SupprimerElement(object sender, RoutedEventArgs e)
        {
            if (TabItem_Emploi_du_temps.IsSelected)
            {
                Enseignement enseignement = listeEnseignements.SelectedItem as Enseignement;
                if (enseignement != null)
                {
                    Semaine semaineEnCours = null;
                    int nrSemaine = -1;

                    try
                    {
                        nrSemaine = Int32.Parse(ComboBox_NumSemaine.Text);
                    }
                    catch (FormatException) { }

                    if (nrSemaine != -1)
                    {
                        foreach (Semaine s in planning.Semaines)
                        {
                            if (s.Numero == nrSemaine)
                            {
                                semaineEnCours = s;
                                break;
                            }
                        }
                    }

                    if (semaineEnCours != null)
                    {
                        if (RadioButton_Lundi.IsChecked == true)
                        {
                            semaineEnCours.Lundi.Enseignements.Remove(enseignement);
                        }
                        else if (RadioButton_Mardi.IsChecked == true)
                        {
                            semaineEnCours.Mardi.Enseignements.Remove(enseignement);
                        }
                        else if (RadioButton_Mercredi.IsChecked == true)
                        {
                            semaineEnCours.Mercredi.Enseignements.Remove(enseignement);
                        }
                        else if (RadioButton_Jeudi.IsChecked == true)
                        {
                            semaineEnCours.Jeudi.Enseignements.Remove(enseignement);
                        }
                        else if (RadioButton_Vendredi.IsChecked == true)
                        {
                            semaineEnCours.Vendredi.Enseignements.Remove(enseignement);
                        }
                    }
                }
            }
            else if (TabItem_Horaires.IsSelected)
            {
                Horaire horaire = listeHoraires.SelectedItem as Horaire;
                if (horaire != null)
                {
                    planning.SupprimerHoraire(horaire);
                }
            }
            else if (TabItem_Enseignants.IsSelected)
            {
                Enseignant enseignant = listeEnseignants.SelectedItem as Enseignant;
                if (enseignant != null)
                {
                    planning.SupprimerEnseignant(enseignant);
                }
            }
            else if (TabItem_Matieres.IsSelected)
            {
                Matiere matiere = listeMatieres.SelectedItem as Matiere;
                if (matiere != null)
                {
                    planning.SupprimerMatiere(matiere);
                }
            }
            else if (TabItem_Salles.IsSelected)
            {
                Salle salle = listeSalles.SelectedItem as Salle;
                if (salle != null)
                {
                    planning.SupprimerSalle(salle);
                }
            }
        }

        private void AjouterElementPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (planning!=null) && (!TabItem_XPath.IsSelected);
        }

        private void SupprimerElementPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            ListView liste = null;
            if (TabPanel != null)
            {
                if (TabItem_Emploi_du_temps.IsSelected)
                {
                    liste = listeEnseignements;
                }
                else if (TabItem_Enseignants.IsSelected)
                {
                    liste = listeEnseignants;
                }
                else if (TabItem_Matieres.IsSelected)
                {
                    liste = listeMatieres;
                }
                else if (TabItem_Horaires.IsSelected)
                {
                    liste = listeHoraires;
                }
                else if (TabItem_Salles.IsSelected)
                {
                    liste = listeSalles;
                }
                e.CanExecute = (planning != null) && (!TabItem_XPath.IsSelected) && (liste.SelectedItem != null);
            }
            else
                e.CanExecute = false;
        }
        
        private void ChangementSelectionHoraire(object sender, SelectionChangedEventArgs e)
        {
            Horaire horaire = listeHoraires.SelectedItem as Horaire;
            if (horaire != null)
                vueHoraire.ChangeHoraire(horaire);
            else
                vueHoraire.ClearView();
        }

        private void ChangementSelectionSalle(object sender, SelectionChangedEventArgs e)
        {
            Salle salle = listeSalles.SelectedItem as Salle;
            if (salle != null)
                vueSalle.ChangeSalle(salle);
            else
                vueSalle.ClearView();
        }

        private void ChangementSelectionMatiere(object sender, SelectionChangedEventArgs e)
        {
            Matiere matiere = listeMatieres.SelectedItem as Matiere;
            if (matiere != null)
                vueMatiere.ChangeMatiere(matiere);
            else
                vueMatiere.ClearView();
        }
        
        private void ChangementSelectionEnseignant(object sender, SelectionChangedEventArgs e)
        {
            Enseignant enseignant = listeEnseignants.SelectedItem as Enseignant;
            if (enseignant != null)
                vueEnseignant.ChangeEnseignant(enseignant);
            else
                vueEnseignant.ClearView();
        }

        private void DeselectionHoraire(object sender, MouseButtonEventArgs e)
        {
            listeHoraires.SelectedIndex = -1;
        }

        private void DeselectionSalle(object sender, MouseButtonEventArgs e)
        {
            listeSalles.SelectedIndex = -1;
        }

        private void DeselectionMatiere(object sender, MouseButtonEventArgs e)
        {
            listeMatieres.SelectedIndex = -1;
        }

        private void DeselectionEnseignant(object sender, MouseButtonEventArgs e)
        {
            listeEnseignants.SelectedIndex = -1;
        }

        private void DeselectionEnseignement(object sender, MouseButtonEventArgs e)
        {
            listeEnseignements.SelectedIndex = -1;
        }

        private void ValidationHoraire(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (TabItem_Horaires.IsSelected)
                {
                    if (listeHoraires.SelectedIndex >= 0)
                    {
                        Horaire horaire = listeHoraires.SelectedItem as Horaire;
                        if (Horaire.IsHeureValide(vueHoraire.Debut.Text) &&
                            Horaire.IsHeureValide(vueHoraire.Fin.Text))
                        {
                            horaire.Debut = vueHoraire.Debut.Text;
                            horaire.Fin = vueHoraire.Fin.Text;
                        }
                    }
                    else
                    {
                        AjouterElement(null, null);
                    }
                }
            }
        }

        private void ValidationSalle(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (TabItem_Salles.IsSelected)
                {
                    if (listeSalles.SelectedIndex >= 0)
                    {
                        Salle salle = listeSalles.SelectedItem as Salle;
                        salle.Nom = vueSalle.Nom.Text;
                        if (vueSalle.Type.SelectedItem != null)
                        {
                            if ((vueSalle.Type.SelectedItem as string).Equals("Amphi"))
                            {
                                salle.Type = ETypeSalles.Amphi;
                            }
                            else
                            {
                                salle.Type = ETypeSalles.Labo;
                            }
                        }
                        else
                        {
                            salle.Type = ETypeSalles.Amphi;
                        }
                    }
                    else
                    {
                        AjouterElement(null, null);
                    }
                }
            }
        }

        private void ValidationMatiere(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (TabItem_Matieres.IsSelected)
                {
                    if (listeMatieres.SelectedIndex >= 0)
                    {
                        Matiere matiere = listeMatieres.SelectedItem as Matiere;
                        matiere.Titre = vueMatiere.Titre.Text;
                        foreach (Enseignant enseignant in vueMatiere.EnseignantsAssocies)
                            matiere.Enseignants.Add(enseignant);
                    }
                    else
                    {
                        AjouterElement(null, null);
                    }
                }
            }
        }

        private void ValidationEnseignant(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (TabItem_Enseignants.IsSelected)
                {
                    if (listeEnseignants.SelectedIndex >= 0)
                    {
                        Enseignant enseignant = listeEnseignants.SelectedItem as Enseignant;
                        enseignant.Nom = vueEnseignant.nom.Text;
                        enseignant.Prenom = vueEnseignant.prenom.Text;
                    }
                    else
                    {
                        AjouterElement(null, null);
                    }
                }
            }
        }

        public bool OperationSurFichierXMLPossible()
        {
            if (nomFichier != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string OpenFileXML()
        {
            string directory = Environment.CurrentDirectory;
            System.Windows.Forms.OpenFileDialog dialogueO = new System.Windows.Forms.OpenFileDialog();
            dialogueO.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogueO.Filter = "Fichier XML (*.xml)|*.xml";

            Environment.CurrentDirectory = directory;

            if (dialogueO.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return dialogueO.FileName;
            }
            else
            {
                return null;
            }
        }

        public static string SaveFileSVG()
        {
            string directory = Environment.CurrentDirectory;
            System.Windows.Forms.SaveFileDialog dialogueS = new System.Windows.Forms.SaveFileDialog();
            dialogueS.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogueS.Filter = "Fichier SVG (*.svg)|*.svg";

            if (dialogueS.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Environment.CurrentDirectory = directory;
                return dialogueS.FileName;
            }
            else
            {
                Environment.CurrentDirectory = directory;
                return null;
            }
        }

        public static void StartExternWebBrowser(string filename)
        {
            filename = filename.Replace(" ", "%20");
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            psi.FileName = "firefox.exe";
            psi.Arguments = @"file://" + filename;
            try
            {
                System.Diagnostics.Process.Start(psi);
            }         
            catch (Win32Exception w32ex)
            {
                MessageBox.Show("Impossible d'ouvrir le navigateur internet Mozilla Firefox (firefox.exe), vérifiez qu'il est bien installé.\nDétails de l'erreur : " + w32ex.Message, "Ouverture de Firefox");
            }
        }

        private void PreviousWeek(object sender, RoutedEventArgs e)
        {
            Int32 nrSemaine = Int32.Parse(ComboBox_NumSemaine.Text);
            nrSemaine = (nrSemaine-53)%52+52;
            bool semainePresente = false;
            foreach (Semaine s in planning.Semaines)
            {
                if (s.Numero == nrSemaine)
                {
                    semainePresente = true;
                    ComboBox_NumSemaine.Text = nrSemaine.ToString();
                    TextBox_DateSemaine.DataContext = s;
                    break;
                }
            }
            if (!semainePresente)
            {
                MessageBoxResult result = MessageBox.Show("Semaine non crée !\nVoulez-vous en créer une ?", "PlanningMaker",
                        MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result==MessageBoxResult.Yes)
                {
                    Semaine s = new Semaine(nrSemaine, "1990-12-01");
                    planning.Semaines.Add(s);
                    TextBox_DateSemaine.DataContext = s;
                    ComboBox_NumSemaine.Text = nrSemaine.ToString();
                }
            }

            ChangementChoixJour(sender, e);
            SetDefaultSemaine();
        }

        private void NewWeek(object sender, ExecutedRoutedEventArgs e)
        {
            VueNewWeek fNewWeek = new VueNewWeek();
            fNewWeek.Owner = this;
            fNewWeek.Planning = planning;
            bool? ok = fNewWeek.ShowDialog();
            if (ok == true)
            {
                MessageBox.Show("Semaine créée avec succès !", "PlanningMaker",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                ComboBox_NumSemaine.SelectedItem = fNewWeek.Semaine;
				ChangementChoixJour(sender, e);
                SetDefaultSemaine();
            }
            else
            {
                MessageBox.Show("Création annulée par l'utilisateur !", "PlanningMaker",
                        MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DelWeek(object sender, ExecutedRoutedEventArgs e)
        {
            if (ComboBox_NumSemaine.SelectedItem != null)
            {
                int position = ComboBox_NumSemaine.SelectedIndex;
                planning.Semaines.Remove(ComboBox_NumSemaine.SelectedItem as Semaine);
                if (position == 0) position = 1;
                ComboBox_NumSemaine.SelectedIndex = position - 1;
                TextBox_DateSemaine.DataContext = ComboBox_NumSemaine.SelectedItem as Semaine;

                ChangementChoixJour(sender, e);
                if (ComboBox_NumSemaine.SelectedItem as Semaine != null)
                    SetDefaultSemaine();
                else
                    ClearSemaine();
            }
        }

        private void NextWeek(object sender, RoutedEventArgs e)
        {
            Int32 nrSemaine = Int32.Parse(ComboBox_NumSemaine.Text);
            nrSemaine = nrSemaine%52 +1;
            bool semainePresente = false;
            foreach (Semaine s in planning.Semaines)
            {
                if (s.Numero == nrSemaine)
                {
                    semainePresente = true;
                    ComboBox_NumSemaine.Text = nrSemaine.ToString();
                    TextBox_DateSemaine.DataContext = s;
                    break;
                }
            }
            if (!semainePresente)
            {
                MessageBoxResult result = MessageBox.Show("Semaine non créée !\nVoulez-vous en créer une ?", "PlanningMaker",
                        MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    Semaine s = new Semaine(nrSemaine, "1990-12-01");
                    planning.Semaines.Add(s);
                    TextBox_DateSemaine.DataContext = s;
                    ComboBox_NumSemaine.Text = nrSemaine.ToString();
                }
            }

            ChangementChoixJour(sender, e);
            SetDefaultSemaine();
        }

        private void ChangementChoixJour(object sender, RoutedEventArgs e)
        {
            Semaine semaine = ComboBox_NumSemaine.SelectedItem as Semaine;
            if (semaine != null)
            {
                if (RadioButton_Lundi.IsChecked == true)
                {
                    listeEnseignements.ItemsSource = semaine.Lundi.Enseignements;
                }
                else if (RadioButton_Mardi.IsChecked == true)
                {
                    listeEnseignements.ItemsSource = semaine.Mardi.Enseignements;
                }
                else if (RadioButton_Mercredi.IsChecked == true)
                {
                    listeEnseignements.ItemsSource = semaine.Mercredi.Enseignements;
                }
                else if (RadioButton_Jeudi.IsChecked == true)
                {
                    listeEnseignements.ItemsSource = semaine.Jeudi.Enseignements;
                }
                else if (RadioButton_Vendredi.IsChecked == true)
                {
                    listeEnseignements.ItemsSource = semaine.Vendredi.Enseignements;
                }
            }
            else listeEnseignements.ItemsSource = null;
        }

        private void NewWeekPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((planning != null) && (ComboBox_NumSemaine.Items.Count <= 52));
        }

        private void DelNextPreviousWeekPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((planning != null) && !(ComboBox_NumSemaine.Text.Equals("")));
        }

        private void ChangementSelectionEnseignement(object sender, SelectionChangedEventArgs e)
        {
            Enseignement enseignement = listeEnseignements.SelectedItem as Enseignement;
            if (enseignement != null)
                vueEnseignement.ChangeEnseignement(enseignement);
            else
                vueEnseignement.ClearView();
        }

        private void ChangementNumSemaine(object sender, SelectionChangedEventArgs e)
        {
            TextBox_DateSemaine.DataContext = ComboBox_NumSemaine.SelectedItem as Semaine;
            if (ComboBox_NumSemaine.SelectedItem as Semaine != null)
            {
                ChangementChoixJour(sender, e);
                SetDefaultSemaine();
            }
        }

        private void SetDefaultSemaine()
        {
            Label_NumSemaine.IsEnabled = true;
            ComboBox_NumSemaine.IsEnabled = true;
            Label_DateSemaine.IsEnabled = true;
            TextBox_DateSemaine.IsEnabled = true;

            StackRadioJours.IsEnabled = true;
            RadioButton_Lundi.IsChecked = true;
            listeEnseignements.IsEnabled = true;
        }

        private void ClearSemaine()
        {
            Label_NumSemaine.IsEnabled = false;
            ComboBox_NumSemaine.IsEnabled = false;
            Label_DateSemaine.IsEnabled = false;
            TextBox_DateSemaine.IsEnabled = false;

            StackRadioJours.IsEnabled = false;

            RadioButton_Lundi.IsChecked = false;
            RadioButton_Mardi.IsChecked = false;
            RadioButton_Mercredi.IsChecked = false;
            RadioButton_Jeudi.IsChecked = false;
            RadioButton_Vendredi.IsChecked = false;

            listeEnseignements.IsEnabled = false;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            ProposerEnregistrement(null, null, "quitter");
        }

        public void SortClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = sender as GridViewColumnHeader;
            String field = column.Tag as String;

            ListView listeTri = null;
            if (TabItem_Emploi_du_temps.IsSelected)
            {
                listeTri = listeEnseignements;
            }
            else if (TabItem_Enseignants.IsSelected)
            {
                listeTri = listeEnseignants;
            }
            else if (TabItem_Matieres.IsSelected)
            {
                listeTri = listeMatieres;
            }
            else if (TabItem_Horaires.IsSelected)
            {
                listeTri = listeHoraires;
            }
            else if (TabItem_Salles.IsSelected)
            {
                listeTri = listeSalles;
            }

            if (colonneCouranteTri != null)
            {
                AdornerLayer.GetAdornerLayer(colonneCouranteTri).Remove(adornerCourantTri);
                listeTri.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (colonneCouranteTri == column && adornerCourantTri.Direction == newDir)
                newDir = ListSortDirection.Descending;

            colonneCouranteTri = column;
            adornerCourantTri = new SortAdorner(colonneCouranteTri, newDir);
            AdornerLayer.GetAdornerLayer(colonneCouranteTri).Add(adornerCourantTri);
            listeTri.Items.SortDescriptions.Add(new SortDescription(field, newDir));
        }
    }    
}
