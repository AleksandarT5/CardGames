using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    interface ICheck
    {
        List<Card> CheckingIfTheParticipantHasNineTrump(List<Card> playerCards, Card openTrumpCard);
        
        Card CheckingForTwenty(Player player, Card openTrumpCard);

        Card CheckingForForty(Player player, Card openTrumpCard);

        //int CheckingFor40and20(Player player, Card openTrumpCard, Card playerCard);

        void CalculationsWhenParticipantHaveSixtySix(Player participant, Player theOtherParticipant);

        bool CheckingForCloseOfDeckOfCardsFromOpponent(Player player, Card openTrumpCard);

        Card CheckingForWeakCard(List<Card> opponentCards, Card openTrumpCard);

        Card CheckingCards(List<Card> opponentCardsNoTrumps, Card openTrumpCard, string[] values, int number);

        void CheckingWhoIsTheWinnerInTheTurn(Player opponent, Player player, Card opponentCard, Card myCard, 
            Card openTrumpCard, DeckOfCards deckOfCards);

        //Card CardPlayedAnswerByPlayerNoDeckOfCards(Card cardPlayedByOpponent, List<Card> cardsPlayer,
        //    Card openTrumpCard);

        //Card DeterminingThePlayerCard(List<Card> cardsPlayer);

        void CheckFor66(Player opponent, Player player);

        void CalculationsAfter12Tour(Player player, Player opponent);
    }
}
