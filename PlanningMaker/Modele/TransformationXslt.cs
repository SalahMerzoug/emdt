using System;
using System.Windows;
using System.Xml.Xsl;

namespace PlanningMaker.Modele
{
    class TransformationXslt
    {

        public void TransformerXslt(string nomFichierXSL, string nomFichierXML)
        {
            // Load the style sheet.
            //XslCompiledTransform xslt = new XslCompiledTransform();
            //xslt.Load(nomFichierXSL);

            String nomFichierXMLsansExtension = nomFichierXML.Substring(0, nomFichierXML.LastIndexOf(".xml"));
            MessageBox.Show(nomFichierXMLsansExtension);

            // Execute the transform and output the results to a file.
            //xslt.Transform(nomFichierXML, nomFichierXMLsansExtension + ".html");
        }
    }
}
