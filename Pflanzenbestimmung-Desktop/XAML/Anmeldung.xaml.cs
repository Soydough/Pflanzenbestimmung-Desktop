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

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für Anmeldung.xaml
    /// </summary>
    public partial class Anmeldung : UserControl
    {
        public Anmeldung()
        {
            InitializeComponent();
        }

        private void AnmeldungButton_Click(object sender, RoutedEventArgs e)
        {
            string benutzername = AnmeldungBenutzernameTextBox.Text;
            string passwort = AnmeldungPasswordBox.Password;

            string hash = Main.GetHashWithSalt(passwort, benutzername);

            Main.benutzer = Main.datenbankverbindung.BenutzerBekommen(benutzername, hash);
        }
    }
}
