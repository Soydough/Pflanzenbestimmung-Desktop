using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für Statistik.xaml
    /// </summary>
    public partial class Statistik : UserControl
    {
        public Statistik()
        {
            InitializeComponent();

            for(int i = 0; i < Main.statistiken.Length; i++)
            {
                listView.Items.Add(Main.statistiken[i].ToString());
            }

            // ((System.ComponentModel.INotifyPropertyChanged)listView).PropertyChanged += (sender, e) =>
            // {
            //     if (e.PropertyName == "ActualWidth")
            //     {
            //         Column.Width = listView.ActualWidth;
            //         System.Windows.MessageBox.Show(Column.Width.ToString());
            //     }
            // };
        }

        private void Hauptmenü_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.changeContent(new Hauptmenü());
        }

        public int TitelBreite {
            get 
            {
                return (int)listView.Width;
            }
        }

        private void AnsehenButton_Click(object sender, RoutedEventArgs e)
        {
            Main.statistik = Main.statistiken[listView.SelectedIndex];

            Main.statistik = Main.api_anbindung.BekommeStatistik(Main.statistik.id_statistik);

            Main.momentanePflanzeAusStatistik = 0;
            MainWindow.dieses.ContentHolder.Content = new AdminQuizStatistik();
        }
    }
}
