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
using System.Xml;

namespace CrazyEightsCosc2200
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Properties.
        private GameLogic game;
        private Card selectedCard = null;

        // New settings object.
        private Settings settings = new Settings();

        public MainWindow()
        {
            InitializeComponent();

            game = new GameLogic();        //  Create object
            this.DataContext = game;       //  Connect UI to GameLogic

            // Apply saved settings upon startup
            ApplySettings();
        }




        // Click event handlers.


        // When player clicks a card in their hand.
        public void CardClick(object sender, RoutedEventArgs e)
        {
            // Get the clicked card as a button
            Button clickedCard = sender as Button;
            // If a card has been clicked
            if (clickedCard != null)
            {
                // Select the card
                selectedCard = clickedCard.DataContext as Card;
            }
        }

        // Reset game click.
        public void resetClick(object sender, RoutedEventArgs e)
        {
            // Call ResetGame() from GameLogic
            game.ResetGame();   
        }

        // Rules click.
        public void rulesClick(object sender, RoutedEventArgs e)
        {
            // Set RulesScreen to visible
            RulesScreen.Visibility = Visibility.Visible;
            // Set DarkBackground to visible
            DarkBackground.Visibility = Visibility.Visible;

        }

        // Close rules click.
        public void CloseRulesClick(object sender, RoutedEventArgs e)
        {
            // Set RulesScreen to invisible
            RulesScreen.Visibility = Visibility.Collapsed;
            // Set DarkBackground to invisible
            DarkBackground.Visibility = Visibility.Collapsed;
        }

        // Draw click.
        public async void drawClick(object sender, RoutedEventArgs e)
        {
            // If its the computers turn
            if (game.currentState != GameState.PlayerTurn)
            {
                // Display an invalid turn message to user
                game.InvalidTurn();
                return;
            }

            // If deck is empty
            if (game.deck.DeckIsEmpty())
            {
                // Reshuffle the pile
                game.deck.ReshuffleFromPile(game.pile);
            }

            // Draw a card from the deck and give it to the players hand
            game.realPlayer.DrawCard(game.deck);

            // Change to computers turn
            await game.NextTurn();

            // After computer plays check for winner
            Player winner = game.AnnounceWinner();
            // If theres a winner
            if (winner != null)
            {
                // Display the winner screen
                ShowWinnerScreen(winner);
                return;
            }
            // Update UI
            game.UpdateUI();

        }

        // Play click.
        public async void playClick(object sender, RoutedEventArgs e)
        {
            // Make sure a card is selected
            if (selectedCard == null)
            {
                // Display a message to user, telling them to select a card
                game.StatusText = "Please select a card first";
                return;
            }

            // Make sure it's the player's turn
            if (game.currentState != GameState.PlayerTurn)
            {
                // If not than display invalidturn message to user
                game.InvalidTurn();
                return;
            }

            // Check if the move is a valid suit
            if (!game.realPlayer.SuitMatch(selectedCard, game.pile, game.currentSuit))
            {
                // If not than display invalidturn message to user
                game.InvalidMove();
                return;
            }

            // Clear status bar
            game.StatusText = "";

            // Play the card
            game.realPlayer.PlayCard(selectedCard, game.pile);

            // If card is an Eight, show suit picker before continuing
            if (game.rules.IsCardEight(selectedCard))
            {
                selectedCard = null;

                // Display suit selection screen
                SuitScreen.Visibility = Visibility.Visible;
                DarkBackground.Visibility = Visibility.Visible;
                return; 
            }
            else
            {
                // Clear current suit
                game.SetCurrentSuit("");
            }

            selectedCard = null;

            // Check if player won
            if (game.realPlayer.hand.HandIsEmpty())
            {
                Player winner = game.AnnounceWinner();
                // If theres a winner
                if (winner != null)
                {
                    // Display winner screen
                    ShowWinnerScreen(winner);
                    return;
                }
            }

            // Move to computer turn
            await game.NextTurn();

            // Check if computer won after their turn
            Player computerWinner = game.AnnounceWinner();
            // If computer won
            if (computerWinner != null)
            {
                // Display winner screen
                ShowWinnerScreen(computerWinner);
                return;
            }

            // Update UI
            game.UpdateUI();
        }

        // Suit selection handlers.
        private async void SuitChosen(string suit)
        {
            // Set current suit to the select suit
            game.SetCurrentSuit(suit);

            // Display suit selection screen
            SuitScreen.Visibility = Visibility.Collapsed;
            DarkBackground.Visibility = Visibility.Collapsed;

            // Now continue after Eight was played
            if (game.realPlayer.hand.HandIsEmpty())
            {
                // Check for winner
                Player winner = game.AnnounceWinner();
                // If theres a winner
                if (winner != null)
                {
                    // Display winner screen
                    ShowWinnerScreen(winner);
                    return;
                }
            }

            // Move to computer turn
            await game.NextTurn();

            // Check if computer won after their turn
            Player computerWinner = game.AnnounceWinner();
            // If computer wins
            if (computerWinner != null)
            {
                // Display winner screen
                ShowWinnerScreen(computerWinner);
                return;
            }

            // Update UI
            game.UpdateUI();
        }

        // Click events for each suit in the suit selection screen.
        public void SuitHearts(object sender, RoutedEventArgs e) => SuitChosen("Hearts");
        public void SuitDiamonds(object sender, RoutedEventArgs e) => SuitChosen("Diamonds");
        public void SuitClubs(object sender, RoutedEventArgs e) => SuitChosen("Clubs");
        public void SuitSpades(object sender, RoutedEventArgs e) => SuitChosen("Spades");


        // Show the winner screen.
        private void ShowWinnerScreen(Player winner)
        {
            // Set DarkBackground to visible
            DarkBackground.Visibility = Visibility.Visible;
            // Set WinnerScreen to visible
            WinnerScreen.Visibility = Visibility.Visible;

            // If the player won
            if (winner.Name == game.realPlayer.Name)
            {
                // Display winner text
                WinnerText.Text = "Congratulations!";
                WinnerSubText.Text = "You won the game!";
            }
            // If computer won
            else
            {
                // Display loser text
                WinnerText.Text = "Better luck next time!";
                WinnerSubText.Text = "The computer won the game.";
            }
        }

        // Play Again button on winner screen.
        public void WinnerResetClick(object sender, RoutedEventArgs e)
        {
            // Set WinnerScreen to invisible
            WinnerScreen.Visibility = Visibility.Collapsed;
            // Set DarkBackground to invisible
            DarkBackground.Visibility = Visibility.Collapsed;

            // Reset to a new game
            game.ResetGame();
        }

        // Exit button on winner screen.
        public void WinnerExitClick(object sender, RoutedEventArgs e)
        {
            // Close program
            Close();
        }


        // Settings click (toggle visibility of the settings screen).
        public void settingsClick(object sender, RoutedEventArgs e)
        {
            // If the settings menu is open
            if (SettingsScreen.Visibility == Visibility.Visible)
            {
                // Close the menu
                SettingsScreen.Visibility = Visibility.Collapsed;
                DarkBackground.Visibility = Visibility.Collapsed;
            }
            // If the settings menu is closed
            else
            {
                // Open the menu
                SettingsScreen.Visibility = Visibility.Visible;
                DarkBackground.Visibility = Visibility.Visible;
            }
        }

        // Apply settings click (when user saves settings - apply them live).
        public void ApplySettings()
        {
            // Use BrushConverter to get the colors (get hex code from the tag in the combo box items)
            var BackgroundBrush = (Brush)new BrushConverter().ConvertFromString(settings.BackgroundColor);
            var MenuBrush = (Brush)new BrushConverter().ConvertFromString(settings.MenuColor);

            // Apply color to background
            GameBackground.Background = BackgroundBrush;

            // Apply color to menu
            GameTopMenu.Background = MenuBrush;
            GameBottomMenu.Background = MenuBrush;
            buttonRules.Background = MenuBrush;
            buttonSettings.Background = MenuBrush;
            buttonReset.Background = MenuBrush;
            buttonExit.Background = MenuBrush;
        }
        

        // Save settings click — inside the settings panel.
        public void SaveSettingsClick(object sender, RoutedEventArgs e)
        {
            // Get the selected background color from the selected combo box item
            if (BackgroundColorPicker.SelectedItem is ComboBoxItem selectedBackgroundColor)
                // Store selected color
                settings.BackgroundColor = selectedBackgroundColor.Tag.ToString();

            // Get the selected menu color from the selected combo box item
            if (MenuColorPicker.SelectedItem is ComboBoxItem selectedMenuColor)
                // Store selected color
                settings.MenuColor = selectedMenuColor.Tag.ToString();

            // Save selected settings
            settings.SaveSettings();
            // Apply selected settings live
            ApplySettings();

            // Close SettingsScreen
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
            // Close program
            Close();
        }
    }
}