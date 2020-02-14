using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    abstract class OpponentStrategyAnswer
    {
        public abstract Card AnswerOnOpponent(Player opponent, Player player, Card playerCard, 
            Card openTrumpCard, Check check);
    }
}
