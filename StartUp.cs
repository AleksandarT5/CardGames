using System;
using System.Collections.Generic;
using System.Linq;

namespace Santase
{
    class StartUp
    {
        static void Main(string[] args)
        {                      
            // Пълнене на тестето и разбъркване на картите
            DeckOfCards deckOfCards = new DeckOfCards();

            Player opponent = new Player(Console.ReadLine(), new List<Card>(), 0, 0, true, false);
            Player player = new Player(Console.ReadLine(), new List<Card>(), 0, 0, false, false);

            while (opponent.Games < 11 && player.Games < 11)
            {
                // Раздаване
                Board board = new Board(1);

                board.HandingOutCards(deckOfCards.OneHandingOutCards, opponent, player, 1);
                Console.Write(string.Format($"{player.Name} cards: {string.Join(", ", player.CardsPlayer)}"));
                Console.WriteLine();
                Console.Write(string.Format($"{opponent.Name} cards: {string.Join(", ", opponent.CardsPlayer)}"));
                Console.WriteLine();

                // Игра
                Card openTrumpCard = deckOfCards.GetTrumpCard();
                // премахване на havePlayerSixtySixPonts за сметка на player.Points

                //bool havePlayerSixtySixPonts = false;

                Check check = new Check();

                while (board.Turns <= 12)
                {
                    Console.WriteLine($"Turn {board.Turns}:");
                    Console.WriteLine($"OpenTrumpCard: {openTrumpCard.ToString()}");
                    Console.WriteLine();
                    Card cardOnOpponentForThisTurn = null;
                    Card cardOnPlayerForThisTurn = null;

                    if (opponent.IsFirstPlay == true)
                    {
                        OpponentStrategyWhenGameFirst opponentStrategyWhenGameFirst =
                            new OpponentStrategyWhenGameFirst(board.Turns);
                        
                        cardOnOpponentForThisTurn = opponentStrategyWhenGameFirst.PlayCard(opponent, player,
                                openTrumpCard, check, deckOfCards);

                        // Промяна
                        if (opponent.Points >= 66)
                        {
                            check.CheckWhenParticipantHaveSixtySix(opponent, player);
                            break;
                        }

                        Console.WriteLine($"The opponent playing: {cardOnOpponentForThisTurn.ToString()}");
                        cardOnPlayerForThisTurn = board.Turns > 6 ? 
                            CardPlayedResponsByPlayerNoDeckOfCards(cardOnOpponentForThisTurn, player.CardsPlayer, openTrumpCard) : 
                            PlayerResponseCard(player.CardsPlayer, openTrumpCard, cardOnOpponentForThisTurn);
                        Console.WriteLine($"{player.Name} playing: {cardOnPlayerForThisTurn.ToString()}");
                        check.CheckWinnerTurn(opponent, player, cardOnOpponentForThisTurn, cardOnPlayerForThisTurn, 
                            openTrumpCard, deckOfCards);
                    }

                    else
                    //player.IsFirstPlay == true;
                    {
                        Card card40 = null;
                        Card card20 = null;
                        if (board.Turns == 1)
                        {
                            cardOnPlayerForThisTurn = PlayerResponseCard(player.CardsPlayer, 
                                openTrumpCard, cardOnOpponentForThisTurn);
                            Console.WriteLine($"{player.Name} playing: {cardOnPlayerForThisTurn.ToString()}");
                        }

                        else if (board.Turns >= 2 && board.Turns <= 5)                        
                        {
                            // 9+, 40, 20, PlayerFirst(нов)
                            openTrumpCard = check.CheckPayerHaveNineTrump(player.CardsPlayer, openTrumpCard);

                            //card40 = check.CheckForForty(player, openTrumpCard);
                            //card20 = check.CheckForTwenty(player, openTrumpCard);

                            cardOnPlayerForThisTurn = PlayerResponseCard(player.CardsPlayer, openTrumpCard, cardOnOpponentForThisTurn);
                        }

                        else if (board.Turns == 6)
                        {
                            // 40, 20, PlayerFiers(нов)
                        }

                        else
                        {
                            // 40, 20
                        }

                        if (player.Points >= 66)
                        {
                            check.CheckWhenParticipantHaveSixtySix(player, opponent);
                            break;
                        }
                    }

                    if (opponent.Points >= 66 || player.Points >= 66)
                    {
                        CheckFor66(opponent, player, check);
                        break;
                    }

                    if (board.Turns < 7)
                    {
                        deckOfCards.TakeCards(opponent, player, openTrumpCard);
                    }

                    if (check.CheckForCloseOfDeckOfCardsFromOpponent(opponent, openTrumpCard) == true)
                    {
                        opponent.HasClosedTheDeckOfCards = true;
                        board.Turns = 6;
                    }          
                    // Същата проверка за Player

                    board.Turns++;
                }
            }

            Console.WriteLine($"{opponent.Name} : {player.Name} - {opponent.Games} : {player.Games}");
        }

        private static void CheckFor66(Player opponent, Player player, Check check)
        {
            if (opponent.Points >= 66)
            {
                check.CheckWhenParticipantHaveSixtySix(opponent, player);
            }

            else if (player.Points >= 66)
            {
                check.CheckWhenParticipantHaveSixtySix(player, opponent);
            }
        }

        private static Card CardPlayedResponsByPlayerNoDeckOfCards(Card cardPlayedByOpponent, 
            List<Card> cardsPlayer, Card openTrumpCard)
        {
            if (cardsPlayer.Count(a => a.Type == cardPlayedByOpponent.Type) > 0)
            {
                List<Card> cardsForAnswer = cardsPlayer.Where(a => a.Type == cardPlayedByOpponent.Type).ToList();
                return PlayerResponseCard(cardsForAnswer, openTrumpCard, cardPlayedByOpponent);                 
            }

            else if (cardPlayedByOpponent.Type != openTrumpCard.Type && 
                cardsPlayer.Count(a => a.Type == openTrumpCard.Type) > 0)
            {
                List<Card> cardsForAnswer = cardsPlayer.Where(a => a.Type == openTrumpCard.Type).ToList();
                return PlayerResponseCard(cardsForAnswer, openTrumpCard, cardPlayedByOpponent);
            }

            return PlayerResponseCard(cardsPlayer, openTrumpCard, cardPlayedByOpponent);
        }

        private static Card PlayerResponseCard(List<Card> cardsPlayer, Card openTrumpCard, Card cardPlayedByOpponent)
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
