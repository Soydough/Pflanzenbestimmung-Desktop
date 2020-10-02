using System.Windows;
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

        public MainWindow()
        {
            InitializeComponent();

            Mouse.SetCursor(Cursors.Wait);
            dieses = this;
            Main.Initialize();
            //changeContent(new Anmeldung());
            changeContent(new Anmeldung());
            Mouse.SetCursor(Cursors.None);

            //Main.EinloggenLaden();
        }

        public static void changeContent(object o)
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
                    Main.AktualisiereAusbilderId();
                    break;
                case "Registrierung":
                    dieses.Title = "Registrierung";
                    ((Registrierung)o).Initialize();
                    break;
                case "Benutzerverwaltung":
                    dieses.Title = "Benutzerverwaltung";
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
