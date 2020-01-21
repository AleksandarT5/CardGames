using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Santase
{
    internal class Check : ICheck
    {
        public Card CheckForTwenty(Player player, Card openTrumpCard)
        {
            List<Card> cardsK = player.CardsPlayer.Where(a => a.Value == "K").ToList();
            for (int numberCardsK = 0; numberCardsK < cardsK.Count; numberCardsK++)
            {
                Card cardK = cardsK[numberCardsK];
                Card cardD = new Card(cardK.Type, "D", 3);
                if (player.CardsPlayer.Contains(cardD))
                {
                    player.Points += 20;
                    return cardK;
                }
            }

            return null;
        }

        public Card CheckForFourty(Player player, Card openTrumpCard)
        {
            List<Card> cardsTrump = player.CardsPlayer.Where(a => a.Type == openTrumpCard.Value).ToList();
            if (cardsTrump.Any(a => a.Value == "D") || cardsTrump.Any(a => a.Value == "K"))
            {
                player.Points += 40;
                Card card = cardsTrump.First(a => a.Value == "K");
                return card;
            }
            return null;
        }

        public Card CheckForCard(List<Card> opponentCards, Card openTrumpCard)
        {
            var values = new string[] { "9", "J", "D", "K", "A", "10" };
            int number = 0;
            Card card = null;
            List<Card> opponentCardsNoTrumps = opponentCards.Where(a => a.Type != openTrumpCard.Type).ToList();

            card = CheckCards(opponentCardsNoTrumps, openTrumpCard, card, values, number);
            opponentCardsNoTrumps.Remove(card);
            return card;
        }

        public Card CheckCards(List<Card> opponentCardsNoTrumps, Card openTrumpCard, Card card, string[] values, int number)
        {
            if (opponentCardsNoTrumps.Any(a => a.Value == values[number]))
            {
                card = opponentCardsNoTrumps.First(a => a.Value == values[number]);
                return card;
            }

            else
            {
                number++;
                return CheckCards(opponentCardsNoTrumps, openTrumpCard, card, values, number);
            }
        }

        public void CheckWinnerTurn(Player opponent, Player player, Card opponentCard, Card playerCard, Card openTrumpCard)
        {
            if (opponentCard.Type == playerCard.Type)
            {
                if (opponentCard.Points > playerCard.Points)
                {
                    PlayerWins(opponent, opponentCard, player, playerCard);
                }

                else
                {
                    PlayerWins(player, playerCard, opponent, opponentCard);
                }
            }

            else
            {
                if (opponentCard.Type == openTrumpCard.Type)
                {
                    PlayerWins(opponent, opponentCard, player, playerCard);
                }

                else if (playerCard.Type == openTrumpCard.Type)
                {
                    PlayerWins(player, playerCard, opponent, opponentCard);
                }

                else if (opponent.IsFirstPlay == true)
                {
                    PlayerWins(opponent, opponentCard, player, playerCard);
                }

                else
                {
                    PlayerWins(player, playerCard, opponent, opponentCard);
                }
            }

            opponent.CardsPlayer.Remove(opponentCard);
            player.CardsPlayer.Remove(playerCard);


        }

        public void CheckPayerHaveNineTrump(List<Card> playerCards, Card openTrumpCard)
        {
            Card wantedCard = new Card(openTrumpCard.Value, "9", 0);
            if (playerCards.Contains(wantedCard))
            {
                playerCards.Add(openTrumpCard);
                openTrumpCard = wantedCard;
                playerCards.Remove(wantedCard);
            }
        }

        // DO IT
        public void CheckPlayerHaveSixtySix(Player player)
        {
            if (player.Points >= 66)
            {

            }
        }

        private void PlayerWins(Player winner, Card winnerCard, Player lost, Card lostCard)
        {
            lost.IsFirstPlay = false;
            winner.IsFirstPlay = true;
            winner.Points += (winnerCard.Points + lostCard.Points);
        }
    }
}
