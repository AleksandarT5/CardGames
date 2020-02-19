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

            if (playerCards.Exists(c => c.Type == openTrumpCard.Type && c.Value == "9"))
            {
                playerCards.Add(openTrumpCard);
                openTrumpCard = new Card(openTrumpCard.Type, "9", 0);
                Card changedCard = playerCards.SingleOrDefault(c => c.Type == openTrumpCard.Type && c.Value == "9");
                playerCards.Remove(changedCard);
                Console.WriteLine($"OpenTrumpCard: {openTrumpCard.ToString()}");
                return openTrumpCard;
            }

            return openTrumpCard;
        }

        public int CheckFor40and20(Player player, Card openTrumpCard, Card playerCard)
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
            if ((playerCard.Value == "K" || playerCard.Value == "D") 
                && player.CardsPlayer.Exists(c => c.Value == "K" || c.Value == "D"))
            {
                return player.CardsPlayer.Where((c => c.Value == "K" || c.Value == "D")).ToList().First();
            }

            return null;
        }

        public Card CheckingForTwenty(Player participant, Card openTrumpCard)
        {
            List<Card> cardsWithValueK = participant.CardsPlayer.Where(c => c.Value == "K").ToList();
            if (cardsWithValueK.Count > 0)
            {
                foreach (var cardK in cardsWithValueK)
                {
                    if (participant.CardsPlayer.Exists(c => c.Type == cardK.Type && c.Value == "D"))
                    {
                        participant.Points += 20;
                        return cardK;
                    }
                }
            }
            
            return null;
        }

        public Card CheckingForForty(Player participant, Card openTrumpCard)
        {
            if (participant.CardsPlayer.Exists(c => c.Type == openTrumpCard.Type && c.Value == "K") &&
                participant.CardsPlayer.Exists(c => c.Type == openTrumpCard.Type && c.Value == "D"))
            {
                participant.Points += 40;
                return participant.CardsPlayer.First(c => c.Type == openTrumpCard.Type && c.Value == "K");
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
        
        public Card CheckForStrongTrump(Player opponent, string type, List<Card> deckOfCardsPlayedCards)
        {
            List<Card> opponentTrumpCards = opponent.CardsPlayer.Where(c => c.Type == type).ToList();
            List<Card> playedTrumpCards = deckOfCardsPlayedCards.Where(c => c.Type == type).ToList();
            List<Card> noPlayedCardsFromType = CreateAllCardsFromType(type, playedTrumpCards);
            Card wantedCard = opponentTrumpCards.Max(c => c.Points) == noPlayedCardsFromType.Max(b => b.Points) ?
                opponentTrumpCards.OrderByDescending(c => c.Points).First() : null;
            //opponent.CardsPlayer.Remove(wantedCard);
            return wantedCard;
        }

        private List<Card> CreateAllCardsFromType(string type, List<Card> playedTrumpCards)
        {
            List<Card> allCardsFromType = new List<Card>();
            string[] values = new string[] { "9", "J", "D", "K", "10", "A" };
            for (int i = 0; i < 6; i++)
            {
                allCardsFromType.Add(new Card(type, values[i]));
            }

            return allCardsFromType.Except(playedTrumpCards).ToList();
        }

        public Card StrongNoTrump(Player opponent, Card openTrumpCard, List<Card> deckOfCardsPlayedCards)
        {
            List<Card> opponentNoTrumpCards = opponent.CardsPlayer.Where(c => c.Type != openTrumpCard.Type)
                .OrderByDescending(b => b.Points).ToList();
            //if (opponentNoTrumpCards.Count > 0 && deckOfCardsPlayedCards.Count > 0)
            //{
            //}
            //foreach (var card in opponent.CardsPlayer)
            //{
            //    List<Card> cards = 
            //}
            foreach (var card in opponentNoTrumpCards)
            {
                if (card.Points > deckOfCardsPlayedCards.Where(c => c.Type == card.Type)
                    .OrderByDescending(b => b.Points).First().Points)
                {
                    //opponent.CardsPlayer.Remove(card);
                    return card;
                }
            }

            return null;
        }

        public Card CheckForWeakCard(List<Card> opponentCards, Card openTrumpCard)
        {
            var values = new string[] { "9", "J", "D", "K", "A", "10" };
            Card card = null;
            List<Card> opponentCardsNoTrumps = opponentCards.Where(a => a.Type != openTrumpCard.Type).ToList();
            if (opponentCardsNoTrumps.Count > 0)
            {
                card = CheckCards(opponentCardsNoTrumps, openTrumpCard, values, 0);
                //opponentCards.Remove(card);
            }
            
            return card;
        }

        internal Card CheckForWeakTrump(List<Card> opponentCards, Card openTrumpCard)
        {
            List<Card> opponentTrumpCards = opponentCards.Where(a => a.Type == openTrumpCard.Type).ToList();
            if (opponentTrumpCards.Count > 0)
            {
                Card card = opponentTrumpCards.OrderBy(a => a.Points).First();
                //opponentCards.Remove(card);
                return card;
            }

            return null;
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

        public Card CardPlayedAnswerByPlayerNoDeckOfCards(Card cardPlayedByOpponent,
            List<Card> cardsPlayer, Card openTrumpCard)
        {
            if (cardsPlayer.Count(a => a.Type == cardPlayedByOpponent.Type) > 0)
            {
                List<Card> cardsForAnswer = cardsPlayer.Where(a => a.Type == cardPlayedByOpponent.Type).ToList();
                //return DeterminingThePlayerCard(cardsForAnswer);
            }

            else if (cardPlayedByOpponent.Type != openTrumpCard.Type &&
                cardsPlayer.Count(a => a.Type == openTrumpCard.Type) > 0)
            {
                List<Card> cardsForAnswer = cardsPlayer.Where(a => a.Type == openTrumpCard.Type).ToList();
                //return DeterminingThePlayerCard(cardsForAnswer);
            }

            return DeterminingThePlayerCard(cardsPlayer);
        }

        public Card DeterminingThePlayerCard(List<Card> cardsPlayer)
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
                        //cardsPlayer.Remove(cardPlayer);
                        break;
                    }
                }
            }

            return cardPlayer;
        }

        public bool CheckForCloseOfDeckOfCardsFromOpponent(Player participant, Card openTrumpCard)
        {
            int pointsOfTheCards = participant.CardsPlayer.Sum(a => a.Points);
            if (pointsOfTheCards + participant.Points >= 40
                && participant.CardsPlayer.Count(a => a.Type == openTrumpCard.Type) >= 3
                && participant.CardsPlayer.Exists(c => c.Type == openTrumpCard.Type && c.Value == "A"))
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
            player.CardsPlayer.Remove(playerCard);
            opponent.CardsPlayer.Remove(opponentCard);
            deckOfCards.PlayedCards.Add(opponentCard);
            deckOfCards.PlayedCards.Add(playerCard);
        }

        private void PlayerWins(Player winner, Card winnerCard, Player loser, Card loserCard)
        {
            loser.IsFirstPlay = false;
            winner.IsFirstPlay = true;
            winner.Points += (winnerCard.Points + loserCard.Points);
        }
    }
}
