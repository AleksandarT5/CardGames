using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Santase
{
    class OpponentAnswerFirstOnSixTour : OpponentStrategyAnswer
    {
        public override Card AnswerOnOpponent(Player opponent, Player player, Card playerCard, 
            Card openTrumpCard, Check check)
        {
            
            Card opponentCard = null;
            if ((playerCard.Type != openTrumpCard.Type && (playerCard.Value == "10" || playerCard.Value == "A"))
                && opponent.CardsPlayer.Count(a => a.Type == openTrumpCard.Type) > 0)
            {
                List<Card> opponentTrumpCards = opponent.CardsPlayer.Where(a => a.Type == openTrumpCard.Type).ToList();

                return opponentCard = check.CheckForWeakTrump(opponent.CardsPlayer, openTrumpCard);
            }

            if (playerCard.Type == openTrumpCard.Type)
            {
                // 1.Отговаря със най-слаба неКоз
            }

            else if (playerCard.Type != openTrumpCard.Type)
            {
                if (playerCard.Value == "10" || playerCard.Value == "A")
                {
                    if (opponent.CardsPlayer.Count(a => a.Value == openTrumpCard.Value) > 0)
                    {
                        // 2.Отговаря с най-слабата коз
                    }

                    else
                    // Няма козове
                    {
                        // 3.Отговаря с най-слабата неКоз
                    }
                }

                else
                // Играя слаба неКоз
                {
                    if (opponent.CardsPlayer.Count(a => a.Type == playerCard.Type) > 0)
                        // Опонента има карти от същия тип
                    {
                        // 4. 
                    }

                    else
                    // Опонента няма карти от същия тип
                    {
                        // 5.Отговаря с най-слабата неКоз
                    }
                }
            }



            return null;
        }
    }
}
