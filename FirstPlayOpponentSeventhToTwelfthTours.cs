using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayOpponentSeventhToTwelfthTours : StrategyOpponentFirst
    {
        public override Card OpponentPlayFirst(Player opponent, Player player, Card openTrumpCard, bool havePlayerSixtySixPonts, Check check)
        {
            Card card = null;
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

            card = check.CheckForATrump(opponent, openTrumpCard);
            if (card != null)
            {
                return card;
            }
            // Проверка за "10" от коза, ако "A" от коза е минала:
            //card = check.CheckFor10Trump(opponent.CardsPlayer, openTrumpCard, deckOfCards);

            card = check.CheckForCard(opponent.CardsPlayer, openTrumpCard);
            if (card != null)
            {
                return card;
            }


            return card;
        }
    }
}
