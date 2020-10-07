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

            for(int i = 0; i < Main.statistiken.Length; i++)
            {
                listView.Items.Add(Main.statistiken[i].ToString());
            }
        }

        private void Hauptmenü_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.changeContent(new Hauptmenü());
        }
    }
}
