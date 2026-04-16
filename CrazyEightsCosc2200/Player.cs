using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyEightsCosc2200
{
    public class Player
    {
        public string Name { get; set; }
        public Hand hand { get; set; }

        // Constructor.
        public Player(string name)
        {
            Name = name;
            hand = new Hand();
        }

        // Play a card onto the pile
        public void PlayCard(Card card, Pile pile)
        {
            hand.RemoveCard(card);
            pile.AddLastCard(card);
        }

        // Draw a card from the deck and add to hand
        public void DrawCard(Deck deck)
        {
            if (!deck.DeckIsEmpty())
            {
                Card drawnCard = deck.Draw();
                hand.AddCard(drawnCard);
            }
        }

        // Check if the played card matches the suit of the last played card
        public bool SuitMatch(Card card, Pile pile, string currentSuit = "")
        {
            Card topCard = pile.LastCard();
            // If an Eight was played, match the chosen suit
            if (!string.IsNullOrEmpty(currentSuit))
                return card.suit.ToString() == currentSuit || card.Rank == Rank.Eight;
            // Otherwise normal rules
            return card.Suit == topCard.Suit || card.Rank == topCard.Rank || card.Rank == Rank.Eight;
        }

    }
}
