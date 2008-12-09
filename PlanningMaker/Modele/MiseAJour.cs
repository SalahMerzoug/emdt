using System.IO;
using System.Net;
using System.Windows;

namespace PlanningMaker.Modele
{
    class MiseAJour
    {
        
        public void VerifierMAJ()
        {
            string message_MAJ, chaineVappli, chaineVnet;
            chaineVappli = GetVersionApplication();
            chaineVnet = GetDerniereVersion();
            if (chaineVappli.CompareTo(chaineVnet) < 0)
            {
                message_MAJ = "MAJ nécessaire" + " : votre version = " + chaineVappli
                    + " / dernière version = " + chaineVnet;
            }
            else message_MAJ = "NO MAJ" + " : votre version = " + chaineVappli
                    + " / dernière version = " + chaineVnet;

            MessageBox.Show(message_MAJ);
        }

        private string GetVersionApplication()
        {
            return MainWindow.getNumeroVersion();
        }

        private string GetDerniereVersion()
        {
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create("http://code.google.com/p/emdt/wiki/Version");
            HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
            Stream oS = webResp.GetResponseStream();
            StreamReader oSReader = new StreamReader(oS, System.Text.Encoding.ASCII);
            string resultat = oSReader.ReadToEnd();

            string chaineAvant = "La version courante du logiciel est : <ul><li>";
            string chaineApres = "</li></ul>";
            int longueurChaineVersion = resultat.LastIndexOf(chaineApres) - 
                (resultat.LastIndexOf(chaineAvant) + chaineAvant.Length);
            resultat = resultat.Substring(resultat.LastIndexOf(chaineAvant) + chaineAvant.Length, longueurChaineVersion);

            oSReader.Close();
            oS.Close();

            return resultat;
        }
    }
}
