using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Market.io;

namespace Market
{
    public static class Program
    {
        public struct ShopKucha
        {
            public List<string> productList;
            public List<int> priceList;
            public int wallet;
        }

        static void Main(string[] args)
        {
            ShopKucha shopStruct = new ShopKucha();
            shopStruct.productList = new List<string>();
            shopStruct.productList.Add("хлеб");
            shopStruct.productList.Add("пиво");
            shopStruct.productList.Add("чай");

            shopStruct.priceList = new List<int>();
            shopStruct.priceList.Add(10);
            shopStruct.priceList.Add(30);
            shopStruct.priceList.Add(20);
            shopStruct.wallet = 0;

            bool option = true;
            while (option)
            {
                Picture.meHouse();
                Console.Write("\tЯ дома. На счету");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0} $", shopStruct.wallet);
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

                Console.Write("Выйти из игры? Для выхода введите (");
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
                string answer = Console.ReadLine();

                Console.Clear();
                switch (answer.ToLower())
                {
                    case "работа":
                        shopStruct.wallet = Job(shopStruct.wallet);
                        break;
                    case "магазин":
                        shopStruct = Store(shopStruct);
                        break;
                    case "выйти":
                        option = false;
                        break;
                    case "сохранить":
                        bool x = true;
                        Console.Write("какой тип сохранения хотите выбрать? ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("текст");
                        Console.ResetColor();
                        Console.Write("овый или ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("бин");
                        Console.ResetColor();
                        Console.WriteLine("арный?");
                        while (x)
                        {
                            answer = Console.ReadLine();
                            switch (answer.ToLower())
                            {
                                case "текст":
                                    Save.saveProduct(shopStruct);
                                    x = false;
                                    break;
                                case "бин":
                                    Save.saveBinary(shopStruct);
                                    x = false;
                                    break;
                                default:
                                    Console.WriteLine(" ОШИБКА!Введите 'текст' или 'бин'.");
                                    break;
                            }
                        }
                        break;
                    case "загрузить":
                        bool y = true;
                        Console.Write("какой тип загрузки хотите выбрать? ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("текст");
                        Console.ResetColor();
                        Console.Write("овый или ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("бин");
                        Console.ResetColor();
                        Console.WriteLine("арный?");
                        while (y)
                        {
                            answer = Console.ReadLine();
                            switch (answer.ToLower())
                            {
                                case "текст":
                                    shopStruct.productList = Loading.productLoad();
                                    shopStruct.priceList = Loading.priceLoad();
                                    shopStruct.wallet = Loading.wallteLoad();
                                    y = false;
                                    break;
                                case "бин":
                                    shopStruct.productList.Clear();
                                    shopStruct.priceList.Clear();
                                    shopStruct = Loading.binaryLoad(shopStruct);
                                    y = false;
                                    break;
                                default:
                                    Console.WriteLine(" ОШИБКА!Введите 'текст' или 'бин'.");
                                    break;
                            }
                        }

                        break;
                    default:
                        Console.WriteLine("ОШИБКА. Проверьте правельность набора");
                        break;

                }
            }
        }

        static int Job(int wallet)
        {
            Picture.meJob();
            Console.WriteLine("\tВы пришли на работу");
            Console.WriteLine("Начать работу?");
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
                        break;
                    case "нет":
                        work = false;
                        break;
                    default:
                        wallet -= 100;
                        Console.WriteLine("Вам засунули лопату в жопу и заставили закопать 100 $. НА ВАШЕМ СЧЕТУ " + wallet +
                                          " $. Вытащить лопату из задницы и продолжить работу?");
                        break;
                }
            }
            Console.Clear();
            return wallet;
        }

        static ShopKucha Store(ShopKucha shop)
        {
            bool option = true;
            while (option)
            {
                Picture.meMarcet();
                Console.Write("\tВы в магазине. Для выхода введите (");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("домой");
                Console.ResetColor();
                Console.WriteLine(")");

                Console.WriteLine("На данный момент в магазине есть:");
                for (int i = 0; i < shop.priceList.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(shop.productList[i]);
                    Console.ResetColor();
                    Console.Write('-');
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(shop.priceList[i]);
                    Console.ResetColor();
                }
                Console.WriteLine("Хотите добавить новый предмет в магазин?");
                var answer = Console.ReadLine();
                Console.Clear();
                switch (answer.ToLower())
                {
                    case "да":
                        Picture.meMarcet();
                        shop.productList = AddProduct(shop.productList);
                        shop.priceList = AddPrice(shop.priceList);
                        break;
                    case "нет":
                        Picture.meMarcet();
                        shop.wallet = Buy(shop);
                        break;
                    case "домой":
                        option = false;
                        break;
                    default:
                        Console.WriteLine("ОШИБКА. Введите (да) или (нет). Для выхода из магазина введите (домой)");
                        break;
                }
            }
            return shop;
        }

        static List<string> AddProduct(List<string> productList)
        {

            Console.WriteLine("Введите название товара");
            string product = Console.ReadLine();
            productList.Add(product);
            return productList;
        }

        static List<int> AddPrice(List<int> priceList)
        {
            Console.WriteLine("Введите цену товара");
            bool result;
            do
            {
                int price;
                result = int.TryParse(Console.ReadLine(), out price);
                if (price < 0)
                {
                    result = false;
                    Console.WriteLine("Использование отрецательных чисел запрещено");
                }
                switch (result)
                {
                    case false:
                        Console.WriteLine("Попробуйте использовать целые положительные числа");
                        break;
                    case true:
                        priceList.Add(price);
                        break;
                }
                
            } while (!result);
            Console.Clear();
            return priceList;
        }

        static int Buy(ShopKucha shop)
        {
            string answer;
            bool result = true;
            do
            {
                for (int i = 0; i < shop.priceList.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(shop.productList[i]);
                    Console.ResetColor();
                    Console.Write('-');
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(shop.priceList[i]);
                    Console.ResetColor();
                }
                Console.Write("Что вы хотите купить? для выхода из торговой зоны введите (");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("ничего");
                Console.ResetColor();
                Console.WriteLine(')');
                answer = Console.ReadLine();
                for (int i = 0; i < shop.productList.Count; i++)
                {
                    if (answer.ToLower() == shop.productList[i])
                    {
                        shop.wallet -= shop.priceList[i];
                        Console.WriteLine("Вы купили " + shop.productList[i] + " остаток на счету " + shop.wallet);
                    }
                }
                if (answer.ToLower() == "ничего")
                {
                    result = false;
                }
            } while (result);
            Console.Clear();
            return shop.wallet;
        }
    }

