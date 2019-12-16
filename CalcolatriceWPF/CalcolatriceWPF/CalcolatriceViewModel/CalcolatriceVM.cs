using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CalcolatriceWPF.CalcolatriceViewModel
{
    public class CalcolatriceVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand<string> AggiornaDisplay { get; set; }
        public RelayCommand<string> SalvaOperazione { get; set; }

        public decimal Buffer { get; set; }

        private string display = string.Empty;
        public string Display
        {
            get { return display; }
            set { if (value != display) { display = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Display")); } }
        }

        List<string> operazioniSpeciali;
        string operazioneSalvata = "+";

        public CalcolatriceVM()
        {
            AggiornaDisplay = new RelayCommand<string>(UpdateDisplay);
            SalvaOperazione = new RelayCommand<string>(Operazione);

            operazioniSpeciali = new List<string>()
            {
                "=","CE"
            };
        }

        void UpdateDisplay(string parametro)
        {
            //se il display sta visualizzando lo zero oppure è vuoto non do la possibilita di inserirlo
            if (Display == "0")
                return;

            Display = Display + parametro;
        }


        void Operazione(string operazione)
        {
            if (operazioniSpeciali.Contains(operazione))
            {
                svolgiOperazioneClassica(operazioneSalvata);
                svolgiOperazioneSpeciale(operazione);
                operazioneSalvata = "DEFAULT";
            }
            else
            {
                svolgiOperazioneClassica(operazioneSalvata);
                operazioneSalvata = operazione;
            }
        }

        void svolgiOperazioneClassica(string _operazione)
        {
            try
            {
                switch (_operazione)
                {
                    case "+":
                        Somma();
                        break;
                    case "-":
                        Sottrazione();
                        break;
                    case "*":
                        Moltiplicazione();
                        break;
                    case "/":
                        Divisione();
                        break;
                    default:
                        //Il default non deve fare nulla
                        break;
                }

                Display = string.Empty;
            }
            catch (FormatException ex)
            {
                operazioneSalvata = "+";
            }
            catch (OverflowException ex)
            {
                operazioneSalvata = "+";
            }

        }


        void svolgiOperazioneSpeciale(string _operazioneSpeciale)
        {
            switch (_operazioneSpeciale)
            {
                case "CE":
                    Buffer = 0;
                    Display = string.Empty;
                    operazioneSalvata = "+";
                    break;
                case "=":
                    Display = Buffer.ToString();
                    break;
                default:
                    break;
            }
        }

        /*        OPERAZIONI      */
        void Somma()
        {
            Buffer = Buffer + Convert.ToDecimal(Display);
        }

        void Sottrazione()
        {
            Buffer = Buffer - Convert.ToDecimal(Display);
        }

        void Moltiplicazione()
        {
            Buffer = Buffer * Convert.ToDecimal(Display);
        }

        void Divisione()
        {
            try
            {
                Buffer = Buffer / Convert.ToDecimal(Display);
            }
            catch (DivideByZeroException ex)
            {
                svolgiOperazioneSpeciale("CE");
                MessageBox.Show("Impossibile dividere per zero");
            }
        }

    }
}
