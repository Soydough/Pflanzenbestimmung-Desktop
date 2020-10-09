using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für PflanzenAnlegung.xaml
    /// </summary>
    public partial class PflanzenAnlegung : UserControl
    {

        public PflanzenAnlegung()
        {

            InitializeComponent(); // TODO Design der Labels / Texboxen

            for (int i = 0; i < Main.kategorien.Count; i++)
            {
                Label lb = new Label();
                string name = "lb" + Main.kategorien[i].kategorie;
                lb.Name = name;
                lb.Content = Main.kategorien[i].kategorie;
                RegisterName(name, lb);
                
                TextBox tb = new TextBox();
                tb.Name = "tb" + Main.kategorien[i].kategorie;

                StackPanel_Quiz_Pflanze.Children.Add(lb);
                
                StackPanel_Quiz_Pflanze.Children.Add(tb);
            }
        }

        private void AbbrechenButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }

        private void SpeichernButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> werte = new List<string>();

            for (int i = 0; i < StackPanel_Quiz_Pflanze.Children.Count; i++)
            {
                object aktuellesObject = StackPanel_Quiz_Pflanze.FindName("tb" + Main.kategorien[i].kategorie);



            }
            //Main.api_anbindung.PflanzeErstellen();
        }
    }
}
