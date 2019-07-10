using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOrLow
{
    enum SKort { Ace = 1, Jack = 11, Queen = 12, King = 13 };
    enum Färg { Spades = 0, Hearts = 1, Diamonds = 2, Clubs = 3 };
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            bool flag = true;
            while (flag)
            {
                switch (Meny())
                {
                    case 0:
                        Game();
                        Console.Clear();
                        break;
                    case 1:
                        Instructions();
                        break;
                    case 2:
                        Exit();
                        break;
                }
            }
        }
        public static int Meny()
        {
            string[] items = { "Play", "Instructions", "Exit" };
            int current = 0;
            while (true)
            {
                Console.WriteLine("█░░█ ░▀░ █▀▀▀ █░░█   █▀▀█ █▀▀█   █░░ █▀▀█ █░░░█");
                Console.WriteLine("█▀▀█ ▀█▀ █░▀█ █▀▀█   █░░█ █▄▄▀   █░░ █░░█ █▄█▄█");
                Console.WriteLine("▀░░▀ ▀▀▀ ▀▀▀▀ ▀░░▀   ▀▀▀▀ ▀░▀▀   ▀▀▀ ▀▀▀▀ ░▀░▀░");
                Console.WriteLine();
                for (int i = 0; i < items.Length; i++)
                {
                    if (i == current) { Console.WriteLine(items[i] + " <---"); }
                    else { Console.WriteLine(items[i]); }
                }

                ConsoleKey input = Console.ReadKey().Key;
                if (input == ConsoleKey.UpArrow)
                {
                    if (current == 0) { current = items.Length - 1; }
                    else { current--; }
                }
                if (input == ConsoleKey.DownArrow)
                {
                    if (current == items.Length - 1) { current = 0; }
                    else { current++; }
                }
                if (input == ConsoleKey.Enter)
                {
                    return current;
                }
                Console.Clear();
            }

        }
        public static void Exit()
        {
            Console.Clear();
            Console.WriteLine("Thanks for playing");
            Console.ReadKey();
            Environment.Exit(-1);
        }
        public static void Instructions()
        {
            Console.Clear();
            Console.WriteLine("How it works: 13 out of 52 cards are placed on the table upside down");
            Console.WriteLine("Then the first card is turned. After it is turned, youre supposed to guess wether the next card is higher or lower");
            Console.WriteLine("Ace works as the lowest AND highest card, and if you get a pair, you lose");
            Console.WriteLine("If you guess all cards right, you win. Otherwise, you lose.");
            Console.WriteLine();
            Console.WriteLine("Press any button to go back");
            Console.ReadKey();
            Console.Clear();
        }
        public static void Game()
        {
            bool loser = false;
            int winsreq = 12;
            List<Kortlek> Kort = new List<Kortlek>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    Kortlek x = new Kortlek((SKort)(j + 1), (Färg)i);
                    Kort.Add(x);
                }
            }
            bool[] replaySecurity = new bool[52];
            for (int i = 0; i < 52; i++)
            {
                replaySecurity[i] = false;
            }
            Kortlek[] randomKort = new Kortlek[winsreq + 1];
            for (int i = 0; i < winsreq + 1; i++)
            {
                randomKort[i] = Randomizer(Kort, replaySecurity);

            }
            for (int i = 0; i < (winsreq); i++)
            {
                Console.Clear();
                switch (Chooser(randomKort, i))
                {
                    case 0:
                        if (randomKort[i + 1].Nummer == (SKort)1 || randomKort[i].Nummer == (SKort)1 )
                        {
                            Console.WriteLine("Nice! Your next card was: " + randomKort[(i + 1)].Nummer + " of " + randomKort[i + 1].Typ); Console.ReadKey(); break;
                        }
                        else
                        {
                            if (randomKort[i].Nummer > randomKort[i + 1].Nummer) { Console.WriteLine("Nice! Your next card was: " + randomKort[(i + 1)].Nummer + " of " + randomKort[i + 1].Typ); Console.ReadKey(); break; }
                            else { Console.WriteLine("You lose, Your next card was: " + randomKort[i + 1].Nummer + " of " + randomKort[i + 1].Typ); Console.ReadKey(); loser = true; break; }
                        }
                    case 1:
                        if (randomKort[i + 1].Nummer == (SKort)1 || randomKort[i].Nummer == (SKort)1)
                        {
                            Console.WriteLine("Nice! Your next card was: " + randomKort[(i + 1)].Nummer + " of " + randomKort[i + 1].Typ); Console.ReadKey(); break;
                        }
                        else
                        {
                            if (randomKort[i].Nummer < randomKort[i + 1].Nummer) { Console.WriteLine("Nice! Your next card was: " + randomKort[(i + 1)].Nummer + " of " + randomKort[i + 1].Typ); Console.ReadKey(); break; }
                            else { Console.WriteLine("You lose, Your next card was: " + randomKort[i + 1].Nummer + " of " + randomKort[i + 1].Typ); Console.ReadKey(); loser = true; break; }
                        }
                }
                if (loser == true)
                {
                    break;
                }
                else if (i == winsreq - 1)
                {
                    Console.WriteLine("CONGRATULATIONS FRIEND! U WON M8, I R8 8/8");
                    Console.ReadKey();
                }
            }
        }
        static Kortlek Randomizer(List<Kortlek> Kort, bool[] replaySecurity)
        {
            var Random = new Random();
            Kortlek r;
            int save;
            while (true)
            {
                save = Random.Next(0, 52);
                r = Kort[save];
                if (replaySecurity[save] == false)
                {
                    replaySecurity[save] = true;
                    break;
                }
            }
            return r;
        }
        static int Chooser(Kortlek[] randomKort, int a)
        {
            string[] items = { "Lower", "Higher" };
            int current = 0;
            while (true)
            {
                Console.WriteLine("Card " + (a + 1) + "/13");
                Console.WriteLine("Your card is:");
                Console.WriteLine(randomKort[a].Nummer + " of " + randomKort[a].Typ);
                Console.WriteLine("Will the next card be higher or lower?");
                for (int i = 0; i < items.Length; i++)
                {
                    if (i == current) { Console.WriteLine(items[i] + " <---"); }
                    else { Console.WriteLine(items[i]); }
                }

                ConsoleKey input = Console.ReadKey().Key;
                if (input == ConsoleKey.UpArrow)
                {
                    if (current == 0) { current = items.Length - 1; }
                    else { current--; }
                }
                if (input == ConsoleKey.DownArrow)
                {
                    if (current == items.Length - 1) { current = 0; }
                    else { current++; }
                }
                if (input == ConsoleKey.Enter)
                {
                    return current;
                }
                Console.Clear();

            }
        }
    }
}

