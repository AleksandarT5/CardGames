using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayPlayerSecondToFifthTours : StrategyPlayerFirst
    {
        public override Card PlayerPlayFirst(Player player, Player opponent, Card openTrumpCard, 
            Check check, DeckOfCards deckOfCards)
        {
            Card card = null;
            openTrumpCard = check.CheckPayerHaveNineTrump(player.CardsPlayer, openTrumpCard);
            // PlayerCard = PlayerPlayFirt(player.Cards, openTrumpCard)
            // Проверка за 40:
            // PlayerCard == Card(openT.Type, K) && Player.Cards.Contains(openT, D) => + 40
            // Проверка за 20:
            // PlayerCard == Card(Player.Card.T, K) && Player.Cards.Contains(Player.Cards.T, D) => +20
            card = check.CheckFor40and20(player, openTrumpCard);
            if (card != null)
            {
                return card;
            }
            // check card40 == игранатаКарта => +40 точки за player.Points
            return card;
        }
    }
}
