using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CrazyEightsCosc2200
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameLogic game;
        public MainWindow()
        {
            InitializeComponent();

            game = new GameLogic();        //  Create object
            this.DataContext = game;       //  Connect UI to GameLogic
        }




        // Click event handlers.

        // Reset game click.
        public void resetClick(object sender, RoutedEventArgs e)
        {
            game.UpdateUI();   // test update
        }

        // Rules click.
        public void rulesClick(object sender, RoutedEventArgs e)
        {

        }

        // Settings click.
        public void settingsClick(object sender, RoutedEventArgs e)
        {

        }

        // Draw click.
        public void drawClick(object sender, RoutedEventArgs e)
        {

        }

        // Play click.
        public void playClick(object sender, RoutedEventArgs e)
        {

        }
    }
}