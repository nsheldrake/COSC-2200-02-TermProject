using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyEightsCosc2200
{
    internal class GameRules
    {
        // Check if a move is valid
        public bool IsMoveValid(Card playedCard, Card topCard, string currentSuit)
        {
            // Rule 1: same suit
            if (playedCard.suit == topCard.suit)
                return true;

            // Rule 2: same rank
            if (playedCard.rank == topCard.rank)
                return true;

            // Rule 3: match current suit (after 8)
            if (playedCard.suit.ToString() == currentSuit)
                return true;

            // Rule 4: 8 can always be played
            if (IsCardEight(playedCard))
                return true;

            return false;
        }

        // Check if card is 8
        public bool IsCardEight(Card card)
        {
            return card.rank.ToString() == "Eight";
        }

        // Change suit when 8 is played
        public string ChangeSuit(Card card)
        {
            if (IsCardEight(card))
            {
                // simple version: random suit
                string[] suits = { "Hearts", "Spades", "Clubs", "Diamonds" };
                Random rand = new Random();
                return suits[rand.Next(suits.Length)];
            }

            return card.suit.ToString();
        }

        // Check if player wins
        public bool IsWinner(Player player)
        {
            return player.hand.Count == 0;
        }
    }
}
