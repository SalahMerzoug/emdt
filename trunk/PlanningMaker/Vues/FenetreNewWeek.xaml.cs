using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PlanningMaker.Modele;

namespace PlanningMaker.Vues
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class FenetreNewWeek : Window
    {
        private Semaine semaine;

        public Semaine Semaine
        {
            get
            {
                return semaine;
            }
        }

        public FenetreNewWeek()
        {
            semaine = null;
            InitializeComponent();
        }

        private void OkCommand(object sender, ExecutedRoutedEventArgs e)
        {
            semaine = new Semaine(Int32.Parse(Numero.Text), Date.Text);
            Close();
        }

        private void OkCommandPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                int numero = 0;
                numero = Int32.Parse(Numero.Text);

                String date = Date.Text;

                if (numero < 1 || numero > 52 || date.Equals(""))
                    e.CanExecute = false;
                else
                    e.CanExecute = true;
            }
            catch (Exception)
            {
                e.CanExecute = false;
            }
        }

        private void CancelCommand(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
