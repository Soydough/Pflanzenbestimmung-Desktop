using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class AdminKategorieErstellen : UserControl
    {

        public AdminKategorieErstellen()
        {
            InitializeComponent();
            Main.kategorien = Main.api_anbindung.Bekommen<Kategorie>().ToList();
            dynamicCheckbox();
        }
     
        private void dynamicCheckbox()
        {            
            var itemList = Main.kategorien.ToList();
            foreach (var item in itemList)
            {
                CheckBox kat = new CheckBox();
                kat.Content = item.kategorie.ToString();
                stack.Children.Add(kat);
            }
        }

        private void btnneuekategorie_Click(object sender, RoutedEventArgs e)
        {
            string name = txtneuekategoriename.Text;
            int gala = 0;
            int zier = 0;
            int werker = 0;
            int imQuiz = 0;
            if (GalaCheckBox.IsChecked == true)
            {
                gala = 1;
            }
            if (ZierCheckBox.IsChecked == true)
            {
                zier = 1;
            }
            if (WerkerCheckBox.IsChecked == true)
            {
                werker = 1;
            }
            if (ImQuizCheckBox.IsChecked == true)
            {
                imQuiz = 1;
            }
            Main.api_anbindung.KategorieErstellen(name, gala, zier, werker, imQuiz);
            txtneuekategoriename.Clear();
            GalaCheckBox.IsChecked = false;
            ZierCheckBox.IsChecked = false;
            WerkerCheckBox.IsChecked = false;
            ImQuizCheckBox.IsChecked = false;
            MainWindow.changeContent(new AdminKategorieErstellen());
        }

        private void btnhomepage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }

        private void btnkatauswahl_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < stack.Children.Count; i++)
            {
                
            }
        }
    }
}
