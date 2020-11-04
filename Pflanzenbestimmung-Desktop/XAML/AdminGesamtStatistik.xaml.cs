using Org.BouncyCastle.Math.EC.Multiplier;
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
            string notiz = Main.azubiStatistik.pflanzen[Main.momentanePflanzeAusStatistik].notiz;

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
            // Notiz Label und Textbox hinzufügen
            Label lbNotiz = new Label();
            lbNotiz.Content = "Notiz:";
            StackPanel.Children.Add(lbNotiz);

            TextBox tbNotiz = new TextBox();
            RegisterName("tbNotiz", tbNotiz);
            if (notiz != null)
            {
                tbNotiz.Text = notiz;
            }
            tbNotiz.IsEnabled = false;

            StackPanel.Children.Add(tbNotiz);

            

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

        private void Notiz_Speichern_Click(object sender, RoutedEventArgs e)
        {
            (StackPanel.FindName("tbNotiz") as TextBox).IsEnabled = false;

            Notiz_bearbeiten.Visibility = Visibility.Visible;
            this.Notiz_Speichern.Visibility = Visibility.Collapsed;

            int idp = Main.azubiStatistik.pflanzen[Main.momentanePflanzeAusStatistik].id_pflanze;
            int ids = Main.azubiStatistik.id_statistik;
            string notiz = (StackPanel.FindName("tbNotiz") as TextBox).Text;

            Main.api_anbindung.ÄndereStatistikNotiz(ids, idp, notiz);
        }

        private void Notiz_bearbeiten_Click(object sender, RoutedEventArgs e)
        {
            (StackPanel.FindName("tbNotiz") as TextBox).IsEnabled = true;
            
            this.Notiz_bearbeiten.Visibility = Visibility.Collapsed;
            Notiz_Speichern.Visibility = Visibility.Visible;
        }
    }
}

