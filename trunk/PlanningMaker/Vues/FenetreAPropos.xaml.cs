using System.Windows;
using System.Windows.Input;

namespace PlanningMaker.Vues
{
    /// <summary>
    /// Logique d'interaction pour FenetreAPropos.xaml
    /// </summary>
    public partial class FenetreAPropos : Window
    {
        public FenetreAPropos()
        {
            InitializeComponent();
        }

        private void TextBlockTitre_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/emdt/");
        }

        private void TextBlockOBeaudoux_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:olivier.beaudoux@eseo.fr");
        }

        private void TextBlockEseo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.eseo.fr/");
        }

        private void TextBlockHDeFleurian_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:hubert.defleurian@reseau.eseo.fr");
        }

        private void TextBlockMDeniaud_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:mathieu.deniaud@reseau.eseo.fr");
        }

        private void TextBlockFDenouille_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:freddy.denouille@reseau.eseo.fr");
        }

        private void TextBlockMRuaud_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:mathieu.ruaud@reseau.eseo.fr");
        }

        private void TextBlockAThomas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:alexis.thomas@reseau.eseo.fr");
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("IN");
            // ... effet sur le texte à ajouter au survol : Alexis.
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("OUT");
            // ... effet sur le texte à ajouter au survol : Alexis.
        }
    }
}
