using System.Windows;

namespace PlanningMaker.Vues
{
    /// <summary>
    /// Logique d'interaction pour VueMiseAJour.xaml
    /// </summary>
    public partial class VueMiseAJour : Window
    {
        public VueMiseAJour()
        {
            InitializeComponent();

            CheckBoxProxy.IsChecked = false;
            TextBoxLogin.IsEnabled = false;
            TextBoxPass.IsEnabled = false;

            ProgBar.IsEnabled = false;
            ProgBar.Visibility = Visibility.Hidden;

            TextBoxContenuResultat.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida rutrum nisl. Vestibulum eu eros. Nunc vehicula sapien in nisi. Nunc nec arcu.";
        }

        private void CheckBoxProxy_Click(object sender, RoutedEventArgs e)
        {
            ChangerEtatProxyTextBoxes();
        }

        private void ChangerEtatProxyTextBoxes()
        {
            if (CheckBoxProxy.IsChecked == true)
            {
                TextBoxLogin.IsEnabled = true;
                TextBoxPass.IsEnabled = true;
            }
            else
            {
                TextBoxLogin.IsEnabled = false;
                TextBoxPass.IsEnabled = false;
            }
        }

        private void BoutonConnexion_Click(object sender, RoutedEventArgs e)
        {
            Modele.MiseAJour maj = new Modele.MiseAJour();
            maj.ThreadMAJ();
        }

        private void BoutonFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ExpanderResultat_Expanded(object sender, RoutedEventArgs e)
        {
            ExpanderResultat.Height += 100;
            WindowVueMiseAJour.Height += 100;
        }

        private void ExpanderResultat_Collapsed(object sender, RoutedEventArgs e)
        {
            ExpanderResultat.Height -= 100;
            WindowVueMiseAJour.Height -= 100;
        }
    }
}
