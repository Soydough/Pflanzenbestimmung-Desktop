using System.Windows;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop.XAML
{
    /// <summary>
    /// Interaktionslogik für AdminGesamtStatistik.xaml
    /// </summary>
    public partial class AdminGesamtStatistik : UserControl
    {
        Azubis azubi;
        public AdminGesamtStatistik(Azubis azubi)
        {
            this.azubi = azubi;
            InitializeComponent();

            StackPanel.Children.Clear();

            StatistikPflanzeAntwort[] antworten = Main.azubiStatistik.pflanzen[Main.momentanePflanzeAusStatistik].antworten;

            for (int i = 0; i < antworten.Length; i++)
            {
                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                Label kategorieNameLabel = new Label
                {
                    Content = antworten[i].kategorie + ":"
                };

                //Setze den Inhalt für das "Korrekt"-Label
                Label korrekteAntwortLabel = new Label
                {
                    Content = antworten[i].korrekt
                };

                Label gegebeneAntwortLabel = new Label
                {
                    Content = antworten[i].eingabe
                };

                //if(korrekteAntwortLabel.Content.Equals(gegebeneAntwortLabel.Content))
                if (Main.IstRichtig(antworten[i].korrekt, antworten[i].eingabe))
                {
                    gegebeneAntwortLabel.Foreground = Main.RichtigFarbe;
                    gegebeneAntwortLabel.Content += " ✓";
                }
                else
                {
                    if (!Main.benutzer.IstWerker)
                    {
                        //Antwort falsch und kein Werker
                        gegebeneAntwortLabel.Foreground = Main.FalschFarbe;
                        gegebeneAntwortLabel.Content += " ×";
                    }
                    else
                    {
                        if (!antworten[i].WirdFürWerkGewertet)
                        {
                            //Antwort falsch, aber Werker
                            gegebeneAntwortLabel.Foreground = Main.FalschAberWerkerFarbe;
                            gegebeneAntwortLabel.Content += " /";
                        }
                        else
                        {
                            //Antwort falsch und Werker, Kategorie wird aber trotzdem gezählt
                            gegebeneAntwortLabel.Foreground = Main.FalschFarbe;
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

        void Hauptmenü_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }

        private void Weiter_Click(object sender, RoutedEventArgs e)
        {
            Main.momentanePflanzeAusStatistik = (Main.momentanePflanzeAusStatistik + 1) % Main.azubiStatistik.pflanzen.Length;

            MainWindow.changeContent(new AdminGesamtStatistik(azubi));
        }
    }
}

