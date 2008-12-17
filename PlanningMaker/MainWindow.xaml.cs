using System;
using System.ComponentModel;
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

            ICollectionView vueSemaines = CollectionViewSource.GetDefaultView(planning.Semaines);
            vueSemaines.SortDescriptions.Add(new SortDescription("Numero", ListSortDirection.Ascending));
            vueSemaines.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Ascending));

            ICollectionView vueEnseignants = CollectionViewSource.GetDefaultView(planning.Enseignants);
            vueEnseignants.SortDescriptions.Add(new SortDescription("Nom", ListSortDirection.Ascending));
            vueEnseignants.SortDescriptions.Add(new SortDescription("Prenom", ListSortDirection.Ascending));

            ICollectionView vueSalles = CollectionViewSource.GetDefaultView(planning.Salles);
            vueSalles.SortDescriptions.Add(new SortDescription("Nom", ListSortDirection.Ascending));
            vueSalles.SortDescriptions.Add(new SortDescription("Type", ListSortDirection.Ascending));
            listeSalles.ItemsSource = planning.Salles;

            ICollectionView vueHoraires = CollectionViewSource.GetDefaultView(planning.Horaires);
            vueHoraires.SortDescriptions.Add(new SortDescription("Debut", ListSortDirection.Ascending));
            vueHoraires.SortDescriptions.Add(new SortDescription("Fin", ListSortDirection.Ascending));
            listeHorraires.ItemsSource = planning.Horaires;

            ICollectionView vueMatieres = CollectionViewSource.GetDefaultView(planning.Matieres);
            vueMatieres.SortDescriptions.Add(new SortDescription("Titre", ListSortDirection.Ascending));

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
                planning.Charger(nomFichier);
                MessageBox.Show("Fichier chargé avec succès dans l'application !", "PlanningMaker",
                        MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            // TODO : détecter que le planning a été modifié depuis le dernier enregistrement
            // - dans ce cas : proposer de l'enregistrer
            // avant de le passer à null
            planning = null;
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
            dialogueO.Filter = "iCalendar (*.ics)|*.ics|Valeurs séparées par des virgules Outlook (*csv)|*csv";

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
            dialogueS.Filter = "iCalendar (*.ics)|*.ics|Valeurs séparées par des virgules Outlook (*csv)|*csv";

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

        private void MenuItemEdT_Click(object sender, RoutedEventArgs e)
        {
            this.TabPanel.SelectedItem = TabItem_Emploi_du_temps;
        }

        private void MenuItemEnseignant_Click(object sender, RoutedEventArgs e)
        {
            this.TabPanel.SelectedItem = TabItem_Enseignants;
        }

        private void MenuItemMatiere_Click(object sender, RoutedEventArgs e)
        {
            this.TabPanel.SelectedItem = TabItem_Matieres;
        }

        private void MenuItemHoraire_Click(object sender, RoutedEventArgs e)
        {
            this.TabPanel.SelectedItem = TabItem_Horaires;
        }

        private void MenuItemSalle_Click(object sender, RoutedEventArgs e)
        {
            this.TabPanel.SelectedItem = TabItem_Salles;
        }

        private void MenuItemValiderXML_Click(object sender, RoutedEventArgs e)
        {
            ValidationXmlXsd validation = new ValidationXmlXsd();
            MessageBox.Show(this, validation.ValiderFichierXml("Semaine37.xml"), "Validation XMLSchema : Semaine37.xml");
        }

        private void MenuItemTransfoXSLT_Click(object sender, RoutedEventArgs e)
        {
            TransformationXslt transformation = new TransformationXslt();
            MessageBox.Show(this, transformation.TransformerXslt("EdTversSVG-IE.xsl", "Semaine37.xml"), "Transformation XSLT : Semaine37.xml");
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
                String debut = vueHoraire.Debut.Text;
                String fin = vueHoraire.Fin.Text;
                planning.Horaires.Add(new Horaire((debut != null) ? debut : "00h00", (fin != null) ? fin : "00h00"));
            }
            else if (TabItem_Enseignants.IsSelected)
            {
                //planning.Enseignants.Add(new Enseignant());
                String nom = vueEnseignant.nom.Text;
                String prenom = vueEnseignant.prenom.Text;
                planning.Enseignants.Add(new Enseignant(nom,prenom));
            }
            else if (TabItem_Matieres.IsSelected)
            {
                //planning.Matieres.Add(new Matiere());
            }
            else if (TabItem_Salles.IsSelected)
            {
                string type = vueSalle.Type.Text;
                string nom = vueSalle.Nom.Text;
                Salle nouvelleSalle = new Salle(nom);
                switch(type){
                    case "Labo": nouvelleSalle.Type = ETypeSalles.Labo;
                        break;
                    default: nouvelleSalle.Type = ETypeSalles.Amphi;
                        break;
                }
                planning.Salles.Add(nouvelleSalle);
            }
        }

        private void SupprimerElement(object sender, RoutedEventArgs e)
        {
        }

        private void AjouterElementPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (planning!=null)&&(!TabItem_XPath.IsSelected)&&(vueEnseignant.nom.Text!="")&&(vueEnseignant.prenom.Text!="");
        }

        private void SupprimerElementPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
        }
        
        private void ChangementSelectionHoraire(object sender, SelectionChangedEventArgs e)
        {
            vueHoraire.DataContext = listeHorraires.SelectedItem;
        }

        private void ChangementSelectionSalle(object sender, SelectionChangedEventArgs e)
        {
            vueSalle.SetSalleContext((Salle)listeSalles.SelectedItem);
        }

        private void ChangementSelectionEnseignant(object sender, SelectionChangedEventArgs e)
        {
            vueEnseignant.DataContext = listeEnseignants.SelectedItem;
        }
	}
    
}