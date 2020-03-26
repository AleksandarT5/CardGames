using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayPlayerFirstTour : PlayerStrategyFirst
    {
        public override Card PlayerPlayFirst(Player player, Card openTrumpCard, Check check)
        {
            return check.DeterminingTheCardOfThePlayer(player.CardsPlayer);
        }
    }
}
