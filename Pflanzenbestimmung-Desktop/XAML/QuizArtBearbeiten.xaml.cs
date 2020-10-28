using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Pflanzenbestimmung_Desktop.XAML
{
    /// <summary>
    /// Interaktionslogik für QuizArtErstellen.xaml
    /// </summary>
    public partial class QuizArtBearbeiten : UserControl
    {
        List<int> pflanzenID;
        int aktuelleGroeße;
        public QuizArtBearbeiten(QuizArt daten)
        {
            InitializeComponent();
            DataGridPflanzenListe.ItemsSource = Main.pflanzen;
            NameDerQuizgrößeTextBox.Text = daten.quizname;
            for (int i = 0; i < daten.pflanzen.Length; i++)
            {
                for (int j = 0; j < DataGridPflanzenListe.Items.Count; j++)
                {
                    if (daten.pflanzen[i].id_pflanze.Equals(Main.pflanzen[j].id_pflanze))
                    {
                        /*CheckBox box =*/
                        DataGridPflanzenListe.Items[j] = (DataGridPflanzenListe.Columns[0].GetCellContent(DataGridPflanzenListe.Items[j]) as CheckBox);
                        //box.IsChecked = true;
                        break;
                    }
                }
                
            }

            pflanzenID = new List<int>();
            aktuelleGroeße = daten.quizgröße;
        }

        
        void OnChecked(object sender, RoutedEventArgs e)
        {
            aktuelleGroeße = aktuelleGroeße + 1;
            aktuelleQuizGroeße.Content = aktuelleGroeße.ToString();
        }

        void OnUnchecked(object sender, RoutedEventArgs e)
        {
            aktuelleGroeße = aktuelleGroeße - 1;
            aktuelleQuizGroeße.Content = aktuelleGroeße.ToString();
        }


        private void ArtErstellenAbbrechenButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }

        private void ArtErstellenSpeichernButton_Click(object sender, RoutedEventArgs e)
        {
            string keinLeererName = NameDerQuizgrößeTextBox.Text.Trim();

            for (int i = 0; i < DataGridPflanzenListe.Items.Count; i++)
            {
                bool checket = (DataGridPflanzenListe.Columns[0].GetCellContent(DataGridPflanzenListe.Items[i]) as CheckBox).IsChecked.Value;

                if (checket)
                {
                    pflanzenID.Add(Main.pflanzen[i].id_pflanze);
                }
            }

            int quizGroeße = pflanzenID.Count;
            if (keinLeererName != "")
            {                 
                    Main.api_anbindung.QuizArtErstellen(keinLeererName, quizGroeße, pflanzenID);
                    MainWindow.changeContent(new QuizartenVerwalten());               
            }
            else
            {
                MessageBox.Show("Bitte alle Felder füllen.");
            }
        }
    }
}
