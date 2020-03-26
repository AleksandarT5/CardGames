using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayOpponentFirstTour : OpponentStrategyFirst
    {
        public override Card OpponentPlayFirst(Player opponent, Player player, Card openTrumpCard,
            Check check, DeckOfCards deckOfCards)
        {
            Card opponentCardForPlay = check.CheckForTheWeakestCard(opponent.CardsPlayer, openTrumpCard);
            return opponentCardForPlay;
        }
    }
}
