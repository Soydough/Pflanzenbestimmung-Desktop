using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für ErscheinendePflanzenEinstellung.xaml
    /// </summary>
    public partial class ErscheinendePflanzenEinstellung : UserControl
    {
        public ErscheinendePflanzenEinstellung()
        {
            InitializeComponent();
        }

        private void Bearbeiten_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void Zurück_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }
    }
}
