using Pflanzenbestimmung_Desktop.XAML;
using System.Windows;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für Administration.xaml
    /// </summary>
    public partial class Administration : UserControl
    {
        public Administration()
        {
            InitializeComponent();
        }

        private void HauptmenüButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Hauptmenü());
        }

        private void Benutzerverwaltung_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Benutzerverwaltung());
        }

        private void ZuLernendeKategorienEinstellungButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new AdminKategorieErstellen());
        }

        private void NeuePflanzeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new PflanzenAnlegung());
        }

        private void NeuerBenutzer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Registrierung());
        }

        private void ErscheinendePflanzen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new ErscheinendePflanzenEinstellung());
        }

        private void StatistikButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new AdminStatistikBenutzerAuswahl());
        }

        private void QuizGroeßeErstellen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new QuizArtErstellen());
        }
    }
}
