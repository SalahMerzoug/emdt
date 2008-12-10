using System.Windows;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace PlanningMaker.Modele
{
    class TransformationXslt
    {

        public void TransformerXslt(string nomFichierXSL, string nomFichierXML)
        {
            string numeroSemaineEnParametre = "37";
            string nomFichierXMLsansExtension = nomFichierXML.Substring(0, nomFichierXML.LastIndexOf(".xml"));

            XsltArgumentList xslArg = new XsltArgumentList();
            xslArg.AddParam("numeroSemaine", "", numeroSemaineEnParametre);

            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(@"..\..\Files\" + nomFichierXSL);

            XPathDocument xpathdocument = new XPathDocument(@"..\..\Files\" + nomFichierXML);

            XmlTextWriter writer = new XmlTextWriter(@"..\..\Files\" + nomFichierXMLsansExtension + ".html", null);

            xslt.Transform(xpathdocument, xslArg, writer);
            writer.Close();

            MessageBox.Show("OK");
        }
    }
}
