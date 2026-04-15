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
    internal class GameLogic : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public Deck deck;
        public Pile pile;
        public Player realPlayer;
        private ComputerPlayer computerPlayer;
        public GameState currentState;
        public string currentSuit = "";

        public GameRules rules = new GameRules();

        public ObservableCollection<Card> RealPlayerHand => realPlayer.hand.hand;
        public ObservableCollection<Card> ComputerHand => computerPlayer.hand.hand;

        public Pile Pile => pile;
        public int DeckCount => deck.TotalCount;

        public string OpponentCardCountText => $"Cards: {computerPlayer.hand.hand.Count}";
        public string PlayerCardCountText => $"Your Cards: {realPlayer.hand.hand.Count}";
        public string CurrentStateText => $"State: {currentState}";
        public string DeckCountText => $"Deck: {deck.TotalCount}";


        // Constructor
        public GameLogic()
        {
            deck = new Deck();
            pile = new Pile();
            realPlayer = new Player("Player");
            computerPlayer = new ComputerPlayer("AI");
            currentState = GameState.StartGame;

            StartGame();
        }

        public void StartGame()
        {
            deck.Shuffle();
            deck.FaceDown();
            DealCards();
            currentState = GameState.PlayerTurn;
            UpdateUI();
            OnPropertyChanged(nameof(Pile));
        }

        // Reset everything back to a fresh game
        public void ResetGame()
        {
            deck = new Deck();
            pile = new Pile();
            realPlayer.hand = new Hand();
            computerPlayer.hand = new Hand();

            StartGame();

            OnPropertyChanged(nameof(RealPlayerHand));
            OnPropertyChanged(nameof(ComputerHand));
            OnPropertyChanged(nameof(Pile));
            OnPropertyChanged(nameof(DeckCountText));
        }

        // Deal 5 cards to each player, flip one card to start the pile
        public void DealCards()
        {
            for (int i = 0; i < 5; i++)
            {
                realPlayer.hand.AddCard(deck.Draw());
                computerPlayer.hand.AddCard(deck.Draw());
            }

            realPlayer.hand.FaceUp();

            // Flip one card to start the discard pile
            pile.AddLastCard(deck.Draw());
        }

        // Set current suit to selected suit.
        public void SetCurrentSuit(string suit)
        {
            currentSuit = suit;
        }

        // Move to the next turn and some computerPlayer logic
        public void NextTurn()
        {
            if (currentState == GameState.PlayerTurn)
            {
                currentState = GameState.ComputerTurn;

                // Reshuffle if deck is empty before computer plays
                if (deck.DeckIsEmpty() && pile.pile.Count > 1)
                    deck.ReshuffleFromPile(pile);

                computerPlayer.PlayCard(pile, deck, currentSuit);

                // Clear suit after computer plays
                currentSuit = "";

                if (computerPlayer.hand.HandIsEmpty())
                {
                    AnnounceWinner();
                    return;
                }

                currentState = GameState.PlayerTurn;
            }

            UpdateUI();
        }

        // Announce the winner
        public void AnnounceWinner()
        {
            if (realPlayer.hand.HandIsEmpty())
                System.Windows.MessageBox.Show("You win!", "Game Over", System.Windows.MessageBoxButton.OK);
            else if (computerPlayer.hand.HandIsEmpty())
                System.Windows.MessageBox.Show("Computer wins!", "Game Over", System.Windows.MessageBoxButton.OK);

            ResetGame();
        }

        // Called when a move is not valid
        public void InvalidMove()
        {
            System.Windows.MessageBox.Show("Invalid move! Card must match suit or rank.", "Invalid Move", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
        }

        // Called when a turn is not valid
        public void InvalidTurn()
        {
            System.Windows.MessageBox.Show("It's not your turn!", "Invalid Turn", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
        }

        public void UpdateUI()
        {
            OnPropertyChanged(nameof(OpponentCardCountText));
            OnPropertyChanged(nameof(PlayerCardCountText));
            OnPropertyChanged(nameof(CurrentStateText));
            OnPropertyChanged(nameof(RealPlayerHand));
            OnPropertyChanged(nameof(ComputerHand));
            OnPropertyChanged(nameof(Pile));
            OnPropertyChanged(nameof(DeckCountText));
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
