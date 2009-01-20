using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PlanningMaker.Modele;

namespace PlanningMaker.Vues
{
    /// <summary>
    /// Logique d'interaction pour FenetreNewWeek.xaml
    /// </summary>
    public partial class FenetreNewWeek : Window
    {
        private Planning planning;
        private Semaine semaine;

        public Planning Planning
        {
            set
            {
                planning = value;
            }
        }

        public Semaine Semaine
        {
            get
            {
                return semaine;
            }
        }

        public FenetreNewWeek()
        {
            semaine = new Semaine();
            planning = null;
            InitializeComponent();
            
            Numero.DataContext = semaine;
            Date.DataContext = semaine;
        }

        private void OkCommand(object sender, ExecutedRoutedEventArgs e)
        {
            semaine = new Semaine(Int32.Parse(Numero.Text), Date.Text);
            planning.Semaines.Add(semaine);
            DialogResult = true;
            Close();
        }

        private void OkCommandPossible(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                int numero = 0;
                numero = Int32.Parse(Numero.Text);

                String date = Date.Text;

                DateValidationRule validateurDate = new DateValidationRule();
                NumeroValidationRule validateurNum = new NumeroValidationRule();
                
                bool semainePresente = false;
                foreach (Semaine s in planning.Semaines)
                {
                    if (s.Numero == semaine.Numero)
                    {
                        semainePresente = true;
                        break;
                    }
                }

                ValidationResult dateValide = validateurDate.Validate(Date.Text, null);
                ValidationResult numeroValide = validateurNum.Validate(Numero.Text, null);

                if (!numeroValide.IsValid || semainePresente || !dateValide.IsValid)
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
            DialogResult = false;
            Close();
        }
    }
}
