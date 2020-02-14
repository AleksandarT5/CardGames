using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    interface IPlayer
    {
        string Name { get; }

        List<Card> CardsPlayer { get; }

        int Points { get; }

        int Games { get; }

        bool IsFirstPlay { get; }

        bool HasClosedTheDeckOfCards { get; }
    }
}
