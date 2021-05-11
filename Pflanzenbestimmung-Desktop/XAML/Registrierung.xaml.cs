using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für Registrierung.xaml
    /// </summary>
    public partial class Registrierung : UserControl
    {
        public static Registrierung dieses;
        public Registrierung()
        {
            InitializeComponent();
            dieses = this;
            Main.LadeAzubiDaten();
        }
        bool admin = false;
        public void Initialize()
        {
            AusbilderComboBox.ItemsSource = Main.ausbilder;
            AusbilderComboBox.DisplayMemberPath = "Value";
            AusbilderComboBox.SelectedIndex = Main.benutzer.id;

            AubildungsartComboBox.ItemsSource = Main.ausbildungsarten;
            AubildungsartComboBox.DisplayMemberPath = "Value";
            AubildungsartComboBox.SelectedIndex = 0;

            FachrichtungComboBox.ItemsSource = Main.fachrichtungen;
            FachrichtungComboBox.DisplayMemberPath = "Value";
            FachrichtungComboBox.SelectedIndex = 0;

            QuizartenComboBox.ItemsSource = Main.quizArt;
            QuizartenComboBox.DisplayMemberPath = "Value";
            QuizartenComboBox.SelectedIndex = 0;
        }

        private void AbbrechenButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }

        private void AbsendenButton_Click(object sender, RoutedEventArgs e)
        {
            string benutzername = null;
            string passwort = null;
            string name = null;
            string vorname = null;
            int pruefung = -1;

            int ausbildungsart = -1, fachrichtung = -1,
                    ausbilderId = -1, quizArtId = -1;

            if (!admin)
            {
                benutzername = BenutzernameTextBox.Text.Trim();
                passwort = PasswordBox.Password.Trim();
                name = NachnameTextBox.Text.Trim();
                vorname = VornameTextBox.Text.Trim();

                ausbildungsart = ((KeyValuePair<int, Ausbildungsart>)dieses.AubildungsartComboBox.SelectedItem).Key;
                fachrichtung = ((KeyValuePair<int, Fachrichtung>)dieses.FachrichtungComboBox.SelectedItem).Key;
                ausbilderId = ((KeyValuePair<int, Administrator>)dieses.AusbilderComboBox.SelectedItem).Key;
                quizArtId = ((KeyValuePair<int, QuizArt>)QuizartenComboBox.SelectedItem).Key;

                if (PruefungsmodusCheckbox.IsChecked == false)
                {
                    pruefung = 1;
                }
                else
                {
                    pruefung = 0;
                }
            }
            else
            {
                benutzername = AdminBenutzernameTextBox.Text.Trim();
                passwort = AdminPasswordBox.Password.Trim();
                name = AdminNachnameTextBox.Text.Trim();
                vorname = AdminVornameTextBox.Text.Trim();
            }

            if (benutzername == "" || passwort == "" || name == "" || vorname == "")
            {
                MessageBox.Show("Bitte alle Felder ausfüllen!");
            }
            else
            {
                passwort = Main.GetHashWithSalt(passwort, benutzername);

                try
                {
                    Main.api_anbindung.BenutzerErstellen(admin, benutzername, passwort, name, vorname, ausbilderId, ausbildungsart, fachrichtung, pruefung, quizArtId);
                    AbbrechenButton_Click(sender, e);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void TabHolder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as TabControl).SelectedIndex == 1)
            {
                admin = true;
            }
            else
            {
                admin = false;
            }
        }
    }
}
