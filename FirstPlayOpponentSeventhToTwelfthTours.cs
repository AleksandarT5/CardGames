using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayOpponentSeventhToTwelfthTours : StrategyOpponentFirst
    {
        public override Card OpponentPlayFirst(Player opponent, Player player, Card openTrumpCard, 
            bool havePlayerSixtySixPonts, Check check, DeckOfCards deckOfCards)
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

            //card = check.CheckForATrump(opponent, openTrumpCard);
            //if (card != null)
            //{
            //    return card;
            //}

            //card = check.CheckFor10Trump(opponent.CardsPlayer, openTrumpCard, deckOfCards);
            //if (card != null)
            //{
            //    return card;
            //}

            card = check.CheckTrump(opponent.CardsPlayer, openTrumpCard, deckOfCards);
            if (card != null)
            {
                return card;
            }


            //Проверка за силна не Коз


            card = check.CheckForCard(opponent.CardsPlayer, openTrumpCard);
            if (card != null)
            {
                return card;
            }
            // Проверка за 40 +
            // Проверка за 20 +
            // Проверка за коз +            
            // Проверка за силна не Коз
            // Проверка за слаба не Коз


            return card;
        }
    }
}
