using System.Windows;
using System.Windows.Controls;

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

#if DEBUG
            AnmeldungBenutzernameTextBox.Text = "SysAdmin";
            AnmeldungPasswordBox.Password = "KarteiAdmin321#";
            AnmeldungButton_Click(null, null);
#endif
        }

        private void AnmeldungButton_Click(object sender, RoutedEventArgs e)
        {
            string benutzername = AnmeldungBenutzernameTextBox.Text;
            string passwort = AnmeldungPasswordBox.Password;

            string hash = Main.GetHashWithSalt(passwort, benutzername);

            Main.benutzer = Main.api_anbindung.BenutzerBekommenAsync(benutzername, hash);

            if (Main.benutzer.IstGueltig)
            {
                MainWindow.changeContent(new Hauptmenü());
            }
            else
            {
                MessageBox.Show("Der Benutzer konnte nicht gefunden werden. Mögliche Ursachen:\n" +
                    "   • Der Benutzername oder das Passwort ist falsch\n" +
                    "   • Es konnte keine Verbindung zur Datenbank hergestellt werden.");
            }
        }
    }
}
