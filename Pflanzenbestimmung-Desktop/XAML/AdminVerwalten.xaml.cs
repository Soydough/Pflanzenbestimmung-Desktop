using System;
using System.Windows;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop.XAML
{
    public partial class AdminVerwalten : UserControl
    {
        Administrator admin;
        QuizArt art = new QuizArt();

        public AdminVerwalten(Administrator admin)
        {
            InitializeComponent();
            Main.LadeAzubiDaten();
            this.admin = admin;
            BenutzernameAendernTextBox.Text = this.admin.Nutzername;
            VornameAendernTextBox.Text = this.admin.Vorname;
            NachnameAendernTextBox.Text = this.admin.Name;
        }

        private void AendernAbbrechenButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Benutzerverwaltung());
        }

        private void SpeichernButton_Click(object sender, RoutedEventArgs e)
        {

            bool admin = true;
            int id = this.admin.ID;
            string name = this.admin.Name;
            string vorname = this.admin.Vorname;
            string nutzername = this.admin.Nutzername;
            string passwort = PasswortAenderndBox.Password.Trim();

            if (this.admin.Nutzername != (BenutzernameAendernTextBox.Text))
            {
                nutzername = BenutzernameAendernTextBox.Text;
            }
            if (this.admin.Vorname != (VornameAendernTextBox.Text))
            {
                vorname = VornameAendernTextBox.Text;
            }
            if (this.admin.Name != (NachnameAendernTextBox.Text))
            {
                name = NachnameAendernTextBox.Text;
            }

            if (passwort != "")
            {
                passwort = PasswortAenderndBox.Password;
                passwort = Main.GetHashWithSalt(passwort, nutzername);
            }

            try
            {
                Main.api_anbindung.BenutzerAendern(admin, id, nutzername, passwort, name, vorname, 0, 0, 0, 0, 0);
                MainWindow.changeContent(new Benutzerverwaltung());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Loeschen_Click(object sender, RoutedEventArgs e)
        {
            string azubiName;
            string azubiVorname;
            string nachricht;
            try
            {
                azubiName = admin.Name;
                azubiVorname = admin.Vorname;
                nachricht = "Sind sie sich sicher, dass der Benutzer:\n'" + azubiName + ", "
                                 + azubiVorname + "'\n gelöscht werden soll?";
                string caption = "Löschen?";
                var result = MessageBox.Show(nachricht, caption, MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Main.api_anbindung.BenutzerLoeschen(admin.ID, true);
                    MainWindow.changeContent(new Benutzerverwaltung());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }//End Class
}//End Namespace

