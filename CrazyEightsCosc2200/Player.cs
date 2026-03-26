using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyEightsCosc2200
{
    internal class Player
    {
        public string Name;
        public List<Card> hand = new List<Card>();

        public Player(string name)
        {
            Name = name;
        }
    }
}
