using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyEightsCosc2200
{
    public class Pile : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // Observable so the UI updates automatically when cards change
        public ObservableCollection<Card> pile { get; private set; }

        // Constructor
        public Pile()
        {
            pile = new ObservableCollection<Card>();
            pile.CollectionChanged += (s, e) =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TopCard)));
        }

        // Returns the last card of the pile (most recently played)
        // Used to check the cards rank and suit to check if the next move is valid
        public Card? TopCard
        {
            get
            {
                return pile.Count > 0 ? pile[^1] : null;
            }
        }

        // Get last played card for move validation.
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

        // Empty pile for resetting game
        public void EmptyPile()
        {
            pile.Clear();
        }
    }
}
