using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class OpponentStrategyWhenGameFirst
    {
        private StrategyOpponentFirst strategyOpponentFirst;

        public OpponentStrategyWhenGameFirst(StrategyOpponentFirst strategyOpponentFirst)
        {
            this.strategyOpponentFirst = strategyOpponentFirst;
        }

        public Card PlayCard(Player opponent, Player player, Card openTrumpCard,
            bool havePlayerSixtySixPonts, Check check, DeckOfCards deckOfCards)
        {
            return this.strategyOpponentFirst.OpponentPlayFirst(opponent, player, openTrumpCard,
            havePlayerSixtySixPonts, check, deckOfCards);
        }
    }
}
