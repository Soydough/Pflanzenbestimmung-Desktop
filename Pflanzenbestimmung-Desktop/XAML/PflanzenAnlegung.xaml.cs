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
            List<string> werte = new List<string>();

            for (int i = 0; i < StackPanelPflanzenAnlegung.Children.Count; i++)
            {
                TextBox aktuellesObject = StackPanelPflanzenAnlegung.FindName("tb" + Main.kategorien[i].kategorie) as TextBox;

                werte.Add(aktuellesObject.Text);
            }

            Main.api_anbindung.PflanzeErstellen(werte);
            Main.pflanzen = Main.api_anbindung.Bekommen<Pflanze>();

            foreach (string s in bilder)
            {
                byte[] b = File.ReadAllBytes(s);
                Main.api_anbindung.BildHochladen(Main.pflanzen[Main.pflanzen.Length].id_pflanze, b);
            }
        }
    }
}
