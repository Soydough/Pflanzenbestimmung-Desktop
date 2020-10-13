using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.IO;
using Flurl.Util;

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

        public AdminPflanzenBearbeitung()
        {
            InitializeComponent();

            StackPanelPflanzenBearbeitung.Children.Clear();

            for (int i = 0; i < Main.kategorien.Count; i++)
            {
                Label lb = new Label();
                string name = "lb" + Main.kategorien[i].kategorie;
                lb.Name = name;
                lb.Content = Main.kategorien[i].kategorie;
                RegisterName(name, lb);

                TextBox tb = new TextBox();
                //tb.Name = "tb" + Main.kategorien[i].kategorie;
                RegisterName("tb" + Main.kategorien[i].kategorie, tb);

                StackPanelPflanzenBearbeitung.Children.Add(lb);

                StackPanelPflanzenBearbeitung.Children.Add(tb);
            }

            CheckBox galaCheckBox = new CheckBox();
            galaCheckBox.Content = "Gilt für Gala";
            galaCheckBox.Margin = new Thickness(0, 10, 0, 10);

            CheckBox zierCheckBox = new CheckBox();
            zierCheckBox.Content = "Gilt für Zier";
            zierCheckBox.Margin = new Thickness(0, 10, 0, 10);

            RegisterName("galaCheckBox", galaCheckBox);
            RegisterName("zierCheckBox", zierCheckBox);

            StackPanelPflanzenBearbeitung.Children.Add(galaCheckBox);
            StackPanelPflanzenBearbeitung.Children.Add(zierCheckBox);

            BilderHochladenFlaeche.DragEnter += new DragEventHandler(DragEnter);
            BilderHochladenFlaeche.Drop += new DragEventHandler(DragDrop);

            PflanzenComboBox.ItemsSource = Main.pflanzen;
            PflanzenComboBox.SelectedIndex = 0;
        }

        public void aktualisiere()
        {
            aktualisiereAnzahlDerBereitsVorhandenenBilder();

            for(int i = 0; i <  Main.kategorien.Count; i++)
            {
                TextBox aktuellesObject = StackPanelPflanzenBearbeitung.FindName("tb" + Main.kategorien[i].kategorie) as TextBox;
                aktuellesObject.Text = "";
            }

            try
            {
                for (int i = 0; i < Main.kategorien.Count; i++)
                {
                    TextBox aktuellesObject = StackPanelPflanzenBearbeitung.FindName("tb" + Main.kategorien[i].kategorie) as TextBox;
                    aktuellesObject.Text = Main.pflanzen[ausgewaehltePflanze].kategorien[i].antwort;
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
                TextBox aktuellesObject = StackPanelPflanzenBearbeitung.FindName("tb" + Main.kategorien[i].kategorie) as TextBox;

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
        }
    }
}
