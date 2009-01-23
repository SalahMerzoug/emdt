using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using PlanningMaker.Modele;

namespace PlanningMaker.Vues
{
    /// <summary>
    /// Logique d'interaction pour VueEnseignement.xaml
    /// </summary>
    public partial class VueEnseignement : UserControl
    {
        private Enseignement enseignement;
        private Boolean refreshingView;

        public Enseignement Enseignement
        {
            get
            {
                return enseignement;
            }
        }

        public VueEnseignement()
        {
            InitializeComponent();
            refreshingView = false;
        }

        public void SetPlanningContext(Planning planning)
        {
            ObjectDataProvider odp_matiere = this.FindResource("ComboSource_Matieres") as ObjectDataProvider;

            if (odp_matiere != null)
                odp_matiere.ObjectInstance = planning.Matieres;
            else
                odp_matiere.ObjectInstance = null;

            ObjectDataProvider odp_horaires = this.FindResource("ComboSource_Horaires") as ObjectDataProvider;

            if (odp_horaires != null)
                odp_horaires.ObjectInstance = planning.Horaires;
            else
                odp_horaires.ObjectInstance = null;

            ObjectDataProvider odp_salles = this.FindResource("ComboSource_Salles") as ObjectDataProvider;

            if (odp_horaires != null)
                odp_salles.ObjectInstance = planning.Salles;
            else
                odp_salles.ObjectInstance = null;
        }

        public void ChangeEnseignement(Enseignement enseignement)
        {
            refreshingView = true;

            this.enseignement = enseignement;
            DataContext = enseignement;
            IsEnabled = true;

            ObjectDataProvider odp_enseignants = this.FindResource("ComboSource_Enseignants") as ObjectDataProvider;

            if (odp_enseignants != null && enseignement.Matiere != null)
                odp_enseignants.ObjectInstance = enseignement.Matiere.Enseignants;
            else
                odp_enseignants.ObjectInstance = null;

            Matiere.SelectedItem = enseignement.Matiere;
            Type.SelectedItem = enseignement.Type;
            Enseignant.SelectedItem = enseignement.Enseignant;
            Salle.SelectedItem = enseignement.Salle;
            Horaire1.SelectedItem = enseignement.Horaire1;
            Horaire2.SelectedItem = enseignement.Horaire2;

            refreshingView = false;
        }

        public void ClearView()
        {
            refreshingView = false;

            enseignement = null;
            DataContext = null;
            IsEnabled = false;

            ObjectDataProvider odp_enseignants = this.FindResource("ComboSource_Enseignants") as ObjectDataProvider;

            if (odp_enseignants != null)
                odp_enseignants.ObjectInstance = null;

            Matiere.SelectedItem = null;
            Type.SelectedItem = null;
            Groupe.Text = null;
            Enseignant.SelectedItem = null;
            Salle.SelectedItem = null;
            Horaire1.SelectedItem = null;
            Horaire2.SelectedItem = null;

            refreshingView = false;
        }

        private void ChangementSelectionEnseignant(object sender, SelectionChangedEventArgs e)
        {
            if (!refreshingView)
            {
                Enseignant enseignant = Enseignant.SelectedItem as Enseignant;
                if (enseignant != null && enseignement != null)
                {
                    enseignement.Enseignant = enseignant;
                }
            }
        }

        private void ChangementSelectionSalle(object sender, SelectionChangedEventArgs e)
        {
            if (!refreshingView)
            {
                Salle salle = Salle.SelectedItem as Salle;
                if (salle != null && enseignement != null)
                {
                    enseignement.Salle = salle;
                }
            }
        }

        private void ChangementSelectionMatiere(object sender, SelectionChangedEventArgs e)
        {
            if (!refreshingView)
            {
                Matiere matiere = Matiere.SelectedItem as Matiere;
                if (matiere != null)
                {
                    if (enseignement != null)
                        enseignement.Matiere = matiere;

                    ObjectDataProvider odp_enseignants = this.FindResource("ComboSource_Enseignants") as ObjectDataProvider;

                    if (odp_enseignants != null)
                        odp_enseignants.ObjectInstance = matiere.Enseignants;

                    Enseignant.SelectedItem = matiere.Enseignants.First();
                }
            }
        }

        private void ChangementSelectionHoraire1(object sender, SelectionChangedEventArgs e)
        {
            if (!refreshingView)
            {
                Horaire horaire = Horaire1.SelectedItem as Horaire;
                if (horaire != null && enseignement != null)
                {

                    enseignement.Horaire1 = horaire;
                    if (enseignement.Horaire2 != null)
                    {
                        ObjectDataProvider odp_horaires = this.FindResource("ComboSource_Horaires") as ObjectDataProvider;

                        if (odp_horaires != null)
                        {
                            ICollection<Horaire> horaires = odp_horaires.ObjectInstance as ICollection<Horaire>;
                            if (horaires != null)
                            {
                                bool estDernierHoraire = true;
                                foreach (Horaire h in horaires)
                                {
                                    if (horaire.EstSuiviPar(h))
                                    {
                                        enseignement.Horaire2 = h;
                                        estDernierHoraire = false;
                                        break;
                                    }
                                }
                                if (estDernierHoraire)
                                {
                                    enseignement.Horaire2 = null;
                                }
                            }
                            else
                                enseignement.Horaire2 = null;
                        }
                        else
                            enseignement.Horaire2 = null;

                        Horaire2.SelectedItem = enseignement.Horaire2;
                    }
                }
            }
        }

        private void ChangementSelectionHoraire2(object sender, SelectionChangedEventArgs e)
        {
            if (!refreshingView)
            {
                Horaire horaire = Horaire2.SelectedItem as Horaire;
                if (horaire != null && enseignement != null)
                {
                    if (horaire != enseignement.Horaire1)
                    {
                        if (enseignement.Horaire1.EstSuiviPar(horaire))
                            enseignement.Horaire2 = horaire;
                        else
                        {
                            MessageBox.Show("Les horaires ne se suivent pas !", "PlanningMaker",
                                MessageBoxButton.OK, MessageBoxImage.Stop);
                            Horaire2.SelectedItem = enseignement.Horaire2;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Les horaires sont identiques !", "PlanningMaker",
                            MessageBoxButton.OK, MessageBoxImage.Stop);
                        Horaire2.SelectedItem = enseignement.Horaire2;
                    }
                }
            }
        }

        private void RemoveHoraire2(object sender, RoutedEventArgs e)
        {
            enseignement.Horaire2 = null;
            Horaire2.SelectedIndex = -1;
        }
    }
}
