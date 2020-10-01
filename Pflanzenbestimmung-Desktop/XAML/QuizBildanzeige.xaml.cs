using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Linq.Expressions;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Pflanzenbestimmung_Desktop.XAML;

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

                        image.Margin = new Thickness(10);

                        image.MouseUp += ImageClickEvent;

                        stackPanel.Children.Add(image);
                    }
                    catch { }
                }
            }

            void ImageClickEvent(object sender, EventArgs e)
            {
                //MessageBox.Show("Opening second window.");
                Image image = sender as Image;
                Main.fullscreenImage = image.Source;
                var popup = new BildanzeigeVollbild();
                popup.Show();

                //var myPopup = new Popup
                //{
                //    Child = new Image
                //    {
                //        Source = image.Source,
                //        Stretch = Stretch.UniformToFill,
                //        Height = SystemParameters.PrimaryScreenHeight,
                //        Width = SystemParameters.PrimaryScreenWidth
                //    }
                //};
                //myPopup.IsOpen = true;
            }


        }

        private void Image_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Weiter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new QuizDateneingabe());
        }
    }
}
