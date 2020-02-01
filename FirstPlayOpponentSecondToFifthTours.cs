using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayOpponentSecondToFifthTours : StrategyOpponentFirst
    {
        public override Card OpponentPlayFirst(Player opponent, Player player, Card openTrumpCard, 
            bool havePlayerSixtySixPonts, Check check)
        {
            check.CheckPayerHaveNineTrump(opponent.CardsPlayer, openTrumpCard);
            check.CheckForFourty(opponent, openTrumpCard, player, havePlayerSixtySixPonts);
            check.CheckForTwenty(opponent, openTrumpCard, player, havePlayerSixtySixPonts);
            check.CheckForCard(opponent.CardsPlayer, openTrumpCard);
            // проверка за приключване на програмата, когато card != null 

            return null;
        }
    }
}
