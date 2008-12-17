using System;
using System.Windows;
using PlanningMaker.Modele;

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
                PasswordBoxPass.IsEnabled = true;
            }
            else
            {
                TextBoxLogin.IsEnabled = false;
                PasswordBoxPass.IsEnabled = false;
            }
        }

        private void BoutonConnexion_Click(object sender, RoutedEventArgs e)
        {
            TextBoxContenuResultat.IsEnabled = true;

            string login="", pass="";

            if (CheckBoxProxy.IsChecked == true)
            {
                login = TextBoxLogin.Text;
                pass = PasswordBoxPass.Password;
            }
            MiseAJour maj = new MiseAJour(login, pass);
            maj.DoWork(this);
        }

        private void BoutonFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBlokTelecharger_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TextBlokTelecharger.FontStyle = FontStyles.Italic;
        }

        private void TextBlokTelecharger_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TextBlokTelecharger.FontStyle = FontStyles.Normal;
        }

        private void TextBlokTelecharger_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/emdt/downloads/list");
        }
    }
}
