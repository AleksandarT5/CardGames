using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayPlayerSecondToFifthTour : PlayerStrategyFirst
    {
        public override Card PlayerPlayFirst(Player player, Card openTrumpCard, Check check)
        {
            check.CheckPayerHaveNineTrump(player.CardsPlayer, openTrumpCard);

            Card playerCard = check.DeterminingThePlayerCard(player.CardsPlayer);

            player.Points  += check.CheckFor40and20(player, openTrumpCard, playerCard);

            return playerCard;
        }
    }
}
