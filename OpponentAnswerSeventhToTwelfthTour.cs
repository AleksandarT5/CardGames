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
            if (opponent.CardsPlayer.Count(c => c.Type == playerCard.Type) > 0)
            {
                return opponent.CardsPlayer.Max(c => c.Points) > playerCard.Points ?
                    opponent.CardsPlayer.OrderByDescending(c => c.Points).First() : 
                    opponent.CardsPlayer.OrderBy(c => c.Points).First();
            }

            else if (opponent.CardsPlayer.Count(c => c.Type == playerCard.Type) == 0 &&
                opponent.CardsPlayer.Count(c => c.Type == openTrumpCard.Type) > 0)
            {
                // Връща коз - силен или слаб ????? - слаб
                return opponent.CardsPlayer.OrderByDescending(c => c.Points).First();
            }

            else if (opponent.CardsPlayer.Count(c => c.Type != openTrumpCard.Type) > 0)
            {
                return opponent.CardsPlayer.Where(c => c.Type != openTrumpCard.Type)
                    .OrderBy(c => c.Points).First();
            }

            //else
            //{
            //    return opponent.CardsPlayer.Count(c => c.Type != openTrumpCard.Type) > 0 ?
            //        opponent.CardsPlayer.Where(c => c.Type != openTrumpCard.Type).OrderBy(c => c.Points)
            //        .First() : opponent.CardsPlayer.OrderBy(c => c.Points).First();
            //}
            else
            {
                return opponent.CardsPlayer.OrderBy(c => c.Points).First();
            }
        }
    }
}
