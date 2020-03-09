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
            //openTrumpCard = check.CheckPayerHaveNineTrump(opponent.CardsPlayer, openTrumpCard);
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

            card = check.CheckForWeakCard(opponent.CardsPlayer, openTrumpCard);
            return card;
        }
    }
}
