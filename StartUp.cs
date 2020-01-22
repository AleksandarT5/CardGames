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
                Board board = new Board(1, true);

                board.HandingOutCards(deckOfCards.OneHandingOutCards, opponent, player, 1);
                Console.Write(string.Format($"{player.Name} cards: {string.Join(", ", player.CardsPlayer)}"));
                Console.WriteLine();

                // Игра
                Card openTrumpCard = deckOfCards.GetTrumpCard();

                Console.WriteLine($"OpenTrumpCard: {openTrumpCard.ToString()}");
                Check check = new Check();

                while (board.Turns <= 12)
                {
                    //STRATEGIES                                
                    if (board.ThereAreCardsInTheBase == true)
                    {
                        if (opponent.IsFirstPlay == true)
                        {
                            if (board.Turns != 1)
                            {

                            }

                            else
                            //board.Turns == 1
                            {

                            }

                            Card cardPlayedByOpponent = check.CheckForCard(opponent.CardsPlayer, openTrumpCard);
                            Console.WriteLine($"The opponent playing: {cardPlayedByOpponent}");
                            Card playerAnswerCard = CardPlayedAnswerByPlayer(player.CardsPlayer, openTrumpCard, cardPlayedByOpponent);
                            Console.WriteLine($"{player.Name} playing: {playerAnswerCard.ToString()}");
                            check.CheckWinnerTurn(opponent, player, cardPlayedByOpponent, playerAnswerCard, openTrumpCard);
                        }

                        else
                        {
                            //player.IsFirstPlay == true;
                            if (board.Turns != 1)
                            {

                            }

                            else
                            //board.Turns == 1
                            {

                            }
                        }

                        deckOfCards.TakeCards(opponent, player, openTrumpCard);
                    }

                    else
                    //board.ThereAreCardsInTheBase == false
                    {
                        
                    }

                    board.Turns++;
                }
            }
        }
        
        static Card CardPlayedAnswerByPlayer(List<Card> cardsPlayer, Card openTrumpCard, Card cardPlayedByOpponent)
        {
            Card playerAnswerCard = null;
            // ERROR - if
            while (playerAnswerCard == null)
            {
                string typeCard = Console.ReadLine();
                string valueCard = Console.ReadLine();
                foreach (var oneCard in cardsPlayer)
                {
                    if (oneCard.Type == typeCard && oneCard.Value == valueCard)
                    {
                        playerAnswerCard = oneCard;
                    }
                }
            }

            return playerAnswerCard;
        }
    }
}
