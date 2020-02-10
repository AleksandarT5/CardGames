using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    interface ICheck
    {
        void CheckPayerHaveNineTrump(List<Card> playerCards, Card openTrumpCard);
        
        Card CheckForTwenty(Player player, Card openTrumpCard, Player theOtherParticipant, bool havePlayerSixtySixPonts);

        Card CheckForFourty(Player player, Card openTrumpCard, Player theOtherParticipant, bool havePlayerSixtySixPonts);

        void CheckWhenPlayerHaveSixtySix(Player player, Player theOtherParticipant);

        bool CheckForCloseOfDeckOfCards(Player player, Card openTrumpCard);

        Card CheckForATrump(Player player, Card openTrumpCard);

        Card CheckFor10Trump(List<Card> playerCards, Card openTrumpCard, DeckOfCards deckOfCards);

        Card CheckForCard(List<Card> opponentCards, Card openTrumpCard);

        Card CheckCards(List<Card> opponentCardsNoTrumps, Card openTrumpCard, Card card, string[] values, int number);

        void CheckWinnerTurn(Player opponent, Player player, Card opponentCard, Card myCard, Card openTrumpCard);

        Card CheckTrump(List<Card> opponentCards, Card openTrumpCard, DeckOfCards deckOfCards);
    }
}
