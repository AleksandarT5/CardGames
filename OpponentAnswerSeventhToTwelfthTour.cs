using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Santase
{
    class OpponentAnswerSeventhToTwelfthTour : OpponentStrategyAnswer
    {
        public override Card AnswerOnOpponent(Player opponent, Player player, Card playerCard, 
            Card openTrumpCard, Check check)
        {
            //Card card = null;
            if (opponent.CardsPlayer.Count(a => a.Type == playerCard.Type) > 0)
            {
                return opponent.CardsPlayer.Max(a => a.Points) > playerCard.Points ?
                    opponent.CardsPlayer.OrderByDescending(a => a.Points).First() : 
                    opponent.CardsPlayer.OrderBy(a => a.Points).First();
            }

            else if (opponent.CardsPlayer.Count(x => x.Type == playerCard.Type) == 0 &&
                opponent.CardsPlayer.Count(x => x.Type == playerCard.Type) == 0)
            {
                // Връща най-слабата си карта
                return null;
            }

            else
            {
                return opponent.CardsPlayer.OrderByDescending(a => a.Points).First();
            }
        }
    }
}
