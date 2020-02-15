using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayOpponentSeventhToTwelfthTour : OpponentStrategyFirst
    {
        public override Card OpponentPlayFirst(Player opponent, Player player, Card openTrumpCard, 
            Check check, DeckOfCards deckOfCards)
        {
            Card card = null;
            // Проверка за 40 +
            // Проверка за 20 +
            // Проверка за коз +            
            // Проверка за силна не Коз
            // Проверка за слаба не Коз - CheckForCard +


            //OK
            card = check.CheckForForty(opponent, openTrumpCard);
            if (opponent.Points >= 66)
            {
                return null;
            }

            if (card != null)
            {
                return card;
            }
            //OK
            card = check.CheckForForty(opponent, openTrumpCard);
            if (opponent.Points >= 66)
            {
                return null;
            }

            if (card != null)
            {
                return card;
            }
            // Проверка за коз, ако е най-силния останал -- за тест
            card = check.CheckForStrongTrump(opponent, openTrumpCard, deckOfCards.PlayedCards);
            if (card != null)
            {
                return card;
            }
            // Проверка за некоз, ако е най-силния останал - за тест
            card = check.StrongNoTrump(opponent, openTrumpCard, deckOfCards.PlayedCards);
            if (card != null)
            {
                return card;
            }
            // Проверка за некоз, най-слабата
            card = check.CheckForWeakCard(opponent.CardsPlayer, openTrumpCard);
            if (card != null)
            {
                return card;
            }        

            return card;
        }
    }
}
