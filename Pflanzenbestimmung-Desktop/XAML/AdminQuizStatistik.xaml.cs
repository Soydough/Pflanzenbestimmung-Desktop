using Pflanzenbestimmung_Desktop.XAML;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für QuizStatistik.xaml
    /// </summary>
    public partial class AdminQuizStatistik : UserControl
    {
        public AdminQuizStatistik()
        {
            InitializeComponent();

            MainWindow.dieses.Title = "Quiz-Auswertung: Pflanze " + (Main.momentanePflanzeAusStatistik + 1) + " von " + Main.statistik.pflanzen.Length;

            if (Main.statistik.pflanzen.Length > 0)
            {

                if (!Main.benutzer.istAdmin)
                {
                    //ist mir doch egal, ob der Nutzer ein Admin ist
                }

                //Entferne Platzhalter-Daten
                StackPanel.Children.Clear();

                //Bekomme die Antworten
                StatistikPflanzeAntwort[] antworten = Main.statistik.pflanzen[Main.momentanePflanzeAusStatistik].antworten;

                for (int i = 0; i < antworten.Length; i++)
                {
                    Grid grid = new Grid();
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition());

                    //Setze den Inhalt für das "Korrekt"-Label
                    Label korrekteAntwortLabel = new Label();
                    korrekteAntwortLabel.Content = antworten[i].kategorie + ": " + antworten[i].korrekt;

                    Label gegebeneAntwortLabel = new Label();
                    gegebeneAntwortLabel.Content = antworten[i].eingabe;

                    //if(korrekteAntwortLabel.Content.Equals(gegebeneAntwortLabel.Content))
                    if (Main.IstRichtig(antworten[i].korrekt, antworten[i].eingabe))
                    {
                        gegebeneAntwortLabel.Foreground = System.Windows.Media.Brushes.LimeGreen;
                        gegebeneAntwortLabel.Content += " ✓";
                    }
                    else
                    {
                        gegebeneAntwortLabel.Foreground = System.Windows.Media.Brushes.Red;
                        gegebeneAntwortLabel.Content += " ×";
                    }

                    //RegisterName(Main.kategorien[i].kategorie + "Label", gegebeneAntwortLabel);

                    grid.Children.Add(korrekteAntwortLabel);
                    grid.Children.Add(gegebeneAntwortLabel);
                    Grid.SetColumn(gegebeneAntwortLabel, 1);

                    StackPanel.Children.Add(grid);
                }
            }
            else
            {

            }
        }

        private void HauptmenüButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.changeContent(new Hauptmenü());
        }

        private void ZurückButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Braucht man das überhaupt? Nein!
            //MainWindow.changeContent(new AdminStatistik());
        }

        private void WeiterButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Main.momentanePflanzeAusStatistik = (Main.momentanePflanzeAusStatistik + 1) % Main.statistik.pflanzen.Length;

            MainWindow.changeContent(new AdminQuizStatistik());
        }
    }
}
