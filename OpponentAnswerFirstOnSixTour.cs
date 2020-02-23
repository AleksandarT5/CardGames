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
            if (AnswerWithStrongestTrump(opponent.CardsPlayer, playerCard, openTrumpCard) == true)
            {
                //playerCard.Type != openTrumpCard.Type && cards.Count(c => c.Type == playerCard.Type) > 0
                //&& cards.Max(c => c.Points) > playerCard.Points;
                opponentCard = opponent.CardsPlayer.Where(c => c.Type == playerCard.Type)
                    .OrderByDescending(c => c.Points).First();
                return opponentCard;
            }

            // ?
            else if ((playerCard.Type != openTrumpCard.Type && playerCard.Value == "10")
                && opponent.CardsPlayer.Exists(c => c.Type == playerCard.Type && c.Value == "A"))
            {
                return opponent.CardsPlayer.Where(c => c.Type == playerCard.Type
                && c.Value == "A").ToList().First();
            }
            //

            else if (AnswerWithWeakTrump(opponent.CardsPlayer, playerCard, openTrumpCard) == true)
            {
                //((playerCard.Type != openTrumpCard.Type && (playerCard.Value == "A"
                //|| playerCard.Value == "10"))) && (cards.Count(c => c.Type == openTrumpCard.Type) > 0);
                opponentCard = check.CheckForWeakTrump(opponent.CardsPlayer, openTrumpCard);
                return opponentCard;
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
