using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    abstract class StrategyOpponentFirst
    {        
        Check check = new Check();
        public abstract Card OpponentPlayFirst(Player opponent, Player player, Card openTrumpCard, 
            bool havePlayerSixtySixPonts, Check check);
    }
}
