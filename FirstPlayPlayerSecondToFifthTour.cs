using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayPlayerSecondToFifthTour : PlayerStrategyFirst
    {
        public override Card PlayerPlayFirst(Player player, Card openTrumpCard, Check check)
        {
            Card playerCard = check.DeterminingTheCardOfThePlayer(player.CardsPlayer);

            player.Points  += check.CheckFor40And20(player, openTrumpCard, playerCard);

            return playerCard;
        }
    }
}
