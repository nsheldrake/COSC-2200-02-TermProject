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
        private Card selectedCard = null;
        private Settings settings = new Settings();

        public MainWindow()
        {
            InitializeComponent();

            game = new GameLogic();        //  Create object
            this.DataContext = game;       //  Connect UI to GameLogic
        }




        // Click event handlers.

        // When player clicks a card in their hand.
        public void CardClick(object sender, RoutedEventArgs e)
        {
            Button clickedCard = sender as Button;
            if (clickedCard != null)
            {
                selectedCard = clickedCard.DataContext as Card;
            }
        }

        // Reset game click.
        public void resetClick(object sender, RoutedEventArgs e)
        {
            game.ResetGame();   
        }

        // Rules click.
        public void rulesClick(object sender, RoutedEventArgs e)
        {
            RulesScreen.Visibility = Visibility.Visible;
            DarkBackground.Visibility = Visibility.Visible;

        }

        // Close rules click.
        public void CloseRulesClick(object sender, RoutedEventArgs e)
        {
            RulesScreen.Visibility = Visibility.Collapsed;
            DarkBackground.Visibility = Visibility.Collapsed;
        }

        // Draw click.
        public void drawClick(object sender, RoutedEventArgs e)
        {
            if (game.currentState != GameState.PlayerTurn)
            {
                game.InvalidTurn();
                return;
            }

            if (game.deck.DeckIsEmpty())
            {
                game.deck.ReshuffleFromPile(game.pile);
            }

            game.realPlayer.DrawCard(game.deck);
            game.NextTurn();
            game.UpdateUI();

        }

        // Play click.
        public void playClick(object sender, RoutedEventArgs e)
        {
            // Make sure a card is selected
            if (selectedCard == null)
            {
                MessageBox.Show("Please select a card first!", "No Card Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Make sure it's the player's turn
            if (game.currentState != GameState.PlayerTurn)
            {
                game.InvalidTurn();
                return;
            }

            // Check if the move is valid suit
            if (!game.realPlayer.SuitMatch(selectedCard, game.pile, game.currentSuit))
            {
                game.InvalidMove();
                return;
            }

            // Play the card
            game.realPlayer.PlayCard(selectedCard, game.pile);

            // If card is an Eight, show suit picker before continuing
            if (game.rules.IsCardEight(selectedCard))
            {
                selectedCard = null;
                SuitScreen.Visibility = Visibility.Visible;
                DarkBackground.Visibility = Visibility.Visible;
                return; // wait for suit selection before next turn
            }
            else
            {
                game.SetCurrentSuit("");
            }

            selectedCard = null;

            // Check if player won
            if (game.realPlayer.hand.HandIsEmpty())
            {
                game.AnnounceWinner();
                return;
            }

            // Move to computer turn
            game.NextTurn();
            game.UpdateUI();
        }

        // Suit selection handlers.
        private void SuitChosen(string suit)
        {
            game.SetCurrentSuit(suit);
            SuitScreen.Visibility = Visibility.Collapsed;
            DarkBackground.Visibility = Visibility.Collapsed;

            // Now continue after Eight was played
            if (game.realPlayer.hand.HandIsEmpty())
            {
                game.AnnounceWinner();
                return;
            }

            game.NextTurn();
            game.UpdateUI();
        }

        // Click events for each suit in suit screen.
        public void SuitHearts(object sender, RoutedEventArgs e) => SuitChosen("Hearts");
        public void SuitDiamonds(object sender, RoutedEventArgs e) => SuitChosen("Diamonds");
        public void SuitClubs(object sender, RoutedEventArgs e) => SuitChosen("Clubs");
        public void SuitSpades(object sender, RoutedEventArgs e) => SuitChosen("Spades");


        // Settings click (toggle visibility of the settings screen).
        public void settingsClick(object sender, RoutedEventArgs e)
        {
            if (SettingsScreen.Visibility == Visibility.Visible)
            {
                SettingsScreen.Visibility = Visibility.Collapsed;
                DarkBackground.Visibility = Visibility.Collapsed;
            }
            else
            {
                SettingsScreen.Visibility = Visibility.Visible;
                DarkBackground.Visibility = Visibility.Visible;
            }
        }
        

        // Save settings click — inside the settings panel.
        public void SaveSettingsClick(object sender, RoutedEventArgs e)
        {
            if (BackgroundColorPicker.SelectedItem is ComboBoxItem selectedBackgroundColor)
                settings.BackgroundColor = selectedBackgroundColor.Tag.ToString();

            if (MenuColorPicker.SelectedItem is ComboBoxItem selectedMenuColor)
                settings.MenuColor = selectedMenuColor.Tag.ToString();

            settings.SaveSettings();
            MessageBox.Show("Settings saved!", "Settings", MessageBoxButton.OK, MessageBoxImage.Information);

            SettingsScreen.Visibility = Visibility.Collapsed;
            DarkBackground.Visibility = Visibility.Collapsed;
        }

        // Exit settings click. 
        public void ExitSettingsClick(object sender, RoutedEventArgs e)
        {
            // Hide settings panel
            SettingsScreen.Visibility = Visibility.Collapsed;

            // Hide dark overlay
            DarkBackground.Visibility = Visibility.Collapsed;
        }

        // Exit program click.
        public void exitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}