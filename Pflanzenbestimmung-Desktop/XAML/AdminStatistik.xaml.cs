using System.Windows;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop.XAML
{
    /// <summary>
    /// Interaktionslogik für AdminStatistik.xaml
    /// </summary>
    public partial class AdminStatistik : UserControl
    {
        public AdminStatistik()
        {
            InitializeComponent();
        }

        private void Ansehen_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow.changeContent(new AdminQuizStatistik());
            MainWindow.changeContent(new AdminGesamtStatistik());
        }

        private void Zurück_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new AdminStatistikBenutzerAuswahl());
        }
    }
}
