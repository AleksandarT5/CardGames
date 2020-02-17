using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayOpponentSixTour : OpponentStrategyFirst
    {
        public override Card OpponentPlayFirst(Player opponent, Player player, Card openTrumpCard, 
            Check check, DeckOfCards deckOfCards)
        {
            Card card = null;
            card = check.CheckingForForty(opponent, openTrumpCard);
            if (opponent.Points >= 66 || card != null)
            {
                return card;
            }

            card = check.CheckingForForty(opponent, openTrumpCard);
            if (opponent.Points >= 66 || card != null)
            {
                return card;
            }

            card = check.CheckForWeakCard(opponent.CardsPlayer, openTrumpCard);
            return card;
        }
    }
}
