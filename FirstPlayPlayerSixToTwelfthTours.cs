using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayPlayerSixToTwelfthTours : PlayerStrategyFirst
    {
        public override Card PlayerPlayFirst(Player player, Card openTrumpCard, Check check)
        {
            Card playerCard = check.DeterminingThePlayerCard(player.CardsPlayer, openTrumpCard);

            playerCard = check.CheckFor40and20(player, openTrumpCard, playerCard);

            return playerCard;
        }
    }
}
