using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyEightsCosc2200
{
    public class Card
    {
        // Properties
        public Suit suit;
        public Rank rank;
        public bool isFaceUp;
        public string FaceImage;
        public string BackImage;

        // Public getters for properties
        public Suit Suit { get { return suit; } }
        public Rank Rank { get { return rank; } }

        // Constructor
        public Card(Suit suit, Rank rank)
        {
            this.suit = suit;
            this.rank = rank;
            this.isFaceUp = false; 

            // Find each cards image via the suit and rank
            string rankName = rank.ToString().ToLower();
            string suitName = suit.ToString().ToLower();
            FaceImage = $"images/{suitName}_of_{rankName}.png";
            BackImage = "images/card_back.png";
        }

        // Flips the card face up
        public void FaceUp()
        {
            isFaceUp = true;
            // Display the cards face
        }

        // Flips the card face down
        public void FaceDown()
        {
            isFaceUp = false;
            // Display the cards back
        }
    }
}
