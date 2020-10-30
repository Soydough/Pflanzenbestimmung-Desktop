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
        List<int> pflanzenID = null;
        List<int> auswahl;
        List<PflanzeMitBool> pflanzenMitBools;
        List<PflanzeMitBool> pflanzenMitBoolsBackUp;

        int aktuelleGroeße;
        QuizArt daten;
        public QuizArtBearbeiten(QuizArt daten)
        {
            InitializeComponent();
            NameDerQuizgrößeTextBox.Text = daten.quizname;
            this.daten = daten;
            pflanzenMitBools = new List<PflanzeMitBool>();
            pflanzenMitBoolsBackUp = new List<PflanzeMitBool>();

            for (int i = 0; i < Main.pflanzen.Length; i++)
            {
                pflanzenMitBools.Add(new PflanzeMitBool(Main.pflanzen[i].id_pflanze,
                                                       Main.pflanzen[i].Name,
                                                       Main.pflanzen[i].zierpflanzenbau,
                                                       Main.pflanzen[i].gartenlandschaftsbau,
                                                       Main.pflanzen[i].kategorien));
                for (int d = 0; d < daten.pflanzen.Length; d++)
                {
                    if (daten.pflanzen[d].id_pflanze.Equals(pflanzenMitBools[i].id_pflanze))
                    {
                        pflanzenMitBools[i].SetImQuizArt(true);
                    }
                }

                pflanzenMitBoolsBackUp.Add(new PflanzeMitBool(Main.pflanzen[i].id_pflanze,
                                                       Main.pflanzen[i].Name,
                                                       Main.pflanzen[i].zierpflanzenbau,
                                                       Main.pflanzen[i].gartenlandschaftsbau,
                                                       Main.pflanzen[i].kategorien));
                for (int d = 0; d < daten.pflanzen.Length; d++)
                {
                    if (daten.pflanzen[d].id_pflanze.Equals(pflanzenMitBools[i].id_pflanze))
                    {
                        pflanzenMitBoolsBackUp[i].SetImQuizArt(true);
                    }
                }
            }

            pflanzenID = new List<int>();

            DataGridPflanzenListeBearbeiten.ItemsSource = this.pflanzenMitBools;

        }


        void OnCheckedBearbeiten(object sender, RoutedEventArgs e)
        {
            auswahl = new List<int>();
            for (int i = 0; i < DataGridPflanzenListeBearbeiten.Items.Count; i++)
            {
                bool bla = (DataGridPflanzenListeBearbeiten.Columns[0].GetCellContent(DataGridPflanzenListeBearbeiten.Items[i]) as CheckBox).IsChecked.Value;

                if (bla)
                {
                    auswahl.Add(Main.pflanzen[i].id_pflanze);
                }
            }
            aktuelleQuizGroeße.Content = auswahl.Count;  
        }

        void OnUncheckedBearbeiten(object sender, RoutedEventArgs e)
        {
            auswahl = new List<int>();
            for (int i = 0; i < DataGridPflanzenListeBearbeiten.Items.Count; i++)
            {
                bool bla = (DataGridPflanzenListeBearbeiten.Columns[0].GetCellContent(DataGridPflanzenListeBearbeiten.Items[i]) as CheckBox).IsChecked.Value;

                if (bla)
                {
                    auswahl.Add(Main.pflanzen[i].id_pflanze);
                }
            }
            aktuelleQuizGroeße.Content = auswahl.Count;
        }


        private void ArtBearbeitenAbbrechenButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new QuizartenVerwalten());
        }

        private void ArtBearbeitenSpeichernButton_Click(object sender, RoutedEventArgs e)
        {
            string keinLeererName = NameDerQuizgrößeTextBox.Text.Trim();
            List<int> LoeschPflanzeZuweisung = new List<int>();
            int quizid = daten.id;           
            for (int i = 0; i < pflanzenMitBoolsBackUp.Count; i++)
            {
                if (pflanzenMitBools[i].ImQuizArt && !pflanzenMitBoolsBackUp[i].ImQuizArt)
                {
                    //Fürs Hinzufügen
                    pflanzenID.Add(pflanzenMitBools[i].id_pflanze);
                }
                else if (!pflanzenMitBools[i].ImQuizArt && pflanzenMitBoolsBackUp[i].ImQuizArt)
                {
                    //Fürs Löschen
                    LoeschPflanzeZuweisung.Add(pflanzenMitBools[i].id_pflanze);
                }
            }

            int quizGroeße = pflanzenID.Count;
            if (keinLeererName != "")
            {
                Main.api_anbindung.QuizArtBearbeiten(quizid, keinLeererName, quizGroeße, pflanzenID, LoeschPflanzeZuweisung);
                MainWindow.changeContent(new QuizartenVerwalten());
            }
            else
            {
                MessageBox.Show("Bitte alle Felder füllen.");
            }
        }

        private class PflanzeMitBool : Pflanze
        {
            bool imQuizArt;
            public PflanzeMitBool(int id, string name, int zier, int gala, KategorieAbfrage[] kat)
            {
                imQuizArt = false;
                id_pflanze = id;
                Name = name;
                zierpflanzenbau = zier;
                gartenlandschaftsbau = gala;
                kategorien = kat;
            }

            public bool ImQuizArt
            {
                get { return imQuizArt; }
                set { imQuizArt = value; }
            }

            public void SetImQuizArt(bool art)
            {
                imQuizArt = art;
            }


        }/// END CLASS
    }///END CLASS
}/// END NAMESPACE
