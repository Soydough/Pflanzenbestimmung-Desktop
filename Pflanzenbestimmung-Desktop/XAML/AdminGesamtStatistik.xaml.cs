using System.Windows;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop.XAML
{
    /// <summary>
    /// Interaktionslogik für AdminGesamtStatistik.xaml
    /// </summary>
    public partial class AdminGesamtStatistik : UserControl
    {
        Azubis azubi;
        public AdminGesamtStatistik(Azubis azubi)
        {
            this.azubi = azubi;
            InitializeComponent();
        }

        void Zurück_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new AdminStatistik(azubi));
        }

        void Hauptmenü_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Hauptmenü());
        }

        private void ZurückButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

