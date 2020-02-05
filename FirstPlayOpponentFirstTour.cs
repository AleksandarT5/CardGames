using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    class FirstPlayOpponentFirstTour : StrategyOpponentFirst
    {
        public override Card OpponentPlayFirst(Player opponent, Player player, Card openTrumpCard,
            bool havePlayerSixtySixPonts, Check check)
        {
            Card opponentCardForPlay = check.CheckForCard(opponent.CardsPlayer, openTrumpCard);
            return opponentCardForPlay;
        }
    }
}
