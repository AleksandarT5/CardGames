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
            
            if (playerCard.Type != openTrumpCard.Type)
            {
                if (AnswerWithWeakTrump(opponent.CardsPlayer, playerCard, openTrumpCard) == true)
                {
                    return check.CheckForTheWeakestTrump(opponent.CardsPlayer, openTrumpCard);
                }

                if (AnswerWithWeakNoTrumpCard(opponent.CardsPlayer, playerCard) == true)
                {
                    return check.CheckForTheWeakestCard(opponent.CardsPlayer, openTrumpCard);
                }
            }
            
            if (AnswerWithStrongestTrump(opponent.CardsPlayer, playerCard, openTrumpCard) == true)
            {
                return opponent.CardsPlayer.Where(c => c.Type == playerCard.Type)
                    .OrderByDescending(c => c.Points).First();
            }
            
            return check.CheckForTheWeakestCard(opponent.CardsPlayer, openTrumpCard);            
        }

        private bool AnswerWithWeakNoTrumpCard(List<Card> cards, Card playerCard)
        {
            return (playerCard.Value != "10" && playerCard.Value != "A")
                    && cards.Count(c => c.Type == playerCard.Type) > 0
                    && cards.Where(c => c.Type == playerCard.Type)
                    .OrderBy(c => c.Points).Last().Points < playerCard.Points;
        }

        private bool AnswerWithStrongestTrump(List<Card> cards, Card playerCard, Card openTrumpCard)
        {
            return playerCard.Type != openTrumpCard.Type && cards.Count(c => c.Type == playerCard.Type) > 0 
                && cards.Max(c => c.Points) > playerCard.Points;

        }

        private bool AnswerWithWeakTrump(List<Card> cards, Card playerCard, Card openTrumpCard)
        {
            return (playerCard.Value == "10" || playerCard.Value == "A")
                    && ((cards.Count(c => c.Type == playerCard.Type) > 0
                    && cards.Where(c => c.Type == playerCard.Type)
                    .OrderByDescending(c => c.Points).First().Points < playerCard.Points)
                    || cards.Count(c => c.Type == playerCard.Type) == 0)
                    && cards.Count(c => c.Type == openTrumpCard.Type) > 0;
        }
    }
}
