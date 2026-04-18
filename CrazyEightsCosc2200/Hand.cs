using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyEightsCosc2200
{
    public class Hand
    {
        // Observable so the UI updates automatically when cards change.
        public ObservableCollection<Card> hand { get; private set; }

        // Constructor.
        public Hand()
        {
            hand = new ObservableCollection<Card>();
        }

        // Add a card to the hand.
        // Used when a player draws a card.
        public void AddCard(Card card)
        {
            hand.Add(card);
        }

        // Remove a card from the hand.
        // Used when a player plays a card.
        public void RemoveCard(Card card)
        {
            hand.Remove(card);
        }

        // Check if hand has no cards left.
        public bool HandIsEmpty()
        {
            // If the amount of cards in the hand is < 1, return true
            if (hand.Count < 1)
            {
                return true;
            }
            // Else return false
            return false;
        }

        // All the cards in your hand will be face up so you can see them.
        public void FaceUp()
        {
            foreach (Card card in hand)
            {
                card.FaceUp();
            }
        }
    }
}
