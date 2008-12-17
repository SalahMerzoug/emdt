using System;
using System.Collections.Generic;
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
	public partial class VueRequetesXPath : UserControl
	{
        private String numSemaine,nom_recherche_1,id_enseignant_2,id_matière_3,id_matière_4,id_enseignant_5,id_salle_6,id_jour_6,id_enseignant_7,id_jour_7;

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
