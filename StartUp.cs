using System;
using System.Collections.Generic;

namespace Santase
{
    class StartUp
    {
        static void Main(string[] args)
        {
            // Пълнене на тестето и разбъркване на картите
            DeckOfCards deckOfCards = new DeckOfCards();

            Player opponent = new Player(Console.ReadLine(), new List<Card>(), 0, 0, true);
            Player player = new Player(Console.ReadLine(), new List<Card>(), 0, 0, false);

            while (true)
            {
                // Раздаване

                //Board

                // Игра

            }           
        }
    }
}
