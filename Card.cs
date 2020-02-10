using System;
using System.Collections.Generic;
using System.Text;

namespace Santase
{
    internal class Card : ICard
    {
        private int points;

        public Card(string type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        public Card(string type, string value, int points)
            : this(type, value)
        {            
            this.Points = points;
        }

        public string Type { get; set; }

        public string Value { get; set; }

        public int Points
        {
            get => this.points;

            set
            {
                if (this.Value == "9")
                {
                    this.points = 0;
                }

                else if (this.Value == "10")
                {
                    this.points = 10;
                }

                else if (this.Value == "J")
                {
                    this.points = 2;
                }

                else if (this.Value == "D")
                {
                    this.points = 3;
                }

                else if (this.Value == "K")
                {
                    this.points = 4;
                }

                else
                {
                    this.points = 11;
                }
            }
        }

        public override string ToString()
        {
            return $"{this.Type} {this.Value}";
        }
    }
}
