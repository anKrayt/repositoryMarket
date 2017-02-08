using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Market
{
    public class Program
    {
        public struct ShopKucha
        {
            public string[] product;
            public int[] price;
            public int wallet;

            public ShopKucha(string[] pt, int[] pe, int w)
            {
                product = pt;
                price = pe;
                wallet = w;
            }
        }

        static void Main(string[] args)
        {
            ShopKucha shop = new ShopKucha();

            shop.product = new string[3];
            shop.product[0] = "хлеб";
            shop.product[1] = "пиво";
            shop.product[2] = "чай";
            shop.price = new int[3];
            shop.price[0] = 10;
            shop.price[1] = 30;
            shop.price[2] = 20;
            shop.wallet = 0;

            bool option = true;

            while (option)
            {
                pictureHouse.meHouse(args);
                Console.WriteLine("              Я дома. На счету " + shop.wallet + "$");

                Console.WriteLine("идти на работу или в магазин?");
                Console.WriteLine("Выйти из игры?Для выхода введите (выйти)");
                Console.WriteLine("Для сохранения игры введите (сохранить)");
                string answer = Console.ReadLine();
                
                Console.Clear();
                switch (answer)
                {
                    case "работа":
                        shop.wallet = Job(shop.wallet, args);
                        break;
                    case "магазин":
                        shop = Store(shop,args);
                        break;
                    case "выйти":
                        option = false;
                        break;
                    case "сохранить":
                        Save.saveProduct(shop);
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
                        Console.WriteLine("получено 100 $. на счету " + wallet + " $. Продолжить работу?");
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
            string answer;
            bool option = true;
            while (option)
            {
                pictureMarket.meMarcet(args);
                Console.WriteLine("              Вы в магазине. Для выхода введите (домой)");
                Console.WriteLine("На данный момент в магазине есть:");
                for (int i = 0; i < shop.price.Length; i++)
                {
                    Console.WriteLine(shop.product[i] + "-" + shop.price[i]);
                }
                Console.WriteLine("Хотите добавить новый предмет в магазин?");
                answer = Console.ReadLine();
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
                        pictureMarket.meMarcet(args);
                        Console.WriteLine("ОШИБКА. Введите (да) или (нет). Для выхода из магазина введите (домой)");
                        break;
                }
            }
            return shop;
        }

        static string[] addProduct(string[] product)
        {
            string[] productNext = new string[product.Length + 1];
            Console.WriteLine("Введите название товара");
            for (int i = 0; i < product.Length; i++)
            {
                productNext[i] = product[i];
            }
            productNext[productNext.Length - 1] = Console.ReadLine();
            product = productNext;
            return product;
        }

        static int[] addPrice(int[] price)
        {
            int[] priceNext = new int[price.Length + 1];
            for (int i = 0; i < price.Length; i++)
            {

                priceNext[i] = price[i];

            }
            Console.WriteLine("Введите цену товара");
            bool result = false;
            while (!result)
            {
                result = int.TryParse(Console.ReadLine(), out priceNext[priceNext.Length - 1]);
                switch (result)
                {
                    case false:
                        Console.WriteLine("Попробуйте использовать целые числа");
                        break;
                }
                if (priceNext[priceNext.Length - 1] < 0)
                {
                    result = false;
                    Console.WriteLine("Использование отрецательных чисел запрещено");
                }
            }
            price = priceNext;
            Console.Clear();
            return price;
        }

        static int buy(ShopKucha shop)
        {
            string option;
            bool result = true;
            while (result)
            {
                Console.WriteLine("Что вы хотите купить?");
                option = Console.ReadLine();
                for (int i = 0; i < shop.product.Length; i++)
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

    public class Save
    {
        public static void saveProduct(Program.ShopKucha shop)
        {
            string writeProduct = "Product.txt";
            string writePrice = "Price.txt";
            string writeWallet = "Wallet.txt";

            StreamWriter sw = new StreamWriter(writeWallet, false, System.Text.Encoding.Default);
            StreamWriter spt = new StreamWriter(writeProduct, false, System.Text.Encoding.Default);
            StreamWriter spe = new StreamWriter(writePrice, false, System.Text.Encoding.Default);
            try
            {
                sw.WriteLine(shop.wallet);
                for (int i = 0; i < shop.product.Length; i++)
                {
                    spt.WriteLine(shop.product[i]);
                    spe.WriteLine(shop.price[i]);
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
            finally
            {
                sw.Close();
                spt.Close();
                spe.Close();
                Console.WriteLine("Сохранение завершено");
            }
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
