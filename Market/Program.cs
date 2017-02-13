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
            public List<string> product;
            public List<int> price;
            public int wallet;

            public ShopKucha(List<string> pt, List<int> pe, int w)
            {
                product = pt;
                price = pe;
                wallet = w;
            }
        }

        static void Main(string[] args)
        {
            ShopKucha shop = new ShopKucha();
            shop.product = new List<string>();
            shop.product.Add("хлеб");
            shop.product.Add("пиво");
            shop.product.Add("чай");

            shop.price = new List<int>();
            shop.price.Add(10);
            shop.price.Add(30);
            shop.price.Add(20);
            shop.wallet = 0;

            bool option = true;

            while (option)
            {
                pictureHouse.meHouse(args);
                Console.Write("              Я дома. На счету ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0} $", shop.wallet);
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

                Console.Write("Выйти из игры?Для выхода введите (");
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
                switch (answer)
                {
                    case "работа":
                        shop.wallet = Job(shop.wallet, args);
                        break;
                    case "магазин":
                        shop = Store(shop, args);
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
                            switch (answer)
                            {
                                case "текст":
                                    Save.saveProduct(shop);
                                    x = false;
                                    break;
                                case "бин":
                                    Save.saveB(shop);
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
                            switch (answer)
                            {
                                case "текст":
                                    shop.product = loading.productSave();
                                    shop.price = loading.priceSave();
                                    shop.wallet = loading.wallte();
                                    y = false;
                                    break;
                                case "бин":
                                    shop.product.Clear();
                                    shop.price.Clear();
                                    shop = loading.binarySave(shop);
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

        static int Job(int wallet, string[] args)
        {
            pictureJob.meJob(args);
            Console.WriteLine("             Вы пришли на работу");
            Console.WriteLine("Начать работу?");
            bool option = true;
            while (option)
            {
                string answer = Console.ReadLine();
                switch (answer)
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
                        option = false;
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

        static ShopKucha Store(ShopKucha shop, string[] args)
        {
            bool option = true;
            while (option)
            {
                pictureMarket.meMarcet(args);
                Console.Write("              Вы в магазине. Для выхода введите (");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("домой");
                Console.ResetColor();
                Console.WriteLine(")");

                Console.WriteLine("На данный момент в магазине есть:");
                for (int i = 0; i < shop.price.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(shop.product[i]);
                    Console.ResetColor();
                    Console.Write("-");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(shop.price[i]);
                    Console.ResetColor();
                }
                Console.WriteLine("Хотите добавить новый предмет в магазин?");
                var answer = Console.ReadLine();
                Console.Clear();
                switch (answer)
                {
                    case "да":
                        pictureMarket.meMarcet(args);
                        shop.product = addProduct(shop.product);
                        shop.price = addPrice(shop.price);
                        break;
                    case "нет":
                        pictureMarket.meMarcet(args);
                        shop.wallet = buy(shop);
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

        static List<string> addProduct(List<string> product)
        {

            Console.WriteLine("Введите название товара");
            string answer = Console.ReadLine();
            product.Add(answer);
            return product;
        }

        static List<int> addPrice(List<int> price)
        {
            Console.WriteLine("Введите цену товара");
            bool result = false;
            while (!result)
            {
                int num;
                result = int.TryParse(Console.ReadLine(), out num);
                switch (result)
                {
                    case false:
                        Console.WriteLine("Попробуйте использовать целые числа");
                        break;
                    case true:
                        price.Add(num);
                        break;
                }
                if (price[price.Count - 1] < 0)
                {
                    result = false;
                    Console.WriteLine("Использование отрецательных чисел запрещено");
                }
            }
            Console.Clear();
            return price;
        }

        static int buy(ShopKucha shop)
        {
            string option;
            bool result = true;
            while (result)
            {
                for (int i = 0; i < shop.price.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(shop.product[i]);
                    Console.ResetColor();
                    Console.Write("-");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(shop.price[i]);
                    Console.ResetColor();
                }
                Console.Write("Что вы хотите купить? для выхода из торговой зоны введите (");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("ничего");
                Console.ResetColor();
                Console.WriteLine(")");
                option = Console.ReadLine();
                for (int i = 0; i < shop.product.Count; i++)
                {
                    if (option == shop.product[i])
                    {
                        shop.wallet -= shop.price[i];
                        Console.WriteLine("Вы купили " + shop.product[i] + " остаток на счету " + shop.wallet + "");
                    }
                }
                switch (option)
                {
                    case "ничего":
                        result = false;
                        break;
                }
            }
            Console.Clear();
            return shop.wallet;
        }
    }

    public class pictureHouse
    {
        public static void meHouse(string[] args)
        {
            Console.WriteLine("                 /\\\\\\\\\\\\\\\\\\\\\\\\         ");
            Console.WriteLine("                /  \\\\\\\\\\\\\\\\\\\\\\\\        ");
            Console.WriteLine("               /    \\\\\\\\\\\\\\\\\\\\\\\\      ");
            Console.WriteLine("              / (__) \\\\\\\\\\\\\\\\\\\\\\\\      ");
            Console.WriteLine("             /________\\\\\\\\___\\\\\\\\\\      ");
            Console.WriteLine("             | ___    |  | | |    |         ");
            Console.WriteLine("             ||   |   |  |-+-|    |         ");
            Console.WriteLine("             ||=  |   |  |_|_|    |         ");
            Console.WriteLine("             ||___|___|___________|         ");
        }
    }

    public class pictureMarket
    {
        public static void meMarcet(string[] args)
        {
            Console.WriteLine("                  _________________________________                                    ");
            Console.WriteLine("                 |         ________________        |   ");
            Console.WriteLine("                 |        |_____СИЛЬПО_____|       |    ");
            Console.WriteLine("                 |        ___________________      |    ");
            Console.WriteLine("                 |       /  ДОБРО ПОЖАЛОВАТЬ \\\\    |    ");
            Console.WriteLine("                 |      /_____________________\\\\   |    ");
            Console.WriteLine("                 |        | ____       ____ |      |    ");
            Console.WriteLine("                 |        ||    |     |    ||      |    ");
            Console.WriteLine("                 |        || =  |     |  = ||      |    ");
            Console.WriteLine("                 |________||____|_____|____||______|                                     ");
        }
    }

    public class pictureJob
    {
        public static void meJob(string[] args)
        {
            Console.WriteLine("                   _____        / /|          ");
            Console.WriteLine("                 /_____/|      /_/ |          ");
            Console.WriteLine("               __|     ||_ /|_|  |_|_______   ");
            Console.WriteLine("             /   |_____|/ | | |  | /      /   ");
            Console.WriteLine("            /____________#| |_|__|/______/|   ");
            Console.WriteLine("            ||||        __| |          ||||   ");
            Console.WriteLine("            ||||      /___|/|          ||||   ");
            Console.WriteLine("            ||        |    ||          ||     ");
            Console.WriteLine("            ||        |    |           ||     ");
        }
    }
}
