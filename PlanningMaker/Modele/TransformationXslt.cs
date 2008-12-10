using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace PlanningMaker.Modele
{
    class TransformationXslt
    {

        private String messageValidation;

        public string TransformerXslt(string nomFichierXSL, string nomFichierXML)
        {
            try
            {
                string numeroSemaineEnParametre = "37";
                string nomFichierXMLsansExtension = nomFichierXML.Substring(0, nomFichierXML.LastIndexOf(".xml"));
                string extensionEnSortie = ".html";

                if (nomFichierXSL.CompareTo("EdTversSVG-FF.xsl") == 0) extensionEnSortie = ".svg";

                XsltArgumentList xslArg = new XsltArgumentList();
                xslArg.AddParam("numeroSemaine", "", numeroSemaineEnParametre);

                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(@"..\..\Files\" + nomFichierXSL);

                XPathDocument xpathdocument = new XPathDocument(@"..\..\Files\" + nomFichierXML);

                XmlTextWriter writer = new XmlTextWriter(@"..\..\Files\" + nomFichierXMLsansExtension + extensionEnSortie, null);

                xslt.Transform(xpathdocument, xslArg, writer);
                writer.Close();

                messageValidation = "Transfomation OK.";
            }
            catch (Exception e)
            {
                messageValidation = "Erreur ayant interrompu la transformation : " + e.Message + ".";
            }

            return messageValidation;
        }
    }
}
