using System.Windows;
using System.Windows.Controls;
using PlanningMaker.Modele;
using System.Windows.Data;
using System;
using System.Collections.ObjectModel;

namespace PlanningMaker.Vues
{
	public partial class VueRequetesXPath : UserControl
	{
        public VueRequetesXPath()
		{
            InitializeComponent();

            ObservableCollection<String> listeJours = new ObservableCollection<String>();
            foreach(String s in Enum.GetNames(typeof(EJours)))
                listeJours.Add(s);

            ObjectDataProvider odp_jours = this.FindResource("ComboSource_Jours") as ObjectDataProvider;

            if (odp_jours != null)
                odp_jours.ObjectInstance = listeJours;
            else
                odp_jours.ObjectInstance = null;
		}

        private void ButtonXPathClicked(object sender, RoutedEventArgs e)
        {
            Semaine semaine = comboBox_Semaine.SelectedItem as Semaine;
            String numSemaine = (semaine != null) ? semaine.Numero.ToString() : "";

            Enseignant enseignant_Req2 = comboBox_Req2.SelectedItem as Enseignant;
            String idEnseignant_Req2 = (enseignant_Req2 != null) ? enseignant_Req2.Id : "";

            Enseignant enseignant_Req5 = comboBox_Req5.SelectedItem as Enseignant;
            String idEnseignant_Req5 = (enseignant_Req5 != null) ? enseignant_Req5.Id : "";

            Enseignant enseignant_Req7 = comboBox_Req7.SelectedItem as Enseignant;
            String idEnseignant_Req7 = (enseignant_Req7 != null) ? enseignant_Req7.Id : "";

            Matiere matiere_Req3 = comboBox_Req3.SelectedItem as Matiere;
            String idMatiere_Req3 = (matiere_Req3 != null) ? matiere_Req3.Id : "";

            Matiere matiere_Req4 = comboBox_Req4.SelectedItem as Matiere;
            String idMatiere_Req4 = (matiere_Req4 != null) ? matiere_Req4.Id : "";

            Salle salle_Req6 = comboBox_Req6.SelectedItem as Salle;
            String idSalle_Req6 = (salle_Req6 != null) ? salle_Req6.Id : "";

            RequetesXPath requetesXPath = new RequetesXPath();
            
            requetesXPath.ExecRequetesXPath("RequetesXPath.xsl", "Semaine37.xml",
                numSemaine,
                champ_nom_recherche_1.Text,
                idEnseignant_Req2,
                idMatiere_Req3,
                idMatiere_Req4,
                idEnseignant_Req5,
                idSalle_Req6,
                comboBox_JourReq6.Text,
                idEnseignant_Req7,
                comboBox_JourReq7.Text);

            this.ClearView();

        }

        public void SetPlanningContext(Planning planning)
        {
            ObjectDataProvider odp_semaines = this.FindResource("ComboSource_Semaines") as ObjectDataProvider;

            if (odp_semaines != null)
                odp_semaines.ObjectInstance = planning.Semaines;
            else
                odp_semaines.ObjectInstance = null;


            ObjectDataProvider odp_matieres = this.FindResource("ComboSource_Matieres") as ObjectDataProvider;

            if (odp_matieres != null)
                odp_matieres.ObjectInstance = planning.Matieres;
            else
                odp_matieres.ObjectInstance = null;

            ObjectDataProvider odp_enseignants = this.FindResource("ComboSource_Enseignants") as ObjectDataProvider;

            if (odp_enseignants != null)
                odp_enseignants.ObjectInstance = planning.Enseignants;
            else
                odp_enseignants.ObjectInstance = null;

            ObjectDataProvider odp_salles = this.FindResource("ComboSource_Salles") as ObjectDataProvider;

            if (odp_salles != null)
                odp_salles.ObjectInstance = planning.Salles;
            else
                odp_salles.ObjectInstance = null;
        }

        public void ClearView()
        {
            comboBox_Semaine.SelectedItem = null;
            champ_nom_recherche_1.Text = null;
            comboBox_Req2.SelectedItem = null;
            comboBox_Req3.SelectedItem = null;
            comboBox_Req4.SelectedItem = null;
            comboBox_Req5.SelectedItem = null;
            comboBox_Req6.SelectedItem = null;
            comboBox_JourReq6.SelectedItem = null;
            comboBox_Req7.SelectedItem = null;
            comboBox_JourReq7.SelectedItem = null;
        }

	}
}
