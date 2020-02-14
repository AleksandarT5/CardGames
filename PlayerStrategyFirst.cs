using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    abstract class PlayerStrategyFirst
    {
        public abstract Card PlayerPlayFirst(Player player, Card openTrumpCard, Check check);
    }
}
