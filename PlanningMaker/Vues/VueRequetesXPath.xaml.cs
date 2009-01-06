using System.Windows;
using System.Windows.Controls;
using PlanningMaker.Modele;

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
            RequetesXPath requetesXPath = new RequetesXPath();
            requetesXPath.ExecRequetesXPath("RequetesXPath.xsl", "Semaine37.xml", 
                champ_numSemaine.Text,
                champ_nom_recherche_1.Text,
                champ_id_enseignant_2.Text,
                champ_id_matière_3.Text,
                champ_id_matière_4.Text,
                champ_id_enseignant_5.Text, 
                champ_id_salle_6.Text,
                champ_id_jour_6.Text,
                champ_id_enseignant_7.Text,
                champ_id_jour_7.Text);
        }

	}
}
