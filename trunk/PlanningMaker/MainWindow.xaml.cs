﻿using System;
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

            MenuItem_Fermer.IsEnabled = false;
            MenuItem_Enr.IsEnabled = false;
            MenuItem_EnrSous.IsEnabled = false;
            MenuItem_Apercu.IsEnabled = false;
            MenuItem_Imprimer.IsEnabled = false;
            MenuItem_Annuler.IsEnabled = false;
            MenuItem_Rétablir.IsEnabled = false;
            MenuItem_Couper.IsEnabled = false;
            MenuItem_Copier.IsEnabled = false;
            MenuItem_Coller.IsEnabled = false;
            MenuItem_Supprimer.IsEnabled = false;
        }

        public static string getNumeroVersion()
        {
            // TO CHANGE
            return numeroVersion;
        }

        private void New(object sender, RoutedEventArgs e)
        {
            // TODO
        }
        private void Open(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialogueO = new System.Windows.Forms.OpenFileDialog();
            dialogueO.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogueO.Filter = "Fichier XML (*.xml)|*.xml";

            if (dialogueO.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                nomFichier = dialogueO.FileName;
                string nomFichierSansPath = dialogueO.SafeFileName;
                MessageBox.Show(nomFichierSansPath);
            }
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            // TODO
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            // TODO
        }
        private void SaveAs(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dialogueS = new System.Windows.Forms.SaveFileDialog();
            dialogueS.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogueS.Filter = "Fichier XML (*.xml)|*.xml";
            dialogueS.DefaultExt = "*.xml";

            if (dialogueS.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // TODO
            }
        }
        private void PrintPreview(object sender, RoutedEventArgs e)
        {
            // TODO
        }
        private void Print(object sender, RoutedEventArgs e)
        {
            // TODO
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Undo(object sender, RoutedEventArgs e)
        {
            // TODO
        }
        private void Redo(object sender, RoutedEventArgs e)
        {
            // TODO
        }
        private void Cut(object sender, RoutedEventArgs e)
        {
            // TODO
        }
        private void Copy(object sender, RoutedEventArgs e)
        {
            // TODO
        }
        private void Paste(object sender, RoutedEventArgs e)
        {
            // TODO
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            // TODO
        }
        private void Help(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        private void MenuItemEdT_Click(object sender, RoutedEventArgs e)
        {
            this.TabPanel.SelectedItem = TabItem_Emploi_du_temps;
        }
        private void MenuItemEnseignant_Click(object sender, RoutedEventArgs e)
        {
            this.TabPanel.SelectedItem = TabItem_Enseignants;
        }
        private void MenuItemMatiere_Click(object sender, RoutedEventArgs e)
        {
            this.TabPanel.SelectedItem = TabItem_Matieres;
        }
        private void MenuItemHoraire_Click(object sender, RoutedEventArgs e)
        {
            this.TabPanel.SelectedItem = TabItem_Horaires;
        }
        private void MenuItemSalle_Click(object sender, RoutedEventArgs e)
        {
            this.TabPanel.SelectedItem = TabItem_Salles;
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

        private void MenuItemRequetesXPath_Click(object sender, RoutedEventArgs e)
        {
            RequetesXPath requetesXPath = new RequetesXPath();
            requetesXPath.ExecRequetesXPath("RequetesXPath.xsl", "Semaine37.xml");
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