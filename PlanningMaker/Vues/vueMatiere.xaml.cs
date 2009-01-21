using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PlanningMaker.Modele;

namespace PlanningMaker.Vues
{
    /// <summary>
    /// Logique d'interaction pour VueMatiere.xaml
    /// </summary>
    public partial class VueMatiere : UserControl
    {
        private Matiere matiere;
        private ICollection<Enseignant> enseignantsAssocies;

        public Matiere Matiere
        {
            get
            {
                return matiere;
            }
        }

        public ICollection<Enseignant> EnseignantsAssocies
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

        public void setEnseignantsContext(ICollection<Enseignant> enseignants)
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
                matiere.Enseignants.Add(enseignant);
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

    }
}
