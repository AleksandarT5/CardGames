using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    interface IBoard
    {
        //Card OpenTrumpCard { get; set; }

        int Turns { get; set; }

        void HandingOutCards(Action<Player, Player, int> handlingHands, Player opponent, Player player, int count);
    }
}
