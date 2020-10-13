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

            for (int i = 0; i < Main.kategorien.Count; i++)
            {
                Label lb = new Label();
                string name = "lb" + Main.kategorien[i].kategorie;
                lb.Name = name;
                lb.Content = Main.kategorien[i].kategorie;
                RegisterName(name, lb);
                
                TextBox tb = new TextBox();
                tb.Name = "tb" + Main.kategorien[i].kategorie;

                StackPanelPflanzenAnlegung.Children.Add(lb);

                StackPanelPflanzenAnlegung.Children.Add(tb);
            }

            CheckBox galaCheckBox = new CheckBox();
            galaCheckBox.Content = "Gilt für Gala";
            galaCheckBox.Margin = new Thickness(0, 0, 0, 60);

            CheckBox zierCheckBox = new CheckBox();
            zierCheckBox.Content = "Gilt für Zier";
            zierCheckBox.Margin = new Thickness(0, 0, 0, 60);

            RegisterName("galaCheckBox", galaCheckBox);
            RegisterName("zierCheckBox", zierCheckBox);

            StackPanelPflanzenAnlegung.Children.Add(galaCheckBox);
            StackPanelPflanzenAnlegung.Children.Add(zierCheckBox);

            BilderHochladenFlaeche.DragEnter += new DragEventHandler(DragEnter);
            BilderHochladenFlaeche.Drop += new DragEventHandler(DragDrop);
        }

        void DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effects = DragDropEffects.Copy;
        }

        bool erstesBild = true;

        void DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if(Path.GetExtension(file).ToLower().IsAnyOf(".png", ".jpg", ".jpeg", ".gif", ".bmp"))
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
                        //Schon 10 Bilder da
                        MessageBox.Show("Die Maximalanzahl der Bilder wurde erreicht!");
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

            for (int i = 0; i < StackPanelPflanzenAnlegung.Children.Count; i++)
            {
                TextBox aktuellesObject = StackPanelPflanzenAnlegung.FindName("tb" + Main.kategorien[i].kategorie) as TextBox;

                werte.Add((Main.kategorien[i].id, aktuellesObject.Text));
            }

            bool istGala = (StackPanelPflanzenAnlegung.FindName("galaCheckBox") as CheckBox).IsChecked.Value;
            bool istZier = (StackPanelPflanzenAnlegung.FindName("zierCheckBox") as CheckBox).IsChecked.Value;

            Main.api_anbindung.PflanzeErstellen(istGala, istZier, werte);
            Main.pflanzen = Main.api_anbindung.Bekommen<Pflanze>();

            foreach (string s in bilder)
            {
                byte[] b = File.ReadAllBytes(s);
                Main.api_anbindung.BildHochladen(Main.pflanzen[Main.pflanzen.Length].id_pflanze, b);
            }

            bilder = new List<string>();

            MainWindow.changeContent(new Hauptmenü());
        }
    }
}
