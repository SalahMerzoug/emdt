using System;
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

            String nomFichierXMLsansExtension = nomFichierXML.Substring(0, nomFichierXML.LastIndexOf(".xml"));

            // Execute the transform and output the results to a file.
            xslt.Transform(@"..\..\Files\" + nomFichierXML, @"..\..\Files\" + nomFichierXMLsansExtension + "-RequêtesXPath.html");
        }
    }
}
