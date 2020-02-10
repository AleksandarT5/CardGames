using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    abstract class StrategyOpponentFirst
    {        
        public abstract Card OpponentPlayFirst(Player opponent, Player player, Card openTrumpCard, 
            bool havePlayerSixtySixPonts, Check check, DeckOfCards deckOfCards);
    }
}
