using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pflanzenbestimmung_Desktop.XAML
{
    /// <summary>
    /// Interaktionslogik für AdminStatistikBenutzerAuswahl.xaml
    /// </summary>
    public partial class AdminStatistikBenutzerAuswahl : UserControl
    {
        public AdminStatistikBenutzerAuswahl()
        {
            InitializeComponent();
        }

        private void Weiter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new AdminStatistik());
        }

        private void Zurück_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }
    }
}
