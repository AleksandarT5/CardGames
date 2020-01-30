using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    abstract class StrategyOpponentFirst
    {
        //Check check = new Check();
        //Check тук

        public abstract Card OpponentPlayFirst(List<Card> opponentCards, Card openTrumpCard);

    }
}
