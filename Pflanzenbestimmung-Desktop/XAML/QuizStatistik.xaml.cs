using System;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für QuizStatistik.xaml
    /// </summary>
    /// 

    [Obsolete("Veraltet! Bitte stattdessen AdminStatistik verwenden (ja, der Name ist unpassend)", true)]
    public partial class QuizStatistik : UserControl
    {
        public static int pflanzenIndex = 0;
        public QuizStatistik()
        {
            InitializeComponent();

            StackPanel.Children.Clear();

            pflanzenIndex = Main.momentanePflanzeAusQuiz++;

            for (int i = 0; i < Main.kategorien.Count; i++)
            {
                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                Label korrekteAntwortLabel = new Label();
                korrekteAntwortLabel.Content = Main.quiz[pflanzenIndex].pflanze.kategorieAbfragen[i].antwort;

                Label gegebeneAntwortLabel = new Label();
                gegebeneAntwortLabel.Content = Main.quiz[pflanzenIndex].pflanze.kategorieAbfragen[i].gegebeneAntwort;

                //if(korrekteAntwortLabel.Content.Equals(gegebeneAntwortLabel.Content))
                if (Main.IstRichtig(Main.quiz[pflanzenIndex].pflanze.kategorieAbfragen[i].antwort, Main.quiz[pflanzenIndex].pflanze.kategorieAbfragen[i].gegebeneAntwort))
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
            // Dauerhaft deaktiviert :)
        }

        private void WeiterButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Main.momentanePflanzeAusQuiz++;
            MainWindow.changeContent(new QuizStatistik());
        }
    }
}
