using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

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
            TextVersion.Text = "◈ " + "Version " + MainWindow.getNumeroVersion() + " ◈";
            ((Storyboard)FindResource("Storyboard1")).Begin();
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Storyboard)FindResource("Storyboard1")).Pause();
        }
        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Storyboard)FindResource("Storyboard1")).Resume();
        }

		// Liens hypertextes cliquables
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

		// Mise en italique des liens au survol
        private void TextBlockTitre_MouseEnter(object sender, MouseEventArgs e)
        {
            TextTitre.FontStyle = FontStyles.Italic;
        }
		private void TextBlockOBeaudoux_MouseEnter(object sender, MouseEventArgs e)
        {
            TextOBeaudoux.FontStyle = FontStyles.Italic;
        }
		private void TextBlockEseo_MouseEnter(object sender, MouseEventArgs e)
        {
            TextEseo.FontStyle = FontStyles.Italic;
        }
		private void TextBlockHDeFleurian_MouseEnter(object sender, MouseEventArgs e)
        {
            TextHDeFleurian.FontStyle = FontStyles.Italic;
        }
		private void TextBlockMDeniaud_MouseEnter(object sender, MouseEventArgs e)
        {
            TextMDeniaud.FontStyle = FontStyles.Italic;
        }
		private void TextBlockFDenouille_MouseEnter(object sender, MouseEventArgs e)
        {
            TextFDenouille.FontStyle = FontStyles.Italic;
        }
		private void TextBlockMRuaud_MouseEnter(object sender, MouseEventArgs e)
        {
            TextMRuaud.FontStyle = FontStyles.Italic;
        }
		private void TextBlockAThomas_MouseEnter(object sender, MouseEventArgs e)
        {
            TextAThomas.FontStyle = FontStyles.Italic;
        }

		// retour au style normal après le survol des liens
        private void TextBlockTitre_MouseLeave(object sender, MouseEventArgs e)
        {
            TextTitre.FontStyle = FontStyles.Normal;
        }
		private void TextBlockOBeaudoux_MouseLeave(object sender, MouseEventArgs e)
        {
            TextOBeaudoux.FontStyle = FontStyles.Normal;
        }
		private void TextBlockEseo_MouseLeave(object sender, MouseEventArgs e)
        {
            TextEseo.FontStyle = FontStyles.Normal;
        }
		private void TextBlockHDeFleurian_MouseLeave(object sender, MouseEventArgs e)
        {
            TextHDeFleurian.FontStyle = FontStyles.Normal;
        }
		private void TextBlockMDeniaud_MouseLeave(object sender, MouseEventArgs e)
        {
            TextMDeniaud.FontStyle = FontStyles.Normal;
        }
		private void TextBlockFDenouille_MouseLeave(object sender, MouseEventArgs e)
        {
            TextFDenouille.FontStyle = FontStyles.Normal;
        }
		private void TextBlockMRuaud_MouseLeave(object sender, MouseEventArgs e)
        {
            TextMRuaud.FontStyle = FontStyles.Normal;
        }
		private void TextBlockAThomas_MouseLeave(object sender, MouseEventArgs e)
        {
            TextAThomas.FontStyle = FontStyles.Normal;
        }
    }
}
