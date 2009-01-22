using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using PlanningMaker.Modele;

namespace PlanningMaker.Vues
{
    /// <summary>
    /// Logique d'interaction pour VueMiseAJour.xaml
    /// </summary>
    public partial class VueMiseAJour : Window
    {

        private BackgroundWorker bw, bw2;
        private MiseAJour maj;
        string resMaj = "aucun résultat";

        public VueMiseAJour()
        {
            InitializeComponent();

            bw = new BackgroundWorker();
            bw2 = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw2.DoWork += new DoWorkEventHandler(bw2_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
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
            TextBlockTelecharger.Visibility = Visibility.Hidden;
            TextBlockTelecharger.IsEnabled = false;

            string login="", pass="";
            if (CheckBoxProxy.IsChecked == true)
            {
                login = TextBoxLogin.Text;
                pass = PasswordBoxPass.Password;
            }
            maj = new MiseAJour(login, pass);

            bw.RunWorkerAsync();
        }

        private void BoutonFermer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBlockTelecharger_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlockTelecharger.FontStyle = FontStyles.Italic;
        }

        private void TextBlockTelecharger_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlockTelecharger.FontStyle = FontStyles.Normal;
        }

        private void TextBlockTelecharger_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/emdt/downloads/list");
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            bw2.RunWorkerAsync();
            while (bw2.IsBusy)
            {
                bw.ReportProgress(0, "10");
                Thread.Sleep(100);
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int valeurInc = Int32.Parse(e.UserState.ToString());
            TextBoxContenuResultat.Text = "\n\t... en cours ...";
            ProgBar.Value = (ProgBar.Value + valeurInc) % 100;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TextBoxContenuResultat.Text = resMaj;
            ProgBar.Value = 100;

            if (resMaj.Contains("nécessaire"))
            {
                TextBlockTelecharger.Visibility = Visibility.Visible;
                TextBlockTelecharger.IsEnabled = true;
            }
            else
            {
                TextBlockTelecharger.Visibility = Visibility.Hidden;
                TextBlockTelecharger.IsEnabled = false;
            }
        }

        private void bw2_DoWork(object sender, DoWorkEventArgs e)
        {
            resMaj = maj.VerifierMAJ();
        }
    }
}
