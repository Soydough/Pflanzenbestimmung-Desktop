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
using System.Windows.Shapes;

namespace Pflanzenbestimmung_Desktop.XAML
{
    /// <summary>
    /// Interaktionslogik für AdminKategorieErstellen.xaml
    /// </summary>
    public partial class AdminKategorieErstellen : Window
    {
        public AdminKategorieErstellen()
        {
            InitializeComponent();
            Main.kategorien = Main.api_anbindung.Bekommen<Kategorie>().ToList();
        }

        private void btnneuekategorie_Click(object sender, RoutedEventArgs e)
        {
            string name = txtneuekategoriename.Text;
            Main.api_anbindung.KategorieErstellen(name);
        }
    }
}
