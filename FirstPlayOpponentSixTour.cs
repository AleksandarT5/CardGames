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
            card = check.CheckForForty(opponent, openTrumpCard);
            if (opponent.Points >= 66 || card != null)
            {
                return card;
            }

            card = check.CheckForForty(opponent, openTrumpCard);
            if (opponent.Points >= 66 || card != null)
            {
                return card;
            }

            card = check.CheckForTheWeakestCard(opponent.CardsPlayer, openTrumpCard);
            return card;
        }
    }
}
