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
    /// Interaktionslogik für QuizartenVerwalten.xaml
    /// </summary>
    public partial class QuizartenVerwalten : UserControl
    {
        public QuizartenVerwalten()
        {
            InitializeComponent();
            Main.Initialize();
            //Lädt alle Quizarten 
            DataGridQuizArten.ItemsSource = Main.InitializeQuizArtVerwaltungListe();
        }

        private void zurückButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }
        int id = -1;
        private void quizLoeschenButton_Click(object sender, RoutedEventArgs e)
        {      
            string quizname = null;
            QuizArtPflanze[] idPflanzenLoeschen = null;
            for (int i = 0; i < Main.QuizArtVerwaltungListe.Count; i++)
            {               
                if (Main.QuizArtVerwaltungListe.IndexOf(Main.QuizArtVerwaltungListe[i]) == DataGridQuizArten.SelectedIndex)
                {
                    id = Main.quizArt[i].id;
                    quizname = Main.quizArt[i].quizname;
                    idPflanzenLoeschen = Main.quizArt[i].pflanzen;
                    break;
                }
            }
            string nachricht = "Sind sie sich sicher, dass das Quiz:\n'" + quizname +
                                  "'\n gelöscht werden soll?";
            string caption = "Löschen?";
            var result = MessageBox.Show(nachricht, caption, MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Main.api_anbindung.QuizArtLoeschen(id, idPflanzenLoeschen);
                MainWindow.changeContent(new QuizartenVerwalten());
            }
        }

        private void quizBearbeitenButton_Click(object sender, RoutedEventArgs e)
        {
            QuizArt auswahl = null;
            if (DataGridQuizArten.SelectedItem == null)
            {
                MessageBox.Show("Keine Quizart ausgewählt.");
            }
            else
            {
                for (int i = 0; i < Main.QuizArtVerwaltungListe.Count; i++)
                {
                    if (DataGridQuizArten.SelectedItem.Equals(Main.QuizArtVerwaltungListe[i]))
                    {
                        auswahl = Main.QuizArtVerwaltungListe[i];
                        break;
                    }
                }
                MainWindow.changeContent(new QuizArtBearbeiten(auswahl));
            }
        }
    }
}
