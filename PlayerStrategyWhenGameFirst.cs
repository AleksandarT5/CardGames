using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class PlayerStrategyWhenGameFirst
    {
        private PlayerStrategyFirst strategyPlayerFirst;

        private int turns;

        public PlayerStrategyWhenGameFirst(int turns)
        {
            this.turns = turns;
            this.strategyPlayerFirst = Method(turns);
        }

        private PlayerStrategyFirst Method(int turns)
        {
            if (this.turns == 1)
            {
                return new FirstPlayPlayerFirstTour();
            }

            else if (this.turns >= 2 && this.turns <= 5)
            {
                return new FirstPlayPlayerSecondToFifthTour();
            }

            return new FirstPlayPlayerSixToTwelfthTour();
        }

        public Card PlayCard(Player opponent, Player player, Card openTrumpCard,
            Check check, DeckOfCards deckOfCards)
        {
            return this.strategyPlayerFirst.PlayerPlayFirst(player, openTrumpCard,
                check);
        }
    }
}
