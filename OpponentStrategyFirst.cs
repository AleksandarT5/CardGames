using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    abstract class OpponentStrategyFirst
    {        
        public abstract Card OpponentPlayFirst(Player opponent, Player player, Card openTrumpCard, 
            Check check, DeckOfCards deckOfCards);
    }
}
