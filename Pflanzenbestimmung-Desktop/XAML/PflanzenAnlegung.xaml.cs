using System.Windows;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für PflanzenAnlegung.xaml
    /// </summary>
    public partial class PflanzenAnlegung : UserControl
    {
        public PflanzenAnlegung()
        {
            InitializeComponent();
        }

        private void AbbrechenButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }

        private void SpeichernButton_Click(object sender, RoutedEventArgs e)
        {
            string gattung = GattungTextBox.Text;
            string art = ArtTextBox.Text;
            string deName = DeutscherNameTextBox.Text;
            string famname = FamilienNameTextBox.Text;
            string herkunft = HerkunftTextBox.Text;
            string bluete = BlueteTextBox.Text;
            string blueteZeit = BluetezeitTextBox.Text;
            string blatt = BlattTextBox.Text;
            string wuchs = HabitusTextBox.Text;
            string besonderheiten = BesonderheitenTextBox.Text;

            Main.datenbankverbindung.FuegePflanzeHinzu(
                gattung, art, deName, famname, herkunft,
                bluete, blueteZeit, blatt, wuchs, besonderheiten);
        }
    }
}
