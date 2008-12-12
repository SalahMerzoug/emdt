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
using PlanningMaker.Vues;
using PlanningMaker.Modele;

namespace PlanningMaker.Vues
{
    /// <summary>
    /// Logique d'interaction pour VueSalle.xaml
    /// </summary>
    public partial class VueSalle : UserControl
    {
        private Salle salle;

        public VueSalle()
        {
            InitializeComponent();
            salle = new Amphi();
            DataContext = salle;
        }

        private void ChangementSelectionTypeSalle(object sender, SelectionChangedEventArgs e)
        {
            Salle nouvelleSalle;

            if (Type.Text == "Labo" && DataContext is Amphi)
            {
                MessageBox.Show("Labo => Amphi");
                nouvelleSalle = new Labo(salle.Nom);
                salle = nouvelleSalle;
            }
            else if (Type.Text == "Amphi" && DataContext is Labo)
            {
                MessageBox.Show("Amphi => Labo");
                nouvelleSalle = new Amphi(salle.Nom);
                salle = nouvelleSalle;
            }

            DataContext = salle;

        }

    }
}
