using System;
using System.Windows;
using PlanningMaker.Modele;

namespace PlanningMaker
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        private String nomFichier;
        private static string numeroVersion = "0.1.18";

		public MainWindow()
		{
			this.InitializeComponent();
			
			// Insert code required on object creation below this point.
        }

        public static string getNumeroVersion()
        {
            return numeroVersion;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialogue = new System.Windows.Forms.OpenFileDialog();
            dialogue.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialogue.Filter = "XML File (*.xml)|*.xml";

            if (dialogue.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                nomFichier = dialogue.FileName;
                string nomFichierSansPath = dialogue.SafeFileName;
                MessageBox.Show(nomFichierSansPath);
            }
        }

        private void MenuItemValiderXML_Click(object sender, RoutedEventArgs e)
        {
            ValidationXmlXsd validation = new ValidationXmlXsd();
            MessageBox.Show(this, validation.ValiderFichierXml("Semaine37.xml"), "Validation XMLSchema : Semaine37.xml");
        }

        private void MenuItemTransfoXSLT_Click(object sender, RoutedEventArgs e)
        {
            TransformationXslt transformation = new TransformationXslt();
            transformation.TransformerXslt("EdTversSVG.xsl", "Semaine37.xml");
        }

        private void MenuItemMAJ_Click(object sender, RoutedEventArgs e)
        {
            MiseAJour maj = new MiseAJour();
            maj.VerifierMAJ();
        }

        private void MenuItemAPropos_Click(object sender, RoutedEventArgs e)
        {
            Vues.FenetreAPropos fAPropos = new Vues.FenetreAPropos();
            fAPropos.Owner = this;
            this.Opacity = 0.8;
            fAPropos.ShowDialog();
            this.Opacity = 1;
        }
	}
}