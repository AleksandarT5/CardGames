using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayOpponentSecondToFifthTour : OpponentStrategyFirst
    {
        public override Card OpponentPlayFirst(Player opponent, Player player, Card openTrumpCard,
            Check check, DeckOfCards deckOfCards)
        {
            Card card = null;
            check.CheckPayerHaveNineTrump(opponent.CardsPlayer, openTrumpCard);
            card = check.CheckingForForty(opponent, openTrumpCard);
            if (card != null)
            {
                return card;
            }

            card = check.CheckingForTwenty(opponent, openTrumpCard);
            if (card != null)
            {
                return card;
            }

            card = check.CheckForWeakCard(opponent.CardsPlayer, openTrumpCard);
            return card;
        }
    }
}
