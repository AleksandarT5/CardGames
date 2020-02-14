using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    internal class Player : IPlayer
    {
        private string name;

        public Player(string name, List<Card> cardsPlayer, int points, int games, bool isFirstPlay,
            bool hasClosedTheDeckOfCards)
        {
            this.Name = name;
            this.CardsPlayer = new List<Card>();
            this.Points = 0;
            this.Games = 0;
            this.IsFirstPlay = isFirstPlay;
            this.HasClosedTheDeckOfCards = hasClosedTheDeckOfCards;
        }

        public string Name
        {
            get => this.name;
            private set
            {                
                this.name = value;
            }
        }

        public List<Card> CardsPlayer { get; set; }

        public int Points { get; set; }

        public int Games { get; set; }

        public bool IsFirstPlay { get; set; }

        public bool HasClosedTheDeckOfCards { get; set; }
    }
}
