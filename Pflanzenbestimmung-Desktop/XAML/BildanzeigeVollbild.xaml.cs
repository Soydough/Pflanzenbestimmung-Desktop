using System;
using System.Windows;
using System.Windows.Input;

namespace Pflanzenbestimmung_Desktop.XAML
{
    /// <summary>
    /// Interaktionslogik für BildanzeigeVollbild.xaml
    /// </summary>
    public partial class BildanzeigeVollbild : Window
    {
        public BildanzeigeVollbild()
        {
            InitializeComponent();

            Bild.Source = Main.fullscreenImage;
            PreviewKeyDown += new KeyEventHandler(HandleEsc);

            Bild.MouseUp += ImageClickEvent;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        void ImageClickEvent(object sender, EventArgs e)
        {
            Close();
        }
    }
}
