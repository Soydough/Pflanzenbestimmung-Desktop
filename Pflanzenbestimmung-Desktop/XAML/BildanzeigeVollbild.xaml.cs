using System;
using System.Linq;
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

            Bild.MouseUp += ImageClickEvent;
            KeyDown += KeyPressEvent;
        }

        void ImageClickEvent(object sender, EventArgs e)
        {
            Close();
        }

        //Dieser Code gehört so. Bitte nicht ändern. Danke.
        Key[] \u006B\u006F\u005F\u006E\u0061\u005F\u005F\u006D\u0069 = new Key[] { \u004B\u0065\u0079.\u0055\u0070, \u004B\u0065\u0079.\u0055\u0070, \u004B\u0065\u0079.\u0044\u006F\u0077\u006E, \u004B\u0065\u0079.\u0044\u006F\u0077\u006E, \u004B\u0065\u0079.\u004C\u0065\u0066\u0074, \u004B\u0065\u0079.\u0052\u0069\u0067\u0068\u0074, \u004B\u0065\u0079.\u004C\u0065\u0066\u0074, \u004B\u0065\u0079.\u0052\u0069\u0067\u0068\u0074, \u004B\u0065\u0079.\u0042, \u004B\u0065\u0079.\u0041 };
        int index = 0;
        void KeyPressEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == \u004B\u0065\u0079.\u0045\u0073\u0063\u0061\u0070\u0065)
                \u0043\u006C\u006F\u0073\u0065();
            else if (new Key[]{ \u004B\u0065\u0079.\u0055\u0070, \u004B\u0065\u0079.\u0044\u006F\u0077\u006E, \u004B\u0065\u0079.\u004C\u0065\u0066\u0074, \u004B\u0065\u0079.\u0052\u0069\u0067\u0068\u0074, \u004B\u0065\u0079.\u0041, \u004B\u0065\u0079.\u0042 }.Contains(e.Key))
            {
                if(\u006B\u006F\u005F\u006E\u0061\u005F\u005F\u006D\u0069[index] == e.Key)
                {
                    index++;
                    if(index >= \u006B\u006F\u005F\u006E\u0061\u005F\u005F\u006D\u0069.Length)
                    {
                        \u004D\u0065\u0073\u0073\u0061\u0067\u0065\u0042\u006F\u0078.\u0053\u0068\u006F\u0077("\u0041\u006C\u006C\u0020\u0079\u006F\u0075\u0072\u0020\u0062\u0061\u0073\u0065\u0020\u0061\u0072\u0065\u0020\u0062\u0065\u006C\u006F\u006E\u0067\u0020\u0074\u006F\u0020\u0075\u0073\u0021");
                    }
                }
                else
                {
                    index = 0;
                }
            }
        }
    }
}
