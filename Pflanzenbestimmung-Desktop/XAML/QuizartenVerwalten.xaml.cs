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
            //Lädt alle Quizarten 
            DataGridQuizArten.ItemsSource = Main.quizArt;
           
                
        }
    }
}
