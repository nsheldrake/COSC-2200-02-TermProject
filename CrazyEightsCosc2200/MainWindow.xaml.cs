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

        // When player clicks a card in their hand
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
            MessageBox.Show(
                "CRAZY EIGHTS RULES:\n\n" +
                "1. Each player is dealt 5 cards.\n" +
                "2. The remaining cards form the draw pile.\n" +
                "3. The top card of the draw pile is flipped to start the discard pile.\n" +
                "4. On your turn, play a card that matches the suit or rank of the top discard card.\n" +
                "5. Eights are wild — play an Eight on any card and choose the new suit.\n" +
                "6. If you cannot play, draw a card from the deck.\n" +
                "7. The first player to empty their hand wins!\n",
                "How to Play Crazy Eights",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );

        }

        // Draw click.
        public void drawClick(object sender, RoutedEventArgs e)
        {
            if (game.currentState != GameState.PlayerTurn)
            {
                game.InvalidTurn();
                return;
            }

            if (!game.deck.DeckIsEmpty())
            {
                game.realPlayer.DrawCard(game.deck);
                game.NextTurn();
                game.UpdateUI();
            }

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

            // Check if the move is valid
            if (!game.realPlayer.SuitMatch(selectedCard, game.pile))
            {
                game.InvalidMove();
                return;
            }

            // Play the card
            game.realPlayer.PlayCard(selectedCard, game.pile);
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

        // Settings click. (toggle visibility of the settings panel)
        public void settingsClick(object sender, RoutedEventArgs e)
        {
            SettingsPanel.Visibility = SettingsPanel.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        // Save settings click — inside the settings panel
        public void SaveSettingsClick(object sender, RoutedEventArgs e)
        {
            if (BackgroundColorPicker.SelectedItem != null)
                settings.BackgroundColor = ((ComboBoxItem)BackgroundColorPicker.SelectedItem).Content.ToString();

            if (MenuColorPicker.SelectedItem != null)
                settings.MenuColor = ((ComboBoxItem)MenuColorPicker.SelectedItem).Content.ToString();

            settings.SaveSettings();
            MessageBox.Show("Settings saved!", "Settings", MessageBoxButton.OK, MessageBoxImage.Information);
            SettingsPanel.Visibility = Visibility.Collapsed;
        }
    }
}