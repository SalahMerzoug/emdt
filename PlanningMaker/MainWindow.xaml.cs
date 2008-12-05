using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PlanningMaker.Modele;
using Microsoft.Win32;

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
	}
}