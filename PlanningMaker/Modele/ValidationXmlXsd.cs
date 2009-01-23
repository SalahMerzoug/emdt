using System;
using System.Xml;
using System.Xml.Schema;

namespace PlanningMaker.Modele
{
    class ValidationXmlXsd
    {
        private String messageValidation;

        public String ValiderFichierXml(String nomDuFichierXml)
        {
            try
            {
                // Create the XmlSchemaSet class.
                XmlSchemaSet sc = new XmlSchemaSet();

                // Add the schema to the collection.
                sc.Add(null, @"..\..\Files\SchemaEdT.xsd");

                messageValidation = "Validation réussie";

                // Set the validation settings.
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas = sc;
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

                // Create the XmlReader object.
                XmlReader reader = XmlReader.Create(nomDuFichierXml, settings);

                // Parse the file. 
                while (reader.Read()) ;
            }
            catch (Exception e)
            {
                messageValidation = "Erreur ayant interrompu la validation :\n" + e.Message;
            }

            return messageValidation;
        }

        // Display any validation errors.
        private void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            messageValidation = "Echec de la validation :\n" + e.Message;
        }
    }
}
