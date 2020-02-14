using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayOpponentSeventhToTwelfthTours : OpponentStrategyFirst
    {
        public override Card OpponentPlayFirst(Player opponent, Player player, Card openTrumpCard, 
            Check check, DeckOfCards deckOfCards)
        {
            Card card = null;
            card = check.CheckForForty(opponent, openTrumpCard);
            if (opponent.Points >= 66)
            {
                return null;
            }

            if (card != null)
            {
                return card;
            }

            card = check.CheckForForty(opponent, openTrumpCard);
            if (opponent.Points >= 66)
            {
                return null;
            }

            if (card != null)
            {
                return card;
            }
            
            card = check.CheckForStrongOrWeakCard("Trump", opponent.CardsPlayer, openTrumpCard, deckOfCards);
            if (card != null)
            {
                return card;
            }

            card = check.CheckForWeakCard(opponent.CardsPlayer, openTrumpCard);
            if (card != null)
            {
                return card;
            }
            // Проверка за 40 +
            // Проверка за 20 +
            // Проверка за коз +            
            // Проверка за силна не Коз
            // Проверка за слаба не Коз - CheckForCard +


            return card;
        }
    }
}
