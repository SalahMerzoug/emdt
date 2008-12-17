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
using System.Collections;
using System.Collections.ObjectModel;

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
            salle = new Salle();
            DataContext = s;
        }

        private void ChangementSelectionTypeSalle(object sender, SelectionChangedEventArgs e)
        {
                        
        }

    }
}
