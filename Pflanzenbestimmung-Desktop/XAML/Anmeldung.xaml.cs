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
            //AnmeldungBenutzernameTextBox.Text = "SysUserA";
            AnmeldungBenutzernameTextBox.Text = "SysAdmin";
            AnmeldungPasswordBox.Password = "KarteiAdmin321#";
            //AnmeldungButton_Click(null, null);
            //AnmeldungPasswordBox.Password = "test";
            // TODO Löschen der Anmeldedaten vor dem Ausliefern.
            // Anmerkung Dirk: lol, das bringt sicher viel
#endif
        }

        private void AnmeldungButton_Click(object sender, RoutedEventArgs e)
        {
            Main.LadenStart();
            string benutzername = AnmeldungBenutzernameTextBox.Text;
            string passwort = AnmeldungPasswordBox.Password;

            string hash = Main.GetHashWithSalt(passwort, benutzername);

            Main.benutzer = Main.api_anbindung.Login(benutzername, hash);

            Main.Laden();

            if (Main.benutzer.IstGueltig)
            {
                Main.LadeStatistiken();
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
