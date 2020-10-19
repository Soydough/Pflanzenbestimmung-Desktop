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

            LinkesGrid.RowDefinitions.Add(new RowDefinition());
            RechtesGrid.RowDefinitions.Add(new RowDefinition());

            //MainWindow.DebugChangeTitle(Main.quiz[Main.momentanePflanzeAusQuiz].pflanze.kategorieAbfragen[0].antwort);

            for (int i = 0; i < Main.quiz[Main.momentanePflanzeAusQuiz].pflanze.kategorien.Length; i++)
            {
                Kategorie k = Main.kategorien[i];

                Label kategorie = new Label();
                kategorie.Content = k.kategorie;
                TextBox eingabe = new TextBox();
                //eingabe.Name = k.kategorie + "TextBox";
                RegisterName(k.kategorie + "TextBox", eingabe);

                if (i % 2 == 0)
                {
                    LinkesGrid.RowDefinitions.Add(new RowDefinition());

                    LinkesGrid.Children.Add(kategorie);
                    LinkesGrid.Children.Add(eingabe);

                    int zeile = 1;
                    for (int j = 0; j < i; j += 2)
                    {
                        zeile++;
                    }

                    Grid.SetRow(kategorie, zeile);
                    Grid.SetRow(eingabe, zeile);

                    Grid.SetColumn(eingabe, 1);
                }
                else
                {
                    RechtesGrid.RowDefinitions.Add(new RowDefinition());

                    RechtesGrid.Children.Add(kategorie);
                    RechtesGrid.Children.Add(eingabe);

                    int zeile = 1;
                    for (int j = 1; j < i; j += 2)
                    {
                        zeile++;
                    }

                    //Grid.SetRow(kategorie, (i - 1) / 2 + 1);
                    //Grid.SetRow(eingabe, (i + 1) / 2 - 1);
                    Grid.SetRow(kategorie, zeile);
                    Grid.SetRow(eingabe, zeile);

                    Grid.SetColumn(eingabe, 1);
                }
            }
        }

        private void Weiter_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Main.einzelStatistiken[Main.momentanePflanzeAusQuiz] = new StatistikPflanze();
            Main.einzelStatistiken[Main.momentanePflanzeAusQuiz].antworten = new StatistikPflanzeAntwort[Main.quiz[Main.momentanePflanzeAusQuiz].pflanze.kategorien.Length - 1];

            //Antworten speichern
            for (int i = 0; i < Main.einzelStatistiken[Main.momentanePflanzeAusQuiz].antworten.Length; i++)
            {
                string eingabe = ((TextBox)FindName(Main.kategorien[i].kategorie + "TextBox")).Text;

                /*  if(string.IsNullOrWhiteSpace(eingabe))
                  {
                      eingabe = "kеine Eingabe gemacht!";
                  } */ // Soll prüfungsvorbereitend sein und da sagt dir auch keiner wenn du ein Feld nicht ausgefüllt hast.

                Main.quiz[Main.momentanePflanzeAusQuiz].pflanze.kategorien[i].gegebeneAntwort = eingabe;

                Main.einzelStatistiken[Main.momentanePflanzeAusQuiz].antworten[i] = new StatistikPflanzeAntwort();

                //Einzelstatistik speichern
                Main.einzelStatistiken[Main.momentanePflanzeAusQuiz].antworten[i].eingabe = eingabe;
                Main.einzelStatistiken[Main.momentanePflanzeAusQuiz].antworten[i].korrekt = Main.quiz[Main.momentanePflanzeAusQuiz].pflanze.kategorien[i].antwort;
                Main.einzelStatistiken[Main.momentanePflanzeAusQuiz].antworten[i].kategorie = Main.quiz[Main.momentanePflanzeAusQuiz].pflanze.kategorien[i].kategorie_name;
                Main.einzelStatistiken[Main.momentanePflanzeAusQuiz].antworten[i].WirdFürWerkGewertet = Main.quiz[Main.momentanePflanzeAusQuiz].pflanze.kategorien[i].wirdFürWerkGewertet;
            }
            //Speichere ID weitere Daten für die Einzelstatistik
            Main.einzelStatistiken[Main.momentanePflanzeAusQuiz].id_pflanze = Main.quiz[Main.momentanePflanzeAusQuiz].pflanze.id_pflanze;

            if (Main.momentanePflanzeAusQuiz >= Main.quiz.Length - 1)
            {
                //Quiz ist zu Ende. Ergebnisse in Datenbank speichern
                Main.quizTimer.Stop();
                Main.LadeStatistikenHoch();
                Main.LadeStatistiken();
                Main.azubiStatistik = Main.azubiStatistiken[Main.azubiStatistiken.Length - 1];

                Main.azubiStatistik = Main.api_anbindung.BekommeStatistik(Main.azubiStatistik.id_statistik);

                //Ergebnisse anzeigen
                Main.momentanePflanzeAusQuiz = -1;

                Main.momentanePflanzeAusStatistik = 0;

                //MainWindow.changeContent(new QuizStatistik());
                MainWindow.changeContent(new AdminQuizStatistik());
            }
            else
            {
                //Quiz ist nicht zu Ende. Nächste Pflanze anzeigen
                MainWindow.changeContent(new QuizBildanzeige());
            }
        }
    }
}
