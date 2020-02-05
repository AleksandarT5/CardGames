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
            Card card = null;
            check.CheckPayerHaveNineTrump(opponent.CardsPlayer, openTrumpCard);
            card = check.CheckForFourty(opponent, openTrumpCard, player, havePlayerSixtySixPonts);
            if (havePlayerSixtySixPonts == true || card != null)
            {
                return card;
            }

            card = check.CheckForTwenty(opponent, openTrumpCard, player, havePlayerSixtySixPonts);
            if (havePlayerSixtySixPonts == true || card != null)
            {
                return card;
            }
            
            card = check.CheckForCard(opponent.CardsPlayer, openTrumpCard);

            return card;
        }
    }
}
