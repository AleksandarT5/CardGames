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

        Card CheckFor40and20(Player player, Card openTrumpCard);

        void CheckWhenParticipantHaveSixtySix(Player player, Player theOtherParticipant);

        bool CheckForCloseOfDeckOfCardsFromOpponent(Player player, Card openTrumpCard);

        Card CheckForWeakCard(List<Card> opponentCards, Card openTrumpCard);

        Card CheckCards(List<Card> opponentCardsNoTrumps, Card openTrumpCard, Card card, string[] values, int number);

        void CheckWinnerTurn(Player opponent, Player player, Card opponentCard, Card myCard, 
            Card openTrumpCard, DeckOfCards deckOfCards);

        Card CheckForStrongOrWeakCard(string keyWord, List<Card> opponentCards, Card openTrumpCard, DeckOfCards deckOfCards);
    }
}
