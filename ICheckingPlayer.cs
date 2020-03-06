using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    interface ICheckingPlayer
    {
        int CheckingFor40and20(Player player, Card openTrumpCard, Card playerCard);

        Card CardPlayedAnswerByPlayerNoDeckOfCards(Card cardPlayedByOpponent, List<Card> cardsPlayer,
            Card openTrumpCard);

        Card DeterminingThePlayerCard(List<Card> cardsPlayer);

    }
}
