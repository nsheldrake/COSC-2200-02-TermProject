using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CrazyEightsCosc2200
{
    public class Deck
    {
        // Observable so the UI updates automatically when cards change
        public ObservableCollection<Card> deck { get; private set; }

        // Constructor
        public Deck()
        {
            deck = new ObservableCollection<Card>();
            InitializeDeck();
        }

        private void InitializeDeck()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    deck.Add(new Card(suit, rank));
                }
            }
        }

        // Replace all cards in the deck randomly when it runs out of cards
        public void Shuffle()
        {
            // If DeckIsEmpty() = true

            // automatically shuffle the deck randomly

        }

        // Take a card from the deck
        // Used when a player clicks the draw button
        public Draw()
        {
        }

        // Check if the deck has no cards left
        public bool DeckIsEmpty()
        {
            // If the amount of cards in the deck is < 1, return true

            // Else return false
        }

        // All the cards in the deck will be face down so you dont see the face until you draw a card
        public void FaceDown()
        {
            foreach (Card card in deck)
            {
                card.FaceDown();
            }
        }
    }
}