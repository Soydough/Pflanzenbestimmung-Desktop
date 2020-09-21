using System.Windows;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für Benutzerverwaltung.xaml
    /// </summary>
    public partial class Benutzerverwaltung : UserControl
    {
        public Benutzerverwaltung()
        {
            InitializeComponent();
        }

        private void Zurück_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }

        private void Bearbeiten_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }
    }
}
