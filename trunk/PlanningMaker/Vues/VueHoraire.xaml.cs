using System.Windows.Controls;
using PlanningMaker.Modele;

namespace PlanningMaker.Vues
{
	/// <summary>
	/// Interaction logic for VueHoraire.xaml
	/// </summary>
	public partial class VueHoraire : UserControl
	{
        private Horaire horaire;

        public Horaire Horaire
        {
            get
            {
                return horaire;
            }
        }

		public VueHoraire()
		{
            InitializeComponent();
		}

        public void ChangeHoraire(Horaire horaire)
        {
            this.horaire = horaire;
            DataContext = horaire;
            IsEnabled = true;
        }

        public void ClearView()
        {
            this.horaire = null;
            DataContext = null;
            IsEnabled = false;
        }
	}
}