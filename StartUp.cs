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

            Player opponent = new Player(Console.ReadLine(), new List<Card>(), 0, 0, true);
            Player player = new Player(Console.ReadLine(), new List<Card>(), 0, 0, false);

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

                Console.WriteLine($"OpenTrumpCard: {openTrumpCard.ToString()}");
                Check check = new Check();

                while (board.Turns <= 12)
                {
                    Console.WriteLine($"Turn {board.Turns}:");
                    Card cardPlayedByOpponent = null;
                    Card cardPlayedByPlayer = null;
                    bool havePlayerSixtySixPonts = false;

                    if (opponent.IsFirstPlay == true)
                    {
                        if (board.Turns == 1)
                        {
                            OpponentStrategyWhenGameFirst opponentStrategyWhenGameFirst = 
                                new OpponentStrategyWhenGameFirst(new FirstPlayOpponentFirstTour());
                            cardPlayedByOpponent = opponentStrategyWhenGameFirst.PlayCard(opponent, player, 
                                openTrumpCard,havePlayerSixtySixPonts, check, deckOfCards);                            
                        }

                        else if(board.Turns >= 2 && board.Turns <= 5)
                        {
                            OpponentStrategyWhenGameFirst opponentStrategyWhenGameFirst =
                                new OpponentStrategyWhenGameFirst(new FirstPlayOpponentSecondToFifthTours());
                            cardPlayedByOpponent = opponentStrategyWhenGameFirst.PlayCard(opponent, player,
                                openTrumpCard, havePlayerSixtySixPonts, check, deckOfCards);
                        }

                        else if (board.Turns == 6)
                        {
                            OpponentStrategyWhenGameFirst opponentStrategyWhenGameFirst =
                                new OpponentStrategyWhenGameFirst(new FirstPlayOpponentSixTour());
                            cardPlayedByOpponent = opponentStrategyWhenGameFirst.PlayCard(opponent, player,
                                openTrumpCard, havePlayerSixtySixPonts, check, deckOfCards);
                        }

                        else
                        {
                            OpponentStrategyWhenGameFirst opponentStrategyWhenGameFirst =
                                new OpponentStrategyWhenGameFirst(new FirstPlayOpponentSeventhToTwelfthTours());
                            cardPlayedByOpponent = opponentStrategyWhenGameFirst.PlayCard(opponent, player,
                                openTrumpCard, havePlayerSixtySixPonts, check, deckOfCards);                            
                        }

                        Console.WriteLine($"The opponent playing: {cardPlayedByOpponent.ToString()}");
                        cardPlayedByPlayer = board.Turns > 6 ? 
                            CardPlayedAnswerByPlayerNoDeckOfCards(cardPlayedByOpponent, player.CardsPlayer, openTrumpCard) : 
                            CardPlayedAnswerByPlayer(player.CardsPlayer, openTrumpCard, cardPlayedByOpponent);
                        Console.WriteLine($"{player.Name} playing: {cardPlayedByPlayer.ToString()}");
                        check.CheckWinnerTurn(opponent, player, cardPlayedByOpponent, cardPlayedByPlayer, openTrumpCard);
                    }

                    else
                    //player.IsFirstPlay == true;

                    {
                        if (board.Turns == 1)
                        {
                            cardPlayedByPlayer = CardPlayedAnswerByPlayer(player.CardsPlayer, 
                                openTrumpCard, cardPlayedByOpponent);
                            Console.WriteLine($"{player.Name} playing: {cardPlayedByPlayer.ToString()}");
                        }

                        else if (board.Turns >= 2 && board.Turns <= 5)                        
                        {
                            // 9, 40, 20
                           //var cardAfterCheck = 
                            check.CheckPayerHaveNineTrump(player.CardsPlayer, openTrumpCard);
                           //openTrumpCard = cardAfterCheck;
                        }

                        else if (board.Turns == 6)
                        {
                            // 40, 20
                        }

                        else
                        {
                            // 40, 20
                        }
                    }
                    if (havePlayerSixtySixPonts == true)
                    {

                    }

                    deckOfCards.TakeCards(opponent, player, openTrumpCard);                   
                    board.Turns++;
                }
            }

            Console.WriteLine($"{opponent.Name} : {player.Name} - {opponent.Games} : {player.Games}");
        }

        private static Card CardPlayedAnswerByPlayerNoDeckOfCards(Card cardPlayedByOpponent, 
            List<Card> cardsPlayer, Card openTrumpCard)
        {
            if (cardsPlayer.Count(a => a.Type == cardPlayedByOpponent.Type) > 0)
            {
                List<Card> cardsForAnswer = cardsPlayer.Where(a => a.Type == cardPlayedByOpponent.Type).ToList();
                return CardPlayedAnswerByPlayer(cardsForAnswer, openTrumpCard, cardPlayedByOpponent);                 
            }

            else if (cardPlayedByOpponent.Type == openTrumpCard.Type)
            {
                if (cardsPlayer.Count(a => a.Type == openTrumpCard.Type) > 0)
                {
                    List<Card> cardsForAnswer = cardsPlayer.Where(a => a.Type == openTrumpCard.Type).ToList();
                    return CardPlayedAnswerByPlayer(cardsForAnswer, openTrumpCard, cardPlayedByOpponent);
                }

                else
                {
                    return CardPlayedAnswerByPlayer(cardsPlayer, openTrumpCard, cardPlayedByOpponent);
                }

            }

            else
            {
                List<Card> cardsForAnswer = cardsPlayer.Where(a => a.Type == openTrumpCard.Type).ToList();

            }

            throw new NotImplementedException();
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
                    }
                }
            }

            return playerAnswerCard;
        }
    }
}
