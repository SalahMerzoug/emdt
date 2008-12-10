using System.Windows;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;


namespace PlanningMaker.Modele
{
    class RequetesXPath
    {

        public void ExecRequetesXPath(string nomFichierXSL, string nomFichierXML)
        {

            // Load the style sheet.
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(@"..\..\Files\" + nomFichierXSL);
          
            // Create the XsltArgumentList.
            string numSemaine = "37";
            string nom_recherche_1 = "oug";
            string id_enseignant_2 = "obeaudoux";
            string id_matière_3 = "maths";
            string id_matière_4 = "anglais";
            string id_enseignant_5 = "kdrouet";
            string id_salle_6 = "langevin";
            string id_jour_6 = "mardi";
            string id_enseignant_7 = "kdrouet";
            string id_jour_7 = "mardi";

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

            string nomFichierXMLsansExtension = nomFichierXML.Substring(0, nomFichierXML.LastIndexOf(".xml"));
            XPathDocument xpathdocument = new XPathDocument(@"..\..\Files\" + nomFichierXML);
            XmlTextWriter writer = new XmlTextWriter(@"..\..\Files\" + nomFichierXMLsansExtension + "-RequêtesXPath.html", null);
            
            // Transform the file.
            xslt.Transform(xpathdocument, xslArg, writer);
            writer.Close();

            MessageBox.Show("Génération des requêtes XPath : OK");
       }
    }
}
