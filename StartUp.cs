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
                        check.CheckPayerHaveNineTrump(opponent.CardsPlayer, openTrumpCard, board.Turns);
                        //if (board.Turns >= 2 && board.Turns <= 5)
                        //{
                        //    opponent.CardsPlayer = check.NineTrump(opponent.CardsPlayer, openTrumpCard);
                        //    Console.WriteLine($"OpenTrumpCard: {openTrumpCard.ToString()}");
                        //    Console.WriteLine($"{opponent.Name} cards: {string.Join(", ", opponent.CardsPlayer)}");
                        //}

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
                        check.CheckPayerHaveNineTrump(player.CardsPlayer, openTrumpCard, board.Turns);
                        //if (board.Turns >= 2 && board.Turns <= 5)
                        //{
                        //    player.CardsPlayer = check.NineTrump(player.CardsPlayer, openTrumpCard);
                        //    Console.WriteLine($"OpenTrumpCard: {openTrumpCard.ToString()}");
                        //    Console.WriteLine($"{player.Name} cards: {string.Join(", ", player.CardsPlayer)}");
                        //}

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


                    if ((board.Turns >= 2 && board.Turns <= 5) && openTrumpCard.Value != "9" && (opponent
                        .CardsPlayer.Contains(openTrumpCard) || player.CardsPlayer.Contains(openTrumpCard)))
                    {
                        openTrumpCard.Value = "9";
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

                    //if (opponent.IsFirstPlay == true 
                    //    && check.CheckForCloseOfDeckOfCardsFromOpponent(opponent, openTrumpCard) == true)
                    //{
                    //    opponent.HasClosedTheDeckOfCards = true;
                    //    board.Turns = 6;
                    //}
                    board.Turns = check.CheckForCloseDeckOfCards(opponent, player, openTrumpCard, board.Turns);
                    //if ((board.Turns >= 1 && board.Turns <= 4) && player.IsFirstPlay == true)
                    //{
                    //    Console.Write($"Will the {player.Name} close Deck of Cards(for \"Yes\", write \"Y\"):");
                    //    string answer = Console.ReadLine();
                    //    if (answer == "Y")
                    //    {
                    //        player.HasClosedTheDeckOfCards = true;
                    //        board.Turns = 6;
                    //    }
                    //}

                    Console.WriteLine($"{player.Name} cards: {string.Join(", ", player.CardsPlayer)}");
                    Console.WriteLine($"{opponent.Name} cards: {string.Join(", ", opponent.CardsPlayer)}");

                    board.Turns++;
                }

                check.CheckAfter12Tour(player, opponent);
            }

            Console.WriteLine($"{opponent.Name} : {player.Name} - {opponent.Games} : {player.Games}");
        }

                
    }
}
