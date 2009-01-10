using System;
using System.IO;
using System.Net;

namespace PlanningMaker.Modele
{
    class MiseAJour
    {
        private string loginMAJ;
        private string passMAJ;

        public MiseAJour(string login, string pass)
        {
            loginMAJ = login;
            passMAJ = pass;
        }

        public string VerifierMAJ()
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
                    message_MAJ = "• MAJ nécessaire" + " :\n\t· votre version = " + chaineVappli
                        + "\n\t· dernière version = " + chaineVnet;
                }
                else message_MAJ = "• NO MAJ" + " :\n\t· votre version = " + chaineVappli
                        + "\n\t· dernière version = " + chaineVnet;
            }

            return message_MAJ;
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

                // identifiants nécessaires pour le proxy
                WebRequest.DefaultWebProxy.Credentials = new NetworkCredential(loginMAJ, passMAJ);

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
                    + "\n→ Identifiants proxy incorrects.";
                }
                else
                {
                    resultat = "• Erreur lors de la connexion à internet •\n" + e2.Message
                    + "\n→ Vérifiez vos configurations de proxy et de pare-feu.";
                }
            }

            return resultat;
        }
    }
}
