/////////////////////////////////////////////////////////  ЛАБИРИНТ /////////////////////////



using System;
using System.Text;
using System.IO;

using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using System.Media;

public class Coord
{
    public int x;
    public int y;
}

public class cc
{
    public Coord c;
    public int dir;
    public cc()
    {
        c = new Coord();
    }
}

public class Win32Interop
{
    [DllImport("crtdll.dll")]
    public static extern int _kbhit();

    [DllImport("kernel32.dll")]
    public static extern void ExitProcess([In] uint uExitCode);
}

class Sample
{
    static int width = 70;
    static int height = 20;
    static bool bombflag = false;
    static bool kickflag = false;
    static Coord c = new Coord();
    static Coord bomb = new Coord();
    static Coord kickPos = new Coord();
    static int[,] maze = new int[height, width];

    static int money = 0;
    static int lives = 3;
    static int energy = 500;
    static int bombs = 3;
    static int bomb_radius = 2;
    static int enemy_quant = 0; // количество врагов
    static int money_total = 0;

    static cc[] enemy = new cc[100];
    static ConsoleKeyInfo k;

    static void ShowMoney()
    {
        Console.SetCursorPosition(width + 5, 5);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("$ -                                          ");

        Console.SetCursorPosition(width + 5, 5);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("$ - " + money + "   $$$ - " + money_total);
    }

    static void ShowLives()
    {
        Console.SetCursorPosition(width + 5, 7);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write((char)1 + " -    ");

        Console.SetCursorPosition(width + 5, 7);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write((char)1 + " - " + lives);
    }

    static void ShowEnergy()
    {
        Console.SetCursorPosition(width + 5, 9);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Energy" + " -    ");

        Console.SetCursorPosition(width + 5, 9);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Energy" + " - " + energy);
    }

    static void ShowBombs()
    {
        Console.SetCursorPosition(width + 5, 11);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("* -    ");

        Console.SetCursorPosition(width + 5, 11);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("* - " + bombs);

        if (bomb_radius == 2)
            Console.Write("   2x2       ");
        if (bomb_radius == 3)
            Console.Write("   3x3       ");
        if (bomb_radius == 4)
            Console.Write("   4x4       ");
        if (bomb_radius == 5)
            Console.Write("   5x5 (max)");
    }

    static void ShowEnemy()
    {
        Console.SetCursorPosition(width + 5, 13);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write((char)1 + " -    ");

        Console.SetCursorPosition(width + 5, 13);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write((char)1 + " - " + enemy_quant);
    }

    public static void Kick(object source, ElapsedEventArgs e)
    {
        SoundPlayer player = new SoundPlayer();
        player.SoundLocation = Environment.CurrentDirectory + @"\Assets\Kick.wav";
        player.Play();
        Thread.Sleep(1000);
        player.SoundLocation = Environment.CurrentDirectory + @"\Assets\Maze.wav";
        player.Play();

        kickflag = false;
        //Console.BackgroundColor = ConsoleColor.DarkRed;
        //Console.SetCursorPosition(kickPos.x, kickPos.y);
        //Console.Write(" ");
        //maze[kickPos.y, kickPos.x] = 0;

        if ((kickPos.x - 1) > 0)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
            Console.SetCursorPosition(kickPos.x - 1, kickPos.y);
            Console.Write(" ");

            if (maze[kickPos.y, (kickPos.x - 1)] == 3)                    // check if enemy
            {
                for (int i = 0; i < enemy_quant; i++)
                {
                    if ((kickPos.x - 1) == enemy[i].c.x && kickPos.y == enemy[i].c.y)
                    {
                        enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                        enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                        break;
                    }
                }

                enemy_quant--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowEnemy();
            }

            maze[kickPos.y, kickPos.x - 1] = 0;
        }

        if ((kickPos.x + 1) > 0)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
            Console.SetCursorPosition(kickPos.x + 1, kickPos.y);
            Console.Write(" ");

            if (maze[kickPos.y, (kickPos.x + 1)] == 3)                    // check if enemy
            {
                for (int i = 0; i < enemy_quant; i++)
                {
                    if ((kickPos.x + 1) == enemy[i].c.x && kickPos.y == enemy[i].c.y)
                    {
                        enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                        enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                        break;
                    }
                }

                enemy_quant--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowEnemy();
            }

