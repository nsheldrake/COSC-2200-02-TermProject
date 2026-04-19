using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CrazyEightsCosc2200
{
    // INotifyPropertyChanged so changes update live in UI.
    internal class GameLogic : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // Properties.
        public Deck deck;
        public Pile pile;
        public Player realPlayer;
        private ComputerPlayer computerPlayer;
        public GameState currentState;
        public string currentSuit = "";
        public string statusText = "";

        public string StatusText
        {
            get => statusText;
            set
            {
                statusText = value;
                OnPropertyChanged(nameof(StatusText));
            }
        }

        // Get GameRules.
        public GameRules rules = new GameRules();

        // Find users hand for UI binding.
        public ObservableCollection<Card> RealPlayerHand => realPlayer.hand.hand;
        // Find computers hand for UI binding.
        public ObservableCollection<Card> ComputerHand => computerPlayer.hand.hand;

        // Find the new / current pile object.
        public Pile Pile => pile;
        // Find the number of cards remaining in the deck to display in UI.
        public int DeckCount => deck.TotalCount;

        // Find the amount of cards in the computers hand.
        public string OpponentCardCountText => $"Cards: {computerPlayer.hand.hand.Count}";
        // Find the amount of cards in the users hand.
        public string PlayerCardCountText => $"Your Cards: {realPlayer.hand.hand.Count}";
        // Find the current state.
        public string CurrentStateText => $"State: {currentState}";
        // Display the amount of cards left in the deck in the UI.
        public string DeckCountText => $"Deck: {deck.TotalCount}";

        public string TurnText => currentState == GameState.PlayerTurn
            ? "Your Turn" : "Computer Turn";

        // Constructor.
        public GameLogic()
        {
            deck = new Deck();
            pile = new Pile();
            realPlayer = new Player("Player");
            computerPlayer = new ComputerPlayer("AI");
            currentState = GameState.StartGame;
            // When program starts, start a new game
            StartGame();
        }

        // Used to start new games.
        public void StartGame()
        {
            deck.Shuffle();
            deck.FaceDown();
            DealCards();
            currentState = GameState.PlayerTurn;
            UpdateUI();
            OnPropertyChanged(nameof(Pile));
        }

        // Reset everything back to a fresh game.
        public void ResetGame()
        {
            // Create new deck, pile, and hands
            deck = new Deck();
            pile = new Pile();
            realPlayer.hand = new Hand();
            computerPlayer.hand = new Hand();

            // Start new game
            StartGame();

            // Update UI components
            OnPropertyChanged(nameof(RealPlayerHand));
            OnPropertyChanged(nameof(ComputerHand));
            OnPropertyChanged(nameof(Pile));
            OnPropertyChanged(nameof(DeckCountText));
        }

        // Deal 5 cards to each player, flip one card to start the pile.
        public void DealCards()
        {
            // Take 5 cards from deck
            for (int i = 0; i < 5; i++)
            {
                // Add 5 cards to players hand
                realPlayer.hand.AddCard(deck.Draw());
                // Add 5 cards to computers hand
                computerPlayer.hand.AddCard(deck.Draw());
            }

            // Set users hand face up so they can see their cards
            realPlayer.hand.FaceUp();

            // Flip one card to start the discard pile
            pile.AddLastCard(deck.Draw());
        }

        // Set current suit to selected suit.
        public void SetCurrentSuit(string suit)
        {
            currentSuit = suit;
        }

        // Move to the next turn and some computerPlayer logic.
        // Async so it uses the delay.
        public async Task NextTurn()
        {
            // If currentState = PlayerTurn
            if (currentState == GameState.PlayerTurn)
            {
                // Switch to ComputerTurn
                currentState = GameState.ComputerTurn;
                // Clear status bar
                StatusText = "";
                // Update the ui here so status text can render before the delay
                UpdateUI();

                // Reshuffle if deck is empty before computer plays
                if (deck.DeckIsEmpty() && pile.pile.Count > 1)
                    // Display Shuffling... to status bar to show that the deck is being shuffled
                    StatusText = "Shuffling...";
                    // Update UI to show this message
                    UpdateUI();
                    // Display message for 1 second
                    await Task.Delay(1000);
                    // Reshuffle
                    deck.ReshuffleFromPile(pile);
                    // Set status bar back to default
                    StatusText = "";
                    // Update UI to show default again
                    UpdateUI();

                // Make computer play a card
                // Use await for delay
                await computerPlayer.PlayCard(pile, deck, currentSuit);

                // Clear suit after computer plays
                currentSuit = "";

                // Set state back to PlayerTurn
                currentState = GameState.PlayerTurn;
            }

            // Update UI
            UpdateUI();
        }

        // Announce the winner.
        public Player AnnounceWinner()
        {
            // If players hand is empty
            if (realPlayer.hand.HandIsEmpty())
                // Return player
                return realPlayer;
            // If computers hand is empty
            else if (computerPlayer.hand.HandIsEmpty())
                // Return computer
                return computerPlayer;

            // Else return null
            return null;
        }

        // Called when a move is not valid.
        public void InvalidMove()
        {
            StatusText = "Invalid move! Card must match suit or rank.";
        }

        // Called when a turn is not valid.
        public void InvalidTurn()
        {
            StatusText = "It's not your turn!";
        }

        // Update UI components live.
        public void UpdateUI()
        {
            OnPropertyChanged(nameof(OpponentCardCountText));
            OnPropertyChanged(nameof(PlayerCardCountText));
            OnPropertyChanged(nameof(CurrentStateText));
            OnPropertyChanged(nameof(RealPlayerHand));
            OnPropertyChanged(nameof(ComputerHand));
            OnPropertyChanged(nameof(Pile));
            OnPropertyChanged(nameof(DeckCountText));
            OnPropertyChanged(nameof(TurnText));
            OnPropertyChanged(nameof(StatusText));
        }

        // Sends the OnPropertyChanged event to notify UI elements when a property has changed.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
