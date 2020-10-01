using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Linq.Expressions;

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

            Main.quiz[0].pflanze.id_pflanze = 1;
            //Bekomme aktuelle Bilder
            Main.PflanzenbilderBekommen();


            if (Main.pflanzenbilder.IsNullOrEmpty())
            {
                string pflanze = Main.quiz[Main.momentanePflanzeAusQuiz].pflanze.kategorieAbfragen[3].antwort;
                MessageBox.Show($"Für die Pflanze {pflanze} sind keine Bilder hinterlegt!");
            }
            else
            {
                stackPanel.Children.Clear();

                for (int i = 0; i < Main.pflanzenbilder.Length; i++)
                {
                    //TODO: Convert try to convert image to bitmap
                    try
                    {
                        Image image = new Image();
                        byte[] b = Main.pflanzenbilder[i].bild.ToBytes();

                        BitmapImage bmp = new BitmapImage();

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
                        image.Source = bmp;
                        stackPanel.Children.Add(image);
                    }
                    catch { }
                }
            }
        }
    }
}
