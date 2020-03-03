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
                if (opponent.Games != 0 || player.Games != 0)
                {
                    deckOfCards.ReturnTheCardsToTheDeck(player, opponent);
                }
                // Раздаване
                Board board = new Board(1);

                board.HandingOutCards(deckOfCards.OneHandingOutCards, opponent, player, 1);
                Console.Write($"{player.Name} cards: {string.Join(", ", player.CardsPlayer)}");
                Console.WriteLine();
                Console.Write($"{opponent.Name} cards: {string.Join(", ", opponent.CardsPlayer)}");
                Console.WriteLine();
                Console.Write($"Cards: {string.Join(", ", deckOfCards.GameCards)}");
                Console.WriteLine();

                // Игра
                Card openTrumpCard = deckOfCards.GetTrumpCard();
                
                Check check = new Check();

                while (board.Turns <= 12)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Turn {board.Turns}:");
                    Console.WriteLine($"OpenTrumpCard: {openTrumpCard.ToString()}");
                    Console.WriteLine();
                    Card cardOnOpponentForThisTurn = null;
                    Card cardOnPlayerForThisTurn = null;                    

                    if (opponent.IsFirstPlay == true)
                    {
                        check.CheckParticianHaveNineTrump(opponent.CardsPlayer, openTrumpCard, board.Turns);

                        OpponentStrategyWhenGameFirst opponentStrategyWhenGameFirst =
                            new OpponentStrategyWhenGameFirst(board.Turns);

                        cardOnOpponentForThisTurn = opponentStrategyWhenGameFirst.PlayCard(opponent, player,
                                openTrumpCard, check, deckOfCards);

                        if (opponent.Points >= 66)
                        {
                            check.CalculationsWhenParticipantHaveSixtySix(opponent, player);
                            break;
                        }

                        Console.WriteLine($"The {opponent.Name} playing: {cardOnOpponentForThisTurn.ToString()}");
                        cardOnPlayerForThisTurn = board.Turns > 6 ?
                            check.CardPlayedAnswerByPlayerNoDeckOfCards(cardOnOpponentForThisTurn, player.CardsPlayer, openTrumpCard) :
                            check.DeterminingThePlayerCard(player.CardsPlayer);
                        Console.WriteLine($"{player.Name} playing: {cardOnPlayerForThisTurn.ToString()}");                        
                    }

                    else
                    {
                        check.CheckParticianHaveNineTrump(player.CardsPlayer, openTrumpCard, board.Turns);

                        PlayerStrategyWhenGameFirst playerStrategyWhenGameFirst =
                                new PlayerStrategyWhenGameFirst(board.Turns);
                        cardOnPlayerForThisTurn = playerStrategyWhenGameFirst.PlayCard(opponent, player,
                            openTrumpCard, check, deckOfCards);

                        if (player.Points >= 66)
                        {
                            check.CalculationsWhenParticipantHaveSixtySix(player, opponent);
                            break;
                        }

                        Console.WriteLine($"{player.Name} playing: {cardOnPlayerForThisTurn.ToString()}");

                        OpponentStrategyWhenAnswer opponentStrategyWhenAnswer =
                            new OpponentStrategyWhenAnswer(board.Turns);
                        cardOnOpponentForThisTurn = opponentStrategyWhenAnswer.PlayCard(opponent, player,
                            cardOnPlayerForThisTurn, openTrumpCard, check);
                        Console.WriteLine($"{opponent.Name} playing: {cardOnOpponentForThisTurn.ToString()}");
                    }

                    check.CheckWinnerTurn(opponent, player, cardOnOpponentForThisTurn, cardOnPlayerForThisTurn,
                            openTrumpCard, deckOfCards);
                    
                    Console.WriteLine($"{player.Name}: {player.Points}");
                    Console.WriteLine($"{opponent.Name}: {opponent.Points}");
                    
                    if (opponent.Points >= 66 || player.Points >= 66)
                    {
                        check.CheckFor66(opponent, player);
                        break;
                    }

                    if (board.Turns < 7)
                    {
                        deckOfCards.TakeCards(opponent, player, openTrumpCard);
                    }

                    Console.WriteLine($"{player.Name} cards: {string.Join(", ", player.CardsPlayer)}");
                    Console.WriteLine($"{opponent.Name} cards: {string.Join(", ", opponent.CardsPlayer)}");

                    board.Turns = check.CheckForCloseDeckOfCards(opponent, player, openTrumpCard, board.Turns);

                    board.Turns++;
                }

                check.CalculationsAfter12Tour(player, opponent);
                // openTrumpCard в DeckOfCards ???
                if (deckOfCards.GameCards.Count > 0)
                {
                    deckOfCards.GameCards.Add(openTrumpCard);
                    openTrumpCard = null;
                }

                //deckOfCards.ReturnTheCardsToTheDeck(player, opponent);
            }

            Console.WriteLine($"{opponent.Name} : {player.Name} - {opponent.Games} : {player.Games}");
        }

                
    }
}
