using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Santase
{
    class CheckingPlayer : ICheckingPlayer
    {
        public int CheckingFor40and20(Player player, Card openTrumpCard, Card playerCard)
        {
            Card card = CheckForExtraPoints(player, playerCard);
            if (card != null)
            {
                int extraPoints = card.Type == openTrumpCard.Type ? 40 : 20;
                return extraPoints;
            }

            return 0;
        }

        private Card CheckForExtraPoints(Player player, Card playerCard)
        {
            List<Card> cards = player.CardsPlayer.Where(c => c.Type == playerCard.Type).ToList();
            if ((playerCard.Value == "K" && cards.Exists(c => c.Value == "D"))
                || (playerCard.Value == "D" && cards.Exists(c => c.Value == "K")))
            {
                return cards.Where((c => c.Value == "K" || c.Value == "D")).ToList().First();
            }

            return null;
        }

        public Card CardPlayedAnswerByPlayerNoDeckOfCards(Card cardPlayedByOpponent,
            List<Card> cardsPlayer, Card openTrumpCard)
        {
            if (cardsPlayer.Count(a => a.Type == cardPlayedByOpponent.Type) > 0)
            {
                List<Card> cardsForAnswer = cardsPlayer.Where(a => a.Type == cardPlayedByOpponent.Type).ToList();
                return DeterminingThePlayerCard(cardsForAnswer);
            }

            else if (cardPlayedByOpponent.Type != openTrumpCard.Type &&
                cardsPlayer.Count(a => a.Type == openTrumpCard.Type) > 0)
            {
                List<Card> cardsForAnswer = cardsPlayer.Where(a => a.Type == openTrumpCard.Type).ToList();
                return DeterminingThePlayerCard(cardsForAnswer);
            }

            return DeterminingThePlayerCard(cardsPlayer);
        }

        public Card DeterminingThePlayerCard(List<Card> cardsPlayer)
        {
            Card cardPlayer = null;

            while (cardPlayer == null)
            {
                Console.Write("The type of card which will you playing: ");
                string typeCard = Console.ReadLine();
                Console.Write("The value of card which will you playing: ");
                string valueCard = Console.ReadLine();
                if (cardsPlayer.Exists(c => c.Type == typeCard && c.Value == valueCard))
                {
                    cardPlayer = cardsPlayer.First(c => c.Type == typeCard && c.Value == valueCard);
                }
            }

            return cardPlayer;
        }
    }
}
