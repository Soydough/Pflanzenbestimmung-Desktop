using System;
using System.Windows;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop.XAML
{
    /// <summary>
    /// Interaktionslogik für AdminStatistik.xaml
    /// </summary>
    public partial class AdminStatistik : UserControl
    {
        Azubis azubi;
        public AdminStatistik(Azubis azubi)
        {
            this.azubi = azubi;
            InitializeComponent();
            Main.LadeStatistiken(azubi.ID);
            for (int i = 0; i < Main.azubiStatistiken.Length; i++)
            {
                //Lädt die Statistiken für den ausgewählten Azubi
                StatistikSelectedAzubi.Items.Add(Main.azubiStatistiken[i]);
            }

        }

        private void Ansehen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (StatistikSelectedAzubi.SelectedIndex == -1)
                {
                    StatistikSelectedAzubi.SelectedIndex = 0;
                    Main.azubiStatistik = Main.azubiStatistiken[StatistikSelectedAzubi.SelectedIndex];
                    Main.azubiStatistik = Main.api_anbindung.BekommeStatistik(Main.azubiStatistik.id_statistik);
                }
                else
                {
                    Main.azubiStatistik = Main.azubiStatistiken[StatistikSelectedAzubi.SelectedIndex];
                    Main.azubiStatistik = Main.api_anbindung.BekommeStatistik(Main.azubiStatistik.id_statistik);
                }

                if (Main.azubiStatistik.pflanzen.Length > 0)
                {
                    Main.momentanePflanzeAusStatistik = 0;
                    MainWindow.changeContent(new AdminGesamtStatistik(azubi));
                }
                else if (Main.azubiStatistik != null)
                {
                    MessageBox.Show("Die gewählte Statistik enthält keine Pflanzen");
                }
            }
            catch (Exception @as)
            {
                MessageBox.Show(@as + "");
                throw;
            }

        }

        private void Zurück_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new AdminStatistikBenutzerAuswahl());
        }
    }
}
