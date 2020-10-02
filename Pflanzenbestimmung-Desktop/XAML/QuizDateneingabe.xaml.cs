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
            Main.momentanePflanzeAusQuiz++;

            //Antworten speichern
            //for (int i = 0; i < Main.kategorien.Length; i++)
            //{
            //    string eingabe = ((TextBox)FindName(Main.kategorien[i].kategorie + "TextBox")).Text;
            //    Main.quiz[Main.momentanePflanzeAusQuiz].k.gegebeneAntwort = eingabe;
            //}

            for (int i = 0; i < Main.kategorien.Length; i++)
            {
                string eingabe = ((TextBox)FindName(Main.kategorien[i].kategorie + "TextBox")).Text;
                Main.quiz[Main.momentanePflanzeAusQuiz].pflanze.kategorieAbfragen[i].gegebeneAntwort = eingabe;
            }

            MainWindow.changeContent(new QuizStatistik());
        }
    }
}
