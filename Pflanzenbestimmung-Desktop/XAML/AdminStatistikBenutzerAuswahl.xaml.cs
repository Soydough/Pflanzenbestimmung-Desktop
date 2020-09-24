using System.Windows;
using System.Windows.Controls;

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
