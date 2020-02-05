using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayOpponentSixTour : StrategyOpponentFirst
    {
        public override Card OpponentPlayFirst(Player opponent, Player player, Card openTrumpCard, 
            bool havePlayerSixtySixPonts, Check check)
        {
            // начало на проверката
            check.CheckForFourty(opponent, openTrumpCard, player, havePlayerSixtySixPonts);
            check.CheckForTwenty(opponent, openTrumpCard, player, havePlayerSixtySixPonts);
            // край
            check.CheckForCard(opponent.CardsPlayer, openTrumpCard);

            return null;
        }
    }
}
