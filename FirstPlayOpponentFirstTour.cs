using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayOpponentFirstTour : StrategyOpponentFirst
    {
        public override Card OpponentPlayFirst(Player opponent, Player player, Card openTrumpCard,
            Check check, DeckOfCards deckOfCards)
        {
            Card opponentCardForPlay = check.CheckForWeakCard(opponent.CardsPlayer, openTrumpCard);
            return opponentCardForPlay;
        }
    }
}
