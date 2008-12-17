using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows;

namespace PlanningMaker.Modele
{
    class MiseAJour
    {
        public void ThreadMAJ()
        {
            // Déclaration du thread
            Thread myThread;

            // Instanciation du thread, on spécifie dans le 
            // délégué ThreadStart le nom de la méthode qui
            // sera exécutée lorsque l'on appele la méthode
            // Start() de notre thread.
            myThread = new Thread(new ThreadStart(ThreadLoopMAJ));

            // Lancement du thread
            myThread.Start();
        }

        private void ThreadLoopMAJ()
        {
            VerifierMAJ();
            Thread.CurrentThread.Abort();
        }

        private void VerifierMAJ()
        {
            string message_MAJ, chaineVappli, chaineVnet;
            chaineVappli = MainWindow.getNumeroVersion();
            chaineVnet = GetDerniereVersion();

            if (chaineVnet.Contains("Erreur"))
            {
                message_MAJ = chaineVnet;
            }
            else
            {
                if (chaineVappli.CompareTo(chaineVnet) < 0)
                {
                    message_MAJ = "• MAJ nécessaire" + " :\n\tvotre version = " + chaineVappli
                        + " / dernière version = " + chaineVnet;
                }
                else message_MAJ = "• NO MAJ" + " :\n\tvotre version = " + chaineVappli
                        + " / dernière version = " + chaineVnet;
            }

            MessageBox.Show(message_MAJ);
        }

        private string GetDerniereVersion()
        {
            string resultat = "";
            string adresseSiteWeb = "http://code.google.com/p/emdt/wiki/Version";
            HttpWebRequest webReq;
            HttpWebResponse webResp;

            try
            {
                webReq = (HttpWebRequest)WebRequest.Create(adresseSiteWeb);
                webReq.Timeout = 5000;
                webResp = (HttpWebResponse)webReq.GetResponse();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("(407)"))
                {
                    // identifiants nécessaires pour le proxy
                    WebRequest.DefaultWebProxy.Credentials = new NetworkCredential("user", "pass");
                }
            }

            try
            {
                webReq = (HttpWebRequest)WebRequest.Create(adresseSiteWeb);
                webReq.Timeout = 5000;
                webResp = (HttpWebResponse)webReq.GetResponse();
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
            catch (Exception e2)
            {
                if (e2.Message.Contains("(407)"))
                {
                    resultat = "• Erreur lors de la connexion à internet •\n"
                    + "\n→ Identifiants proxy incorrects ; veuillez recommencer.";
                }
                else
                {
                    resultat = "• Erreur lors de la connexion à internet •\n" + e2.Message
                    + "\n\n→ Vérifiez vos configurations de proxy et de firewall.";
                }
            }

            return resultat;
        }
    }
}
