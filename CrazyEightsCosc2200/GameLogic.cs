using System;
using System.Collections.Generic;
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

        public string OpponentCardCountText
        {
            get => $"Cards: {computerPlayer.hand.hand.Count}";
        }

        public string PlayerCardCountText
        {
            get => $"Your Cards: {realPlayer.hand.hand.Count}";
        }

        public string CurrentStateText
        {
            get => $"State: {currentState}";
        }

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
        }

        // Reset everything back to a fresh game
        public void ResetGame()
        {
            deck = new Deck();
            pile = new Pile();
            realPlayer = new Player("Player");
            computerPlayer = new ComputerPlayer("AI");
            StartGame();
        }

        // Deal 5 cards to each player, flip one card to start the pile
        public void DealCards()
        {
            for (int i = 0; i < 5; i++)
            {
                realPlayer.hand.AddCard(deck.Draw());
                computerPlayer.hand.AddCard(deck.Draw());
            }

            // Flip one card to start the discard pile
            pile.AddLastCard(deck.Draw());
        }

        // Move to the next turn
        public void NextTurn()
        {
            if (currentState == GameState.PlayerTurn)
            {
                currentState = GameState.ComputerTurn;
                computerPlayer.PlayCard(pile, deck);
            
                // Check if computer won
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
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
