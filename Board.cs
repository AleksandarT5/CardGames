using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    internal class Board : IBoard
    {
        public Board(int turns, bool thereAreCardsInTheBase)
        {
            this.Turns = 1;
            this.ThereAreCardsInTheBase = true;
        }

        public int Turns { get; set; }

        public bool ThereAreCardsInTheBase { get; set; }
        
        public void HandingOutCards(Action<Player, Player, int> handlingHands, Player opponent, Player player, int count)
        {
            handlingHands.Invoke(opponent, player, count);
        }
    }
}
