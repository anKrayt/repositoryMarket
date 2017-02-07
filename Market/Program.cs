using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Market
{
    class Program
    {
        struct ShopKucha
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

            bool w = true;

            while (w)
            {
                Console.WriteLine("Я дома. На счету " + shop.wallet + "$");

                Console.WriteLine("идти на работу или в магазин?");
                Console.WriteLine("Выйти из игры?Для выхода введите (выйти)");
                Console.WriteLine("Для сохранения игры введите (сохранить)");
                string answer = Console.ReadLine();

                switch (answer)
                {
                    case "работа":
                        shop.wallet = Job(shop.wallet);
                        break;
                    case "магазин":
                        shop = Store(shop);
                        break;
                    case "выйти":
                        w = false;
                        break;
                    default:
                        Console.WriteLine("ОШИБКА. Проверьте правельность набора");
                        break;
                }
            }
        }

        static int Job(int wallet)
        {
            
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
            return wallet;
        }

        static ShopKucha Store(ShopKucha shop)
        {
            string answer;
            bool option = true;
            while (option)
            {
                Console.WriteLine("Вы в магазине. Для выхода введите (домой)");
                Console.WriteLine("На данный момент в магазине есть:");
                for (int i = 0; i < shop.price.Length; i++)
                {
                    Console.WriteLine(shop.product[i] + "-" + shop.price[i]);
                }
                Console.WriteLine("Хотите добавить новый предмет в магазин?");
                answer = Console.ReadLine();
                switch (answer)
                {
                    case "да":
                        shop.product = addProduct(shop.product);
                        shop.price = addPrice(shop.price);
                        break;
                    case "нет":
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
            return shop.wallet;
        }
    }
}
