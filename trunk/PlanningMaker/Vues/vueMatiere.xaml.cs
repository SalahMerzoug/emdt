using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using PlanningMaker.Modele;

namespace PlanningMaker.Vues
{
    /// <summary>
    /// Logique d'interaction pour VueMatiere.xaml
    /// </summary>
    public partial class VueMatiere : UserControl
    {
        public event RoutedEventHandler SuppressionEnseignantEvent;

        private Matiere matiere;
        private Collection<Enseignant> enseignantsAssocies;

        private GridViewColumnHeader colonneCouranteTri = null;
        private SortAdorner adornerCourantTri = null;

        public Matiere Matiere
        {
            get
            {
                return matiere;
            }
        }

        public Collection<Enseignant> EnseignantsAssocies
        {
            get
            {
                return enseignantsAssocies;
            }
        }

        public VueMatiere()
        {
            InitializeComponent();
            matiere = new Matiere();
            enseignantsAssocies = new ObservableCollection<Enseignant>();
            DataContext = matiere;
            ListeProfs.ItemsSource = matiere.Enseignants;
        }

        public void ChangeMatiere(Matiere matiere)
        {
            this.matiere = matiere;
            DataContext = matiere;
            ListeProfs.ItemsSource = matiere.Enseignants;
            IsEnabled = true;
        }

        public void SetEnseignantsContext(Collection<Enseignant> enseignants)
        {
            ComboEnseignants.ItemsSource = enseignants;
        }

        private void ChangementSelectionProf(object sender, SelectionChangedEventArgs e)
        {
            ComboEnseignants.SelectedItem = ListeProfs.SelectedItem as Enseignant;
        }

        private void AjouterProf(object sender, RoutedEventArgs e)
        {
            Enseignant enseignant = ComboEnseignants.SelectedItem as Enseignant;
            if (enseignant != null)
            {
                matiere.Enseignants.Insert(0, enseignant);
                ListeProfs.SelectedItem = enseignant;
            }
        }

        private void AjouterProfPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            if (matiere != null && ComboEnseignants.SelectedItem != null && !matiere.Enseignants.Contains(ComboEnseignants.SelectedItem as Enseignant))
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void SupprimerProf(object sender, RoutedEventArgs e)
        {
            matiere.Enseignants.Remove(ListeProfs.SelectedItem as Enseignant);
            if(SuppressionEnseignantEvent != null)
                SuppressionEnseignantEvent(this, new RoutedEventArgs());
        }

        private void SupprimerProfPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ListeProfs != null && ComboEnseignants.SelectedItem != null && ComboEnseignants.SelectedItem == ListeProfs.SelectedItem)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        public void ClearView()
        {
            matiere = null;
            DataContext = null;
            IsEnabled = false;
            Titre.Text = null;
            ComboEnseignants.SelectedItem = null;
            ListeProfs.ItemsSource = null;
        }

        private void SortClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = sender as GridViewColumnHeader;
            String field = column.Tag as String;

            if (colonneCouranteTri != null)
            {
                AdornerLayer.GetAdornerLayer(colonneCouranteTri).Remove(adornerCourantTri);
                this.ListeProfs.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (colonneCouranteTri == column && adornerCourantTri.Direction == newDir)
                newDir = ListSortDirection.Descending;

            colonneCouranteTri = column;
            adornerCourantTri = new SortAdorner(colonneCouranteTri, newDir);
            AdornerLayer.GetAdornerLayer(colonneCouranteTri).Add(adornerCourantTri);
            this.ListeProfs.Items.SortDescriptions.Add(new SortDescription(field, newDir));
        }
    }
}
