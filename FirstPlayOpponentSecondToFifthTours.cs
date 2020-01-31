using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayOpponentSecondToFifthTours : StrategyOpponentFirst
    {
        public override Card OpponentPlayFirst(List<Card> opponentCards, Card openTrumpCard)
        {
            Check check = new Check();
            //40
            //66
            //20
            //66
            // проверка за 9 коз
            // проверка ниска карта


            return null;
        }
    }
}
