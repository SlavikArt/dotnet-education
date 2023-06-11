using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_Intro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Task1 
            /* Написать программу, которая находит 
             * среднее арифметическое значение 
             * трёх вещественных чисел */

            double a, b, c;

            Console.WriteLine("Task 1");
            Console.WriteLine("Enter first num:");
            a = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter second num:");
            b = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter third num:");
            c = double.Parse(Console.ReadLine());

            double average = (a + b + c) / 3;

            Console.WriteLine("Average: " + average + "\n");
            #endregion

            #region Task 2
            /* Написать программу, которая 
             * находит корень линейного 
             * уравнения ax + b = 0 */

            double x, a1, b1;

            Console.WriteLine("Task 2");
            Console.WriteLine("ax + b = 0");

            Console.WriteLine("Enter a:");
            a1 = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter b:");
            b1 = double.Parse(Console.ReadLine());

            if (a1 == 0)
            {
                Console.WriteLine("No solution\n");
            }
            else
            {
                x = -b1 / a1;
                Console.WriteLine("x: " + x + "\n");
            }
            #endregion

            #region Task 3
            /* Пользователь вводит число и степень.
             * Программа вычисляет указанную степень 
             * этого числа (пригодится функция pow) */

            double num, pow;

            Console.WriteLine("Task 3");
            Console.WriteLine("Enter number:");
            num = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter power:");
            pow = double.Parse(Console.ReadLine());

            Console.WriteLine(Math.Pow(num, pow) + "\n");
            #endregion

            #region Task 4
            /* Написать программу, которая предлагает 
             * пользователю ввести радиус окружности,
             * а затем считает площадь и длину этой окружности. 
             * Число Pi задать в программе как вещественную константу */

            const double Pi = 3.14;
            double r;

            Console.WriteLine("Task 4");
            Console.WriteLine("Enter radius:");
            r = double.Parse(Console.ReadLine());

            Console.WriteLine("Area: " + Pi * r * r);
            Console.WriteLine("Circle length: " + 2 * Pi * r + "\n");
            #endregion

            #region Task 5
            /* Написать программу, которая предоставляет 
             * пользователю возможность ввести с клавиатуры 
             * количество гривен, и переводит это 
             * количество в доллары и евро */

            const double usdRate = 36.7;
            const double eurRate = 39.3;

            double uah;

            Console.WriteLine("Task 5");
            Console.WriteLine("Enter UAH amount:");
            uah = double.Parse(Console.ReadLine());

            Console.WriteLine(uah + " UAH in USD: " + uah / usdRate);
            Console.WriteLine(uah + " UAH in EUR: " + uah / eurRate + "\n");
            #endregion

            #region Task 6
            /* Написать программу, которая переводит 
             * километры в сухопутные и морские мили */

            const double landMileInKm = 1.60934;
            const double seaMileInKm = 1.852;

            double km;

            Console.WriteLine("Task 6");
            Console.WriteLine("Enter km:");
            km = double.Parse(Console.ReadLine());

            Console.WriteLine(km + " km in land miles: " + km / landMileInKm);
            Console.WriteLine(km + " km in sea miles: " + km / seaMileInKm + "\n");
            #endregion

            #region Task 7
            /* Написать программу, которая 
             * находит процент P от числа N */

            double n, p;

            Console.WriteLine("Task 7");
            Console.WriteLine("Enter N:");
            n = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter P:");
            p = double.Parse(Console.ReadLine());

            Console.WriteLine(p + "% of " + n + ": " + n * p / 100 + "\n");
            #endregion

            #region Task 8
            /* Написать программу, которая переводит 
             * указанное количество градусов по Цельсию 
             * в градусы по шкале Фаренгейта. 
             * Затем реализовать перевод из градусов 
             * по Фаренгейту в градусы по Цельсию */

            double celsius, fahrenheit;

            Console.WriteLine("Task 8");
            Console.WriteLine("Enter degrees in Celsius:");
            celsius = double.Parse(Console.ReadLine());

            fahrenheit = celsius * 9 / 5 + 32;

            Console.WriteLine(celsius + "°C in Fahrenheit: " + fahrenheit + "°F");

            Console.WriteLine("Enter degrees in Fahrenheit:");
            fahrenheit = double.Parse(Console.ReadLine());

            celsius = (fahrenheit - 32) * 5 / 9;

            Console.WriteLine(fahrenheit + "°F in Celsius: " + celsius + "°C\n");
            #endregion
        }
    }
}
