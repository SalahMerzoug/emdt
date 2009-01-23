using System.Windows;
using System.Windows.Controls;
using PlanningMaker.Modele;
using System.Windows.Data;
using System;

namespace PlanningMaker.Vues
{
	public partial class VueRequetesXPath : UserControl
	{
        public VueRequetesXPath()
		{
          InitializeComponent();
		}

        private void ButtonXPathClicked(object sender, RoutedEventArgs e)
        {
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
                champ_numSemaine.Text,
                champ_nom_recherche_1.Text,
                idEnseignant_Req2,
                idMatiere_Req3,
                idMatiere_Req4,
                idEnseignant_Req5,
                idSalle_Req6,
                champ_id_jour_6.Text,
                idEnseignant_Req7,
                champ_id_jour_7.Text);
        }

        public void SetPlanningContext(Planning planning)
        {
            ObjectDataProvider odp_matiere = this.FindResource("ComboSource_Matieres") as ObjectDataProvider;

            if (odp_matiere != null)
                odp_matiere.ObjectInstance = planning.Matieres;
            else
                odp_matiere.ObjectInstance = null;

            ObjectDataProvider odp_horaires = this.FindResource("ComboSource_Enseignants") as ObjectDataProvider;

            if (odp_horaires != null)
                odp_horaires.ObjectInstance = planning.Enseignants;
            else
                odp_horaires.ObjectInstance = null;

            ObjectDataProvider odp_salles = this.FindResource("ComboSource_Salles") as ObjectDataProvider;

            if (odp_horaires != null)
                odp_salles.ObjectInstance = planning.Salles;
            else
                odp_salles.ObjectInstance = null;
        }

        public void ClearView()
        {
            champ_numSemaine.Text = null;
            champ_nom_recherche_1.Text = null;
            comboBox_Req2.SelectedItem = null;
            comboBox_Req3.SelectedItem = null;
            comboBox_Req4.SelectedItem = null;
            comboBox_Req5.SelectedItem = null;
            comboBox_Req6.SelectedItem = null;
            champ_id_jour_6.Text = null;
            comboBox_Req7.SelectedItem = null;
            champ_id_jour_7.Text = null;
        }

	}
}
