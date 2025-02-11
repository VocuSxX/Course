using System;

namespace ProjektGame
{

    class Program
    {
        static void Main(string[] args)
        {
            List<Hero> heroes = new List<Hero>();
            heroes.Add(new Wizard("Wizard", 500, Colors.green));
            heroes.Add(new Knight("Knight", 700, Colors.red));

            Hero player1;
            Hero player2;

            bool isChosed = false;
            do
            {
                isChosed = ChoosenHero(out player1, 1, ref heroes);
            } while (!isChosed);

            do
            {
                isChosed = ChoosenHero(out player2, 2, ref heroes);
            } while (!isChosed);


            bool isPlayer1Turn = true;

            do
            {
                Console.Clear();
                switch (player1.Color)
                {
                    case Colors.red:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case Colors.green:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Player 1");
                Console.WriteLine(player1);

                switch (player2.Color)
                {
                    case Colors.red:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case Colors.green:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    default:
                        break;
                }
                Console.CursorTop = 0;
                Console.CursorLeft = 25;
                Console.WriteLine("Player 2");
                Console.CursorTop = 1;
                Console.CursorLeft = 25;
                Console.WriteLine(player2);
                Console.ResetColor();


                Hero actualPlayer = isPlayer1Turn ? player1 : player2;
                Hero otherPlayer = isPlayer1Turn ? player2 : player1;

                Console.WriteLine($"\nRuch gracza: {(isPlayer1Turn ? 1 : 2)}");
                Console.WriteLine("Co chcesz zrobić?");
                Console.WriteLine("1. Postawowy atak");
                Console.WriteLine("2. Ulecz");

                if (actualPlayer is ISpecialAttack && !actualPlayer.UsedSpecialAttack)
                {
                    Console.WriteLine("3. Atak Specialny!");
                }

                ConsoleKey key;
                do
                {
                    key = Console.ReadKey().Key;
                    switch (key)
                    {
                        case ConsoleKey.D1:
                            actualPlayer.DefoultAttack(otherPlayer);
                            break;
                        case ConsoleKey.D2:
                            actualPlayer.Heal();
                            break;
                        case ConsoleKey.D3:
                            if (actualPlayer is ISpecialAttack && !actualPlayer.UsedSpecialAttack)
                            {
                                ((ISpecialAttack)actualPlayer).SpecialAttack(otherPlayer);
                                actualPlayer.UsedSpecialAttack = true;
                            }
                            else
                            {
                                actualPlayer.DefoultAttack(otherPlayer);
                            }
                                break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Nie ma takiego ruchu.");
                            Console.ResetColor();
                            break;
                    }    
                }while (key != ConsoleKey.D1 && key != ConsoleKey.D2 && key !=ConsoleKey.D3);

                if (player1.ActualHP == 0  || player2.ActualHP == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Niestety! Gracz {(isPlayer1Turn ? 2 : 1)} zginął!");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Wygrał! Gracz: {(isPlayer1Turn ? 1 : 2)}.");


                    Console.ResetColor();
                }
                else
                {
                    isPlayer1Turn = !isPlayer1Turn;
                }

                Console.ReadKey();

            } while (player1.ActualHP > 0 && player2.ActualHP > 0);

            static bool ChoosenHero(out Hero hero, int player, ref List<Hero> heroes)
            {
                Console.Clear();
                Console.WriteLine($"Gracz {player} wybiera swoją postać: ");

                for (int i = 0; i < heroes.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {heroes[i].Name}");
                }

                int num;
                int.TryParse(Console.ReadLine(), out num);

                if (num >= 1 && num <= heroes.Count)
                {
                    hero = heroes[num - 1];
                    heroes.Remove(hero);
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nie ma takiego bohatera.");
                    Console.ResetColor();
                    hero = null;
                    Console.ReadKey();
                    return false;
                }
            }
        }
    }
}