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
    /// Interaktionslogik für Registrierung.xaml
    /// </summary>
    public partial class Registrierung : UserControl
    {
        public static Registrierung dieses;
        public Registrierung()
        {
            InitializeComponent();
            dieses = this;
        }

        public void Initialize()
        {
            AusbilderComboBox.ItemsSource = Main.ausbilder;
            AusbilderComboBox.DisplayMemberPath = "Value";
            AusbilderComboBox.SelectedIndex = Main.ausbilderId;

            AubildungsartComboBox.ItemsSource = Main.ausbildungsarten;
            AubildungsartComboBox.DisplayMemberPath = "Value";
            AubildungsartComboBox.SelectedIndex = 0;

            FachrichtungComboBox.ItemsSource = Main.fachrichtungen;
            FachrichtungComboBox.DisplayMemberPath = "Value";
            FachrichtungComboBox.SelectedIndex = 0;
        }

        private void AbbrechenButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }

        private void AbsendenButton_Click(object sender, RoutedEventArgs e)
        {
            string benutzername = dieses.BenutzernameTextBox.Text;
            string passwort = dieses.PasswordBox.Password;
            passwort = Main.GetHashWithSalt(passwort, benutzername);


            int ausbildungsart = ((KeyValuePair<int, string>)dieses.AubildungsartComboBox.SelectedItem).Key;
            int fachrichtung = ((KeyValuePair<int, string>)dieses.FachrichtungComboBox.SelectedItem).Key;
            int ausbilderId = ((KeyValuePair<int, string>)dieses.AusbilderComboBox.SelectedItem).Key;

            Main.datenbankverbindung.FuegeBenutzerHinzu(benutzername, passwort, ausbilderId, ausbildungsart, fachrichtung);
        }
    }
}
