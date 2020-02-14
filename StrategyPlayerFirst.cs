using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    abstract class StrategyPlayerFirst
    {
        public abstract Card PlayerPlayFirst(Player player, Player opponent, Card openTrumpCard,
            Check check, DeckOfCards deckOfCards);
    }
}
