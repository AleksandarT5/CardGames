using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayPlayerSecondToFifthTour : PlayerStrategyFirst
    {
        public override Card PlayerPlayFirst(Player player, Card openTrumpCard, Check check)
        {
            openTrumpCard = check.CheckPayerHaveNineTrump(player.CardsPlayer, openTrumpCard);

            Card playerCard = check.DeterminingThePlayerCard(player.CardsPlayer, openTrumpCard);

            //playerCard = 
            check.CheckFor40and20(player, openTrumpCard, playerCard);

            return playerCard;
        }
    }
}
