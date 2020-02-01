using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Santase
{
    internal class Check : ICheck
    {
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

        public Card CheckForTwenty(Player participant, Card openTrumpCard, Player theOtherParticipant, bool havePlayerSixtySixPonts)
        {
            List<Card> cardsK = participant.CardsPlayer.Where(a => a.Value == "K").ToList();
            for (int numberCardsK = 0; numberCardsK < cardsK.Count; numberCardsK++)
            {
                Card cardK = cardsK[numberCardsK];
                Card cardD = new Card(cardK.Type, "D", 3);
                if (participant.CardsPlayer.Contains(cardD))
                {
                    participant.Points += 20;

                    if (participant.Points >= 66)
                    {
                        havePlayerSixtySixPonts = true;
                        return null;
                    }

                    return cardK;
                }
            }

            return null;
        }

        public Card CheckForFourty(Player participant, Card openTrumpCard, Player theOtherParticipant, bool havePlayerSixtySixPonts)
        {
            List<Card> cardsTrump = participant.CardsPlayer.Where(a => a.Type == openTrumpCard.Value).ToList();
            if (cardsTrump.Any(a => a.Value == "D") || cardsTrump.Any(a => a.Value == "K"))
            {
                participant.Points += 40;
                if (participant.Points >= 66)
                {
                    havePlayerSixtySixPonts = true;
                    return null;
                }
                
                Card card = cardsTrump.First(a => a.Value == "K");
                return card;
            }
            return null;
        }

        public void CheckWhenPlayerHaveSixtySix(Player winer, Player loser)
        {            
                if (loser.Points < 33)
                {
                    winer.Games += 3;
                }

                else if (loser.Points >= 33 && loser.Points < 66)
                {
                    winer.Games += 2;
                }

                else
                {
                    winer.Games++;
                }

                winer.Points = 0;
                loser.Points = 0;
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
                //opponentCard.Points > playerCard.Points ? PlayerWins(opponent, opponentCard, player, playerCard)
                //    : PlayerWins(player, playerCard, opponent, opponentCard);
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

        private void PlayerWins(Player winner, Card winnerCard, Player loser, Card loserCard)
        {
            loser.IsFirstPlay = false;
            winner.IsFirstPlay = true;
            winner.Points += (winnerCard.Points + loserCard.Points);
        }
    }
}
