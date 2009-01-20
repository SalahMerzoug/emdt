using System.Windows.Controls;
using PlanningMaker.Modele;

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
