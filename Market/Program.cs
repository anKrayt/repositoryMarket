using System;
using System.Collections.Generic;
using System.Linq;
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
                string q = Console.ReadLine();

                if (q == "на работу")
                {
                    shop.wallet = Job(shop.wallet);
                }
                else if (q == "в магазин")
                {
                    shop = Store(shop);
                }
                else if (q == "выйти")
                {
                    w = false;
                }
            }
        }

        static int Job(int wallet)
        {
            string q;
            Console.WriteLine("Начать работу?");
            bool p = true;
            while (p)
            {
                q = Console.ReadLine();
                if (q == "да")
                {
                    wallet += 100;
                    Console.WriteLine("получено 100 $. на счету " + wallet + " $. Продолжить работу?");
                }
                else if (q == "нет")
                {
                    p = false;
                }
                else
                {
                    wallet -= 1000;
                    Console.WriteLine("Вам засунули лопату в жопу и заставили закопать 1000 $. НА ВАШЕМ СЧЕТУ " + wallet +
                                      " $. Вытащить лопату из задницы и продолжить работу?");

                }
            }
            return wallet;
        }

        static ShopKucha Store(ShopKucha shop)
        {
            string q;
            bool c = true;
            while (c)
            {

                Console.WriteLine("Вы в магазине. Для выхода введите (домой)");
                Console.WriteLine("На данный момент в магазине есть:");
                for (int i = 0; i < shop.price.Length; i++)
                {
                    Console.WriteLine(shop.product[i] + "-" + shop.price[i]);
                }
                Console.WriteLine("Хотите добавить новый предмет в магазин?");
                q = Console.ReadLine();
                if (q == "да")
                {
                    shop.product = addProduct(shop.product);
                    shop.price = addPrice(shop.price);
                }
                else if (q == "нет")
                {
                    shop.wallet = buy(shop);
                }
                else if (q == "домой")
                {
                    c = false;
                }
                else
                {
                    Console.WriteLine("ОШИБКА. Введите (да) или (нет). Для выхода из магазина введите (домой)");
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
                if (!result)
                {
                    Console.WriteLine("Попробуйте использовать целые числа");
                }
                else if (priceNext[priceNext.Length - 1] < 0)
                {
                    Console.WriteLine("Использование отрецательных чисел запрещено");
                }
            }
            price = priceNext;
            return price;
        }

        static int buy(ShopKucha shop)
        {
            string q;
            bool u = true;
            while (u)
            {
                Console.WriteLine("Что вы хотите купить?");
                q = Console.ReadLine();
                for (int i = 0; i < shop.product.Length; i++)
                {
                    if (q == shop.product[i])
                    {
                        shop.wallet -= shop.price[i];
                        Console.WriteLine("Вы купили " + shop.product[i] + " остаток на счету " + shop.wallet + "");
                    }
                }
                if (q == "ничего")
                {
                    u = false;
                }
            }
            return shop.wallet;
        }
    }
}
