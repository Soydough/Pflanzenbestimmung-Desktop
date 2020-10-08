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
            //Main.api_anbindung.KategorieErstellen(name);  //<-- Auskommentieren, um das Abenteuer zu starten

            /* Anmerkung Dirk:
             * Es tut mir außerordentlich leid, aber die Datenbank wurde aktualisiert und man benötigt nun mehr Informationen,
             * um eine Kategorie zu erstellen.
             * 
             * Was zusätzlich benötigt wird:
             * Ob die Kategorie für Gala angezeigt wird (als int oder bool)
             * Ob die Kategorie für Zier angezeigt wird (als int oder bool)
             * Ob die Kategorie für Werker gewertet wird (sie wird trotzdem angezeigt, wenn sie nicht gewertet wird; ebenfalls int oder bool)
             */
        }
    }
}
