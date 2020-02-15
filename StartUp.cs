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
                Console.Write($"{player.Name} cards: {string.Join(", ", player.CardsPlayer)}");
                Console.WriteLine();
                Console.Write($"{opponent.Name} cards: {string.Join(", ", opponent.CardsPlayer)}");
                Console.WriteLine();

                // Игра
                Card openTrumpCard = deckOfCards.GetTrumpCard();

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
                            check.CardPlayedAnswerByPlayerNoDeckOfCards(cardOnOpponentForThisTurn, player.CardsPlayer, openTrumpCard) :
                            check.DeterminingThePlayerCard(player.CardsPlayer, openTrumpCard);
                        Console.WriteLine($"{player.Name} playing: {cardOnPlayerForThisTurn.ToString()}");
                        check.CheckWinnerTurn(opponent, player, cardOnOpponentForThisTurn, cardOnPlayerForThisTurn,
                            openTrumpCard, deckOfCards);
                    }

                    else
                    {
                        PlayerStrategyWhenGameFirst playerStrategyWhenGameFirst =
                                new PlayerStrategyWhenGameFirst(board.Turns);
                        cardOnPlayerForThisTurn = playerStrategyWhenGameFirst.PlayCard(opponent, player,
                            openTrumpCard, check, deckOfCards);

                        if (player.Points >= 66)
                        {
                            check.CheckWhenParticipantHaveSixtySix(player, opponent);
                            break;
                        }

                        Console.WriteLine($"{player.Name} playing: {cardOnPlayerForThisTurn.ToString()}");

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
                    // Проверка за Player за затваряне
                    // ???

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
    }
}
