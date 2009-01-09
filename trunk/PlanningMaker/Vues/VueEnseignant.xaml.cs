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
    public partial class VueEnseignant : UserControl
    {
        private Enseignant enseignant;

        public Enseignant Enseignant
        {
            get
            {
                return enseignant;
            }
        }

        public VueEnseignant()
        {
            InitializeComponent();
        }

        public void ChangeEnseignant(Enseignant enseignant)
        {
            this.enseignant = enseignant;
            DataContext = enseignant;
            IsEnabled = true;
        }

        public void ClearView()
        {
            this.enseignant = null;
            DataContext = null;
            IsEnabled = false;
        }
    }
}
