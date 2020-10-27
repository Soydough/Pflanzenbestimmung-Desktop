using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            //txtneuekategoriename.Clear();
            //GalaCheckBox.IsChecked = false;
            //ZierCheckBox.IsChecked = false;
            //WerkerCheckBox.IsChecked = false;
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

        private void btnkatAenderungSpeich_Click(object sender, RoutedEventArgs e)
        {
            int a = 0;

            if (GridKategorienBearbeiten.SelectedItem == null)
            {
                MessageBox.Show("Niemanden ausgewählt!");
            }
            else
            {
                a = GridKategorienBearbeiten.SelectedIndex;
                string vergleichKat = Main.kategorieVerwaltungListe[a].kategorie;
                int id = Main.kategorieVerwaltungListe[a].id;
                string katUebergabe = null;
                int anzeigeGala = 0;
                int anzeigeZier = 0;
                int wertungWerker = 0;
                int imQuiz = 0;

                if (vergleichKat == txtneuekategoriename.Text)
                {
                    katUebergabe = vergleichKat;
                }
                else
                {
                    katUebergabe = txtneuekategoriename.Text;
                }
                if (GalaCheckBox.IsChecked == true)
                {
                    anzeigeGala = 1;
                }
                if (ZierCheckBox.IsChecked == true)
                {
                    anzeigeZier = 1;
                }
                if (WerkerCheckBox.IsChecked == true)
                {
                    wertungWerker = 1;
                }
                if (ImQuizCheckBox.IsChecked == true)
                {
                    imQuiz = 1;
                }
                Main.api_anbindung.UpdateKategorie(id, katUebergabe, anzeigeGala, anzeigeZier, wertungWerker, imQuiz);
                MainWindow.changeContent(new AdminKategorieErstellen());
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

        private void loeschen_click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            string Kategorie = null;
            if (GridKategorienBearbeiten.SelectedItem == null)
            {
                MessageBox.Show("Niemanden ausgewählt!");
            }
            else
            {
                for (int i = 0; i < Main.kategorieVerwaltungListe.Count; i++)
                {
                    if (GridKategorienBearbeiten.SelectedItem.Equals(Main.kategorieVerwaltungListe[i]))
                    {
                        id = Main.kategorieVerwaltungListe[i].id;
                        Kategorie = Main.kategorieVerwaltungListe[i].kategorie;
                    }
                }
                API_Anbindung loeschen = new API_Anbindung();

                string nachricht = "Sind sie sich sicher, dass der Benutzer:\n'" + Kategorie
                                   + "'\n gelöscht werden soll?";
                string caption = "Löschen?";
                var result = MessageBox.Show(nachricht, caption, MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    loeschen.LoescheKategorie(id);
                    MainWindow.changeContent(new AdminKategorieErstellen());
                }

            }
        }
    }
}
