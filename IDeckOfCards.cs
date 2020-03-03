using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    interface IDeckOfCards
    {
        //Card OpenTrumpCard { get; set; }

        List<Card> FillDeckOfCards(List<Card> cards, string[] valuesCards);

        void FillDeck(List<Card> cards);

        void OneHandingOutCards(Player participant, Player secondParticipant, int count = 1);

        Card GetTrumpCard();

        void TakeCards(Player opponent, Player player, Card openTrumpCard);
    }
}
