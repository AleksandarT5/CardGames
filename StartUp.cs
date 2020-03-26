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
            Console.Write("Opponent's name: ");
            Player opponent = new Player(Console.ReadLine(), new List<Card>(), 0, 0, true, false);
            Console.Write("Your's name: ");
            Player player = new Player(Console.ReadLine(), new List<Card>(), 0, 0, false, false);
            Console.WriteLine();
            Check check = new Check();

            while (opponent.Games < 11 && player.Games < 11)
            {
                Console.WriteLine($"New Game\n{new string('*', 30)}");
                Board board = new Board(1);
                // Раздаване

                board.HandingOutCards(deckOfCards.OneHandingOutCards, opponent, player, 1);
                Console.Write($"{player.Name} cards: {string.Join(", ", player.CardsPlayer)}");
                Console.WriteLine();

                // Игра
                Card openTrumpCard = deckOfCards.GetTrumpCard();
                while (board.Turns <= 12)
                {
                    Console.WriteLine($"Turn {board.Turns}:");
                    Console.WriteLine($"OpenTrumpCard: {openTrumpCard.ToString()}");
                    Console.WriteLine();
                    Card cardOnOpponentForThisTurn = null;
                    Card cardOnPlayerForThisTurn = null;                    

                    if (opponent.IsFirstPlay == true)
                    {
                        check.CheckParticipantHasNineTrumpAndSwap(opponent.CardsPlayer, openTrumpCard, board.Turns);
                        
                        OpponentStrategyWhenGameFirst opponentStrategyWhenGameFirst =
                            new OpponentStrategyWhenGameFirst(board.Turns);

                        cardOnOpponentForThisTurn = opponentStrategyWhenGameFirst.PlayCard(opponent, player,
                                openTrumpCard, check, deckOfCards);

                        if (opponent.Points >= 66)
                        {
                            check.CalculationsWhenParticipantHasSixtySix(opponent, player);
                            break;
                        }

                        Console.WriteLine($"The {opponent.Name} playing: {cardOnOpponentForThisTurn.ToString()}");
                        cardOnPlayerForThisTurn = board.Turns > 6 ?
                            check.PlayerAnswersWhenDeckOfCardsIsOver(cardOnOpponentForThisTurn, player.CardsPlayer, openTrumpCard) :
                            check.DeterminingTheCardOfThePlayer(player.CardsPlayer);
                        Console.WriteLine($"{player.Name} playing: {cardOnPlayerForThisTurn.ToString()}");                        
                    }

                    else
                    {
                        check.CheckParticipantHasNineTrumpAndSwap(player.CardsPlayer, openTrumpCard, board.Turns);

                        PlayerStrategyWhenGameFirst playerStrategyWhenGameFirst =
                                new PlayerStrategyWhenGameFirst(board.Turns);
                        cardOnPlayerForThisTurn = playerStrategyWhenGameFirst.PlayCard(opponent, player,
                            openTrumpCard, check, deckOfCards);

                        if (player.Points >= 66)
                        {
                            check.CalculationsWhenParticipantHasSixtySix(player, opponent);
                            break;
                        }

                        Console.WriteLine($"{player.Name} playing: {cardOnPlayerForThisTurn.ToString()}");

                        OpponentStrategyWhenAnswer opponentStrategyWhenAnswer =
                            new OpponentStrategyWhenAnswer(board.Turns);
                        cardOnOpponentForThisTurn = opponentStrategyWhenAnswer.PlayCard(opponent, player,
                            cardOnPlayerForThisTurn, openTrumpCard, check);
                        Console.WriteLine($"{opponent.Name} playing: {cardOnOpponentForThisTurn.ToString()}");
                    }

                    check.CheckWhoIsTheWinnerInTheTurn(opponent, player, cardOnOpponentForThisTurn, cardOnPlayerForThisTurn,
                            openTrumpCard, deckOfCards);
                    
                    Console.WriteLine($"{player.Name}: {player.Points}");
                    Console.WriteLine($"{opponent.Name}: {opponent.Points}");
                    Console.WriteLine(new string('-', 30));
                    
                    if (opponent.Points >= 66 || player.Points >= 66)
                    {
                        check.CheckFor66(opponent, player);
                        break;
                    }

                    check.CheckAfterTheTurnIsOver(opponent, player, openTrumpCard, deckOfCards, board);
                }

                check.CalculationsAfter12Tour(player, opponent, openTrumpCard, deckOfCards);
                // openTrumpCard в DeckOfCards ???                
            }

            Console.WriteLine(player.Games >= 11 ? check.PrintFinalResult(player, opponent, player) 
                : check.PrintFinalResult(opponent, player, player));
        }
    }
}
