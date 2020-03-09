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
            card = check.CheckForForty(opponent, openTrumpCard);
            if (card != null)
            {
                return card;
            }

            card = check.CheckForTwenty(opponent, openTrumpCard);
            if (card != null)
            {
                return card;
            }
            // Проверка за коз, ако е най-силния останал -- за тест
            card = check.CheckForStrongCardFromType(opponent, openTrumpCard.Type, deckOfCards.PlayedCards);
            if (card != null)
            {
                return card;
            }
            // Проверка за некоз, ако е най-силния останал - за тест
            card = check.CheckForStrongNoTrumpCard(opponent, openTrumpCard, deckOfCards.PlayedCards);
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

            return check.CheckForWeakTrump(opponent.CardsPlayer, openTrumpCard);
        }
    }
}
