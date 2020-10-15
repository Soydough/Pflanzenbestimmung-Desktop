using System.Windows;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für Statistik.xaml
    /// </summary>
    public partial class Statistik : UserControl
    {
        public Statistik()
        {
            InitializeComponent();

            for (int i = 0; i < Main.azubiStatistiken.Length; i++)
            {
                listView.Items.Add(Main.azubiStatistiken[i].ToString());
            }

            listView.SelectedIndex = 0;
        }

        private void Hauptmenü_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.changeContent(new Hauptmenü());
        }

        public int TitelBreite
        {
            get
            {
                return (int)listView.Width;
            }
        }

        private void AnsehenButton_Click(object sender, RoutedEventArgs e)
        {
            Main.azubiStatistik = Main.azubiStatistiken[listView.SelectedIndex];
            Main.azubiStatistik = Main.api_anbindung.BekommeStatistik(Main.azubiStatistik.id_statistik);

            if (Main.azubiStatistik.pflanzen.Length > 0)
            {
                Main.momentanePflanzeAusStatistik = 0;
                MainWindow.changeContent(new AdminQuizStatistik());
            }
            else if (Main.azubiStatistik != null)
            {
                MessageBox.Show("Die gewählte Statistik enthält keine Pflanzen");
            }
        }
    }
}
