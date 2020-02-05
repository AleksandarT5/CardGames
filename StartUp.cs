using System;
using System.Collections.Generic;

namespace Santase
{
    class StartUp
    {
        static void Main(string[] args)
        {
            // Пълнене на тестето и разбъркване на картите
            DeckOfCards deckOfCards = new DeckOfCards();

            Player opponent = new Player(Console.ReadLine(), new List<Card>(), 0, 0, true);
            Player player = new Player(Console.ReadLine(), new List<Card>(), 0, 0, false);

            while (true)
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

                Console.WriteLine($"OpenTrumpCard: {openTrumpCard.ToString()}");
                Check check = new Check();

                while (board.Turns <= 12)
                {
                    Console.WriteLine($"Turn {board.Turns}:");
                    Card cardPlayedByOpponent = null;

                    //
                    Console.WriteLine(string.Format($"Player cards: {string.Join(", ", player.CardsPlayer)}"));
                    Console.WriteLine(string.Format($"Opponent cards: {string.Join(", ", opponent.CardsPlayer)}"));
                    //

                    if (opponent.IsFirstPlay == true)
                    {
                        bool havePlayerSixtySixPonts = false;
                        if (board.Turns == 1)
                        {
                            OpponentStrategyWhenGameFirst opponentStrategyWhenGameFirst = 
                                new OpponentStrategyWhenGameFirst(new FirstPlayOpponentFirstTour());
                            cardPlayedByOpponent = opponentStrategyWhenGameFirst.PlayCard(opponent, player, 
                                openTrumpCard,havePlayerSixtySixPonts, check);

                            //Console.WriteLine($"The opponent playing: {cardPlayedByOpponent.ToString()}");
                            //Console.Write(string.Format($"{opponent.Name} cards: {string.Join(", ", opponent.CardsPlayer)}"));
                            //Console.WriteLine();
                        }

                        else if(board.Turns >= 2 && board.Turns <= 5)
                        {
                            OpponentStrategyWhenGameFirst opponentStrategyWhenGameFirst =
                                new OpponentStrategyWhenGameFirst(new FirstPlayOpponentSecondToFifthTours());
                            cardPlayedByOpponent = opponentStrategyWhenGameFirst.PlayCard(opponent, player,
                                openTrumpCard, havePlayerSixtySixPonts, check);

                            //Console.WriteLine($"The opponent playing: {cardPlayedByOpponent.ToString()}");
                            //Console.Write(string.Format($"{opponent.Name} cards: {string.Join(", ", opponent.CardsPlayer)}"));
                            //Console.WriteLine();
                        }

                        else if (board.Turns == 6)
                        {
                            OpponentStrategyWhenGameFirst opponentStrategyWhenGameFirst =
                                new OpponentStrategyWhenGameFirst(new FirstPlayOpponentSixTour());
                            cardPlayedByOpponent = opponentStrategyWhenGameFirst.PlayCard(opponent, player,
                                openTrumpCard, havePlayerSixtySixPonts, check);

                            //Console.WriteLine($"The opponent playing: {cardPlayedByOpponent.ToString()}");
                            //Console.Write(string.Format($"{opponent.Name} cards: {string.Join(", ", opponent.CardsPlayer)}"));
                            //Console.WriteLine();
                        }

                        else
                        {
                            OpponentStrategyWhenGameFirst opponentStrategyWhenGameFirst =
                                new OpponentStrategyWhenGameFirst(new FirstPlayOpponentSeventhToTwelfthTours());
                            cardPlayedByOpponent = opponentStrategyWhenGameFirst.PlayCard(opponent, player,
                                openTrumpCard, havePlayerSixtySixPonts, check);
                        }

                        Console.WriteLine($"The opponent playing: {cardPlayedByOpponent.ToString()}");
                        //Console.Write(string.Format($"{opponent.Name} cards: {string.Join(", ", opponent.CardsPlayer)}"));
                        //Console.WriteLine();
                        Card playerAnswerCard = CardPlayedAnswerByPlayer(player.CardsPlayer, openTrumpCard, cardPlayedByOpponent);
                        Console.WriteLine($"{player.Name} playing: {playerAnswerCard.ToString()}");
                        check.CheckWinnerTurn(opponent, player, cardPlayedByOpponent, playerAnswerCard, openTrumpCard);
                    }

                    else
                    {
                        //player.IsFirstPlay == true;
                        if (board.Turns == 1)
                        {

                        }

                        else if (board.Turns >= 2 && board.Turns <= 5)                        
                        {

                        }

                        else if (board.Turns == 6)
                        {

                        }

                        else
                        {

                        }
                    }

                    deckOfCards.TakeCards(opponent, player, openTrumpCard);                   
                    board.Turns++;
                }
            }
        }
        
        static Card CardPlayedAnswerByPlayer(List<Card> cardsPlayer, Card openTrumpCard, Card cardPlayedByOpponent)
        {
            Card playerAnswerCard = null;

            while (playerAnswerCard == null)
            {
                string typeCard = Console.ReadLine();
                string valueCard = Console.ReadLine();
                foreach (var oneCard in cardsPlayer)
                {
                    if (oneCard.Type == typeCard && oneCard.Value == valueCard)
                    {
                        playerAnswerCard = oneCard;
                        cardsPlayer.Remove(playerAnswerCard);
                        break;
                        //тест
                    }
                }
            }

            return playerAnswerCard;
        }
    }
}
