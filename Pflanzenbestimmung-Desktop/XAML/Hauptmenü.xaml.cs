using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für Hauptmenü.xaml
    /// </summary>
    public partial class Hauptmenü : UserControl
    {
        public Hauptmenü()
        {
            InitializeComponent();
        }

        private void AusloggenButton_Click(object sender, RoutedEventArgs e)
        {
            Main.benutzer = null;
            MainWindow.changeContent(new Anmeldung());
        }

        private void AdministrationButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }
    }
}
