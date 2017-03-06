using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using Market.io;

namespace Market
{
    static class Program
    {
        static float wallet;

        static List<string> productList = new List<string>();

        static List<float> priceList = new List<float>();

        static string username = Environment.UserName;

        public static float Wallet
        {
            set
            {
                if (value >= 0)
                {
                    wallet = value;
                }
                else
                {
                    Console.WriteLine("Ошибка! отрецательные значения недопустимы");
                }
            }
            get { return wallet; }
        }

        static void Main(string[] args)
        {
            productList.Add("хлеб");
            productList.Add("пиво");
            productList.Add("чай");

            priceList.Add(10);
            priceList.Add(30);
            priceList.Add(20);
            Wallet = 0f;
            bool replayHouse = true;
            do
            {
                PictureUtils.meHouse();
                Console.Write("\t{0} вы дома. На счету ", username);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0:0.##}", Wallet);
                Console.ResetColor();

                Console.Write("Идти ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("работа");
                Console.ResetColor();
                Console.Write("ть или в ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("магазин");
                Console.ResetColor();
                Console.WriteLine("?");

                Console.Write("Если хотите выйти из игры? Для выхода введите (");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("выйти");
                Console.ResetColor();
                Console.WriteLine(")");

                Console.Write("Для сохранения игры введите (");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("сохранить");
                Console.ResetColor();
                Console.WriteLine(")");

                Console.WriteLine("для загрузки игры введите (загрузить)");
                string answer = Console.ReadLine().ToLower().Trim();

                Console.Clear();
                switch (answer)
                {
                    case "работа":
                        GoToJob();
                        break;
                    case "магазин":
                        GoToStore(productList, priceList);
                        break;
                    case "выйти":
                        replayHouse = false;
                        break;
                    case "сохранить":
                        SaveUtils.saveProduct(productList, priceList);
                        break;
                    case "загрузить":
                        productList = LoadingUtils.ProductLoad();
                        priceList = LoadingUtils.PriceLoad();
                        Wallet = LoadingUtils.WalletLoad();
                        break;
                    default:
                        Console.WriteLine("ОШИБКА. {0} проверьте правельность набора", username);
                        Console.Beep(100, 500);
                        break;
                }
            } while (replayHouse);
        }

        static void GoToJob()
        {
            PictureUtils.drawJob();
            Console.WriteLine("\t{0} вы пришли на работу", username);
            Console.WriteLine("Начать работу?");
            bool replayWork = true;
            do
            {
                string answer = Console.ReadLine();
                switch (answer.ToLower())
                {
                    case "да":
                        Wallet += 100;
                        Console.Write("Получено 100 $. на счету ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("{0} $", Wallet);
                        Console.ResetColor();
                        Console.WriteLine(". Продолжыть работу?");
                        break;
                    case "нет":
                        replayWork = false;
                        break;
                    default:
                        Wallet -= 100;
                        Console.WriteLine(
                            "{0} вам засунули лопату в жопу и заставили закопать 100 $. НА ВАШЕМ СЧЕТУ " + Wallet +
                            " $. Вытащить лопату из задницы и продолжить работу?", username);
                        Console.Beep(100, 500);
                        break;
                }
            } while (replayWork);
            Console.Clear();
        }

        static void GoToStore(List<string> productList, List<float> priceList)
        {
            bool replayStore = true;
            do
            {
                PictureUtils.drawMarcet();
                Console.Write("\t{0} вы в магазине. На счету {1} Для выхода введите (", username, Wallet);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("домой");
                Console.ResetColor();
                Console.WriteLine(")");

                Console.WriteLine("На данный момент в магазине есть:");
                for (int i = 0; i < priceList.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(productList[i]);
                    Console.ResetColor();
                    Console.Write('-');
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("{0:0.##}", priceList[i]);
                    Console.ResetColor();
                }
                Console.WriteLine("Хотите добавить новый предмет в магазин?");
                var answer = Console.ReadLine();
                Console.Clear();
                switch (answer.ToLower())
                {
                    case "да":
                        PictureUtils.drawMarcet();
                        AddProduct(productList);
                        AddPrice(priceList);
                        break;
                    case "нет":
                        PictureUtils.drawMarcet();
                        Wallet = Buy(productList, priceList);
                        break;
                    case "домой":
                        replayStore = false;
                        break;
                    default:
                        Console.WriteLine("ОШИБКА. Введите (да) или (нет). Для выхода из магазина введите (домой)");
                        Console.Beep(100, 500);
                        break;
                }
            } while (replayStore);
        }

        static void AddProduct(List<string> productList)
        {
            Console.WriteLine("Введите название товара");
            productList.Add(Console.ReadLine());
        }

        static void AddPrice(List<float> priceList)
        {
            Console.WriteLine("Введите цену товара");
            Console.Beep();
            bool replayAddPrice;
            do
            {
                float price;
                replayAddPrice = float.TryParse(Console.ReadLine(), out price);
                if (price < 0)
                {
                    replayAddPrice = false;
                    Console.WriteLine("Использование отрецательных чисел запрещено");
                    Console.Beep(100, 500);
                }
                else if (replayAddPrice == false)
                {
                    Console.WriteLine(
                        "Попробуйте использовать положительные числа. Для ввода дробного числа используйте (,)");
                    Console.Beep(100, 500);
                }
                else if (replayAddPrice == true)
                {
                    priceList.Add(price);
                    break;
                }

            } while (!replayAddPrice);
            Console.Clear();
        }

        static float Buy(List<string> productList, List<float> priceList)
        {
            string answer;
            bool replayBuy = true;
            do
            {
                for (int i = 0; i < priceList.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(productList[i]);
                    Console.ResetColor();
                    Console.Write('-');
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("{0:0.##}", priceList[i]);
                    Console.ResetColor();
                }
                Console.Write("{0} что вы хотите купить? для выхода из торговой зоны введите (", username);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("ничего");
                Console.ResetColor();
                Console.WriteLine(')');
                Console.Beep();
                answer = Console.ReadLine();
                for (int i = 0; i < productList.Count; i++)
                {
                    if (answer.ToLower() == productList[i])
                    {
                        if (Wallet >= priceList[i])
                        {
                            Wallet -= priceList[i];
                            Console.WriteLine("Вы купили " + productList[i] + " остаток на счету " + Wallet);
                            Console.Beep(500, 150);
                            Console.Beep(900, 600);
                        }
                        else
                        {
                            Console.WriteLine("{0} недостаточно денег для этой покупки, сходите на работу", username);
                            Console.Beep(100, 500);
                        }
                    }
                }
                if (answer.ToLower() == "ничего")
                {
                    replayBuy = false;
                }
            } while (replayBuy);
            Console.Clear();
            return Wallet;
        }
    }

    class PictureUtils
    {
        public static void meHouse()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t    /\\\\\\\\\\\\\\\\\\\\\\\\");
            Console.WriteLine("\t   /  \\\\\\\\\\\\\\\\\\\\\\\\");
            Console.WriteLine("\t  /    \\\\\\\\\\\\\\\\\\\\\\\\");
            Console.WriteLine("\t / (__) \\\\\\\\\\\\\\\\\\\\\\\\");
            Console.WriteLine("\t/________\\\\\\\\___\\\\\\\\\\");
            Console.WriteLine("\t| ___    |  | | |    |");
            Console.WriteLine("\t||   |   |  |-+-|    |");
            Console.WriteLine("\t||=  |   |  |_|_|    |");
            Console.WriteLine("\t||___|___|___________|");
            Console.ResetColor();

        }

        public static void drawMarcet()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\t _________________________________");
            Console.WriteLine("\t|         ________________        |");
            Console.WriteLine("\t|        |_____СИЛЬПО_____|       |");
            Console.WriteLine("\t|        ___________________      |");
            Console.WriteLine("\t|       /  ДОБРО ПОЖАЛОВАТЬ \\\\    |");
            Console.WriteLine("\t|      /_____________________\\\\   |");
            Console.WriteLine("\t|        | ____       ____ |      |");
            Console.WriteLine("\t|        ||    |     |    ||      |");
            Console.WriteLine("\t|        || =  |     |  = ||      |");
            Console.WriteLine("\t|________||____|_____|____||______|");
            Console.ResetColor();
        }

        public static void drawJob()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t       _____        / /|");
            Console.WriteLine("\t     /_____/|      /_/ |");
            Console.WriteLine("\t   __|     ||_ /|_|  |_|_______");
            Console.WriteLine("\t /   |_____|/ | | |  | /      /");
            Console.WriteLine("\t/____________#| |_|__|/______/|");
            Console.WriteLine("\t||||        __| |          ||||");
            Console.WriteLine("\t||||      /___|/|          ||||");
            Console.WriteLine("\t||        |    ||          ||");
            Console.WriteLine("\t||        |    |           ||");
            Console.ResetColor();
        }
    }
}
