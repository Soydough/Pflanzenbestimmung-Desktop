using Pflanzenbestimmung_Desktop.XAML;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für Hauptmenü.xaml
    /// </summary>
    public partial class Hauptmenü : UserControl
    {
        public Hauptmenü()
        {
            InitializeComponent();
            //Main.QuizBekommen();
        }

        private void AusloggenButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Sind Sie sich sicher?", "Abmelde-Bestätigung", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Main.benutzer = null;
                MainWindow.changeContent(new Anmeldung());
            }
        }

        private void AdministrationButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }

        private void AktuellesQuizButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.StartLoading();
            Main.QuizBekommen();

            Main.Laden();

            if (!Main.quiz.IsNullOrEmpty())
            {
                Main.quizAngefangenZeit = DateTime.Now;
                Main.quizTimer.Start();

                //Main.PflanzenbilderBekommen();
                ////Das erste Pflanzenbild anzeigen
                MainWindow.changeContent(new QuizBildanzeige());
                //MainWindow.changeContent(new QuizDateneingabe());
            }
        }

        private void StatistikButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Statistik());
        }

        private void ZufälligesQuizButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.StartLoading();
            Main.ZufälligesQuizBekommen();

            Main.Laden();

            if (!Main.quiz.IsNullOrEmpty())
            {
                Main.quizAngefangenZeit = DateTime.Now;
                Main.quizTimer.Start();

                ////Das erste Pflanzenbild anzeigen
                MainWindow.changeContent(new QuizBildanzeige());
                //MainWindow.changeContent(new QuizDateneingabe());
            }
        }

        bool eineminuteErstesMal = true;
        private void EineMinuteProPflanzeButton_Click(object sender, RoutedEventArgs e)
        {
            if (eineminuteErstesMal)
            {
                MessageBox.Show("Dieser Button macht momentan noch nichts");
                eineminuteErstesMal = false;
            }
        }

        private void BugReportButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new BugReport());
        }
    }
}
