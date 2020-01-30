using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayOpponentFirstTour : StrategyOpponentFirst
    {
        public override Card OpponentPlayFirst(List<Card> opponentCards, Card openTrumpCard)
        {
            Check check = new Check();
            Card opponentCardForPlay = check.CheckForCard(opponentCards, openTrumpCard);
            return opponentCardForPlay;
        }
    }
}
