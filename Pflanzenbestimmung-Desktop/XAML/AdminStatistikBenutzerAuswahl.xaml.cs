using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System;

namespace Pflanzenbestimmung_Desktop.XAML
{
    /// <summary>
    /// Interaktionslogik für AdminStatistikBenutzerAuswahl.xaml
    /// </summary>
    public partial class AdminStatistikBenutzerAuswahl : UserControl
    {
        public AdminStatistikBenutzerAuswahl()
        {
            InitializeComponent();            
            Main.InitializeAzubiVerwaltungListe();
            ObservableCollection<Azubis> selectAzubiForStatistic = new ObservableCollection<Azubis>();
            for (int i = 0; i < Main.azubiVerwaltungListe.Count; i++)
            {
                string adminName = Main.azubiVerwaltungListe[i].Ausbilder.Replace(" ", "");

                if (adminName == Main.benutzer.nutzername)
                {
                    selectAzubiForStatistic.Add(Main.azubiVerwaltungListe[i]);
                }
            }
            SelctUserForStatisticDataGrid.ItemsSource = selectAzubiForStatistic;
        }

        private void Weiter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Azubis auswahl = null;
                if (SelctUserForStatisticDataGrid.SelectedItem == null)
                {
                    MessageBox.Show("Es wurde kein Auszubildender ausgewählt.");
                }
                else
                {
                    for (int i = 0; i < Main.azubiVerwaltungListe.Count; i++)
                    {
                        if (SelctUserForStatisticDataGrid.SelectedItem.Equals(Main.azubiVerwaltungListe[i]))
                        {
                            auswahl = Main.azubiVerwaltungListe[i];
                            break;
                        }
                    }                 
                        MainWindow.changeContent(new AdminStatistik(auswahl));
                }
            }
            catch (Exception t )
            {
                MessageBox.Show("Ein unerwarteter Fehler ist aufgetreten. Vergewissern Sie sich das eine Statistik für den Auszubildenden vorhanden ist. Wenn der Fehler wiederholt auftritt wenden Sie sich an den System-Administrator.");
            }
          
        }

        private void Zurück_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }
    }
}
