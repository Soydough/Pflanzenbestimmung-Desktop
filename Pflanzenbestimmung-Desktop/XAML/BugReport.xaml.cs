using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop.XAML
{
    /// <summary>
    /// Interaktionslogik für BugReport.xaml
    /// </summary>
    public partial class BugReport : UserControl
    {
        public BugReport()
        {
            InitializeComponent();
        }

        private void btnZurück_Click(object sender, RoutedEventArgs e)
        {
            hauptmenü();
        }

        private void btnAbsenden_Click(object sender, RoutedEventArgs e)
        {
            sendEmail(txtMessage.Text);
            hauptmenü();
        }

        private void hauptmenü()
        {
            MainWindow.changeContent(new Hauptmenü());
        }

        private void sendEmail(string nachricht)
        {
            nachricht += "\nDiese Nachricht wurde durch das Pflanzenbestimmungs-Desktop-Programm versendet";
            try
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection()
                    {
                        ["text"] = nachricht
                    };
                    var response = client.UploadValues("https://pbstsbw.000webhostapp.com/email.php", values);
                    var responseString = Encoding.Default.GetString(response);
                    MessageBox.Show("Danke für Ihr Feedback!");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }


    }
}
