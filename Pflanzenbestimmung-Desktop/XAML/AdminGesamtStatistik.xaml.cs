using System.Windows;
using System.Windows.Controls;

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
