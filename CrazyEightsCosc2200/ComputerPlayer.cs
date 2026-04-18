using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CrazyEightsCosc2200
{
    public class ComputerPlayer : Player
    {
        // Constructor.
        // Inherits from Player.
        public ComputerPlayer(string name) : base(name)
        {
        }

        // Play a valid card onto the pile automatically.
        public void PlayCard(Pile pile, Deck deck, string currentSuit = "")
        {
            // Use RandomPlay so ComputerPlayers moves are more random and similar to "AI"
            Card cardToPlay = RandomPlay(pile);

            if (cardToPlay != null)
            {
                // Remove card from hand
                hand.RemoveCard(cardToPlay);
                // Add card to pile
                pile.AddLastCard(cardToPlay);
            }
            else
            {
                // no valid card = draw instead
                DrawCard(deck); 
            }
        }

        // Pick a random valid card from hand.
        private Card RandomPlay(Pile pile, string currentSuit = "")
        {
            // Find cards that are considered "valid moves" by gamelogic
            List<Card> validCards = hand.hand
                .Where(card => SuitMatch(card, pile, currentSuit))
                .ToList();

            // If theres no valid cards then return null
            if (validCards.Count == 0)
                return null;

            // Return a random valid card using Random()
            Random rng = new Random();
            return validCards[rng.Next(validCards.Count)];
        }



    }
}
