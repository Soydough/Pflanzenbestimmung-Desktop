using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Pflanzenbestimmung_Desktop.XAML
{
    /// <summary>
    /// Interaktionslogik für AdminPflanzenBearbeitung.xaml
    /// </summary>
    public partial class AdminPflanzenBearbeitung : UserControl
    {
        public static List<string> bilder = new List<string>();

        int ausgewaehltePflanze = 0;
        int anzahlDerBereitsVorhandenenPflanzen = 0;

        Image[] bilderArr;

        public AdminPflanzenBearbeitung()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            StackPanelPflanzenBearbeitung.Children.Clear();

            for (int i = 0; i < Main.kategorien.Count; i++)
            {
                Label lb = new Label();
                string name = "lb" + Main.kategorien[i].kategorie;
                //lb.Name = name;
                lb.Content = Main.kategorien[i].kategorie;

                //try
                //{
                //    UnregisterName(name);
                //}
                //catch { }

                //RegisterName(name, lb);

                TextBox tb = new TextBox();
                //tb.Name = "tb" + Main.kategorien[i].kategorie;

                try
                {
                    UnregisterName("tb" + Main.kategorien[i].kategorie.Replace("-", "_"));
                }
                catch { }

                RegisterName("tb" + Main.kategorien[i].kategorie.Replace("-", "_"), tb);

                StackPanelPflanzenBearbeitung.Children.Add(lb);

                StackPanelPflanzenBearbeitung.Children.Add(tb);
            }

            CheckBox galaCheckBox = new CheckBox();
            galaCheckBox.Content = "Gilt für Gala";
            galaCheckBox.Margin = new Thickness(0, 10, 0, 10);

            CheckBox zierCheckBox = new CheckBox();
            zierCheckBox.Content = "Gilt für Zier";
            zierCheckBox.Margin = new Thickness(0, 10, 0, 10);


            try
            {
                UnregisterName("galaCheckBox");
                UnregisterName("zierCheckBox");
            }
            catch { }
            RegisterName("galaCheckBox", galaCheckBox);
            RegisterName("zierCheckBox", zierCheckBox);

            StackPanelPflanzenBearbeitung.Children.Add(galaCheckBox);
            StackPanelPflanzenBearbeitung.Children.Add(zierCheckBox);

            BilderHochladenFlaeche.DragEnter += new DragEventHandler(DragEnter);
            BilderHochladenFlaeche.Drop += new DragEventHandler(DragDrop);

            //string[] pflanzennamen = Main.pflanzen.Select(pflanze => pflanze.ToString()).ToArray();

            //PflanzenDataGrid.ItemsSource = Main.pflanzen;
            PflanzenDataGrid.ItemsSource = Main.pflanzen;
            PflanzenDataGrid.SelectedIndex = 0;
        }

        public void aktualisiere()
        {
            aktualisiereAnzahlDerBereitsVorhandenenBilder();
            aktualisiereBilder(true);

            for (int i = 0; i < Main.kategorien.Count; i++)
            {
                TextBox aktuellesObject = StackPanelPflanzenBearbeitung.FindName("tb" + Main.kategorien[i].kategorie.Replace("-", "_")) as TextBox;
                aktuellesObject.Text = "";
            }

            try
            {
                for (int i = 0; i < Main.kategorien.Count; i++)
                {
                    TextBox aktuellesObject = StackPanelPflanzenBearbeitung.FindName("tb" + Main.kategorien[i].kategorie) as TextBox;
                    //aktuellesObject.Text = Main.pflanzen[ausgewaehltePflanze].kategorien[i].antwort;
                    KategorieAbfrage kategorieAbfrage = Main.pflanzen[ausgewaehltePflanze].kategorien.FindeKategorie(Main.kategorien[i].kategorie);
                    if(kategorieAbfrage is null == false)
                    {
                        aktuellesObject.Text = kategorieAbfrage.antwort;
                    }
                }
            }
            catch
            {
                //Wahrscheinlich ein unwichtiger Fehler (kein Eintrag für die Kategorie etc.), also ignorieren
            }

            CheckBox galaCheckBox = FindName("galaCheckBox") as CheckBox;
            CheckBox zierCheckBox = FindName("zierCheckBox") as CheckBox;

            galaCheckBox.IsChecked = Main.pflanzen[ausgewaehltePflanze].IstGala;
            zierCheckBox.IsChecked = Main.pflanzen[ausgewaehltePflanze].IstZier;
        }

        public void aktualisiereAnzahlDerBereitsVorhandenenBilder()
        {
            anzahlDerBereitsVorhandenenPflanzen = Main.api_anbindung.BekommePflanzenbilder(Main.pflanzen[ausgewaehltePflanze].id_pflanze).Length;
        }

        new void DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effects = DragDropEffects.Copy;
        }

        bool erstesBild = true;

        void DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (Path.GetExtension(file).ToLower().IsAnyOf(".png", ".jpg", ".jpeg", ".gif", ".bmp"))
                {
                    if (bilder.Count + anzahlDerBereitsVorhandenenPflanzen < 10)
                    {
                        bilder.Add(file);
                        if (erstesBild)
                        {
                            erstesBild = false;
                            MessageBox.Show("Bilder werden beim Speichern der Pflanze hochgeladen");
                        }
                    }
                    else
                    {
                        //Schon 10 Bilder da
                        MessageBox.Show("Die Maximalanzahl der Bilder wurde erreicht!\n" +
                            "Bitte kontaktieren Sie den System-Administrator, um Bilder zu löschen");
                    }
                }
                else
                {
                    MessageBox.Show("Dateiformat wird nicht unterstützt!\n" +
                        "Die Folgenden Bildformate werden unterstützt:\n" +
                        " • PNG\n" +
                        " • JPEG\n" +
                        " • GIF (derzeit nicht animiert)\n" +
                        " • BMP\n");
                }
            }
        }

        private void AbbrechenButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }

        private void SpeichernButton_Click(object sender, RoutedEventArgs e)
        {
            List<(int, string)> werte = new List<(int, string)>();

            for (int i = 0; i < Main.kategorien.Count; i++)
            {
                TextBox aktuellesObject = StackPanelPflanzenBearbeitung.FindName("tb" + Main.kategorien[i].kategorie.Replace("-", "_")) as TextBox;

                werte.Add((Main.kategorien[i].id, aktuellesObject.Text));
            }

            bool istGala = (StackPanelPflanzenBearbeitung.FindName("galaCheckBox") as CheckBox).IsChecked.Value;
            bool istZier = (StackPanelPflanzenBearbeitung.FindName("zierCheckBox") as CheckBox).IsChecked.Value;

            Main.api_anbindung.PflanzeAktualisieren(Main.pflanzen[ausgewaehltePflanze].id_pflanze, istGala, istZier, werte);
            Main.pflanzen = Main.api_anbindung.Bekommen<Pflanze>();

            foreach (string s in bilder)
            {
                byte[] b = File.ReadAllBytes(s);
                Main.api_anbindung.BildHochladen(Main.pflanzen[ausgewaehltePflanze].id_pflanze, b);
            }
            bilder = new List<string>();

            aktualisiere();
            zeigeBildVorschau();

            MessageBox.Show("Gespeichert!");
        }

        private void PflanzenComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ausgewaehltePflanze = (sender as ComboBox).SelectedIndex;
            if (!bilder.IsNullOrEmpty())
            {
                bilder = new List<string>();
                MessageBox.Show("Hochladen von Bildern abgebrochen");
                erstesBild = true;
            }
            aktualisiere();
            zeigeBildVorschau();
        }

        private void PflanzenDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ausgewaehltePflanze = (sender as DataGrid).SelectedIndex;
            if (!bilder.IsNullOrEmpty())
            {
                bilder = new List<string>();
                MessageBox.Show("Hochladen von Bildern abgebrochen");
                erstesBild = true;
            }
            bilderGeladen = false;
            aktualisiere();
            zeigeBildVorschau();
        }

        bool bilderGeladen = false;
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            aktualisiereBilder();
            aktualisiere();
            //zeigeBildVorschau();
        }

        public void aktualisiereBilder(bool force = false)
        {
            if (!bilderGeladen || force)
            {
                bilderGeladen = true;

                Main.pflanzenbilder = Main.api_anbindung.BekommePflanzenbilder(Main.pflanzen[PflanzenDataGrid.SelectedIndex].id_pflanze);

                bilderArr = new Image[Main.pflanzenbilder.Length];

                for (int i = 0; i < bilderArr.Length; i++)
                {
                    bilderArr[i] = new Image();

                    // Erstelle neue Bitmap
                    BitmapImage bmp = new BitmapImage();

                    // Konvertiere Bytes zu Bitmap
                    using (MemoryStream mem = new MemoryStream(Main.pflanzenbilder[i].bild.ToBytes()))
                    {
                        bmp.BeginInit();
                        bmp.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                        bmp.CacheOption = BitmapCacheOption.OnLoad;
                        bmp.UriSource = null;
                        bmp.StreamSource = mem;
                        bmp.EndInit();
                    }
                    bmp.Freeze();

                    bilderArr[i].Source = bmp;

                    bilderArr[i].Height = 100;
                    bilderArr[i].Width = 150;
                }

                BilderListView.ItemsSource = bilderArr;
            }
            zeigeBildVorschau();
        }

        private void zeigeBildVorschau()
        {
            BilderVorschau.ItemsSource = bilderArr;
        }

        private void LoeschenButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Sind Sie sich sicher, dass Sie die ausgewählten Bilder endgültig löschen wollen? Also so, dass die Bilder wirklich ganz echt richtig nicht mehr zu retten gelöscht werden? Und so, dass man die nicht mehr aufrufen kann? Also in echt jetzt?", "Lösch-Bestätigung", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                int bildIndex = 0;
                for (int i = 0; i < BilderListView.SelectedItems.Count; i++)
                {
                    if (bilderArr[bildIndex] != (BilderListView.SelectedItems[i] as Image))
                    {
                        bildIndex++;
                        continue;
                    }
                    //Main.api_anbindung.
                    //BitmapImage bmp = (BilderListView.SelectedItems[i] as Image).Source as BitmapImage;
                    //int byteInt = bmp.StreamSource;

                    Main.api_anbindung.PflanzenbildLoeschen(Main.pflanzenbilder[bildIndex].id_bild);
                    bildIndex++;
                }

                try
                {
                    aktualisiereBilder(true);
                }
                catch { }
            }
            else
            {
                MessageBox.Show("Löschen der Bilder abgebrochen");
            }
        }

        private void PflanzeLoeschenButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Sind Sie sich sicher, dass Sie die ausgewählte Pflanze endgültig löschen möchten?", "Lösch-Bestätigung", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Main.api_anbindung.LoeschePflanze(Main.pflanzen[PflanzenDataGrid.SelectedIndex].id_pflanze);

                Main.Initialize();
                PflanzenDataGrid.SelectedIndex = 0;
                ausgewaehltePflanze = 0;
                Initialize();
                //MessageBox.Show("Wenn das Löschen nicht erfolreich war; dieser Fehler ist durchaus bekannt");
            }
        }
    }
}
