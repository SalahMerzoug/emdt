using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PlanningMaker.Modele;
using System.Collections.ObjectModel;

namespace PlanningMaker.Vues
{
    /// <summary>
    /// Logique d'interaction pour vueMatiere.xaml
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

        private void ChangementSelectionUnEnseignant(object sender, SelectionChangedEventArgs e)
        {

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
            if (ListeProfs != null && ComboEnseignants.SelectedItem == ListeProfs.SelectedItem)
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
