using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    interface ICheck
    {
        Card CheckForTwenty(Player player, Card openTrumpCard);

        Card CheckForFourty(Player player, Card openTrumpCard);

        Card CheckForCard(List<Card> opponentCards, Card openTrumpCard);

        Card CheckCards(List<Card> opponentCardsNoTrumps, Card openTrumpCard, Card card, string[] values, int number);

        void CheckWinnerTurn(Player opponent, Player player, Card opponentCard, Card myCard, Card openTrumpCard);

        void CheckPlayerHaveSixtySix(Player player);

        void CheckPayerHaveNineTrump(List<Card> playerCards, Card openTrumpCard);
    }
}
