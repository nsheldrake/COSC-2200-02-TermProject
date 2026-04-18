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
        // Observable so the UI updates automatically when cards change.
        public ObservableCollection<Card> deck { get; private set; }

        // Constructor.
        public Deck()
        {
            deck = new ObservableCollection<Card>();
            InitializeDeck();
        }

        // To check deck count.
        public int TotalCount => deck.Count;

        // Add a card to the deck with each rank, for each suit.
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

        // Shuffle all cards in the deck randomly when it runs out of cards.
        public void Shuffle()
        {
            // Fisher Yates Shuffle
            // Reference: https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
            Random rng = new Random();
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card temp = deck[k];
                deck[k] = deck[n];
                deck[n] = temp;
            }
        }

        // When deck is empty automatically shuffle all cards except top card in pile.
        public void ReshuffleFromPile(Pile pile)
        {
            // Keep the top card, reshuffle the rest back into the deck
            Card topCard = pile.TopCard;

            // Take all pile cards except the top one back into the deck
            var reshuffleCards = pile.pile.Where(card => card != topCard).ToList();
            foreach (Card card in reshuffleCards)
            {
                // Set the cards facedown, add them to the deck, and remove from the pile
                card.FaceDown();
                deck.Add(card);
                pile.pile.Remove(card);
            }

            // Reshuffle
            Shuffle();
        }

        // Take a card from the deck.
        // Used when a player clicks the draw button.
        public Card Draw()
        {
            if (DeckIsEmpty())
                throw new InvalidOperationException("Cannot draw, deck is empty.");

            // Find the top card
            Card topCard = deck[0];
            // Remove it from the deck
            deck.RemoveAt(0);
            // Give it to the players hand
            return topCard;
        }

        // Check if the deck has no cards left.
        public bool DeckIsEmpty()
        {
            // If the amount of cards in the deck is < 1, return true
            if (deck.Count < 1)
            {
                return true;
            }
            // Else return false
            return false;
        }

        // All the cards in the deck will be face down so you dont see the face until you draw a card.
        public void FaceDown()
        {
            foreach (Card card in deck)
            {
                card.FaceDown();
            }
        }
    }
}