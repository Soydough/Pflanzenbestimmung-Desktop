using Flurl.Util;
using Pflanzenbestimmung_Desktop.XAML;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
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
            Main.LadeAzubiDaten();
            Azubiliste.ItemsSource = Main.InitializeMylist();
        }

        private void Zurück_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }

        private void Bearbeiten_Click(object sender, RoutedEventArgs e)
        {
            Azubis blarg = null;
            if (Azubiliste.SelectedItem == null)
            {
                MessageBox.Show("Bitte eine Auswahl treffen.");
            }
            else
            {
                for (int i = 0; i < Main.MyList.Count; i++)
                {


                    if (Azubiliste.SelectedItem.Equals(Main.MyList[i]))
                    {
                        blarg = Main.MyList[i];
                    }
                }
                MainWindow.changeContent(new BenutzerVerwalten(blarg));
            }
        }
    }
}
