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

        public Salle Salle
        {
            get
            {
                return salle;
            }
        }

        public VueSalle()
        {
            InitializeComponent();
        }

        public void ChangeSalle(Salle salle)
        {
            this.salle = salle;
            DataContext = salle;
            IsEnabled = true;
        }

        public void ClearView()
        {
            this.salle = null;
            DataContext = null;
            IsEnabled = false;
        }

    }
}
