using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Santase
{
    internal class DeckOfCards : IDeckOfCards
    {
        private string[] valuesCards = { "9", "J", "D", "K", "10", "A" };

        public List<Card> GameCards { get; set; } = new List<Card>();
        public Card OpenTrumpCard { get; set; }

        public List<Card> PlayedCards { get; set; } = new List<Card>();

        public DeckOfCards()
        {
            CreateDeck();
        }

        private void CreateDeck()
        {
            FillDeckOfCards(this.GameCards, this.valuesCards);
            FillDeck(this.GameCards);
        }


        public List<Card> FillDeckOfCards(List<Card> cards, string[] valuesCards)
        {
            for (int i = 0; i < valuesCards.Length; i++)
            {
                cards.Add(new Card(TypeCard.Club.ToString(), valuesCards[i], 0));
                cards.Add(new Card(TypeCard.Diamond.ToString(), valuesCards[i], 0));
                cards.Add(new Card(TypeCard.Heart.ToString(), valuesCards[i], 0));
                cards.Add(new Card(TypeCard.Spade.ToString(), valuesCards[i], 0));
            }

            return cards;
        }

        public void FillDeck(List<Card> deckOfCards)
        {
            Random random = new Random();
            for (int i = 0; i < 24; i++)
            {
                int cardNumber = random.Next(0, deckOfCards.Count);
                var newPosition = deckOfCards[cardNumber];
                deckOfCards[cardNumber] = deckOfCards[i];
                deckOfCards[i] = newPosition;
            }
        }

        public void OneHandingOutCards(Player participant, Player secondParticipant, int count = 1)
        {
            var current = count % 2 == 0 ? secondParticipant : participant;
            HandingOutCardHand(this.GameCards, current);
            if (count < 4)
            {
                count += 1;
                OneHandingOutCards(participant, secondParticipant, count);
            }
            participant.CardsPlayer = participant.CardsPlayer.OrderBy(t => t.Type).ThenByDescending(v => v.Value).ToList();
            //Console.WriteLine(string.Format($"Opponent cards: {string.Join(", ", participant.CardsPlayer)}"));

            secondParticipant.CardsPlayer = secondParticipant.CardsPlayer.OrderBy(t => t.Type).ThenByDescending(v => v.Value).ToList();
            //Console.WriteLine(string.Format($"Player cards: {string.Join(", ", secondParticipant.CardsPlayer)}"));
        }

        private void HandingOutCardHand(List<Card> basicDeckOfCards, Player participant)
        {
            for (int i = 0; i < 3; i++)
            {
                Card oneCard = basicDeckOfCards[basicDeckOfCards.Count - 1];
                participant.CardsPlayer.Add(oneCard);
                basicDeckOfCards.Remove(oneCard);
            }
        }

        public Card GetTrumpCard()
        {
            var card = this.GameCards[this.GameCards.Count - 1];
            this.GameCards.RemoveAt(this.GameCards.Count - 1);
            return card;
        }

        public void TakeCards(Player opponent, Player player, Card openTrumpCard)
        {
            if (opponent.IsFirstPlay == true)
            {
                TakeCardsFromTheBase(opponent, player, this.GameCards, openTrumpCard);
            }

            else
            {
                TakeCardsFromTheBase(player, opponent, this.GameCards, openTrumpCard);
            }

        }

        private void TakeCardsFromTheBase(Player winner, Player lost, List<Card> basicCards, Card openTrumpCard)
        {
            winner.CardsPlayer.Add(basicCards[basicCards.Count - 1]);
            basicCards.Remove(basicCards[basicCards.Count - 1]);
            winner.CardsPlayer = winner.CardsPlayer.OrderBy(t => t.Type).ThenByDescending(v => v.Value).ToList();
            Console.WriteLine($"{winner.Name} cards: {string.Join(", ", winner.CardsPlayer)}");
            if (basicCards.Count == 0)
            {
                lost.CardsPlayer.Add(openTrumpCard);
            }

            else
            {
                lost.CardsPlayer.Add(basicCards[basicCards.Count - 1]);
                basicCards.Remove(basicCards[basicCards.Count - 1]);
            }
            lost.CardsPlayer = lost.CardsPlayer.OrderBy(t => t.Type).ThenByDescending(v => v.Value).ToList();
            Console.WriteLine(string.Format($"{lost.Name} cards: {string.Join(", ", lost.CardsPlayer)}"));
        }        
    }
}
