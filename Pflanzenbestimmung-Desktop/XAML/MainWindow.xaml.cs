using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow dieses;

        Point GetMousePos() => PointToScreen(Mouse.GetPosition(this));

        public MainWindow()
        {
            InitializeComponent();

            //Test
            //string s = Main.api_anbindung.BekommePflanzenbilder(1)[0].bild;

            Mouse.SetCursor(Cursors.Wait);
            dieses = this;
            Main.Initialize();
            //changeContent(new Anmeldung());
            changeContent(new Anmeldung());
            Mouse.SetCursor(Cursors.None);

            //Main.EinloggenLaden();

#if geheim
            MouseMove += MausBewegt;
            Pupille.Visibility = Visibility.Visible;
            Nicht_Pupille.Visibility = Visibility.Visible;
#else
            Pupille.Visibility = Visibility.Collapsed;
            Nicht_Pupille.Visibility = Visibility.Collapsed;
#endif
        }

        private void MausBewegt(object sender, MouseEventArgs e)
        {
            var center = PointToScreen(new Point(0, 0));

            center.X += Width / 2;
            center.Y += Height / 2;

            var x1 = center.X;
            var y1 = center.Y;

            var x2 = GetMousePos().X;
            var y2 = GetMousePos().Y;

            var xDiff = x2 - x1;
            var yDiff = y2 - y1;

            //var angle = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
            var angle = Math.Atan2(yDiff, xDiff);

            var r = 60d;

            var dist = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));

            dist = Math.Abs(dist);

            if (dist < r)
            {
                r = dist;
            }

            var xoff = r * Math.Cos(angle);
            var yoff = r * Math.Sin(angle);

            double xoff1 = 0;
            double xoff2 = 0;
            double yoff1 = 0;
            double yoff2 = 0;

            if(xoff < 0)
            {
                xoff2 = -1 * xoff;
            }
            else
            {
                xoff1 = xoff;
            }

            if(yoff < 0)
            {
                yoff2 = -1 * yoff;
            }
            else
            {
                yoff1 = yoff;
            }

            Pupille.Margin = new Thickness(xoff1, yoff1, xoff2, yoff2);
        }

        public static void changeContent(UserControl o)
        {
            dieses.ContentHolder.Content = o;

            switch (o.GetType().Name)
            {
                case "Administration":
                    dieses.Title = "Administration";
                    break;

                case "Anmeldung":
                    dieses.Title = "Anmeldung";
                    break;

                case "Hauptmenü":
                    dieses.Title = "Pflanzenbestimmung";
                    if (!Main.benutzer.istAdmin)
                        ((Hauptmenü)o).AdministrationButton.Visibility = Visibility.Collapsed;
                    else
                    {
                        Hauptmenü h = o as Hauptmenü;
                        h.AktuellesQuizButton.Visibility = Visibility.Collapsed;
                        h.ZufälligesQuizButton.Visibility = Visibility.Collapsed;
                        h.StatistikButton.Visibility = Visibility.Collapsed;
                        h.EineMinuteProPflanzeButton.Visibility = Visibility.Collapsed;
                    }
                    Main.AktualisiereAusbilderId();
                    break;

                case "Registrierung":
                    dieses.Title = "Registrierung";
                    ((Registrierung)o).Initialize();
                    break;

                case "Benutzerverwaltung":
                    dieses.Title = "Benutzerverwaltung";
                    break;

                case "AdminQuizStatistik":
                    dieses.Title = "Quiz-Auswertung: Pflanze " + (Main.momentanePflanzeAusStatistik + 1) + " von " + Main.azubiStatistik.pflanzen.Length;
                    break;

                default:
                    dieses.Title = "Pflanzenbestimmung";
                    break;
            }
        }

        public static void StartLoading()
        {
            dieses.Laden.Visibility = Visibility.Visible;
            dieses.ContentHolder.Background = new SolidColorBrush(Colors.White) { Opacity = 0.3 };
            Mouse.OverrideCursor = Cursors.Wait;
        }
        public static void StopLoading()
        {
            dieses.Laden.Visibility = Visibility.Hidden;
            dieses.ContentHolder.Background = new SolidColorBrush(Colors.White) { Opacity = 1 };
            Mouse.OverrideCursor = null;
        }
    }
}
