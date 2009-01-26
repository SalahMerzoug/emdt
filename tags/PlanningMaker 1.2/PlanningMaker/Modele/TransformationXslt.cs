using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace PlanningMaker.Modele
{
    class TransformationXslt
    {
        public string TransformerXslt(string numeroSemaine, string nomFichierXSL, string nomFichierXML, string nomFichierSVG)
        {
            string messageValidation = "";
            try
            {
                XsltArgumentList xslArg = new XsltArgumentList();
                xslArg.AddParam("numeroSemaine", "", numeroSemaine);

                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(@"..\..\Files\" + nomFichierXSL);

                XPathDocument xpathdocument = new XPathDocument(nomFichierXML);

                XmlTextWriter writer = new XmlTextWriter(nomFichierSVG, null);

                xslt.Transform(xpathdocument, xslArg, writer);
                writer.Close();

                messageValidation = "Transfomation OK";
            }
            catch (Exception e)
            {
                messageValidation = "Erreur ayant interrompu la transformation\n: " + e.Message + ".";
            }

            return messageValidation;
        }
    }
}