            maze[kickPos.y, kickPos.x + 1] = 0;
        }

        if ((kickPos.y - 1) > 0)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
            Console.SetCursorPosition(kickPos.x, kickPos.y - 1);
            Console.Write(" ");

            if (maze[(kickPos.y - 1), kickPos.x] == 3)                    // check if enemy
            {
                for (int i = 0; i < enemy_quant; i++)
                {
                    if ((kickPos.x) == enemy[i].c.x && kickPos.y - 1 == enemy[i].c.y)
                    {
                        enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                        enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                        break;
                    }
                }

                enemy_quant--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowEnemy();
            }

            maze[kickPos.y - 1, kickPos.x] = 0;
        }

        if ((kickPos.y + 1) > 0)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
            Console.SetCursorPosition(kickPos.x, kickPos.y + 1);
            Console.Write(" ");

            if (maze[(kickPos.y + 1), kickPos.x] == 3)                    // check if enemy
            {
                for (int i = 0; i < enemy_quant; i++)
                {
                    if ((kickPos.x) == enemy[i].c.x && kickPos.y + 1 == enemy[i].c.y)
                    {
                        enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                        enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                        break;
                    }
                }

                enemy_quant--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowEnemy();
            }

            maze[kickPos.y + 1, kickPos.x] = 0;
        }

        if ((kickPos.y + 1) > 0 && (kickPos.x + 1) > 0)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
            Console.SetCursorPosition(kickPos.x + 1, kickPos.y + 1);
            Console.Write(" ");

            if (maze[(kickPos.y + 1), (kickPos.x + 1)] == 3)                    // check if enemy
            {
                for (int i = 0; i < enemy_quant; i++)
                {
                    if ((kickPos.x + 1) == enemy[i].c.x && kickPos.y + 1 == enemy[i].c.y)
                    {
                        enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                        enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                        break;
                    }
                }

                enemy_quant--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowEnemy();
            }

            maze[kickPos.y + 1, kickPos.x + 1] = 0;
        }

        if ((kickPos.y - 1) > 0 && (kickPos.x - 1) > 0)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
            Console.SetCursorPosition(kickPos.x - 1, kickPos.y - 1);
            Console.Write(" ");

            if (maze[(kickPos.y - 1), (kickPos.x - 1)] == 3)                    // check if enemy
            {
                for (int i = 0; i < enemy_quant; i++)
                {
                    if ((kickPos.x - 1) == enemy[i].c.x && kickPos.y - 1 == enemy[i].c.y)
                    {
                        enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                        enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                        break;
                    }
                }

                enemy_quant--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowEnemy();
            }

            maze[kickPos.y - 1, kickPos.x - 1] = 0;
        }

        if ((kickPos.y + 1) > 0 && (kickPos.x - 1) > 0)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
            Console.SetCursorPosition(kickPos.x - 1, kickPos.y + 1);
            Console.Write(" ");

            if (maze[(kickPos.y + 1), (kickPos.x - 1)] == 3)                    // check if enemy
            {
                for (int i = 0; i < enemy_quant; i++)
                {
                    if ((kickPos.x + 1) == enemy[i].c.x && kickPos.y - 1 == enemy[i].c.y)
                    {
                        enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                        enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                        break;
                    }
                }

                enemy_quant--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowEnemy();
            }

            maze[kickPos.y - 1, kickPos.x + 1] = 0;
        }

        if ((kickPos.y - 1) > 0 && (kickPos.x + 1) > 0)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
            Console.SetCursorPosition(kickPos.x + 1, kickPos.y - 1);
            Console.Write(" ");

            if (maze[(kickPos.y - 1), (kickPos.x + 1)] == 3)                    // check if enemy
            {
                for (int i = 0; i < enemy_quant; i++)
                {
                    if ((kickPos.x + 1) == enemy[i].c.x && kickPos.y - 1 == enemy[i].c.y)
                    {
                        enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                        enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                        break;
                    }
                }

                enemy_quant--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowEnemy();
            }

            maze[kickPos.y - 1, kickPos.x + 1] = 0;
        }

        Thread.Sleep(150);

        if ((kickPos.x - 1) > 0)
        {
            //Console.BackgroundColor = ConsoleColor.Black;         /// explode color
            //if (kickPos.x - 1 == 2)
            //{
            //    Console.ForegroundColor = ConsoleColor.Yellow;
            //    Console.BackgroundColor = ConsoleColor.Black;
            //    Console.SetCursorPosition(kickPos.x - 1, kickPos.y);
            //    Console.Write("$");
            //}
            //else
            //{
            //    Console.SetCursorPosition(kickPos.x - 1, kickPos.y);
            //    Console.Write(" ");
            //}
            Console.BackgroundColor = ConsoleColor.Black;         /// explode color
            Console.SetCursorPosition(kickPos.x - 1, kickPos.y);
            Console.Write(" ");
        }

        if ((kickPos.x + 1) > 0)
        {
            Console.BackgroundColor = ConsoleColor.Black;         /// explode color
            Console.SetCursorPosition(kickPos.x + 1, kickPos.y);
            Console.Write(" ");
        }

        if ((kickPos.y - 1) > 0)
        {
            Console.BackgroundColor = ConsoleColor.Black;         /// explode color
            Console.SetCursorPosition(kickPos.x, kickPos.y - 1);
            Console.Write(" ");
        }

        if ((kickPos.y + 1) > 0)
        {
            Console.BackgroundColor = ConsoleColor.Black;         /// explode color
            Console.SetCursorPosition(kickPos.x, kickPos.y + 1);
            Console.Write(" ");
        }

        if ((kickPos.y + 1) > 0 && (kickPos.x + 1) > 0)
        {
            Console.BackgroundColor = ConsoleColor.Black;         /// explode color
            Console.SetCursorPosition(kickPos.x + 1, kickPos.y + 1);
            Console.Write(" ");
        }

        if ((kickPos.y - 1) > 0 && (kickPos.x - 1) > 0)
        {
            Console.BackgroundColor = ConsoleColor.Black;         /// explode color
            Console.SetCursorPosition(kickPos.x - 1, kickPos.y - 1);
            Console.Write(" ");
        }

        if ((kickPos.y + 1) > 0 && (kickPos.x - 1) > 0)
        {
            Console.BackgroundColor = ConsoleColor.Black;         /// explode color
            Console.SetCursorPosition(kickPos.x - 1, kickPos.y + 1);
            Console.Write(" ");
        }

        if ((kickPos.y - 1) > 0 && (kickPos.x + 1) > 0)
        {
            Console.BackgroundColor = ConsoleColor.Black;         /// explode color
            Console.SetCursorPosition(kickPos.x + 1, kickPos.y - 1);
            Console.Write(" ");
        }

        //Console.SetCursorPosition(kickPos.x, kickPos.y);
        //Console.Write(" ");
    }
    public static void BombExplode(object source, ElapsedEventArgs e)
    {
        SoundPlayer player = new SoundPlayer();
        player.SoundLocation = Environment.CurrentDirectory + @"\Assets\BombExploded.wav";
        player.Play();
        Thread.Sleep(1000);
        player.SoundLocation = Environment.CurrentDirectory + @"\Assets\Maze.wav";
        player.Play();

        bombflag = false;
        Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color                                                                           
        Console.SetCursorPosition(bomb.x, bomb.y);
        Console.Write(" ");
        maze[bomb.y, bomb.x] = 0;

        if ((bomb.x - 1) > 0)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
            Console.SetCursorPosition(bomb.x - 1, bomb.y);
            Console.Write(" ");

            if (maze[bomb.y, (bomb.x - 1)] == 2)                    // check if money
            {
                money_total--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowMoney();
                Thread.Sleep(20);
            }

            if (maze[bomb.y, (bomb.x - 1)] == 3)                    // check if enemy
            {
                for (int i = 0; i < enemy_quant; i++)
                {
                    if ((bomb.x - 1) == enemy[i].c.x && bomb.y == enemy[i].c.y)
                    {
                        enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                        enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                        break;
                    }
                }

                enemy_quant--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowEnemy();
            }

            maze[bomb.y, bomb.x - 1] = 0;
        }

        if ((bomb.x - 2) > 0)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
            Console.SetCursorPosition(bomb.x - 2, bomb.y);
            Console.Write(" ");

            if (maze[bomb.y, (bomb.x - 2)] == 2)
            {
                money_total--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowMoney();
                Thread.Sleep(20);
            }

            if (maze[bomb.y, (bomb.x - 2)] == 3)                    // check if enemy
            {
                for (int i = 0; i < enemy_quant; i++)
                {
                    if ((bomb.x - 2) == enemy[i].c.x && bomb.y == enemy[i].c.y)
                    {
                        enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                        enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                        break;
                    }
                }

                enemy_quant--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowEnemy();
            }

            maze[bomb.y, bomb.x - 2] = 0;
        }

        if ((bomb.x + 1) < width - 1)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
            Console.SetCursorPosition(bomb.x + 1, bomb.y);
            Console.Write(" ");

            if (maze[bomb.y, (bomb.x + 1)] == 2)
            {
                money_total--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowMoney();
                Thread.Sleep(20);
            }

            if (maze[bomb.y, (bomb.x + 1)] == 3)                    // check if enemy
            {
                for (int i = 0; i < enemy_quant; i++)
                {
                    if ((bomb.x + 1) == enemy[i].c.x && bomb.y == enemy[i].c.y)
                    {
                        enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                        enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                        break;
                    }
                }

                enemy_quant--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowEnemy();
            }

            maze[bomb.y, bomb.x + 1] = 0;
        }

        if ((bomb.x + 2) < width - 1)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
            Console.SetCursorPosition(bomb.x + 2, bomb.y);
            Console.Write(" ");

            if (maze[bomb.y, (bomb.x + 2)] == 2)
            {
                money_total--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowMoney();
                Thread.Sleep(20);
            }

            if (maze[bomb.y, (bomb.x + 2)] == 3)                    // check if enemy
            {
                for (int i = 0; i < enemy_quant; i++)
                {
                    if ((bomb.x + 2) == enemy[i].c.x && bomb.y == enemy[i].c.y)
                    {
                        enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                        enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                        break;
                    }
                }

                enemy_quant--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowEnemy();
            }

            maze[bomb.y, bomb.x + 2] = 0;
        }

        if ((bomb.y - 1) > 0)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
            Console.SetCursorPosition(bomb.x, bomb.y - 1);
            Console.Write(" ");

            if (maze[(bomb.y - 1), bomb.x] == 2)
            {
                money_total--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowMoney();
                Thread.Sleep(20);
            }

            if (maze[(bomb.y - 1), bomb.x] == 3)                    // check if enemy
            {
                for (int i = 0; i < enemy_quant; i++)
                {
                    if (bomb.x == enemy[i].c.x && (bomb.y - 1) == enemy[i].c.y)
                    {
                        enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                        enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                        break;
                    }
                }

                enemy_quant--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowEnemy();
            }

            maze[bomb.y - 1, bomb.x] = 0;
        }

        if ((bomb.y - 2) > 0)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
            Console.SetCursorPosition(bomb.x, bomb.y - 2);
            Console.Write(" ");

            if (maze[(bomb.y - 2), bomb.x] == 2)
            {
                money_total--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowMoney();
                Thread.Sleep(20);
            }

            if (maze[(bomb.y - 2), bomb.x] == 3)                    // check if enemy
            {
                for (int i = 0; i < enemy_quant; i++)
                {
                    if (bomb.x == enemy[i].c.x && (bomb.y - 2) == enemy[i].c.y)
                    {
                        enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                        enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                        break;
                    }
                }

                enemy_quant--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowEnemy();
            }

            maze[bomb.y - 2, bomb.x] = 0;
        }

        if ((bomb.y + 1) < height - 1)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
            Console.SetCursorPosition(bomb.x, bomb.y + 1);
            Console.Write(" ");

            if (maze[(bomb.y + 1), bomb.x] == 2)
            {
                money_total--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowMoney();
                Thread.Sleep(20);
            }

            if (maze[(bomb.y + 1), bomb.x] == 3)                    // check if enemy
            {
                for (int i = 0; i < enemy_quant; i++)
                {
                    if (bomb.x == enemy[i].c.x && (bomb.y + 1) == enemy[i].c.y)
                    {
                        enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                        enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                        break;
                    }
                }

                enemy_quant--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowEnemy();
            }

            maze[bomb.y + 1, bomb.x] = 0;
        }

        if ((bomb.y + 2) < height - 1)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
            Console.SetCursorPosition(bomb.x, bomb.y + 2);
            Console.Write(" ");

            if (maze[(bomb.y + 2), bomb.x] == 2)
            {
                money_total--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowMoney();
                Thread.Sleep(20);
            }
            if (maze[(bomb.y + 2), bomb.x] == 3)                    // check if enemy
            {
                for (int i = 0; i < enemy_quant; i++)
                {
                    if (bomb.x == enemy[i].c.x && (bomb.y + 2) == enemy[i].c.y)
                    {
                        enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                        enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                        break;
                    }
                }

                enemy_quant--;
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.ShowEnemy();
            }

            maze[bomb.y + 2, bomb.x] = 0;
        }
        //////////////////////////////  IF BOMB RADIUS > 2 /////////////////////////////////////////////////////////////////////
        if (bomb_radius > 2)       // bomb_radius = 3
        {
            if ((bomb.x - 3) > 0)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
                Console.SetCursorPosition(bomb.x - 3, bomb.y);
                Console.Write(" ");

                if (maze[bomb.y, (bomb.x - 3)] == 2)
                {
                    money_total--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowMoney();
                    Thread.Sleep(20);
                }

                if (maze[bomb.y, (bomb.x - 3)] == 3)                    // check if enemy
                {
                    for (int i = 0; i < enemy_quant; i++)
                    {
                        if ((bomb.x - 3) == enemy[i].c.x && bomb.y == enemy[i].c.y)
                        {
                            enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                            enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                            break;
                        }
                    }

                    enemy_quant--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowEnemy();
                }

                maze[bomb.y, bomb.x - 3] = 0;
            }

            if ((bomb.x + 3) < width - 1)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
                Console.SetCursorPosition(bomb.x + 3, bomb.y);
                Console.Write(" ");

                if (maze[bomb.y, (bomb.x + 3)] == 2)
                {
                    money_total--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowMoney();
                    Thread.Sleep(20);
                }

                if (maze[bomb.y, (bomb.x + 3)] == 3)                    // check if enemy
                {
                    for (int i = 0; i < enemy_quant; i++)
                    {
                        if ((bomb.x + 3) == enemy[i].c.x && bomb.y == enemy[i].c.y)
                        {
                            enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                            enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                            break;
                        }
                    }

                    enemy_quant--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowEnemy();
                }

                maze[bomb.y, bomb.x + 3] = 0;
            }

            if ((bomb.y - 3) > 0)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
                Console.SetCursorPosition(bomb.x, bomb.y - 3);
                Console.Write(" ");

                if (maze[(bomb.y - 3), bomb.x] == 2)
                {
                    money_total--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowMoney();
                    Thread.Sleep(20);
                }

                if (maze[(bomb.y - 3), bomb.x] == 3)                    // check if enemy
                {
                    for (int i = 0; i < enemy_quant; i++)
                    {
                        if (bomb.x == enemy[i].c.x && (bomb.y - 3) == enemy[i].c.y)
                        {
                            enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                            enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                            break;
                        }
                    }

                    enemy_quant--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowEnemy();
                }

                maze[bomb.y - 3, bomb.x] = 0;
            }

            if ((bomb.y + 3) < height - 1)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
                Console.SetCursorPosition(bomb.x, bomb.y + 3);
                Console.Write(" ");

                if (maze[(bomb.y + 3), bomb.x] == 2)
                {
                    money_total--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowMoney();
                    Thread.Sleep(20);
                }

                if (maze[(bomb.y + 3), bomb.x] == 3)                    // check if enemy
                {
                    for (int i = 0; i < enemy_quant; i++)
                    {
                        if (bomb.x == enemy[i].c.x && (bomb.y + 3) == enemy[i].c.y)
                        {
                            enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                            enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                            break;
                        }
                    }

                    enemy_quant--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowEnemy();
                }

                maze[bomb.y + 3, bomb.x] = 0;
            }
        }

        if (bomb_radius > 3)       // bomb_radius = 4
        {
            if ((bomb.x - 4) > 0)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
                Console.SetCursorPosition(bomb.x - 4, bomb.y);
                Console.Write(" ");

                if (maze[bomb.y, (bomb.x - 4)] == 2)
                {
                    money_total--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowMoney();
                    Thread.Sleep(20);
                }

                if (maze[bomb.y, (bomb.x - 4)] == 3)                    // check if enemy
                {
                    for (int i = 0; i < enemy_quant; i++)
                    {
                        if ((bomb.x - 4) == enemy[i].c.x && bomb.y == enemy[i].c.y)
                        {
                            enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                            enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                            break;
                        }
                    }

                    enemy_quant--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowEnemy();
                }

                maze[bomb.y, bomb.x - 4] = 0;
            }

            if ((bomb.x + 4) < width - 1)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
                Console.SetCursorPosition(bomb.x + 4, bomb.y);
                Console.Write(" ");

                if (maze[bomb.y, (bomb.x + 4)] == 2)
                {
                    money_total--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowMoney();
                    Thread.Sleep(20);
                }

                if (maze[bomb.y, (bomb.x + 4)] == 3)                    // check if enemy
                {
                    for (int i = 0; i < enemy_quant; i++)
                    {
                        if ((bomb.x + 4) == enemy[i].c.x && bomb.y == enemy[i].c.y)
                        {
                            enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                            enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                            break;
                        }
                    }

                    enemy_quant--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowEnemy();
                }

                maze[bomb.y, bomb.x + 4] = 0;
            }

            if ((bomb.y - 4) > 0)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
                Console.SetCursorPosition(bomb.x, bomb.y - 4);
                Console.Write(" ");
                Thread.Sleep(20);

                if (maze[(bomb.y - 4), bomb.x] == 2)
                {
                    money_total--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowMoney();
                }

                if (maze[(bomb.y - 4), bomb.x] == 3)                    // check if enemy
                {
                    for (int i = 0; i < enemy_quant; i++)
                    {
                        if (bomb.x == enemy[i].c.x && (bomb.y - 4) == enemy[i].c.y)
                        {
                            enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                            enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                            break;
                        }
                    }

                    enemy_quant--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowEnemy();
                }

                maze[bomb.y - 4, bomb.x] = 0;
            }

            if ((bomb.y + 4) < height - 1)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
                Console.SetCursorPosition(bomb.x, bomb.y + 4);
                Console.Write(" ");

                if (maze[(bomb.y + 4), bomb.x] == 2)
                {
                    money_total--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowMoney();
                    Thread.Sleep(20);
                }

                if (maze[(bomb.y + 4), bomb.x] == 3)                    // check if enemy
                {
                    for (int i = 0; i < enemy_quant; i++)
                    {
                        if (bomb.x == enemy[i].c.x && (bomb.y + 4) == enemy[i].c.y)
                        {
                            enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                            enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                            break;
                        }
                    }

                    enemy_quant--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowEnemy();
                    Thread.Sleep(20);
                }

                maze[bomb.y + 4, bomb.x] = 0;
            }
        }

        if (bomb_radius > 4)       // bomb_radius = 5
        {
            if ((bomb.x - 5) > 0)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
                Console.SetCursorPosition(bomb.x - 5, bomb.y);
                Console.Write(" ");

                if (maze[bomb.y, (bomb.x - 5)] == 2)
                {
                    money_total--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowMoney();
                    Thread.Sleep(20);
                }

                if (maze[bomb.y, (bomb.x - 5)] == 3)                    // check if enemy
                {
                    for (int i = 0; i < enemy_quant; i++)
                    {
                        if ((bomb.x - 5) == enemy[i].c.x && bomb.y == enemy[i].c.y)
                        {
                            enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                            enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                            break;
                        }
                    }

                    enemy_quant--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowEnemy();
                }

                maze[bomb.y, bomb.x - 5] = 0;
            }

            if ((bomb.x + 5) < width - 1)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
                Console.SetCursorPosition(bomb.x + 5, bomb.y);
                Console.Write(" ");

                if (maze[bomb.y, (bomb.x + 5)] == 2)
                {
                    money_total--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowMoney();
                    Thread.Sleep(20);
                }

                if (maze[bomb.y, (bomb.x + 5)] == 3)                    // check if enemy
                {
                    for (int i = 0; i < enemy_quant; i++)
                    {
                        if ((bomb.x + 5) == enemy[i].c.x && bomb.y == enemy[i].c.y)
                        {
                            enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                            enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                            break;
                        }
                    }

                    enemy_quant--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowEnemy();
                }

                maze[bomb.y, bomb.x + 5] = 0;
            }

            if ((bomb.y - 5) > 0)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
                Console.SetCursorPosition(bomb.x, bomb.y - 5);
                Console.Write(" ");

                if (maze[(bomb.y - 5), bomb.x] == 2)
                {
                    money_total--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowMoney();
                    Thread.Sleep(20);
                }

                if (maze[(bomb.y - 5), bomb.x] == 3)                    // check if enemy
                {
                    for (int i = 0; i < enemy_quant; i++)
                    {
                        if (bomb.x == enemy[i].c.x && (bomb.y - 5) == enemy[i].c.y)
                        {
                            enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                            enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                            break;
                        }
                    }

                    enemy_quant--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowEnemy();
                }

                maze[bomb.y - 5, bomb.x] = 0;
            }

            if ((bomb.y + 5) < height - 1)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;         /// explode color
                Console.SetCursorPosition(bomb.x, bomb.y + 5);
                Console.Write(" ");

                if (maze[(bomb.y + 5), bomb.x] == 2)
                {
                    money_total--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowMoney();
                    Thread.Sleep(20);
                }

                if (maze[(bomb.y + 5), bomb.x] == 3)                    // check if enemy
                {
                    for (int i = 0; i < enemy_quant; i++)
                    {
                        if (bomb.x == enemy[i].c.x && (bomb.y + 5) == enemy[i].c.y)
                        {
                            enemy[i].c.x = enemy[enemy_quant - 1].c.x;
                            enemy[i].c.y = enemy[enemy_quant - 1].c.y;
                            break;
                        }
                    }

                    enemy_quant--;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Sample.ShowEnemy();
                }

                maze[bomb.y + 5, bomb.x] = 0;
            }
        }

        ////////////////////////////////////////////////////

        Thread.Sleep(150);
        Console.BackgroundColor = ConsoleColor.Black;

        Console.SetCursorPosition(bomb.x, bomb.y);
        Console.Write(" ");

        if ((bomb.x - 1) > 0)
        {
            Console.SetCursorPosition(bomb.x - 1, bomb.y);
            Console.Write(" ");
        }

        if ((bomb.x - 2) > 0)
        {
            Console.SetCursorPosition(bomb.x - 2, bomb.y);
            Console.Write(" ");
        }

        if ((bomb.x + 1) < width - 1)
        {
            Console.SetCursorPosition(bomb.x + 1, bomb.y);
            Console.Write(" ");
        }

        if ((bomb.x + 2) < width - 1)
        {
            Console.SetCursorPosition(bomb.x + 2, bomb.y);
            Console.Write(" ");
        }

        if ((bomb.y - 1) > 0)
        {
            Console.SetCursorPosition(bomb.x, bomb.y - 1);
            Console.Write(" ");
        }

        if ((bomb.y - 2) > 0)
        {
            Console.SetCursorPosition(bomb.x, bomb.y - 2);
            Console.Write(" ");
        }

        if ((bomb.y + 1) < height - 1)
        {
            Console.SetCursorPosition(bomb.x, bomb.y + 1);
            Console.Write(" ");
        }

        if ((bomb.y + 2) < height - 1)
        {
            Console.SetCursorPosition(bomb.x, bomb.y + 2);
            Console.Write(" ");
        }


        if (bomb_radius > 2)       // bomb_radius = 3
        {
            if ((bomb.x - 3) > 0)
            {
                Console.SetCursorPosition(bomb.x - 3, bomb.y);
                Console.Write(" ");
            }

            if ((bomb.x + 3) < width - 1)
            {
                Console.SetCursorPosition(bomb.x + 3, bomb.y);
                Console.Write(" ");
            }

            if ((bomb.y - 3) > 0)
            {
                Console.SetCursorPosition(bomb.x, bomb.y - 3);
                Console.Write(" ");
            }

            if ((bomb.y + 3) < height - 1)
            {
                Console.SetCursorPosition(bomb.x, bomb.y + 3);
                Console.Write(" ");
            }
        }

        if (bomb_radius > 3)       // bomb_radius = 4
        {
            if ((bomb.x - 4) > 0)
            {
                Console.SetCursorPosition(bomb.x - 4, bomb.y);
                Console.Write(" ");
            }

            if ((bomb.x + 4) < width - 1)
            {
                Console.SetCursorPosition(bomb.x + 4, bomb.y);
                Console.Write(" ");
            }

            if ((bomb.y - 4) > 0)
            {
                Console.SetCursorPosition(bomb.x, bomb.y - 4);
                Console.Write(" ");
            }

            if ((bomb.y + 4) < height - 1)
            {
                Console.SetCursorPosition(bomb.x, bomb.y + 4);
                Console.Write(" ");
            }
        }

        if (bomb_radius > 4)       // bomb_radius = 5
        {
            if ((bomb.x - 5) > 0)
            {
                Console.SetCursorPosition(bomb.x - 5, bomb.y);
                Console.Write(" ");
            }

            if ((bomb.x + 5) < width - 1)
            {
                Console.SetCursorPosition(bomb.x + 5, bomb.y);
                Console.Write(" ");
            }

            if ((bomb.y - 5) > 0)
            {
                Console.SetCursorPosition(bomb.x, bomb.y - 5);
                Console.Write(" ");
            }

            if ((bomb.y + 5) < height - 1)
            {
                Console.SetCursorPosition(bomb.x, bomb.y + 5);
                Console.Write(" ");
            }
        }

        ///////////////////////  CHECK IF PLAYER  //////////////////////////////////////////////////////////////////

        if ((bomb.x == c.x && bomb.y == c.y) || ((bomb.x - 1) == c.x && bomb.y == c.y) || ((bomb.x - 2) == c.x && bomb.y == c.y)
            || ((bomb.x + 1) == c.x && bomb.y == c.y) || ((bomb.x + 2) == c.x && bomb.y == c.y) || ((bomb.x) == c.x && (bomb.y - 1) == c.y)
            || ((bomb.x) == c.x && (bomb.y - 2) == c.y) || ((bomb.x) == c.x && (bomb.y + 1) == c.y) || ((bomb.x) == c.x && (bomb.y + 2) == c.y))
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Sample.Died();
        }

        if (bomb_radius > 2)
        {
            if (((bomb.x - 3) == c.x && bomb.y == c.y) || ((bomb.x + 3) == c.x && bomb.y == c.y)
                || (bomb.x == c.x && (bomb.y - 3) == c.y) || (bomb.x == c.x && (bomb.y + 3) == c.y))
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.Died();
            }
        }

        if (bomb_radius > 3)
        {
            if (((bomb.x - 4) == c.x && bomb.y == c.y) || ((bomb.x + 4) == c.x && bomb.y == c.y)
                || (bomb.x == c.x && (bomb.y - 4) == c.y) || (bomb.x == c.x && (bomb.y + 4) == c.y))
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.Died();
            }
        }

        if (bomb_radius > 4)
        {
            if (((bomb.x - 5) == c.x && bomb.y == c.y) || ((bomb.x + 5) == c.x && bomb.y == c.y)
                || (bomb.x == c.x && (bomb.y - 5) == c.y) || (bomb.x == c.x && (bomb.y + 5) == c.y))
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Sample.Died();
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
    }


    static void Died()
    {
        c.x = c.y = 1;
        lives--;
        ShowLives();
        bomb_radius = 5;
        ShowBombs();

        Console.SetCursorPosition(c.x, c.y);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write((char)1);

        Console.SetCursorPosition(width + 5, 20);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("You died!!!");

        Thread.Sleep(2000);

        Console.SetCursorPosition(width + 5, 20);
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write("           ");
    }

    public static void Main()
    {
        Console.SetWindowSize(100, 50);
        Console.BufferWidth = 100;
        Console.BufferHeight = 50;

        Console.Title = "Лабиринт";
        Console.CursorVisible = false;

        c.x = c.y = 1;
        Random r = new Random();

        /////////////////  DRAW MAZE  /////////////////////////////////////////////////////////////////////////////
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1) maze[y, x] = 1;
                else maze[y, x] = r.Next(0, 5);

                if (x == 1 && y == 1) maze[y, x] = 7;

                if (maze[y, x] == 1) // если стенка
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(Convert.ToChar(0x2593));
                }
                else if (maze[y, x] == 2) // если монетка
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("$");
                    money_total++;
                }
                else if (maze[y, x] == 7)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write((char)1);
                }
                else if (maze[y, x] == 3) // если враг
                {
                    if (r.Next(0, 15) == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write((char)1);
                    }
                    else
                    {
                        maze[y, x] = 0;
                        Console.Write(" ");
                    }
                }
                else if (maze[y, x] == 4)
                {
                    if (r.Next(0, 15) == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write('U');
                    }
                    else
                    {
                        maze[y, x] = 0;
                        Console.Write(" ");
                    }
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }

        //////////////////////////////////////////  ENEMY QUANT /////////////////////////////////////////////////////

        for (int j = 0; j < height; j++)
            for (int i = 0; i < width; i++)
                if (maze[j, i] == 3) enemy_quant++;

        //cc[] enemy = new cc[enemy_quant];

        int cur = 0; // заполнение информации о координатах и направлении врагов

        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                if (maze[j, i] == 3)
                {
                    enemy[cur] = new cc();
                    enemy[cur].dir = 2;
                    enemy[cur].c.x = i;
                    enemy[cur].c.y = j;
                    cur++;
                }
            }
        }

        ///////////////////////////////////////   SHOW INFO   /////////////////////////////////////////////////////////

        Sample.ShowMoney();
        Sample.ShowLives();
        Sample.ShowEnergy();
        Sample.ShowBombs();
        Sample.ShowEnemy();

        /* BUY BOMBS TEXT*/
        Console.SetCursorPosition(5, height + 3);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("PRESS '");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Z");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("' TO BUY BOMBS (");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("10$");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(" FOR ONE)");

        /* BUY BOMBS UPGRADE TEXT*/
        Console.SetCursorPosition(5, height + 5);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("PRESS '");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("X");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("' TO BUY BOMB UPGRADE (");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("15$");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(" FOR ONE)");

        /* BUY LIFE TEXT*/
        Console.SetCursorPosition(5, height + 7);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("PRESS '");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("C");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("' TO BUY LIFE (");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("20$");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(" FOR ONE)");

        /* SET BOMB TEXT*/
        Console.SetCursorPosition(5, height + 9);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("PRESS '");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("SPACE");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("' TO SET BOMB");

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        SoundPlayer player = new SoundPlayer();
        player.SoundLocation = Environment.CurrentDirectory + @"\Assets\Maze.wav";
        player.Play();

        while (true)
        {
            if (lives == 0)         // END GAME
            {
                Console.SetCursorPosition(width + 5, 20);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("YOU LOSE, GOOD BUY!!!   ");
                Win32Interop.ExitProcess(0);
            }

            if (energy == 0)
            {
                Console.SetCursorPosition(width + 5, 20);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("YOU LOSE, GOOD BUY!!!   ");
                Win32Interop.ExitProcess(0);
            }

            if (enemy_quant == 0)         // WIN GAME
            {
                Console.SetCursorPosition(width + 5, 20);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("YOU WIN, THANK YOU!!!   ");
                Win32Interop.ExitProcess(0);
            }

            if (money_total == 0)         // WIN GAME
            {
                Console.SetCursorPosition(width + 5, 20);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("YOU WIN, THANK YOU!!!   ");
                Win32Interop.ExitProcess(0);
            }

            if (Win32Interop._kbhit() != 0)
            {
                if (bombflag == true) ;
                k = Console.ReadKey(true);

                Console.SetCursorPosition(c.x, c.y);

                if (maze[c.y, c.x] != 999)  // if bomb
                    Console.Write(" ");

                /////////////////////////////////////////////  ARROW KEYS ////////////////////////////////////////////////////////////

                if (k.Key == ConsoleKey.DownArrow && maze[c.y + 1, c.x] != 1 && maze[c.y + 1, c.x] != 999)
                {
                    if (maze[c.y + 1, c.x] == 2)
                    {
                        money++;
                        money_total--;
                        Sample.ShowMoney();
                    }
                    else if (maze[c.y + 1, c.x] == 4)
                    {
                        energy += 25;
                    }

                    c.y++;
                }

                if (k.Key == ConsoleKey.UpArrow && maze[c.y - 1, c.x] != 1 && maze[c.y - 1, c.x] != 999)
                {
                    if (maze[c.y - 1, c.x] == 2)
                    {
                        money++;
                        money_total--;
                        Sample.ShowMoney();
                    }
                    else if (maze[c.y - 1, c.x] == 4)
                    {
                        energy += 25;
                    }

                    c.y--;
                }

                if (k.Key == ConsoleKey.LeftArrow && maze[c.y, c.x - 1] != 1 && maze[c.y, c.x - 1] != 999)
                {
                    if (maze[c.y, c.x - 1] == 2)
                    {
                        money++;
                        money_total--;
                        Sample.ShowMoney();
                    }
                    else if (maze[c.y, c.x - 1] == 4)
                    {
                        energy += 25;
                    }

                    c.x--;
                }

                if (k.Key == ConsoleKey.RightArrow && maze[c.y, c.x + 1] != 1 && maze[c.y, c.x + 1] != 999)
                {
                    if (maze[c.y, c.x + 1] == 2)
                    {
                        money++;
                        money_total--;
                        Sample.ShowMoney();
                    }
                    else if (maze[c.y, c.x + 1] == 4)
                    {
                        energy += 25;
                    }

                    c.x++;
                }
                energy--;
                Sample.ShowEnergy();

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                Console.SetCursorPosition(c.x, c.y);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write((char)1);

                //////////////////////////////////// BUY KEYS ///////////////////////////////////////////////////////////////////////

                if (k.Key == ConsoleKey.Spacebar)   // bombs
                {
                    if (bombs != 0 && bombflag != true)
                    {
                        bombs--;
                        Sample.ShowBombs();

                        Console.SetCursorPosition(c.x, c.y);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("*");

                        maze[c.y, c.x] = 999;
                        bombflag = true;
                        bomb.x = c.x;
                        bomb.y = c.y;

                        System.Timers.Timer t = new System.Timers.Timer();
                        t.AutoReset = false;
                        t.Interval = 3000;
                        t.Elapsed += new ElapsedEventHandler(BombExplode);
                        t.Start();
                    }
                }

                if (k.Key == ConsoleKey.Q)   // bombs
                {
                    if (energy >= 50 && kickflag != true)
                    {
                        energy -= 10;
                        Sample.ShowEnergy();

                        kickflag = true;
                        kickPos.x = c.x;
                        kickPos.y = c.y;

                        System.Timers.Timer t = new System.Timers.Timer();
                        t.AutoReset = false;
                        t.Interval = 500;
                        t.Elapsed += new ElapsedEventHandler(Kick);
                        t.Start();
                    }

                }

                    if (k.Key == ConsoleKey.Z)   // BUY BOMBS
                {
                    if ((money - 10) >= 0)
                    {
                        money -= 10;
                        bombs += 1;
                        ShowMoney();
                        ShowBombs();

                        Console.SetCursorPosition(width + 5, 13);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("$$$ BOMB + 1 $$$ ");

                        Thread.Sleep(600);

                        Console.SetCursorPosition(width + 5, 13);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("                ");
                    }
                    else
                    {
                        Console.SetCursorPosition(width + 5, 13);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("NOT ENOUGH");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" MONEY");

                        Thread.Sleep(600);

                        Console.SetCursorPosition(width + 5, 13);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("                ");
                    }
                }

                if (k.Key == ConsoleKey.X)   // BUY BOMB UPGRADE
                {
                    if ((money - 15) >= 0 && bomb_radius != 5)
                    {
                        money -= 15;
                        bomb_radius += 1;
                        ShowMoney();
                        ShowBombs();

                        Console.SetCursorPosition(width + 5, 13);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("$$$ BOMB UPGRADE + 1 $$$ ");

                        Thread.Sleep(600);

                        Console.SetCursorPosition(width + 5, 13);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("                           ");
                    }
                    else
                    {
                        if (bomb_radius == 5)
                        {
                            Console.SetCursorPosition(width + 5, 13);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("MAX RADIUS");
                        }
                        else
                        {
                            Console.SetCursorPosition(width + 5, 13);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("NOT ENOUGH");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(" MONEY");
                        }

                        Thread.Sleep(600);

                        Console.SetCursorPosition(width + 5, 13);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("                ");
                    }
                }

                if (k.Key == ConsoleKey.C)   // BUY LIFE
                {
                    if ((money - 20) >= 0)
                    {
                        money -= 20;
                        lives += 1;
                        ShowMoney();
                        ShowLives();

                        Console.SetCursorPosition(width + 5, 13);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("$$$ LIFE + 1 $$$ ");

                        Thread.Sleep(600);

                        Console.SetCursorPosition(width + 5, 13);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("                ");
                    }
                    else
                    {
                        Console.SetCursorPosition(width + 5, 13);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("NOT ENOUGH");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" MONEY");

                        Thread.Sleep(600);

                        Console.SetCursorPosition(width + 5, 13);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("                ");
                    }
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }

            ///////////////////////////////////////////////////// ENEMY MOVES //////////////////////////////////////////////////////////////

            else
            {
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        if (maze[j, i] == 3)
                        {
                            int num = -1;
                            for (int q = 0; q < enemy_quant; q++)
                            {
                                if (enemy[q].c.x == i && enemy[q].c.y == j) { num = q; break; }
                            }
                            if (num < 0) continue;
                            if (enemy[num].dir == 1)
                            {
                                if (enemy[num].c.y == c.y && enemy[num].c.x == c.x)
                                {
                                    Sample.Died();
                                }

                                Console.SetCursorPosition(enemy[num].c.x, enemy[num].c.y);
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.Write(" ");
                                maze[enemy[num].c.y, enemy[num].c.x] = 0;
                                enemy[num].c.x--;

                                if (maze[enemy[num].c.y, enemy[num].c.x] == 2)
                                {
                                    maze[enemy[num].c.y, enemy[num].c.x] = 0;
                                    money_total--;
                                    Sample.ShowMoney();

                                }

                                if (maze[enemy[num].c.y, enemy[num].c.x] != 0)
                                {
                                    enemy[num].dir = r.Next(1, 5);
                                    enemy[num].c.x++;
                                    maze[enemy[num].c.y, enemy[num].c.x] = 3;
                                    Console.SetCursorPosition(enemy[num].c.x, enemy[num].c.y);
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write((char)2);
                                }
                                else
                                {
                                    Console.SetCursorPosition(enemy[num].c.x, enemy[num].c.y);
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write((char)2);
                                    maze[enemy[num].c.y, enemy[num].c.x] = 3;
                                }


                            }
                            else if (enemy[num].dir == 2)
                            {
                                if (enemy[num].c.y == c.y && enemy[num].c.x == c.x)
                                {
                                    Sample.Died();
                                }

                                Console.SetCursorPosition(enemy[num].c.x, enemy[num].c.y);
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.Write(" ");
                                maze[enemy[num].c.y, enemy[num].c.x] = 0;
                                enemy[num].c.y--;

                                if (maze[enemy[num].c.y, enemy[num].c.x] == 2)
                                {
                                    maze[enemy[num].c.y, enemy[num].c.x] = 0;
                                    money_total--;
                                    Sample.ShowMoney();

                                }

                                if (maze[enemy[num].c.y, enemy[num].c.x] != 0)
                                {
                                    enemy[num].dir = r.Next(1, 5);
                                    enemy[num].c.y++;
                                    maze[enemy[num].c.y, enemy[num].c.x] = 3;
                                    Console.SetCursorPosition(enemy[num].c.x, enemy[num].c.y);
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write((char)2);
                                }
                                else
                                {
                                    Console.SetCursorPosition(enemy[num].c.x, enemy[num].c.y);
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write((char)2);
                                    maze[enemy[num].c.y, enemy[num].c.x] = 3;
                                }
                            }
                            else if (enemy[num].dir == 3)
                            {
                                if (enemy[num].c.y == c.y && enemy[num].c.x == c.x)
                                {
                                    Sample.Died();
                                }

                                Console.SetCursorPosition(enemy[num].c.x, enemy[num].c.y);
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.Write(" ");
                                maze[enemy[num].c.y, enemy[num].c.x] = 0;
                                enemy[num].c.x++;

                                if (maze[enemy[num].c.y, enemy[num].c.x] == 2)
                                {
                                    maze[enemy[num].c.y, enemy[num].c.x] = 0;
                                    money_total--;
                                    Sample.ShowMoney();

                                }

                                if (maze[enemy[num].c.y, enemy[num].c.x] != 0)
                                {
                                    enemy[num].dir = r.Next(1, 5);
                                    enemy[num].c.x--;
                                    maze[enemy[num].c.y, enemy[num].c.x] = 3;
                                    Console.SetCursorPosition(enemy[num].c.x, enemy[num].c.y);
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write((char)2);
                                }
                                else
                                {
                                    Console.SetCursorPosition(enemy[num].c.x, enemy[num].c.y);
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write((char)2);
                                    maze[enemy[num].c.y, enemy[num].c.x] = 3;
                                }
                                if (i < width - 1) i++;
                            }
                            else if (enemy[num].dir == 4)
                            {
                                if (enemy[num].c.y == c.y && enemy[num].c.x == c.x)
                                {
                                    Sample.Died();
                                }

                                Console.SetCursorPosition(enemy[num].c.x, enemy[num].c.y);
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.Write(" ");
                                maze[enemy[num].c.y, enemy[num].c.x] = 0;
                                enemy[num].c.y++;

                                if (maze[enemy[num].c.y, enemy[num].c.x] == 2)
                                {
                                    maze[enemy[num].c.y, enemy[num].c.x] = 0;
                                    money_total--;
                                    Sample.ShowMoney();

                                }

                                if (maze[enemy[num].c.y, enemy[num].c.x] != 0)
                                {
                                    enemy[num].dir = r.Next(1, 5);
                                    enemy[num].c.y--;
                                    maze[enemy[num].c.y, enemy[num].c.x] = 3;
                                    Console.SetCursorPosition(enemy[num].c.x, enemy[num].c.y);
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write((char)2);
                                }
                                else
                                {
                                    Console.SetCursorPosition(enemy[num].c.x, enemy[num].c.y);
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write((char)2);
                                    maze[enemy[num].c.y, enemy[num].c.x] = 3;
                                }
                                if (j < height - 1) j++;
                            }
                        }
                    }
                }

                Thread.Sleep(60); // ENEMY SPEED
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
    }
}