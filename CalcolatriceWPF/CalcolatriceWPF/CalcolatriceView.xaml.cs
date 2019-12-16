using CalcolatriceWPF.CalcolatriceViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CalcolatriceWPF
{
    /// <summary>
    /// Logica di interazione per CalcolatriceView.xaml
    /// </summary>
    public partial class CalcolatriceView : Window
    {
        public CalcolatriceView()
        {
            InitializeComponent();

            DataContext = new CalcolatriceVM();
        }
    }
}
