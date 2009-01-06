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
    public partial class vueMatiere : UserControl
    {
        private Matiere matiere;
        private ICollection<Enseignant> enseignantsAssocies;

        public ICollection<Enseignant> EnseignantsAssocies
        {
            get
            {
                return enseignantsAssocies;
            }
        }

        public vueMatiere()
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

        }

        private void AjouterProf(object sender, RoutedEventArgs e)
        {
            matiere.Enseignants.Add(ComboEnseignants.SelectedItem as Enseignant);
        }

        private void AjouterProfPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (matiere != null);
        }

        private void SupprimerProf(object sender, RoutedEventArgs e)
        {
            matiere.Enseignants.Remove(ListeProfs.SelectedItem as Enseignant);
        }

        private void SupprimerProfPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (ListeProfs.SelectedItem != null);
        }

        public void ClearView()
        {
            matiere = null;
            DataContext = null;

            Titre.Text = null;
            ComboEnseignants.SelectedItem = null;
            ListeProfs.ItemsSource = null;
        }

    }
}
