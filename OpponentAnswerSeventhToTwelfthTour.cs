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
            if (opponent.CardsPlayer.Count(c => c.Type == playerCard.Type) > 0)
            {
                List<Card> sameTypeOfCardsAsPlayerCard = opponent.CardsPlayer
                    .Where(c => c.Type == playerCard.Type).ToList();
                return sameTypeOfCardsAsPlayerCard.Max(c => c.Points) > playerCard.Points ?
                    sameTypeOfCardsAsPlayerCard.OrderByDescending(c => c.Points).First() :
                    sameTypeOfCardsAsPlayerCard.OrderBy(c => c.Points).First();
            }

            else if (playerCard.Type != openTrumpCard.Type 
                && opponent.CardsPlayer.Count(c => c.Type == playerCard.Type) == 0
                && opponent.CardsPlayer.Count(c => c.Type == openTrumpCard.Type) > 0)
            {
                return check.CheckForTheWeakestTrump(opponent.CardsPlayer, openTrumpCard);
            }

            return check.CheckForTheWeakestCard(opponent.CardsPlayer, openTrumpCard);

        }
    }
}