    public class Picture
    {
        public static void meHouse()
        {
            Console.WriteLine("\t    /\\\\\\\\\\\\\\\\\\\\\\\\         ");
            Console.WriteLine("\t   /  \\\\\\\\\\\\\\\\\\\\\\\\        ");
            Console.WriteLine("\t  /    \\\\\\\\\\\\\\\\\\\\\\\\      ");
            Console.WriteLine("\t / (__) \\\\\\\\\\\\\\\\\\\\\\\\      ");
            Console.WriteLine("\t/________\\\\\\\\___\\\\\\\\\\      ");
            Console.WriteLine("\t| ___    |  | | |    |         ");
            Console.WriteLine("\t||   |   |  |-+-|    |         ");
            Console.WriteLine("\t||=  |   |  |_|_|    |         ");
            Console.WriteLine("\t||___|___|___________|         ");
        }

        public static void meMarcet()
        {
            Console.WriteLine("\t _________________________________                                    ");
            Console.WriteLine("\t|         ________________        |   ");
            Console.WriteLine("\t|        |_____СИЛЬПО_____|       |    ");
            Console.WriteLine("\t|        ___________________      |    ");
            Console.WriteLine("\t|       /  ДОБРО ПОЖАЛОВАТЬ \\\\    |    ");
            Console.WriteLine("\t|      /_____________________\\\\   |    ");
            Console.WriteLine("\t|        | ____       ____ |      |    ");
            Console.WriteLine("\t|        ||    |     |    ||      |    ");
            Console.WriteLine("\t|        || =  |     |  = ||      |    ");
            Console.WriteLine("\t|________||____|_____|____||______|                                     ");
        }

        public static void meJob()
        {
            Console.WriteLine("\t       _____        / /|          ");
            Console.WriteLine("\t     /_____/|      /_/ |          ");
            Console.WriteLine("\t   __|     ||_ /|_|  |_|_______   ");
            Console.WriteLine("\t /   |_____|/ | | |  | /      /   ");
            Console.WriteLine("\t/____________#| |_|__|/______/|   ");
            Console.WriteLine("\t||||        __| |          ||||   ");
            Console.WriteLine("\t||||      /___|/|          ||||   ");
            Console.WriteLine("\t||        |    ||          ||     ");
            Console.WriteLine("\t||        |    |           ||     ");
        }
    }
}
