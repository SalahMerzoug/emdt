using System.IO;
using System.Net;
using System.Windows;
using System;

namespace PlanningMaker.Modele
{
    class MiseAJour
    {
        
        public void VerifierMAJ()
        {
            string message_MAJ, chaineVappli, chaineVnet;
            chaineVappli = GetVersionApplication();
            chaineVnet = GetDerniereVersion();

            if (chaineVnet.Substring(0, 6).CompareTo("Erreur") == 0)
            {
                message_MAJ = chaineVnet;
            }
            else
            {
                if (chaineVappli.CompareTo(chaineVnet) < 0)
                {
                    message_MAJ = "MAJ nécessaire" + " : votre version = " + chaineVappli
                        + " / dernière version = " + chaineVnet;
                }
                else message_MAJ = "NO MAJ" + " : votre version = " + chaineVappli
                        + " / dernière version = " + chaineVnet;
            }

            MessageBox.Show(message_MAJ);
        }

        private string GetVersionApplication()
        {
            return MainWindow.getNumeroVersion();
        }

        private string GetDerniereVersion()
        {
            string resultat;

            try
            {
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create("http://code.google.com/p/emdt/wiki/Version");
                
                // test proxy à ajouter

                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                Stream oS = webResp.GetResponseStream();
                StreamReader oSReader = new StreamReader(oS, System.Text.Encoding.ASCII);
                resultat = oSReader.ReadToEnd();

                string chaineAvant = "La version courante du logiciel est : <ul><li>";
                string chaineApres = " </li></ul>";
                int longueurChaineVersion = resultat.LastIndexOf(chaineApres) -
                    (resultat.LastIndexOf(chaineAvant) + chaineAvant.Length);
                resultat = resultat.Substring(resultat.LastIndexOf(chaineAvant) + chaineAvant.Length, longueurChaineVersion);

                oSReader.Close();
                oS.Close();
            }
            catch (Exception e)
            {
                resultat = "Erreur lors de la connexion à internet : " + e.Message;
            }

            return resultat;
        }
    }
}
