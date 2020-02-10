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
            if (playerCards.Exists(x => x.Type == openTrumpCard.Type && x.Value == "9"))
            {
                playerCards.Add(openTrumpCard);
                openTrumpCard = new Card(openTrumpCard.Type, "9", 0);
                var changedCard = playerCards.SingleOrDefault(a => a.Type == openTrumpCard.Type && a.Value == "9");
                playerCards.Remove(changedCard);
            }
        }
        /// <summary>
        /// За тест
        /// </summary>
        public Card CheckForTwenty(Player participant, Card openTrumpCard, Player theOtherParticipant, bool havePlayerSixtySixPonts)
        {
            List<Card> cardsK = participant.CardsPlayer.Where(a => a.Value == "K").ToList();
            if (cardsK.Count != 0)
            {
                for (int numberCardsK = 0; numberCardsK < cardsK.Count; numberCardsK++)
                {
                    Card cardK = cardsK[numberCardsK];
                    Card cardD = new Card(cardK.Type, "D", 3);
                    if (participant.CardsPlayer.Exists(x => x.Type == cardK.Type && x.Value == "D"))
                    //if (participant.CardsPlayer.Contains(cardD))
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
            }            

            return null;
        }

        /// <summary>
        /// За тест
        /// </summary>
        public Card CheckForFourty(Player participant, Card openTrumpCard, Player theOtherParticipant, bool havePlayerSixtySixPonts)
        {
            if (participant.CardsPlayer.Exists(a => a.Type == openTrumpCard.Type && a.Value == "K") &&
                participant.CardsPlayer.Exists(a => a.Type == openTrumpCard.Type && a.Value == "D"))
            //if (cardsTrump.Any(a => a.Value == "D") && cardsTrump.Any(a => a.Value == "K"))
            {
                participant.Points += 40;
                if (participant.Points >= 66)
                {
                    havePlayerSixtySixPonts = true;
                    return null;
                }
                
                Card card = participant.CardsPlayer.First(a => a.Type == openTrumpCard.Type && a.Value == "K");
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
            opponentCards.Remove(card);
            return card;
        }

        public Card CheckCards(List<Card> cards, Card openTrumpCard, Card card, string[] values, int number)
        {
            if (cards.Any(a => a.Value == values[number]))
            {
                card = cards.First(a => a.Value == values[number]);
                return card;
            }

            else
            {
                number++;
                return CheckCards(cards, openTrumpCard, card, values, number);
            }
        }

        public bool CheckForCloseOfDeckOfCards(Player player, Card openTrumpCard)
        {
            int pointsOfTheCards = player.CardsPlayer.Sum(a => a.Points);
            if (pointsOfTheCards + player.Points >= 40 
                && player.CardsPlayer.Count(a => a.Type == openTrumpCard.Type) >= 3
                && player.CardsPlayer.Contains(new Card(openTrumpCard.Type, "A", 11)))
            {
                //
                //
                return true;
            }

            return false;
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

                else if (player.IsFirstPlay == true)
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

        /// <summary>
        /// НЕ
        /// </summary>
        public Card CheckForATrump(Player player, Card openTrumpCard)
        {            
            Card wantedCard = new Card(openTrumpCard.Type, "A", 11);
            Card card = player.CardsPlayer.Contains(wantedCard) ? wantedCard : null;
            return card;
        }
        /// <summary>
        /// НЕ
        /// </summary>        
        public Card CheckFor10Trump(List<Card> playerCards, Card openTrumpCard, DeckOfCards deckOfCards)
        {
            Card wantedCard = new Card(openTrumpCard.Type, "10", 10);
            Card card = playerCards.Contains(wantedCard) && deckOfCards.PlayedCards.Contains(
                new Card(openTrumpCard.Type, "A", 11)) ? wantedCard : null;
            return card;
        }

        public Card CheckTrump(List<Card> opponentCards, Card openTrumpCard, DeckOfCards deckOfCards)
        {
            List<Card> allCardsFromType = AllCardsFromOneType(openTrumpCard.Type);
            List<Card> playedCards = deckOfCards.PlayedCards;
            bool IsThereAllTheWantedCardsInPlayedCards = false;

            for (int i = 0; i < allCardsFromType.Count; i++)
            {
                Card wantedCard = allCardsFromType[i];
                if (opponentCards.Contains(wantedCard))
                {
                    if (i == 0)
                    {
                        return wantedCard;
                    }
                    else
                    {
                        List<Card> wantedTrupmCards = allCardsFromType.Take(i).ToList();
                        IsThereAllTheWantedCardsInPlayedCards = playedCards.Except(wantedTrupmCards).Any();
                        if (IsThereAllTheWantedCardsInPlayedCards == true)
                        {
                            return wantedCard;
                        }
                    }
                }                
            }

            return null;
        }

        private List<Card> AllCardsFromOneType(string typeCard)
        {
            string[] cardsValues = new string[] { "A", "10", "K", "D", "J", "9" };
            List<Card> allCardsFromType = new List<Card>();
            for (int i = 0; i < cardsValues.Length; i++)
            {
                allCardsFromType.Add(new Card(typeCard, cardsValues[i]));
            }

            return allCardsFromType;
        }
    }
}
