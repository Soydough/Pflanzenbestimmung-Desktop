using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für PflanzenAnlegung.xaml
    /// </summary>
    public partial class PflanzenAnlegung : UserControl
    {
        List<string> bilder = new List<string>();
        

        public PflanzenAnlegung()
        {
            InitializeComponent(); // TODO Design der Labels / Texboxen

            //Entfernt die Platzhalter
            StackPanelPflanzenAnlegung.Children.Clear();

            for (int i = 0; i < Main.kategorien.Count; i++)
            {
                //Vorbereitung der dynamisch generierten Labels
                Label lb = new Label();
                string name = "lb" + Main.kategorien[i].kategorie;
                lb.Content = Main.kategorien[i].kategorie;

                //Vorbereitung der dynamisch generierten Textboxen
                TextBox tb = new TextBox();
                RegisterName("tb" + Main.kategorien[i].kategorie.MakeValid(), tb);

                tb.Text = Main.kategorien[i].kategorie + " D"; // Default Werte

                //Hinzufügen des Labels
                StackPanelPflanzenAnlegung.Children.Add(lb);
                //Hinzufügen der Textbox
                StackPanelPflanzenAnlegung.Children.Add(tb);
            }

            //CheckBox für IstGala
            CheckBox galaCheckBox = new CheckBox();
            galaCheckBox.Content = "Gilt für den Bereich: Gartenlandschaftsbau";
            galaCheckBox.Margin = new Thickness(0, 10, 0, 10);

            //CheckBox für IstZier
            CheckBox zierCheckBox = new CheckBox();
            zierCheckBox.Content = "Gilt für den Bereich: Ziergartenbau";
            zierCheckBox.Margin = new Thickness(0, 10, 0, 10);

            RegisterName("galaCheckBox", galaCheckBox);
            RegisterName("zierCheckBox", zierCheckBox);

            StackPanelPflanzenAnlegung.Children.Add(galaCheckBox);
            StackPanelPflanzenAnlegung.Children.Add(zierCheckBox);

            //Event zum Hochladen der Bilder
            BilderHochladenFlaeche.DragEnter += new DragEventHandler(DragEnter);
            BilderHochladenFlaeche.Drop += new DragEventHandler(DragDrop);
        }

        new void DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effects = DragDropEffects.Copy;
        }

        //Beim ersten Hereinziehen von Bildern soll eine Nachricht angezeigt werden
        bool erstesBild = true;

        void DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (Path.GetExtension(file).ToLower().IsAnyOf(".png", ".jpg", ".jpeg", ".gif", ".bmp"))
                {
                    if (bilder.Count < 10)
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
                        //Schon 10 Bilder da; die Maximalanzahl der Bilder
                        MessageBox.Show("Die Maximalanzahl der Bilder wurde erreicht!");
                    }
                }
                else
                {
                    //Ungültiges oder noch nicht unterstütztes Bild
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
            //Die Eingaben für die Kategorien
            List<(int, string)> werte = new List<(int, string)>();

            for (int i = 0; i < Main.kategorien.Count; i++)
            {
                TextBox aktuellesObject = FindName("tb" + Main.kategorien[i].kategorie.MakeValid()) as TextBox;
                werte.Add((Main.kategorien[i].id, aktuellesObject.Text));
            }

            bool istGala = (StackPanelPflanzenAnlegung.FindName("galaCheckBox") as CheckBox).IsChecked.Value;
            bool istZier = (StackPanelPflanzenAnlegung.FindName("zierCheckBox") as CheckBox).IsChecked.Value;

            Main.api_anbindung.PflanzeErstellen(istGala, istZier, werte);
            Main.pflanzen = Main.api_anbindung.Bekommen<Pflanze>();

            //Lädt die Bilder aus den Dateien und lädt sie hoch
            foreach (string s in bilder)
            {
                byte[] b = File.ReadAllBytes(s);
                Main.api_anbindung.BildHochladen(Main.pflanzen[Main.pflanzen.Length].id_pflanze, b);
            }

            bilder = new List<string>();

            MessageBox.Show("Gespeichert!");
            MainWindow.changeContent(new Administration());
        }
    }
}
