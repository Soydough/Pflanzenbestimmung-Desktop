using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;


namespace Pflanzenbestimmung_Desktop.XAML
{
    /// <summary>
    /// Interaktionslogik für QuizArtErstellen.xaml
    /// </summary>
    public partial class QuizArtBearbeiten : UserControl
    {
        List<int> pflanzenID;
        List<PlanzeMitBool> pflanzenMitBools;

        int aktuelleGroeße;
        
        public QuizArtBearbeiten(QuizArt daten)
        {
            InitializeComponent();
            NameDerQuizgrößeTextBox.Text = daten.quizname;
            pflanzenMitBools = new List<PlanzeMitBool>();

            for (int i = 0; i < Main.pflanzen.Length; i++)
            {
                pflanzenMitBools.Add(new PlanzeMitBool(Main.pflanzen[i].id_pflanze,
                                                       Main.pflanzen[i].Name, 
                                                       Main.pflanzen[i].zierpflanzenbau, 
                                                       Main.pflanzen[i].gartenlandschaftsbau, 
                                                       Main.pflanzen[i].kategorien));


                for (int d = 0; d < daten.pflanzen.Length; d++)
                {
                    if (daten.pflanzen[d].id_pflanze.Equals(pflanzenMitBools[i].id_pflanze))
                    {
                        pflanzenMitBools[i].imQuiz = true;
                    }
                }

            }

            pflanzenID = new List<int>();
            aktuelleGroeße = daten.quizgröße;
            
            DataGridPflanzenListe.ItemsSource = this.pflanzenMitBools;
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

        private class PlanzeMitBool : Pflanze
        {
            public PlanzeMitBool(int id, string name,  int zier, int gala, KategorieAbfrage[] kat)
            {
                imQuiz = false;
                id_pflanze = id;
                Name = name;
                zierpflanzenbau = zier;
                gartenlandschaftsbau = gala;
                kategorien = kat;
            }

            public bool imQuiz
            {
                get { return imQuiz; }
                set { }
            }


        }/// END CLASS
    }///END CLASS
}/// END NAMESPACE
