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
            //
            if (playerCard.Type != openTrumpCard.Type)
            {
                if ((playerCard.Value != "10" && playerCard.Value != "A") 
                    && opponent.CardsPlayer.Where(c => c.Type == playerCard.Type)
                    .OrderBy(c => c.Points).Last().Points < playerCard.Points)
                {
                    return check.CheckForWeakCard(opponent.CardsPlayer, openTrumpCard);
                }
            }
            //
            if (AnswerWithStrongestTrump(opponent.CardsPlayer, playerCard, openTrumpCard) == true)
                //playerCard.Type != openTrumpCard.Type && cards.Count(c => c.Type == playerCard.Type) > 0
                //&& cards.Max(c => c.Points) > playerCard.Points;
            {
                return opponent.CardsPlayer.Where(c => c.Type == playerCard.Type)
                    .OrderByDescending(c => c.Points).First();
            }

            else if (AnswerWithWeakTrump(opponent.CardsPlayer, playerCard, openTrumpCard) == true)
                //((playerCard.Type != openTrumpCard.Type && (playerCard.Value == "A"
                //|| playerCard.Value == "10"))) && (cards.Count(c => c.Type == openTrumpCard.Type) > 0);
            {
                return check.CheckForWeakTrump(opponent.CardsPlayer, openTrumpCard);
            }

            return check.CheckForWeakCard(opponent.CardsPlayer, openTrumpCard);            
        }

        private bool AnswerWithStrongestTrump(List<Card> cards, Card playerCard, Card openTrumpCard)
        {
            return playerCard.Type != openTrumpCard.Type && cards.Count(c => c.Type == playerCard.Type) > 0 
                && cards.Max(c => c.Points) > playerCard.Points;

        }

        private bool AnswerWithWeakTrump(List<Card> cards, Card playerCard, Card openTrumpCard)
        {
            return ((playerCard.Type != openTrumpCard.Type && (playerCard.Value == "A"
                || playerCard.Value == "10"))) && (cards.Count(c => c.Type == openTrumpCard.Type) > 0);
        }
    }
}
