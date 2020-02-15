using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class OpponentStrategyWhenGameFirst
    {
        private OpponentStrategyFirst strategyOpponentFirst;
        
        private int turn;

        public OpponentStrategyWhenGameFirst(int turn)
        {
            this.turn = turn;
            this.strategyOpponentFirst = Method(turn);
        }
        private OpponentStrategyFirst Method(int turns)
        {
            if (turns == 1)
            {
                return new FirstPlayOpponentFirstTour();
            }

            else if (turns >= 2 && turns <= 5)
            {
                return new FirstPlayOpponentSecondToFifthTour();
            }

            else if (turns == 6)
            {
                return new FirstPlayOpponentSixTour();
            }

            else
            {
                return new FirstPlayOpponentSeventhToTwelfthTour();
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
