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

namespace PlanningMaker.Vues
{
    /// <summary>
    /// Logique d'interaction pour VueEnseignant.xaml
    /// </summary>
    public partial class VueEnseignement : UserControl
    {
        private Enseignement enseignement;

        public VueEnseignement()
        {
            InitializeComponent();
            enseignement = new Enseignement();
            DataContext = enseignement;
        }

        public void SetPlanningContext(Planning planning)
        {
            ObjectDataProvider odp_matiere = this.FindResource("ComboSource_Matieres") as ObjectDataProvider;

            if (odp_matiere != null)
                odp_matiere.ObjectInstance = planning.Matieres;

            ObjectDataProvider odp_horaires = this.FindResource("ComboSource_Horaires") as ObjectDataProvider;

            if (odp_horaires != null)
                odp_horaires.ObjectInstance = planning.Horaires;

            ObjectDataProvider odp_salles = this.FindResource("ComboSource_Salles") as ObjectDataProvider;

            if (odp_horaires != null)
                odp_salles.ObjectInstance = planning.Salles;

        }

        public void ChangeEnseignement(Enseignement enseignement)
        {
            this.enseignement = enseignement;
            DataContext = enseignement;

            ObjectDataProvider odp_enseignants = this.FindResource("ComboSource_Enseignants") as ObjectDataProvider;

            if (odp_enseignants != null)
                odp_enseignants.ObjectInstance = enseignement.Matiere.Enseignants;

            Matiere.SelectedItem = enseignement.Matiere;
            Type.SelectedItem = enseignement.Type;
            Enseignant.SelectedItem = enseignement.Enseignant;
            Salle.SelectedItem = enseignement.Salle;
                        
        }

        public void ClearView()
        {
            enseignement = null;
            DataContext = null;

            ObjectDataProvider odp_enseignants = this.FindResource("ComboSource_Enseignants") as ObjectDataProvider;

            if (odp_enseignants != null)
                odp_enseignants.ObjectInstance = null;

            Matiere.SelectedItem = null;
            Type.SelectedItem = null;
            Enseignant.SelectedItem = null;
            Salle.SelectedItem = null;
        }

        private void ChangementSelectionEnseignant(object sender, SelectionChangedEventArgs e)
        {
            Enseignant enseignant = Enseignant.SelectedItem as Enseignant;
            if (enseignant != null && enseignement!=null)
            {
                enseignement.Enseignant = enseignant;
            }
        }

        private void ChangementSelectionSalle(object sender, SelectionChangedEventArgs e)
        {
            Salle salle = Salle.SelectedItem as Salle;
            if (salle != null && enseignement!=null)
            {
                enseignement.Salle = salle;
            }
        }

        private void ChangementSelectionMatiere(object sender, SelectionChangedEventArgs e)
        {
            Matiere matiere = Matiere.SelectedItem as Matiere;
            if (matiere != null && enseignement != null)
            {
                enseignement.Matiere = matiere;
                ObjectDataProvider odp_enseignants = this.FindResource("ComboSource_Enseignants") as ObjectDataProvider;

                if (odp_enseignants != null)
                    odp_enseignants.ObjectInstance = enseignement.Matiere.Enseignants;
                Enseignant.SelectedItem = enseignement.Matiere.Enseignants.First();
            }
        }
    }
}
