using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop
{
    /// <summary>
    /// Interaktionslogik für ZuLernendeKategorienEinstellung.xaml
    /// </summary>
    public partial class ZuLernendeKategorienEinstellung : UserControl
    {
        public ZuLernendeKategorienEinstellung()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.changeContent(new Administration());
        }
    }
}
