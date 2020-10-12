using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pflanzenbestimmung_Desktop.XAML
{
    /// <summary>
    /// Interaktionslogik für AdminKategorieErstellen.xaml
    /// </summary>
    public partial class AdminKategorieErstellen : UserControl
    {

        public AdminKategorieErstellen()
        {
            InitializeComponent();
            Main.kategorien = Main.api_anbindung.Bekommen<Kategorie>().ToList();
            var itemList = Main.kategorien.ToList();
            GridKategorienBearbeiten.ItemsSource = Main.InitializeKategorieVerwaltungListe();
            listvkategorieausw.ItemsSource = Main.InitializeKategorieVerwaltungListe();
        }

        private void btnneuekategorie_Click(object sender, RoutedEventArgs e)
        {
            string name = txtneuekategoriename.Text;
            int gala = 0;
            int zier = 0;
            int werker = 0;
            int imQuiz = 0;
            if (GalaCheckBox.IsChecked == true)
            {
                gala = 1;
            }
            if (ZierCheckBox.IsChecked == true)
            {
                zier = 1;
            }
            if (WerkerCheckBox.IsChecked == true)
            {
                werker = 1;
            }
            if (ImQuizCheckBox.IsChecked == true)
            {
                imQuiz = 1;
            }
            Main.api_anbindung.KategorieErstellen(name, gala, zier, werker, imQuiz);
            txtneuekategoriename.Clear();
            GalaCheckBox.IsChecked = false;
            ZierCheckBox.IsChecked = false;
            WerkerCheckBox.IsChecked = false;
            ImQuizCheckBox.IsChecked = false;
            MainWindow.changeContent(new AdminKategorieErstellen());
        }

        private void btnhomepage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }

        private void btnkatauswahl_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnkatnamenbearbeiten_Click(object sender, RoutedEventArgs e)
        {
            if (GridKategorienBearbeiten.SelectedItem == null)
            {
                MessageBox.Show("Niemanden ausgewählt!");
            }
        }

        private void GridKategorienBearbeiten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (int i = 0; i < Main.kategorieVerwaltungListe.Count; i++)
            {
                if (GridKategorienBearbeiten.SelectedItem.Equals(Main.kategorieVerwaltungListe[i]))
                {
                    txtneuekategoriename.Text = Main.kategorieVerwaltungListe[i].kategorie;
                    GalaCheckBox.IsChecked = Main.kategorieVerwaltungListe[i].wirdFürGalaAngezeigt;
                    ZierCheckBox.IsChecked = Main.kategorieVerwaltungListe[i].wirdFürZierAngezeigt;
                    WerkerCheckBox.IsChecked = Main.kategorieVerwaltungListe[i].wirdFürWerkGewertet;
                    ImQuizCheckBox.IsChecked = Main.kategorieVerwaltungListe[i].wirdImQuizAngezeigt;
                    break;
                }
            }
        }
    }
}
