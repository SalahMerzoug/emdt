using System;
using System.Windows;
using Microsoft.Win32;
using PlanningMaker.Modele;

namespace PlanningMaker
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        private String nomFichier;

		public MainWindow()
		{
			this.InitializeComponent();
			
			// Insert code required on object creation below this point.
		}

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialogue = new OpenFileDialog();
            if (dialogue.ShowDialog() == true)
            {
                nomFichier = dialogue.FileName;
                System.Console.WriteLine(nomFichier);
            }
        }

        private void MenuItemValiderXML_Click(object sender, RoutedEventArgs e)
        {
            ValidationXmlXsd validation = new ValidationXmlXsd();
            MessageBox.Show(this, validation.ValiderFichierXml("Semaine37.xml"), "Validation XMLSchema : Semaine37.xml");
        }

        private void MenuItemAPropos_Click(object sender, RoutedEventArgs e)
        {
            Vues.FenetreAPropos fAPropos = new Vues.FenetreAPropos();
            fAPropos.Owner = this;
            this.Opacity *= 0.8;
            fAPropos.ShowDialog();
            this.Opacity = 1;
            //this.IsActive = false;
            /*if (fAPropos.IsMouseOver)
                fAPropos.RenderTransform.BeginAnimation(
             * 
             * ... à venir : Alexis.
             * 
             */
        }
	}
}