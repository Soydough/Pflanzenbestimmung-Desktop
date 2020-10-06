using Flurl.Util;
using System;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für QuizDateneingabe.xaml
    /// </summary>
    public partial class QuizDateneingabe : UserControl
    {
        public QuizDateneingabe()
        {
            InitializeComponent();

            LinkesGrid.Children.Clear();
            RechtesGrid.Children.Clear();

            //MainWindow.DebugChangeTitle(Main.quiz[Main.momentanePflanzeAusQuiz].pflanze.kategorieAbfragen[0].antwort);

            for (int i = 0; i < Main.kategorien.Length; i++)
            {
                Kategorie k = Main.kategorien[i];

                Label kategorie = new Label();
                kategorie.Content = k.kategorie;
                TextBox eingabe = new TextBox();
                //eingabe.Name = k.kategorie + "TextBox";
                RegisterName(k.kategorie + "TextBox", eingabe);

                if (i % 2 == 0)
                {
                    LinkesGrid.Children.Add(kategorie);
                    LinkesGrid.Children.Add(eingabe);

                    Grid.SetRow(kategorie, i / 2 + 1);
                    Grid.SetRow(eingabe, i / 2 + 1);

                    Grid.SetColumn(eingabe, 1);
                }
                else
                {
                    RechtesGrid.Children.Add(kategorie);
                    RechtesGrid.Children.Add(eingabe);

                    Grid.SetRow(kategorie, (i + 1) / 2 - 1);
                    Grid.SetRow(eingabe, (i + 1) / 2 - 1);

                    Grid.SetColumn(eingabe, 1);
                }
            }
        }

        private void Weiter_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Main.einzelStatistiken[Main.momentanePflanzeAusQuiz] = new StatistikPflanze();
            Main.einzelStatistiken[Main.momentanePflanzeAusQuiz].antworten = new StatistikPflanzeAntwort[Main.kategorien.Length];

            //Antworten speichern
            for (int i = 0; i < Main.kategorien.Length; i++)
            {
                string eingabe = ((TextBox)FindName(Main.kategorien[i].kategorie + "TextBox")).Text;

              /*  if(string.IsNullOrWhiteSpace(eingabe))
                {
                    eingabe = "kеine Eingabe gemacht!";
                } */ // Soll prüfungsvorbereitend sein und da sagt dir auch keiner wenn du ein Feld nicht ausgefüllt hast.

                Main.quiz[Main.momentanePflanzeAusQuiz].pflanze.kategorieAbfragen[i].gegebeneAntwort = eingabe;

                Main.einzelStatistiken[Main.momentanePflanzeAusQuiz].antworten[i] = new StatistikPflanzeAntwort();

                //Einzelstatistik speichern
                Main.einzelStatistiken[Main.momentanePflanzeAusQuiz].antworten[i].eingabe = eingabe;
            }
            //Speichere ID von Pflanze der Einzelstatistik
            Main.einzelStatistiken[Main.momentanePflanzeAusQuiz].id_pflanze = Main.quiz[Main.momentanePflanzeAusQuiz].pflanze.id_pflanze;

            if(Main.momentanePflanzeAusQuiz >= Main.quiz.Length - 1)
            {
                //Quiz ist zu Ende. Ergebnisse in Datenbank speichern
                Main.quizTimer.Stop();
                Main.LadeStatistikenHoch();

                //Ergebnisse anzeigen
                Main.momentanePflanzeAusQuiz = -1;
                MainWindow.changeContent(new QuizStatistik());
            }
            else
            {
                //Quiz ist nicht zu Ende. Nächste Pflanze anzeigen
                MainWindow.changeContent(new QuizBildanzeige());
            }
        }
    }
}
