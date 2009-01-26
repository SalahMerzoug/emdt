using System.Windows.Controls;
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
