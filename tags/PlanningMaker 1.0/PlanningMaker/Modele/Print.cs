using System.Drawing;
using System.Drawing.Printing;

namespace PlanningMaker.Modele
{
    public class Print
    {
        private PrintDocument _prDoc = null;
        private PrintAction _printAction = PrintAction.PrintToPreview;
        private int _currentPrintPage = 1;
        private int _maxPage = 1;

        public Print(string st)
        {
            this._prDoc = new PrintDocument();
            this._prDoc.OriginAtMargins = true;
            this._prDoc.DefaultPageSettings.Landscape = true;
            this._prDoc.DocumentName = st + " Emploi du temps " + st;
            this._prDoc.BeginPrint += new PrintEventHandler(this.prDoc_BeginPrint);
            this._prDoc.EndPrint += new PrintEventHandler(this.prDoc_EndPrint);
            this._prDoc.PrintPage += new PrintPageEventHandler(this.prDoc_PrintPage);
            this._prDoc.QueryPageSettings += new QueryPageSettingsEventHandler(this.prDoc_QueryPageSettings);
        }

        public PrintDocument Document
        {
            get { return this._prDoc; }
            set { this._prDoc = value; }
        }

        public int MaxPage
        {
            get { return this._maxPage; }
        }

        private void prDoc_BeginPrint(object sender, PrintEventArgs e)
        {
            this._maxPage = 1;
            this._printAction = e.PrintAction;
        }

        private void prDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            int height = 0;
            // Draw the title
            if (this._currentPrintPage == 1)
            {
                this.PrintTitle(e.Graphics, e.MarginBounds);
                height += 80;
            }

            // Check if there is some elements to prints
            /*if (this._personCounter >= this._persons.Count) e.HasMorePages = false;
            else
            {
                e.HasMorePages = true;
                this._currentPrintPage++;
                if (this._maxPage < this._currentPrintPage) this._maxPage = this._currentPrintPage;
            }*/
        }

        private void prDoc_EndPrint(object sender, PrintEventArgs e)
        {
            // Reinit counters
            this._currentPrintPage = 1;
        }

        private void prDoc_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            if (this._prDoc.DefaultPageSettings.PaperSize.Kind == PaperKind.A5) e.Cancel = true;
        }
        
        private void PrintTitle(Graphics g, Rectangle margin)
        {
            using (Font titleFont = new Font("Arial", 18, FontStyle.Bold | FontStyle.Underline))
            {
                float titleWidth = g.MeasureString(this._prDoc.DocumentName, titleFont).Width;
                g.DrawString(this._prDoc.DocumentName, titleFont, Brushes.Black, (margin.Width - titleWidth) / 2, 0);
            }
        }
    }
}
