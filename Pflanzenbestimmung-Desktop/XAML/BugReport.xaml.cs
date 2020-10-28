using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            var absender = new MailAddress("mineumann77@gmail.com", "Neumann");
            var empfänger = new MailAddress("jan.bellenberg@tsbw.cloud", "Kurbanov, Amir");
            const string absendePSW = "citrix150100";
            const string betreff = "Bug Report Pflanzenbestimmung";
            nachricht += "\n Diese Nachricht wurde über das Pflanzenbestimmung-Programm gesendet.";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,     
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(absender.Address, absendePSW),
            };

            using (var message = new MailMessage(absender, empfänger)
            {
                Subject = betreff,
                Body = nachricht
            })
            {
                try
                {
                    smtp.Send(message);
                    MessageBox.Show("Danke für ihr Feedback!");
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                    string s = e.ToString();
                    MessageBox.Show("Das sollte nicht passieren! :( \nBitte schreiben Sie selbst eine Email an den Projektmanager: A. K. :) \n" +
                        "Bei weiteren Fragen, fragen Sie bitte die entsprechende Person." + 
                        "\nMit freundlichen Grüßen \nIhre Damen und Herren");
                }
            }

            
        }

        
    }
}
