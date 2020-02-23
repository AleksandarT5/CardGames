using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    interface ICheck
    {
        void CheckPayerHaveNineTrump(List<Card> playerCards, Card openTrumpCard);
        
        Card CheckingForTwenty(Player player, Card openTrumpCard);

        Card CheckingForForty(Player player, Card openTrumpCard);

        int CheckFor40and20(Player player, Card openTrumpCard, Card playerCard);

        void CheckWhenParticipantHaveSixtySix(Player player, Player theOtherParticipant);

        bool CheckForCloseOfDeckOfCardsFromOpponent(Player player, Card openTrumpCard);

        Card CheckForWeakCard(List<Card> opponentCards, Card openTrumpCard);

        Card CheckCards(List<Card> opponentCardsNoTrumps, Card openTrumpCard, string[] values, int number);

        void CheckWinnerTurn(Player opponent, Player player, Card opponentCard, Card myCard, 
            Card openTrumpCard, DeckOfCards deckOfCards);

        Card CardPlayedAnswerByPlayerNoDeckOfCards(Card cardPlayedByOpponent, List<Card> cardsPlayer,
            Card openTrumpCard);

        Card DeterminingThePlayerCard(List<Card> cardsPlayer);
    }
}
