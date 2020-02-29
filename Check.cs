using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Santase
{
    internal class Check : ICheck
    {
        public void CheckPayerHaveNineTrump(List<Card> participantCards, Card openTrumpCard, int turns)
        {
            if (turns >= 2 && turns <= 5)
            {
                participantCards = NineTrump(participantCards, openTrumpCard);
                openTrumpCard.Value = "9";
                Console.WriteLine($"Cards: {string.Join(", ", participantCards)}");
                Console.WriteLine($"OpenTrumpCard: {openTrumpCard.ToString()}");
            }
        }
        public int CheckForCloseDeckOfCards(Player opponent, Player player, Card openTrumpCard, int turns)
        {
            if (turns >= 1 && turns <= 4)
            {
                if (opponent.IsFirstPlay == true)
                {
                    if (CheckForCloseOfDeckOfCardsFromOpponent(opponent, openTrumpCard) == true)
                    {
                        opponent.HasClosedTheDeckOfCards = true;
                        turns = 6;
                    }
                }

                else
                {
                    Console.Write($"Will the {player.Name} close Deck of Cards(for \"Yes\", write \"Y\"):");
                    string answer = Console.ReadLine();
                    if (answer == "Y")
                    {
                        player.HasClosedTheDeckOfCards = true;
                        turns = 6;
                    }
                }
            }

            return turns;
        }
        // Не прави исканата промяна - не сменя openTrumpCard
        public List<Card> CheckPayerHaveNineTrump(List<Card> playerCards, Card openTrumpCard)
        {
            if (playerCards.Exists(c => c.Type == openTrumpCard.Type && c.Value == "9"))
            {
                playerCards.Add(openTrumpCard);
                Card changedCard = playerCards.Where(c => c.Type == openTrumpCard.Type
                && c.Value == "9").First();
                playerCards.Remove(changedCard);
            }

            return playerCards;
        }

        public List<Card> NineTrump(List<Card> playerCards, Card openTrumpCard)
        {
            if (playerCards.Exists(c => c.Type == openTrumpCard.Type && c.Value == "9"))
            {
                string openTrumpCardValue = openTrumpCard.Value;
                playerCards.Add(new Card(openTrumpCard.Type, openTrumpCardValue));
                Card changedCard = playerCards.Where(c => c.Type == openTrumpCard.Type
                && c.Value == "9").First();
                //openTrumpCard = changedCard;
                playerCards.Remove(changedCard);
                return playerCards;
            }

            return playerCards;
        }

        public Card CheckOpenTrupmCardIsChanged(Card openTrumpCard, bool changedOpenTrumpCard)
        {
            if (changedOpenTrumpCard == true)
            {
                openTrumpCard.Value = "9";
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
            if ((playerCard.Value == "K" && cards.Exists(c => c.Value == "D")) 
                || (playerCard.Value == "D" && cards.Exists(c => c.Value == "K")))
            {
                return cards.Where((c => c.Value == "K" || c.Value == "D")).ToList().First();
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
        
        public Card CheckForStrongNoTrumpCard(Player opponent, Card openTrumpCard, List<Card> deckOfCardsPlayedCards)
        {
            List<Card> opponentNoTrumpCards = opponent.CardsPlayer.Where(c => c.Type != openTrumpCard.Type)
                .OrderByDescending(b => b.Points).ToList();
            if (opponentNoTrumpCards.Count > 0)
            {
                foreach (var opponentCard in opponentNoTrumpCards)
                {
                    Card checkedCard = CheckForStrongCardFromType(opponent, opponentCard.Type, deckOfCardsPlayedCards);
                    if (checkedCard != null)
                    {
                        return opponentCard;
                    }
                }
            }

            return null;
        }

        public Card CheckForStrongCardFromType(Player opponent, string type, List<Card> deckOfCardsPlayedCards)
        {
            List<Card> opponentCardsFromType = opponent.CardsPlayer.Where(c => c.Type == type).ToList();
            List<Card> playedCardsFromType = deckOfCardsPlayedCards.Where(c => c.Type == type).ToList();
            List<Card> noPlayedCardsFromType = CreateAllCardsFromType(type, playedCardsFromType);

            if (opponentCardsFromType.Count > 0)
            {
                return opponentCardsFromType.Max(c => c.Points) == noPlayedCardsFromType.Max(b => b.Points) ?
                        opponentCardsFromType.OrderByDescending(c => c.Points).First() : null;
            }

            return null;
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

        internal Card CheckForWeakTrump(List<Card> opponentCards, Card openTrumpCard)
        {
            List<Card> opponentTrumpCards = opponentCards.Where(a => a.Type == openTrumpCard.Type).ToList();
            if (opponentTrumpCards.Count > 0)
            {
                return opponentTrumpCards.OrderBy(a => a.Points).First();
            }

            return null;
        }

        public Card CheckForWeakCard(List<Card> opponentCards, Card openTrumpCard)
        {
            List<Card> opponentCardsNoTrumps = opponentCards.Where(c => c.Type != openTrumpCard.Type)
                .OrderBy(c => c.Points).ToList();
            if (opponentCardsNoTrumps.Count > 0)
            {
                return opponentCardsNoTrumps.First();
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
                string typeCard = Console.ReadLine();
                string valueCard = Console.ReadLine();
                if (cardsPlayer.Exists(c => c.Type == typeCard && c.Value == valueCard))
                {
                    cardPlayer = cardsPlayer.First(c => c.Type == typeCard && c.Value == valueCard);
                }
            }

            return cardPlayer;
        }

        public bool CheckForCloseOfDeckOfCardsFromOpponent(Player participant, Card openTrumpCard)
        {
            int pointsOfTheCards = participant.CardsPlayer.Sum(a => a.Points);
            if (pointsOfTheCards + participant.Points >= 40
                && participant.CardsPlayer.Count(a => a.Type == openTrumpCard.Type) >= 3
                && participant.CardsPlayer.Exists(c => c.Type == openTrumpCard.Type 
                && (c.Value == "A" || c.Value == "10")))
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

        public void CalculationsWhenParticipantHaveSixtySix(Player winer, Player loser)
        {
            if (loser.HasClosedTheDeckOfCards == true)
            {
                winer.Games += loser.Points == 0 ? 3 : 2;
            }

            else if (loser.Points == 0)
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
            winer.IsFirstPlay = true;
            loser.IsFirstPlay = false;
            winer.HasClosedTheDeckOfCards = false;
            loser.HasClosedTheDeckOfCards = false;
        }

        public void CheckFor66(Player opponent, Player player)
        {
            if (opponent.Points >= 66)
            {
                CalculationsWhenParticipantHaveSixtySix(opponent, player);
            }

            else if (player.Points >= 66)
            {
                CalculationsWhenParticipantHaveSixtySix(player, opponent);
            }
        }

        public void CheckAfter12Tour(Player player, Player opponent)
        {
            if (player.HasClosedTheDeckOfCards == true)
            {
                CalculationsWhenParticipantHaveSixtySix(opponent, player);
            }

            else if (opponent.HasClosedTheDeckOfCards == true)
            {
                CalculationsWhenParticipantHaveSixtySix(player, opponent);
            }

            else
            {
                player.Points = 0;
                opponent.Points = 0;
            }
        }
    }
}
