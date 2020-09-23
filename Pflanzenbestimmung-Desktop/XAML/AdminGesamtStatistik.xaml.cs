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
    /// Interaktionslogik für AdminGesamtStatistik.xaml
    /// </summary>
    public partial class AdminGesamtStatistik : UserControl
    {
        public AdminGesamtStatistik()
        {
            InitializeComponent();
        }

        private void Zurück_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new AdminStatistikBenutzerAuswahl());
        }

        private void Hauptmenü_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Hauptmenü());
        }
    }
}
