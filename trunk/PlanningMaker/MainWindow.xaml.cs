using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using PlanningMaker.Modele;

namespace PlanningMaker
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        private string nomFichier;
        private Planning planning;

		public MainWindow()
		{
			this.InitializeComponent();
            DataContext = planning;
			
			// Insert code required on object creation below this point.
            MenuItem_Annuler.IsEnabled = false;
            MenuItem_Rétablir.IsEnabled = false;
            MenuItem_Couper.IsEnabled = false;
            MenuItem_Copier.IsEnabled = false;
            MenuItem_Coller.IsEnabled = false;
            MenuItem_Supprimer.IsEnabled = false;
            MenuItem_Aide.IsEnabled = false;
        }

        public Planning Planning
        { 
            get
            {
                return Planning;
            }
        }


        public static string getNumeroVersion()
        {
            Version vrs = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string stringVersion = String.Format("{0}.{1}.{2}", vrs.Major, vrs.Minor, vrs.Build);

            return stringVersion;
        }

        private void New(object sender, RoutedEventArgs e)
        {
            planning = new Planning();
            DataContext = planning;

            TabItem_Emploi_du_temps.IsSelected = true;
            TabPanel.IsEnabled = true;
            vueEnseignement.SetPlanningContext(planning);

            ICollectionView vueSemaines = CollectionViewSource.GetDefaultView(planning.Semaines);
            vueSemaines.SortDescriptions.Add(new SortDescription("Numero", ListSortDirection.Ascending));
            vueSemaines.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Ascending));
            selectionSemaine.ItemsSource = planning.Semaines;

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
            vueMatiere.setEnseignantsContext(planning.Enseignants);
            listeMatieres.ItemsSource = planning.Matieres;

        }

        private void Open(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialogueO = new System.Windows.Forms.OpenFileDialog();
            dialogueO.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogueO.Filter = "Fichier XML (*.xml)|*.xml";

            if (dialogueO.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                nomFichier = dialogueO.FileName;
                New(sender, e);
                try
                {
                    planning.Charger(nomFichier);
                    MessageBox.Show("Fichier chargé avec succès dans l'application !", "PlanningMaker",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    RadioButton_Lundi.IsChecked = true;
                    
                    IEnumerator<Semaine> enumSemaine = planning.Semaines.GetEnumerator();
                    if (enumSemaine.MoveNext())
                    {
                        Semaine firstSemaine = enumSemaine.Current as Semaine;
                        selectionSemaine.Text = firstSemaine.Numero.ToString();
                        listeEnseignements.ItemsSource = firstSemaine.Lundi.Enseignements;
                        
                    }
                    
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Fichier non valide !", "PlanningMaker",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                }
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            // TODO : détecter que le planning a été modifié depuis le dernier enregistrement
            // - dans ce cas : proposer de l'enregistrer
            // avant de le passer à null
            planning = null;

            selectionSemaine.ItemsSource = null;
            listeEnseignements.ItemsSource = null;
            TextBox_Annee.Text = null;
            TextBox_Division.Text = null;
            TextBox_Promotion.Text = null;

            listeSalles.ItemsSource = null;
            listeMatieres.ItemsSource = null;
            listeHoraires.ItemsSource = null;
            listeEnseignants.ItemsSource = null;
            
            vueSalle.DataContext = new Salle();
            vueHoraire.DataContext = new Horaire();
            vueMatiere.DataContext = new Matiere();
            vueEnseignant.DataContext = new Enseignant();

            TabPanel.IsEnabled = false;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            planning.Sauver(nomFichier);
            MessageBox.Show("Planning sauvegardé avec succès !", "PlanningMaker",
                    MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dialogueS = new System.Windows.Forms.SaveFileDialog();
            dialogueS.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogueS.Filter = "Fichier XML (*.xml)|*.xml";
            dialogueS.DefaultExt = "*.xml";

            if (dialogueS.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                nomFichier = dialogueS.FileName;
                Save(sender, e);
            }
        }

        private void FermeturePossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = planningExiste();
        }

        private void EnregistrementPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = planningExiste() && (nomFichier != null);
        }

        private void EnregistrementSousPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = planningExiste();
        }

        private bool planningExiste()
        {
            return (planning != null);
        }

        private void Importer(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialogueO = new System.Windows.Forms.OpenFileDialog();
            dialogueO.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogueO.Filter = "iCalendar (*.ics)|*.ics|Valeurs séparées par des virgules Outlook (*.csv)|*.csv";

            if (dialogueO.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                nomFichier = dialogueO.FileName;
                // TODO : traitements en fonction de l'import choisi
            }
        }

        private void Exporter(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dialogueS = new System.Windows.Forms.SaveFileDialog();
            dialogueS.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogueS.Filter = "iCalendar (*.ics)|*.ics|Valeurs séparées par des virgules Outlook (*.csv)|*.csv";

            if (dialogueS.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                nomFichier = dialogueS.FileName;
                // TODO : traitements en fonction de l'export choisi
            }
        }

        private void ExporterPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = planningExiste();
        }

        private void PrintPreview(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.PrintPreviewDialog printPreviewD = new System.Windows.Forms.PrintPreviewDialog();
            // TODO : mettre notre 'document' dans le PrintPreviewDialog
            // printPreviewD.Document = ...;
            printPreviewD.Show();
        }

        private void Print(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.PageRangeSelection = PageRangeSelection.AllPages;
            printDialog.UserPageRangeEnabled = true;

            Nullable<bool> result = printDialog.ShowDialog();

            if (result == true)
            {
                // TODO : Print document
            }
        }

        private void ApercuPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = planningExiste();
        }

        private void ImprimerPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = planningExiste();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Undo(object sender, RoutedEventArgs e)
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
        }

        private void Help(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        private void MenuItemValiderXML_Click(object sender, RoutedEventArgs e)
        {
            if (OperationSurFichierXMLPossible())
            {
                ValidationXmlXsd validation = new ValidationXmlXsd();
                MessageBox.Show(this, validation.ValiderFichierXml(nomFichier), "Validation XMLSchema");
            }
            else
            {
                MessageBox.Show(this, "Validation impossible", "Validation XMLSchema");
            }
        }

        private void MenuItemTransfoXSLT_Click(object sender, RoutedEventArgs e)
        {
            string nomFichierSVG = SaveFileSVG();
            if (OperationSurFichierXMLPossible() && nomFichierSVG != null)
            {
                TransformationXslt transformation = new TransformationXslt();
                string messageValidation = transformation.TransformerXslt("37", "EdTversSVG-FF.xsl", nomFichier, nomFichierSVG);
                MessageBox.Show(this, messageValidation, "Transformation XSLT vers SVG");
                if (messageValidation.Equals("Transfomation OK."))
                {
                    StartExternWebBrowser(nomFichierSVG);
                }
            }
            else
            {
                MessageBox.Show(this, "Transformation impossible", "Transformation XSLT vers SVG");
            }
        }

        private void MenuItemRequetesXPath_Click(object sender, RoutedEventArgs e)
        {
            RequetesXPath requetesXPath = new RequetesXPath();
            requetesXPath.ExecRequetesXPath("RequetesXPath.xsl", "Semaine37.xml");
        }

        private void MenuItemMAJ_Click(object sender, RoutedEventArgs e)
        {
            Vues.VueMiseAJour vueMAJ = new Vues.VueMiseAJour();
            vueMAJ.Owner = this;
            vueMAJ.ShowDialog();
        }

        private void MenuItemAPropos_Click(object sender, RoutedEventArgs e)
        {
            Vues.FenetreAPropos fAPropos = new Vues.FenetreAPropos();
            fAPropos.Owner = this;
            this.Opacity = 0.8;
            fAPropos.ShowDialog();
            this.Opacity = 1;
        }

        private void AjouterElement(object sender, RoutedEventArgs e)
        {
            if (TabItem_Horaires.IsSelected)
            {
                Horaire nouvelHoraire = new Horaire("00h00", "00h00");
                if (listeHoraires.SelectedIndex == -1)
                {
                    nouvelHoraire.Debut = vueHoraire.Debut.Text;
                    nouvelHoraire.Fin = vueHoraire.Fin.Text;
                }
                planning.Horaires.Add(nouvelHoraire);
                listeHoraires.SelectedItem = nouvelHoraire;
            }
            else if (TabItem_Enseignants.IsSelected)
            {
                Enseignant nouvelEnseignant = new Enseignant("", "");
                if (listeEnseignants.SelectedIndex == -1)
                {
                    nouvelEnseignant.Nom = vueEnseignant.nom.Text;
                    nouvelEnseignant.Prenom = vueEnseignant.prenom.Text;
                }
                planning.Enseignants.Add(nouvelEnseignant);
                listeEnseignants.SelectedItem = nouvelEnseignant;
            }
            else if (TabItem_Matieres.IsSelected)
            {
                Matiere nouvelleMatiere = new Matiere("");
                foreach (Enseignant enseignant in vueMatiere.EnseignantsAssocies)
                    nouvelleMatiere.Enseignants.Add(enseignant);


                if (listeMatieres.SelectedIndex == -1)
                {
                    nouvelleMatiere.Titre = vueMatiere.Titre.Text;
                }
                planning.Matieres.Add(nouvelleMatiere);
                listeMatieres.SelectedItem = nouvelleMatiere;
            }
            else if (TabItem_Salles.IsSelected)
            {
                Salle nouvelleSalle = new Salle("");
                nouvelleSalle.Type = ETypeSalles.Amphi;
                if (listeSalles.SelectedIndex == -1)
                {
                    nouvelleSalle.Nom = vueSalle.Nom.Text;
                    if (vueSalle.Type.SelectedItem != null)
                    {
                        if ((vueSalle.Type.SelectedItem as string).Equals("Amphi"))
                        {
                            nouvelleSalle.Type = ETypeSalles.Amphi;
                        }
                        else
                        {
                            nouvelleSalle.Type = ETypeSalles.Labo;
                        }
                    }
                    else
                    {
                        nouvelleSalle.Type = ETypeSalles.Amphi;
                    }
                }
                planning.Salles.Add(nouvelleSalle);
                listeSalles.SelectedItem = nouvelleSalle;
            }
        }

        private void SupprimerElement(object sender, RoutedEventArgs e)
        {
            if (TabItem_Horaires.IsSelected)
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
            e.CanExecute = (planning != null) && (!TabItem_XPath.IsSelected);
        }
        
        private void ChangementSelectionHoraire(object sender, SelectionChangedEventArgs e)
        {
            vueHoraire.DataContext = listeHoraires.SelectedItem;
        }

        private void ChangementSelectionSalle(object sender, SelectionChangedEventArgs e)
        {
            vueSalle.DataContext = listeSalles.SelectedItem;
        }

        private void ChangementSelectionMatiere(object sender, SelectionChangedEventArgs e)
        {
            vueMatiere.ChangeMatiere(listeMatieres.SelectedItem as Matiere);
        }
        
        private void ChangementSelectionEnseignant(object sender, SelectionChangedEventArgs e)
        {
            vueEnseignant.DataContext = listeEnseignants.SelectedItem;
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
                        horaire.Debut = vueHoraire.Debut.Text;
                        horaire.Fin = vueHoraire.Fin.Text;
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

        private void ValidationEnseignement(object sender, KeyEventArgs   e)
        {
            if (e.Key == Key.Enter)
            {
                if (TabItem_Emploi_du_temps.IsSelected)
                {
                    if (listeEnseignants.SelectedIndex >= 0)
                    {
                        // TODO
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
            System.Windows.Forms.OpenFileDialog dialogueO = new System.Windows.Forms.OpenFileDialog();
            dialogueO.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogueO.Filter = "Fichier XML (*.xml)|*.xml";

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
            System.Windows.Forms.SaveFileDialog dialogueS = new System.Windows.Forms.SaveFileDialog();
            dialogueS.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogueS.Filter = "Fichier SVG (*.svg)|*.svg";

            if (dialogueS.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return dialogueS.FileName;
            }
            else
            {
                return null;
            }
        }

        public static void StartExternWebBrowser(string filename)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            FileInfo info = new FileInfo(filename);
            // TODO : gestion des espaces dans le chemin absolu du fichier
            string path = info.DirectoryName;
            string name = info.Name;
            try
            {
                proc.StartInfo.FileName = "firefox.exe";
                proc.StartInfo.Arguments = "file://" + path + "/" + name;
                proc.Start();
                proc.Close();
            }
            catch (Win32Exception w32ex)
            {
                MessageBox.Show("Impossible d'ouvrir le navigateur internet Mozilla Firefox (firefox.exe), vérifiez qu'il est bien installé.\nDétails de l'erreur : " + w32ex.Message, "Transformation XSLT");
            }
        }

        private void PreviousWeek(object sender, RoutedEventArgs e)
        {
            Int32 nrSemaine = Int32.Parse(selectionSemaine.Text);
            nrSemaine--;
            bool semainePresente = false;
            foreach (Semaine s in planning.Semaines)
            {
                if (s.Numero == nrSemaine)
                {
                    semainePresente = true;
                    selectionSemaine.Text = nrSemaine.ToString();
                    break;
                }
            }
            if (!semainePresente)
            {
                MessageBoxResult result = MessageBox.Show("Semaine non crée !\nVoulez-vous en créer une ?", "PlanningMaker",
                        MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result==MessageBoxResult.Yes)
                {
                    planning.Semaines.Add(new Semaine(nrSemaine, ""));
                    selectionSemaine.Text = nrSemaine.ToString();
                    RadioButton_Lundi.IsChecked = true;
                }
            }
        }

        private void NextWeek(object sender, RoutedEventArgs e)
        {
            Int32 nrSemaine = Int32.Parse(selectionSemaine.Text);
            nrSemaine++;
            bool semainePresente = false;
            foreach (Semaine s in planning.Semaines)
            {
                if (s.Numero == nrSemaine)
                {
                    semainePresente = true;
                    selectionSemaine.Text = nrSemaine.ToString();
                    break;
                }
            }
            if (!semainePresente)
            {
                MessageBoxResult result = MessageBox.Show("Semaine non créée !\nVoulez-vous en créer une ?", "PlanningMaker",
                        MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    planning.Semaines.Add(new Semaine(nrSemaine, ""));
                    selectionSemaine.Text = nrSemaine.ToString();
                    RadioButton_Lundi.IsChecked = true;
                }
            }
        }

        private void ChangementChoixJour(object sender, RoutedEventArgs e)
        {
            Semaine semaine = selectionSemaine.SelectedItem as Semaine;
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
        }

        private void NextWeekPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (planning!=null && !(selectionSemaine.Text.Equals("")));
        }

        private void PreviousWeekPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ((planning != null) && !(selectionSemaine.Text.Equals("")) && Int32.Parse(selectionSemaine.Text) != 0);
        }

        private void ChangementSelectionEnseignement(object sender, SelectionChangedEventArgs e)
        {
            Enseignement enseignement = listeEnseignements.SelectedItem as Enseignement;
            if (enseignement != null)
                vueEnseignement.ChangeEnseignement(enseignement);
            else
                vueEnseignement.ClearView();
        }
	}    
}
