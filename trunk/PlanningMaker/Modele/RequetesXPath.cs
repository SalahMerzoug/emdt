using System;
using System.Windows;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Diagnostics;
using PlanningMaker;

namespace PlanningMaker.Modele
{
    class RequetesXPath
    {
        public static string nomFichierSemaine;

        public static string NomFichierSemaine
        {
            set
            {
                nomFichierSemaine=value;
            }
            get
            {
                return nomFichierSemaine;
            }
        }

        // méthode par défaut
        public void ExecRequetesXPath(string nomFichierXSL, string nomFichierXML)
        {
            // A cause des # dans les chemins absolus entre XP et Vista, on récupère le chemin à la main !
            // Chemin de l'exécutable
            string exepath = Environment.GetCommandLineArgs()[0];
            // Répertoire de l'exécutable
            string exedir = exepath.Substring(0, exepath.LastIndexOf('\\'));
            
            // Charger la feuille de style
            XslCompiledTransform xslt = new XslCompiledTransform();

            // Charger le fichier de transformation XSL
            xslt.Load(exedir + @"\..\..\Files\" + nomFichierXSL);
          
            // Charger le fichier XML à transformer
            XPathDocument xpathdocument = new XPathDocument(exedir + @"\..\..\Files\" + nomFichierXML);

            // Créer le fichier de destination
            string nomFichierXpath = SaveFileXpath();
 
            // Create the XsltArgumentList.
            XsltArgumentList xslArg = new XsltArgumentList();
  
            try
            {
                // Transform the file.
                XmlTextWriter writer = new XmlTextWriter(nomFichierXpath, null);
                xslt.Transform(xpathdocument, xslArg, writer);
                writer.Close();

                // Affichage
                MessageBox.Show("Génération des requêtes XPath par défaut : OK");
                MainWindow.StartExternWebBrowser(nomFichierXpath);

            }
            catch { MessageBox.Show("Génération des requêtes XPath par défaut : Erreur");}

        }

        // méthodes avec arguments
        public void ExecRequetesXPath(string nomFichierXSL, string nomFichierXML,
            string numSemaine, string nom_recherche_1, string id_enseignant_2, string id_matière_3, string id_matière_4, string id_enseignant_5,
            string id_salle_6, string id_jour_6, string id_enseignant_7, string id_jour_7)
        {
            // Chemin de l'exécutable
            string exepath = Environment.GetCommandLineArgs()[0];
            // Répertoire de l'exécutable
            string exedir = exepath.Substring(0, exepath.LastIndexOf('\\'));

            // Charger la feuille de style
            XslCompiledTransform xslt = new XslCompiledTransform();

            // Charger le fichier de transformation XSL
            xslt.Load(exedir + @"\..\..\Files\" + nomFichierXSL);

            // Charger le fichier XML à transformer
            
            try
            {
                XPathDocument test_xpathdocument = new XPathDocument(nomFichierSemaine);
            }
            catch { MessageBox.Show("Erreur au chargement du document XPath !");}

            XPathDocument xpathdocument = new XPathDocument(nomFichierSemaine);
            
            // Créer le fichier de destination
            string nomFichierXpath = SaveFileXpath();

            // Create the XsltArgumentList.
            XsltArgumentList xslArg = new XsltArgumentList();
            xslArg.AddParam("numSemaine", "", numSemaine);
            xslArg.AddParam("nom_recherche_1", "", nom_recherche_1);
            xslArg.AddParam("id_enseignant_2", "", id_enseignant_2);
            xslArg.AddParam("id_matière_3", "", id_matière_3);
            xslArg.AddParam("id_matière_4", "", id_matière_4);
            xslArg.AddParam("id_enseignant_5", "", id_enseignant_5);
            xslArg.AddParam("id_salle_6", "", id_salle_6);
            xslArg.AddParam("id_jour_6", "", id_jour_6);
            xslArg.AddParam("id_enseignant_7", "", id_enseignant_7);
            xslArg.AddParam("id_jour_7", "", id_jour_7);

            try
            {
                // Transform the file.
                XmlTextWriter writer = new XmlTextWriter(nomFichierXpath, null);
                xslt.Transform(xpathdocument, xslArg, writer);
                writer.Close();

                // Affichage
                MessageBox.Show("Génération des requêtes XPath paramétrées : OK");
                MainWindow.StartExternWebBrowser(nomFichierXpath);

            }
            catch { MessageBox.Show("Génération des requêtes XPath paramétrées : Erreur"); }
        }

        public static string SaveFileXpath()
        {
            string directory = Environment.CurrentDirectory;
            System.Windows.Forms.SaveFileDialog dialogueS = new System.Windows.Forms.SaveFileDialog();
            dialogueS.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogueS.Filter = "Fichier HTML (*.html)|*.html";

            if (dialogueS.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
                {
                    Environment.CurrentDirectory = directory;
                    return dialogueS.FileName;
                }
            else 
                {
                    Environment.CurrentDirectory = directory;
                    return null;
                }
        }

    }
}