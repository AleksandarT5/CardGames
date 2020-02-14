using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class OpponentStrategyWhenGameFirst
    {
        private StrategyOpponentFirst strategyOpponentFirst;
        
        private int turns;

        public OpponentStrategyWhenGameFirst(int turns)
        {
            this.turns = turns;
            this.strategyOpponentFirst = Method(turns);
        }
        public StrategyOpponentFirst Method(int turns)
        {
            if (turns == 1)
            {
                return new FirstPlayOpponentFirstTour();
            }

            else if (turns >= 2 && turns <= 5)
            {
                return new FirstPlayOpponentSecondToFifthTours();
            }

            else if (turns == 6)
            {
                return new FirstPlayOpponentSixTour();
            }

            else
            {
                return new FirstPlayOpponentSeventhToTwelfthTours();
            }
        }
        
        public Card PlayCard(Player opponent, Player player, Card openTrumpCard, 
            Check check, DeckOfCards deckOfCards)
        {
            return this.strategyOpponentFirst.OpponentPlayFirst(opponent, player, openTrumpCard, 
                check, deckOfCards);
        }
    }
}
