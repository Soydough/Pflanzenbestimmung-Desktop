using Pflanzenbestimmung_Desktop.XAML;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für QuizBildanzeige.xaml
    /// </summary>
    public partial class QuizBildanzeige : UserControl
    {
        public QuizBildanzeige()
        {
            InitializeComponent();

            //Bilder laden und zu ScrollView hinzufügen
            StackPanel stackPanel = BilderScrollView;

            //Bekomme aktuelle Bilder
            Main.PflanzenbilderBekommen();


            if (Main.pflanzenbilder.IsNullOrEmpty())
            {
                string pflanze = Main.quiz[Main.momentanePflanzeAusQuiz].pflanze.kategorien[3].antwort;
                MessageBox.Show($"Für die Pflanze {pflanze} sind keine Bilder hinterlegt!");
            }
            else
            {
                stackPanel.Children.Clear();

                for (int i = 0; i < Main.pflanzenbilder.Length; i++)
                {
                    try
                    {
                        // Erstelle neues Bild
                        Image image = new Image();
                        // Bekomme Bytes von String aus API
                        byte[] b = Main.pflanzenbilder[i].bild.ToBytes();

                        // Erstelle neue Bitmap
                        BitmapImage bmp = new BitmapImage();

                        // Konvertiere Bytes zu Bitmap
                        using (MemoryStream mem = new MemoryStream(b))
                        {
                            bmp.BeginInit();
                            bmp.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                            bmp.CacheOption = BitmapCacheOption.OnLoad;
                            bmp.UriSource = null;
                            bmp.StreamSource = mem;
                            bmp.EndInit();
                        }
                        bmp.Freeze();

                        // Setze Bitmap als Quelle für Bild
                        image.Source = bmp;

                        // Setze Margin für Bild
                        image.Margin = new Thickness(10);

                        // Füge Click-Event hinzu (Bild wird beim Klicken im Vollbildmodus angezeigt)
                        image.MouseUp += ImageClickEvent;

                        image.ToolTip = "Zum Vergrößern auf das Bild klicken";

                        // Bild wird StackPanel hinzugefügt
                        stackPanel.Children.Add(image);
                    }
                    catch { }
                }
            }

            void ImageClickEvent(object sender, EventArgs e)
            {
                Image image = sender as Image;
                Main.fullscreenImage = image.Source;
                var popup = new BildanzeigeVollbild();
                popup.Show();
            }
        }

        private void Weiter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new QuizDateneingabe());
        }
    }
}
