using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayOpponentSecondToFifthTours : StrategyOpponentFirst
    {
        public override Card OpponentPlayFirst(Player opponent, Player player, Card openTrumpCard,
            bool havePlayerSixtySixPonts, Check check, DeckOfCards deckOfCards)
        {
            Card card = null;
            //openTrumpCard = check.CheckPayerHaveNineTrump(opponent.CardsPlayer, openTrumpCard);
            card = check.CheckForFourty(opponent, openTrumpCard, havePlayerSixtySixPonts);
            if (havePlayerSixtySixPonts == true)
            {
                return null;
            }

            if (card != null)
            {
                return card;
            }
            //if (havePlayerSixtySixPonts == true || card != null)
            //{
            //    return card;
            //}

            card = check.CheckForTwenty(opponent, openTrumpCard, havePlayerSixtySixPonts);
            if (havePlayerSixtySixPonts == true)
            {
                return null;
            }

            if (card != null)
            {
                return card;
            }

            card = check.CheckForCard(opponent.CardsPlayer, openTrumpCard);
            return card;
        }
    }
}
