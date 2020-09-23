using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für QuizDateneingabe.xaml
    /// </summary>
    public partial class QuizDateneingabe : UserControl
    {
        public QuizDateneingabe()
        {
            InitializeComponent();

            //Kategorien laden und hinzufügen
        }

        private void Weiter_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.changeContent(new QuizStatistik());
        }
    }
}
