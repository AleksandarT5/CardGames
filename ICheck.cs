using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    interface ICheck
    {
        //List<Card> CheckingIfTheParticipantHasNineTrump(List<Card> playerCards, Card openTrumpCard);

        Card CheckForTwenty(Player player, Card openTrumpCard);

        Card CheckForForty(Player player, Card openTrumpCard);

        int CheckFor40And20(Player player, Card openTrumpCard, Card playerCard);

        void CalculationsWhenParticipantHasSixtySix(Player participant, Player theOtherParticipant);

        bool CheckForClosingDeckOfCardsByTheOpponent(Player player, Card openTrumpCard);

        Card CheckForTheWeakestCard(List<Card> opponentCards, Card openTrumpCard);

        Card CheckCards(List<Card> opponentCardsNoTrumps, Card openTrumpCard, string[] values, int number);

        void CheckWhoIsTheWinnerInTheTurn(Player opponent, Player player, Card opponentCard, Card myCard,
            Card openTrumpCard, DeckOfCards deckOfCards);

        Card PlayerAnswersWhenDeckOfCardsIsOver(Card cardPlayedByOpponent, List<Card> cardsPlayer,
            Card openTrumpCard);

        Card DeterminingTheCardOfThePlayer(List<Card> cardsPlayer);

        void CheckFor66(Player opponent, Player player);

        void CalculationsAfter12Tour(Player player, Player opponent, Card openTrumpCard, DeckOfCards deckOfCards);

        string PrintFinalResult(Player winner, Player loser, Player player);
    }
}
