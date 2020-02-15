using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    interface ICheck
    {
        Card CheckPayerHaveNineTrump(List<Card> playerCards, Card openTrumpCard);
        
        Card CheckForTwenty(Player player, Card openTrumpCard);

        Card CheckForForty(Player player, Card openTrumpCard);

        void CheckFor40and20(Player player, Card openTrumpCard, Card playerCard);

        void CheckWhenParticipantHaveSixtySix(Player player, Player theOtherParticipant);

        bool CheckForCloseOfDeckOfCardsFromOpponent(Player player, Card openTrumpCard);

        Card CheckForWeakCard(List<Card> opponentCards, Card openTrumpCard);

        Card CheckCards(List<Card> opponentCardsNoTrumps, Card openTrumpCard, string[] values, int number);

        void CheckWinnerTurn(Player opponent, Player player, Card opponentCard, Card myCard, 
            Card openTrumpCard, DeckOfCards deckOfCards);

        Card CardPlayedAnswerByPlayerNoDeckOfCards(Card cardPlayedByOpponent, List<Card> cardsPlayer,
            Card openTrumpCard);

        Card DeterminingThePlayerCard(List<Card> cardsPlayer, Card openTrumpCard);
    }
}
