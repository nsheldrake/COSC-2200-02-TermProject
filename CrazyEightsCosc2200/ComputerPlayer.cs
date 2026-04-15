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
        public ComputerPlayer(string name) : base(name)
        {
        }

        // Play a valid card onto the pile automatically
        public void PlayCard(Pile pile, Deck deck, string currentSuit = "")
        {
            Card cardToPlay = RandomPlay(pile);

            if (cardToPlay != null)
            {
                hand.RemoveCard(cardToPlay);
                pile.AddLastCard(cardToPlay);
            }
            else
            {
                // no valid card = draw instead
                DrawCard(deck); 
            }
        }

        // Pick a random valid card from hand
        private Card RandomPlay(Pile pile, string currentSuit = "")
        {
            List<Card> validCards = hand.hand
                .Where(card => SuitMatch(card, pile, currentSuit))
                .ToList();

            if (validCards.Count == 0)
                return null;

            Random rng = new Random();
            return validCards[rng.Next(validCards.Count)];
        }



    }
}
