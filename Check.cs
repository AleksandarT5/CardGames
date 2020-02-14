using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Santase
{
    internal class Check : ICheck
    {

        public Card CheckPayerHaveNineTrump(List<Card> playerCards, Card openTrumpCard)
        {            

            if (playerCards.Exists(x => x.Type == openTrumpCard.Type && x.Value == "9"))
            {
                playerCards.Add(openTrumpCard);
                openTrumpCard = new Card(openTrumpCard.Type, "9", 0);
                Card changedCard = playerCards.SingleOrDefault(a => a.Type == openTrumpCard.Type && a.Value == "9");
                playerCards.Remove(changedCard);
                Console.WriteLine($"OpenTrumpCard: {openTrumpCard.ToString()}");
                return openTrumpCard;
            }

            return openTrumpCard;
        }        

        public Card CheckFor40and20(Player player, Card openTrumpCard, Card playerCard)
        // или public VOID CheckFor40and20 - това може би е по-добрия вариант - ТЕСТ !!!!!
        // при CardK40 == null и/или CardK20 == null --- ТЕСТ !!!!!
        {
            //card = CheckForForty(player, openTrumpCard);
            List<Card> cards = player.CardsPlayer;
            Card cardK40 = CheckForForty(player, openTrumpCard);
            Card cardK20 = CheckForTwenty(player, openTrumpCard);

            //if (cards.Exists(c => c.Type == openTrumpCard.Type && c.Value == "K") &&
            //    cards.Exists(c => c.Type == openTrumpCard.Type && c.Value == "D"))
            //if (playerCard.Type == openTrumpCard.Type && (playerCard.Value == "K" || playerCard.Value == "D"))
            //{
            //    player.Points += 40;
            //}


            if (cardK40 == playerCard || (playerCard.Type == cardK40.Type && playerCard.Value == "D"))
            {
                player.Points += 40;
                return playerCard;
            }

            if (cardK20 == playerCard || (playerCard.Type == cardK20.Type && playerCard.Value == "D"))
            {
                player.Points += 20;
                return playerCard;
            }
            
            return playerCard;
        }

        public Card CheckForTwenty(Player participant, Card openTrumpCard)
        {
            List<Card> cardsK = participant.CardsPlayer.Where(a => a.Value == "K").ToList();
            if (cardsK.Count != 0)
            {
                for (int numberCardsK = 0; numberCardsK < cardsK.Count; numberCardsK++)
                {
                    Card cardK = cardsK[numberCardsK];
                    if (participant.CardsPlayer.Exists(x => x.Type == cardK.Type && x.Value == "D"))
                    {
                        participant.Points += 20;
                        if (participant.Points >= 66)
                        {
                            return null;
                        }

                        return cardK;
                    }
                }
            }            

            return null;
        }

        public Card CheckForForty(Player participant, Card openTrumpCard)
        {
            if (participant.CardsPlayer.Exists(a => a.Type == openTrumpCard.Type && a.Value == "K") &&
                participant.CardsPlayer.Exists(a => a.Type == openTrumpCard.Type && a.Value == "D"))
            {
                participant.Points += 40;
                if (participant.Points >= 66)
                {
                    return null;
                }
                
                Card card = participant.CardsPlayer.First(a => a.Type == openTrumpCard.Type && a.Value == "K");
                return card;
            }
            return null;
        }

        public void CheckWhenParticipantHaveSixtySix(Player winer, Player loser)
        {
            if (loser.HasClosedTheDeckOfCards == true)
            {
                winer.Games += 3;
            }

            if (loser.Points == 0)
            {
                winer.Games += 3;
            }

            else if (loser.Points < 33)
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

        public Card CheckForWeakCard(List<Card> opponentCards, Card openTrumpCard)
        {
            var values = new string[] { "9", "J", "D", "K", "A", "10" };
            Card card = null;
            List<Card> opponentCardsNoTrumps = opponentCards.Where(a => a.Type != openTrumpCard.Type).ToList();

            card = CheckCards(opponentCardsNoTrumps, openTrumpCard, values, 0);
            opponentCards.Remove(card);
            return card;
        }

        internal Card CheckForWeakTrump(List<Card> opponentCards, Card openTrumpCard)
        {
            var values = new string[] { "9", "J", "D", "K", "10", "A" };
            List<Card> opponentTrumpCards = opponentCards.Where(a => a.Type == openTrumpCard.Type).ToList();
            return CheckCards(opponentTrumpCards, openTrumpCard, values, 0);
        }

        public Card CheckCards(List<Card> cards, Card openTrumpCard, string[] values, int number)
        {
            if (cards.Any(a => a.Value == values[number]))
            {
                return cards.First(a => a.Value == values[number]);
            }

            else
            {
                number++;
                return CheckCards(cards, openTrumpCard, values, number);
            }
        }

        public bool CheckForCloseOfDeckOfCardsFromOpponent(Player participant, Card openTrumpCard)
        {
            int pointsOfTheCards = participant.CardsPlayer.Sum(a => a.Points);
            if (pointsOfTheCards + participant.Points >= 40 
                && participant.CardsPlayer.Count(a => a.Type == openTrumpCard.Type) >= 3
                && participant.CardsPlayer.Contains(new Card(openTrumpCard.Type, "A", 11)))
            {
                return true;
            }

            return false;
        }

        public void CheckWinnerTurn(Player opponent, Player player, Card opponentCard, Card playerCard, 
            Card openTrumpCard, DeckOfCards deckOfCards)
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

                else if (player.IsFirstPlay == true)
                {
                    PlayerWins(player, playerCard, opponent, opponentCard);
                }
            }

            deckOfCards.PlayedCards.Add(opponentCard);
            deckOfCards.PlayedCards.Add(playerCard);
            //opponent.CardsPlayer.Remove(opponentCard);
            //player.CardsPlayer.Remove(playerCard);
        }

        private void PlayerWins(Player winner, Card winnerCard, Player loser, Card loserCard)
        {
            loser.IsFirstPlay = false;
            winner.IsFirstPlay = true;
            winner.Points += (winnerCard.Points + loserCard.Points);
        }

        public Card CheckForStrongOrWeakCard(string keyWord, List<Card> opponentCards, Card openTrumpCard, DeckOfCards deckOfCards)
        {
            List<Card> allCardsFromType = new List<Card>();
            if (keyWord == "Trump")
            {
                allCardsFromType = AllCardsFromOneType(openTrumpCard.Type, "Trump");

            }

            else if (keyWord == "StrongNoTrump")
            {
                //string
                allCardsFromType = AllCardsFromOneType(openTrumpCard.Type, "StrongNoTrump");

            }

            else if (keyWord == "WeakNoTrump")
            {
                allCardsFromType = AllCardsFromOneType(openTrumpCard.Type, "WeakNoTrump");

            }

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

        private List<Card> AllCardsFromOneType(string typeCard, string keyWord)
        {
            string[] cardsValues = new string[] { "A", "10", "K", "D", "J", "9" };
            if (keyWord == "WeakNoTrump")
            {
                Array.Reverse(cardsValues);
            }
            List<Card> allCardsFromType = new List<Card>();
            for (int i = 0; i < cardsValues.Length; i++)
            {
                allCardsFromType.Add(new Card(typeCard, cardsValues[i]));
            }

            return allCardsFromType;
        }

        public Card CardPlayedAnswerByPlayerNoDeckOfCards(Card cardPlayedByOpponent,
            List<Card> cardsPlayer, Card openTrumpCard)
        {
            if (cardsPlayer.Count(a => a.Type == cardPlayedByOpponent.Type) > 0)
            {
                List<Card> cardsForAnswer = cardsPlayer.Where(a => a.Type == cardPlayedByOpponent.Type).ToList();
                return DeterminingThePlayerCard(cardsForAnswer, openTrumpCard);
            }

            else if (cardPlayedByOpponent.Type != openTrumpCard.Type &&
                cardsPlayer.Count(a => a.Type == openTrumpCard.Type) > 0)
            {
                List<Card> cardsForAnswer = cardsPlayer.Where(a => a.Type == openTrumpCard.Type).ToList();
                return DeterminingThePlayerCard(cardsForAnswer, openTrumpCard);
            }

            return DeterminingThePlayerCard(cardsPlayer, openTrumpCard);
        }

        public Card DeterminingThePlayerCard(List<Card> cardsPlayer, Card openTrumpCard)
        {
            Card cardPlayer = null;

            while (cardPlayer == null)
            {
                string typeCard = Console.ReadLine();
                string valueCard = Console.ReadLine();
                foreach (var oneCard in cardsPlayer)
                {
                    if (oneCard.Type == typeCard && oneCard.Value == valueCard)
                    {
                        cardPlayer = oneCard;
                        cardsPlayer.Remove(cardPlayer);
                        break;
                    }
                }
            }

            return cardPlayer;
        }
    }
}
