using Pflanzenbestimmung_Desktop.XAML;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für QuizStatistik.xaml
    /// </summary>
    public partial class AdminQuizStatistik : UserControl
    {
        int pflanzenIndex;
        public AdminQuizStatistik()
        {
            InitializeComponent();

            if(!Main.benutzer.istAdmin)
            {
                //ist mir doch egal, ob der Nutzer ein Admin ist
            }

            StackPanel.Children.Clear();

            pflanzenIndex = Main.momentanePflanzeAusStatistik++;

            for (int i = 0; i < Main.kategorien.Count; i++)
            {
                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                Label korrekteAntwortLabel = new Label();
                //korrekteAntwortLabel.Content = Main.quiz[pflanzenIndex].pflanze.kategorieAbfragen[i].antwort;
                korrekteAntwortLabel.Content = Main.statistik.pflanzen[pflanzenIndex].antworten[i].korrekt;

                Label gegebeneAntwortLabel = new Label();
                //gegebeneAntwortLabel.Content = Main.quiz[pflanzenIndex].pflanze.kategorieAbfragen[i].gegebeneAntwort;
                gegebeneAntwortLabel.Content = Main.statistik.pflanzen[pflanzenIndex].antworten[i].eingabe;

                //if(korrekteAntwortLabel.Content.Equals(gegebeneAntwortLabel.Content))
                if (Main.IstRichtig(Main.statistik.pflanzen[pflanzenIndex].antworten[i].korrekt, Main.statistik.pflanzen[pflanzenIndex].antworten[i].eingabe))
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
