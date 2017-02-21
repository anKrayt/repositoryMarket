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

        static string userName = Environment.UserName;

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
            bool option = true;
            while (option)
            {
                Picture.meHouse();
                Console.Write("\t{0} вы дома. На счету ", userName);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0:0.##}", wallet);
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
                Console.Beep();
                string answer = Console.ReadLine();

                Console.Clear();
                switch (answer.ToLower())
                {
                    case "работа":
                        Wallet = Job(Wallet);
                        break;
                    case "магазин":
                        Store(productList, priceList);
                        break;
                    case "выйти":
                        option = false;
                        break;
                    case "сохранить":
                        bool x = true;
                        Console.Write("{0} какой тип сохранения хотите выбрать? ", userName);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("текст");
                        Console.ResetColor();
                        Console.Write("овый или ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("бин");
                        Console.ResetColor();
                        Console.WriteLine("арный?");
                        Console.Beep();
                        while (x)
                        {
                            answer = Console.ReadLine();
                            switch (answer.ToLower())
                            {
                                case "текст":
                                    Save.saveProduct(productList, priceList);
                                    x = false;
                                    break;
                                case "бин":
                                    Save.saveBinary(productList, priceList);
                                    x = false;
                                    break;
                                default:
                                    
                                    Console.WriteLine(" ОШИБКА! {0} Введите 'текст' или 'бин'.", userName);
                                    Console.Beep(100, 500);
                                    break;
                            }
                        }
                        break;
                    case "загрузить":
                        bool loadComplete = true;
                        Console.Write("{0} какой тип загрузки хотите выбрать? ", userName);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("текст");
                        Console.ResetColor();
                        Console.Write("овый или ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("бин");
                        Console.ResetColor();
                        Console.WriteLine("арный?");
                        Console.Beep();
                        while (loadComplete)
                        {
                            answer = Console.ReadLine();
                            switch (answer.ToLower())
                            {
                                case "текст":
                                    productList = Loading.ProductLoad();
                                    priceList = Loading.PriceLoad();
                                    Wallet = Loading.WalletLoad();
                                    loadComplete = false;
                                    break;
                                case "бин":
                                    productList.Clear();
                                    priceList.Clear();
                                    productList = Loading.binaryProductLoad(productList);
                                    priceList = Loading.binaryPriceLoad(priceList);
                                    Wallet = Loading.binaryWalletLoad(wallet);
                                    loadComplete = false;
                                    break;
                                default:
                                    Console.WriteLine(" ОШИБКА!{0} введите 'текст' или 'бин'.", userName);
                                    Console.Beep(100, 500);
                                    break;
                            }
                        }

                        break;
                    default:
                        Console.WriteLine("ОШИБКА. {0} проверьте правельность набора", userName);
                        Console.Beep(100, 500);
                        break;

                }
            }
        }

        static float Job(float wallet)
        {
            Picture.meJob();
            Console.WriteLine("\t{0} вы пришли на работу", userName);
            Console.WriteLine("Начать работу?");
            Console.Beep();
            bool work = true;
            while (work)
            {
                string answer = Console.ReadLine();
                switch (answer.ToLower())
                {
                    case "да":
                        wallet += 100;
                        Console.Write("Получено 100 $. на счету ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("{0} $", wallet);
                        Console.ResetColor();
                        Console.WriteLine(". Продолжыть работу?");
                        Console.Beep(500, 150);
                        Console.Beep(900, 600);
                        break;
                    case "нет":
                        work = false;
                        break;
                    default:
                        wallet -= 100;
                        Console.WriteLine("{0} вам засунули лопату в жопу и заставили закопать 100 $. НА ВАШЕМ СЧЕТУ " + wallet +
                                          " $. Вытащить лопату из задницы и продолжить работу?", userName);
                        Console.Beep(100, 500);
                        break;
                }
            }
            Console.Clear();
            return wallet;
        }

        static void Store(List<string> productList, List<float> priceList)
        {
            bool option = true;
            while (option)
            {
                Picture.meMarcet();
                Console.Write("\t{0} вы в магазине. На счету {1} Для выхода введите (", userName, wallet);
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
                    Console.WriteLine("{0:0.##}",priceList[i]);
                    Console.ResetColor();
                }
                Console.WriteLine("Хотите добавить новый предмет в магазин?");
                Console.Beep();
                var answer = Console.ReadLine();
                Console.Clear();
                switch (answer.ToLower())
                {
                    case "да":
                        Picture.meMarcet();
                        productList = AddProduct(productList);
                        priceList = AddPrice(priceList);
                        break;
                    case "нет":
                        Picture.meMarcet();
                        Wallet = Buy(productList, priceList);
                        break;
                    case "домой":
                        option = false;
                        break;
                    default:
                        Console.WriteLine("ОШИБКА. Введите (да) или (нет). Для выхода из магазина введите (домой)");
                        Console.Beep(100, 500);
                        break;
                }
            }
        }

        static List<string> AddProduct(List<string> productList)
        {
            Console.WriteLine("Введите название товара");
            Console.Beep();
            string product = Console.ReadLine();
            productList.Add(product);
            return productList;
        }

        static List<float> AddPrice(List<float> priceList)
        {
            Console.WriteLine("Введите цену товара");
            Console.Beep();
            bool result;
            do
            {
                float price;
                result = float.TryParse(Console.ReadLine(), out price);
                if (price < 0)
                {
                    result = false;
                    Console.WriteLine("Использование отрецательных чисел запрещено");
                    Console.Beep(100,500);
                }
                switch (result)
                {
                    case false:
                        Console.WriteLine("Попробуйте использовать положительные числа. Для ввода дробного числа используйте (,)");
                        Console.Beep(100, 500);
                        break;
                    case true:
                        priceList.Add(price);
                        break;
                }
                
            } while (!result);
            Console.Clear();
            return priceList;
        }

        static float Buy(List<string> productList, List<float> priceList)
        {
            string answer;
            bool result = true;
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
                Console.Write("{0} что вы хотите купить? для выхода из торговой зоны введите (", userName);
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
                            Console.WriteLine("{0} недостаточно денег для этой покупки, сходите на работу", userName);
                            Console.Beep(100, 500);
                        }
                    }
                }
                if (answer.ToLower() == "ничего")
                {
                    result = false;
                }
            } while (result);
            Console.Clear();
            return Wallet;
        }
    }

    class Picture
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

        public static void meMarcet()
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

        public static void meJob()
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
