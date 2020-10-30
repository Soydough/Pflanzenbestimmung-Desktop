using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace Pflanzenbestimmung_Desktop.XAML
{
    /// <summary>
    /// Interaktionslogik für QuizArtErstellen.xaml
    /// </summary>
    public partial class QuizArtErstellen : UserControl
    {
        List<int> pflanzenID;
        int aktuelleGroeße;
        public QuizArtErstellen()
        {
            InitializeComponent();
            DataGridPflanzenListe.ItemsSource = Main.pflanzen;
            pflanzenID = new List<int>();
            aktuelleGroeße = 0;
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
                bool bla = (DataGridPflanzenListe.Columns[0].GetCellContent(DataGridPflanzenListe.Items[i]) as CheckBox).IsChecked.Value;

                if (bla)
                {
                    pflanzenID.Add(Main.pflanzen[i].id_pflanze);
                }
            }

            int quizGroeße = pflanzenID.Count;
            if (keinLeererName != "")
            {
                Main.api_anbindung.QuizArtErstellen(keinLeererName, quizGroeße, pflanzenID);
                MainWindow.changeContent(new Administration());
            }
            else
            {
                MessageBox.Show("Bitte alle Felder füllen.");
            }
        }

        private void PflanzenListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //pflanzenID.Add(DataGridPflanzenListe.SelectedIndex);
        }
    }
}
