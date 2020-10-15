using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Pflanzenbestimmung_Desktop.XAML
{
    /// <summary>
    /// Interaktionslogik für QuizArtErstellen.xaml
    /// </summary>
    public partial class QuizArtErstellen : UserControl
    {
        public QuizArtErstellen()
        {
            InitializeComponent();
        }

        private void Numbers(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(QuizgrößeTextBox.Text);
        }

        private void ArtErstellenAbbrechenButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }

        private void ArtErstellenSpeichernButton_Click(object sender, RoutedEventArgs e)
        {
            string keinLeererName = NameDerQuizgrößeTextBox.Text.Trim();
            string keineLeereGroeße = QuizgrößeTextBox.Text.Trim();
            if (keinLeererName != "" && keineLeereGroeße != "")
            {
                string eingabe = QuizgrößeTextBox.Text;
                int zahl;

                bool erfolg = int.TryParse(eingabe, out zahl);
                if (erfolg)
                {
                    Main.api_anbindung.QuizArtErstellen(keinLeererName, keineLeereGroeße);
                    MainWindow.changeContent(new Administration());
                }
                else
                {
                    MessageBox.Show("Bitte keine Buchstaben in der Quizgröße eingeben.");
                }
            }
            else
            {
                MessageBox.Show("Bitte alle Felder füllen.");
            }
        }
    }
}
