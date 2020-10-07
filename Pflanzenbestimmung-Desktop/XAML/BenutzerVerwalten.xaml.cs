using Org.BouncyCastle.Asn1.Cmp;
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
using static Pflanzenbestimmung_Desktop.Helper;

namespace Pflanzenbestimmung_Desktop.XAML
{
    public partial class BenutzerVerwalten : UserControl
    {
        Azubis azubi;
        QuizArt art = new QuizArt();

        public BenutzerVerwalten(Azubis azubi)
        {
            InitializeComponent();
            Main.LadeAzubiDaten();
            this.azubi = azubi;
            BenutzernameAendernTextBox.Text = azubi.Nutzername;
            VornameAendernTextBox.Text = azubi.Vorname;
            NachnameAendernTextBox.Text = azubi.Name;

            AusbilderAendernComboBox.ItemsSource = Main.ausbilder;
            AusbilderAendernComboBox.DisplayMemberPath = "Value";
            for (int i = 0; i < Main.ausbilder.Count; i++)
            {
                string ausb = Main.ausbilder[i].vorname + " " + Main.ausbilder[i].name;
                if (azubi.Ausbilder.Equals(ausb))
                {
                    AusbilderAendernComboBox.SelectedIndex = i;
                }
                else
                {
                    AusbilderAendernComboBox.SelectedIndex = 0;
                }
            }

            AubildungsartAendernComboBox.ItemsSource = Main.ausbildungsarten;
            AubildungsartAendernComboBox.DisplayMemberPath = "Value";
            for (int i = 0; i < Main.ausbildungsarten.Count; i++)
            {
                if (azubi.Ausbildungsart.Equals(Main.ausbildungsarten[i].ausbildungsart))
                {
                    AubildungsartAendernComboBox.SelectedIndex = i;
                }
                else
                {
                    AubildungsartAendernComboBox.SelectedIndex = 0;
                }
            }

            FachrichtungAendernComboBox.ItemsSource = Main.fachrichtungen;
            FachrichtungAendernComboBox.DisplayMemberPath = "Value";
            for (int i = 0; i < Main.fachrichtungen.Count; i++)
            {
                if (azubi.Fachrichtung.Equals(Main.fachrichtungen[i].fachrichtung))
                {
                    FachrichtungAendernComboBox.SelectedIndex = i;
                }
                else
                {
                    FachrichtungAendernComboBox.SelectedIndex = 0;
                }
            }

            QuizartenAendernComboBox.ItemsSource = Main.quizArt;
            QuizartenAendernComboBox.DisplayMemberPath = "Value";
            for (int i = 0; i < Main.quizArt.Count; i++)
            {
                if (azubi.ID_quiz_art.Equals(Main.quizArt[i].id))
                {
                    QuizartenAendernComboBox.SelectedIndex = i;
                }
                else
                {
                    QuizartenAendernComboBox.SelectedIndex = 0;
                }
            }

            if (azubi.Pruefung.Equals(1))
            {
                PruefungsmodusAendernCheckbox.IsChecked = false;
            }
        }


        private void AendernAbbrechenButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.changeContent(new Benutzerverwaltung());
        }

        private void SpeichernButton_Click(object sender, RoutedEventArgs e)
        {

            bool admin = false;
            int id = azubi.ID;
            string name = null;
            string vorname = null;
            string nutzername = null;
            string passwort = PasswortAenderndBox.Password.Trim();
            int ausbildungsart = -1;
            int ausbilder = -1;
            int fachrichtung = -1;
            int id_quiz_art = -1;
            int pruefung = 0;

            if (azubi.Nutzername != (BenutzernameAendernTextBox.Text))
            {
                nutzername = BenutzernameAendernTextBox.Text;
            }
            if (azubi.Vorname!=(VornameAendernTextBox.Text))
            {
                vorname = VornameAendernTextBox.Text;
            }
            if (azubi.Name !=(NachnameAendernTextBox.Text))
            {
                name = NachnameAendernTextBox.Text;
            }
            if (azubi.Ausbildungsart != (Main.ausbildungsarten[AubildungsartAendernComboBox.SelectedIndex].ausbildungsart))
            {
                ausbildungsart = ((KeyValuePair<int, Ausbildungsart>)AubildungsartAendernComboBox.SelectedItem).Key;
            }
            if (azubi.Fachrichtung != (Main.fachrichtungen[FachrichtungAendernComboBox.SelectedIndex].fachrichtung))
            {
                fachrichtung = ((KeyValuePair<int, Fachrichtung>)FachrichtungAendernComboBox.SelectedItem).Key;
            }
          //  if (azubi.Ausbilder != (Main.ausbilder[AusbilderAendernComboBox.SelectedIndex].vorname +" "+ Main.ausbilder[AusbilderAendernComboBox.SelectedIndex].name))
          //  {
                ausbilder = ((KeyValuePair<int, Administrator>)AusbilderAendernComboBox.SelectedItem).Key;
          //  }
            if (azubi.ID_quiz_art != (Main.quizArt[QuizartenAendernComboBox.SelectedIndex].id))
            {
                id_quiz_art = ((KeyValuePair<int, QuizArt>)QuizartenAendernComboBox.SelectedItem).Key;
            }
            if (PruefungsmodusAendernCheckbox.IsChecked == false)
            {
                pruefung = 1;
            }
            else
            {
                pruefung = 0;
            }
            if (passwort != "")
            {
                passwort = PasswortAenderndBox.Password;
            }

            try
            {
                Main.api_anbindung.BenutzerAendern(admin, id, nutzername, passwort, name, vorname, ausbildungsart, fachrichtung, ausbilder, pruefung, id_quiz_art);
                MainWindow.changeContent(new Benutzerverwaltung());
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }//End Class
}//End Namespace

