using Pflanzenbestimmung_Desktop.XAML;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für QuizStatistik.xaml
    /// </summary>
    public partial class AdminQuizStatistik : UserControl
    {
        public AdminQuizStatistik()
        {
            InitializeComponent();
        }

        private void HauptmenüButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.changeContent(new Hauptmenü());
        }

        private void ZurückButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.changeContent(new AdminStatistik());
        }
    }
}
