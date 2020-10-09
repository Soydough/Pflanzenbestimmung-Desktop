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

            if (!Main.benutzer.istAdmin)
            {
                //ist mir doch egal, ob der Nutzer ein Admin ist
                //Der Name war tatsächlich nur Clickbait. Diese Klasse ist für alle Benutzer gedacht
            }

            if(Main.azubiStatistik == null || Main.azubiStatistik.pflanzen == null || Main.azubiStatistik.pflanzen.Length == 0)
            {
                System.Windows.MessageBox.Show("Ein Fehler ist aufgetreten! Weiter zum Hauptmenü...");
                MainWindow.changeContent(new Hauptmenü());
                return;
            }

            //Entferne Platzhalter-Daten
            StackPanel.Children.Clear();

            //Bekomme die Antworten
            StatistikPflanzeAntwort[] antworten = Main.azubiStatistik.pflanzen[Main.momentanePflanzeAusStatistik].antworten;

            for (int i = 0; i < antworten.Length; i++)
            {
                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                Label kategorieNameLabel = new Label();
                kategorieNameLabel.Content = antworten[i].kategorie + ":";

                //Setze den Inhalt für das "Korrekt"-Label
                Label korrekteAntwortLabel = new Label();
                korrekteAntwortLabel.Content = antworten[i].korrekt;

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
                    if (!Main.benutzer.IstWerker)
                    {
                        //Antwort falsch und kein Werker
                        gegebeneAntwortLabel.Foreground = System.Windows.Media.Brushes.Red;
                        gegebeneAntwortLabel.Content += " ×";
                    }
                    else
                    {
                        if (!antworten[i].WirdFürWerkGewertet)
                        {
                            //Antwort falsch, aber Werker
                            gegebeneAntwortLabel.Foreground = System.Windows.Media.Brushes.Orange;
                            gegebeneAntwortLabel.Content += " /";
                        }
                        else
                        {
                            //Antwort falsch und Werker, Kategorie wird aber trotzdem gezählt
                            gegebeneAntwortLabel.Foreground = System.Windows.Media.Brushes.Red;
                            gegebeneAntwortLabel.Content += " ×";
                        }
                    }
                }

                //RegisterName(Main.kategorien[i].kategorie + "Label", gegebeneAntwortLabel);

                grid.Children.Add(kategorieNameLabel);
                grid.Children.Add(korrekteAntwortLabel);
                grid.Children.Add(gegebeneAntwortLabel);

                Grid.SetColumn(korrekteAntwortLabel, 1);
                Grid.SetColumn(gegebeneAntwortLabel, 2);

                StackPanel.Children.Add(grid);
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
            Main.momentanePflanzeAusStatistik = (Main.momentanePflanzeAusStatistik + 1) % Main.azubiStatistik.pflanzen.Length;

            MainWindow.changeContent(new AdminQuizStatistik());
        }
    }
}
