using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CrazyEightsCosc2200
{
    internal class GameLogic : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ComputerPlayer computerPlayer = new ComputerPlayer("AI");

        public string OpponentCardCountText
        {
            get => $"Cards: {computerPlayer.hand.Count}";
        }

        public void UpdateUI()
        {
            OnPropertyChanged(nameof(OpponentCardCountText));
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
