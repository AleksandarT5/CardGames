using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class OpponentStrategyWhenAnswer
    {
        private OpponentStrategyAnswer opponentStrategyAnswer;

        private int turn;

        public OpponentStrategyWhenAnswer(int turn)
        {
            this.turn = turn;
            this.opponentStrategyAnswer = Method(turn);
        }

        private OpponentStrategyAnswer Method(int turn)
        {
            if (turn >= 1 && turn <= 6)
            {
                return new OpponentAnswerFirstOnSixTour();
            }

            return new OpponentAnswerSeventhToTwelfthTour();
        }

        public Card PlayCard(Player opponent, Player player, Card playerCard, Card openTrumpCard, Check check)
        {
            return this.opponentStrategyAnswer.AnswerOnOpponent(opponent, player, playerCard, openTrumpCard, 
                check);
        }
    }
}
