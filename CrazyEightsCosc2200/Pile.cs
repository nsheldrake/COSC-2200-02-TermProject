using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyEightsCosc2200
{
    public class Pile
    {
        // Observable so the UI updates automatically when cards change
        public ObservableCollection<Card> pile { get; private set; }

        // Constructor
        public Pile()
        {
            pile = new ObservableCollection<Card>();
        }

        // Returns the last card of the pile (most recently played)
        // Used to check the cards rank and suit to check if the next move is valid
        public Card LastCard()
        {
            return pile.Last();
        }

        // Adds a card onto the top of the pile
        // Used when a player plays a card
        public void AddLastCard(Card card)
        {
            pile.Add(card);
            card.FaceUp();
        }

        // All the cards in the pile are face up so you know what suit and rank to follow
        public void FaceUp()
        {
            foreach (Card card in pile)
            {
                card.FaceUp();
            }
        }
    }
}
