using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market
{
    class StoreClass
    {
        public static float Store(List<string> productList, List<float> priceList, string userName, float Wallet, List<string> productInFridge, List<int> countProductInFridgeList, List<int> satietyList)
        {
            bool replayStore = true;

            do
            {
                Picture.meMarcet();
                Console.Write("\t{0} вы в магазине. На счету {1} Для выхода введите (", userName, Wallet);
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
                    Console.Write("{0:0.##}", priceList[i]);
                    Console.ResetColor();
                    Console.WriteLine(" восполняет {0} сытости", satietyList[i]);
                }

                Console.WriteLine("Для добавления товара в магазин введите (добавить). Для покупки товаров введите (купить)");

                var answer = Console.ReadLine().Trim().ToLower();

                Console.Clear();

                switch (answer)
                {
                    case "добавить":
                        Picture.meMarcet();
                        productList = AddProduct(productList);
                        priceList = AddPrice(priceList);
                        satietyList = AddSatiety(satietyList);
                        break;
                    case "купить":
                        Picture.meMarcet();
                        Wallet = Buy(productList, priceList, userName, Wallet, productInFridge, countProductInFridgeList, satietyList);
                        break;
                    case "домой":
                        replayStore = false;
                        break;
                    default:
                        Console.WriteLine("ОШИБКА. Введите (добавить) или (купить). Для выхода из магазина введите (домой)");
                        Console.Beep(100, 500);
                        break;
                }
            } while (replayStore);
            return Wallet;
        }

        static List<string> AddProduct(List<string> productList)
        {
            Console.WriteLine("Введите название товара");

            string productName = Console.ReadLine().Trim().ToLower();

            productList.Add(productName);

            return productList;
        }

        static List<float> AddPrice(List<float> priceList)
        {
            Console.WriteLine("Введите цену товара");
            bool replayAddPrice;

            do
            {
                float price;
                replayAddPrice = float.TryParse(Console.ReadLine().Trim(), out price);

                if (price < 0)
                {
                    replayAddPrice = false;
                    Console.WriteLine("Использование отрицательных чисел запрещено");
                    Console.Beep(100, 500);
                }

                switch (replayAddPrice)
                {
                    case false:
                        Console.WriteLine("Попробуйте использовать положительные числа. Для ввода дробного числа используйте (,)");
                        Console.Beep(100, 500);
                        break;
                    case true:
                        priceList.Add(price);
                        break;
                }

            } while (!replayAddPrice);
            return priceList;
        }

        static List<int> AddSatiety(List<int> satietyList)
        {
            Console.WriteLine("Введите какое количество сытости будет восполнять данный продукт.");
            bool replayAddSatiety;

            do
            {
                int satiety;

                replayAddSatiety = int.TryParse(Console.ReadLine().Trim(), out satiety);

                if (satiety < 0)
                {
                    replayAddSatiety = false;
                    Console.WriteLine("Использование отрицательных чисел запрещено");
                    Console.Beep(100, 500);
                }

                switch (replayAddSatiety)
                {
                    case false:
                        Console.WriteLine("Попробуйте использовать целые положительные числа.");
                        Console.Beep(100, 500);
                        break;
                    case true:
                        satietyList.Add(satiety);
                        break;
                }
            } while (!replayAddSatiety);
            Console.Clear();
            return satietyList;

        } 

        static float Buy(List<string> productList, List<float> priceList, string userName, float Wallet, List<string> productInFridge, List<int> countProductInFridgeList, List<int> satietyList)
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
                    Console.Write("{0:0.##}", priceList[i]);
                    Console.ResetColor();
                    Console.WriteLine(" восполняет {0} сытости", satietyList[i]);
                }

                Console.Write("{0} что вы хотите купить? для выхода из торговой зоны введите (", userName);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("назад");
                Console.ResetColor();
                Console.WriteLine(')');

                answer = Console.ReadLine().Trim().ToLower();

                for (int i = 0; i < productList.Count; i++)
                {
                    if (answer.ToLower() == productList[i])
                    {
                        if (Wallet >= priceList[i])
                        {
                            Console.Clear();
                            Wallet -= priceList[i];
                            Console.WriteLine("Вы купили " + productList[i] + " остаток на счету " + Wallet);

                            //добавление в холодильник
                            for (int j = 0; j < productInFridge.Count; j++)
                            {
                                if (productList[i] == productInFridge[j])
                                {
                                    countProductInFridgeList[j]++;
                                    break;
                                }
                                if (j == productInFridge.Count - 1)
                                {
                                    productInFridge.Add(productList[i]);
                                    countProductInFridgeList.Add(1);
                                    break;
                                }
                            }
                            Picture.meMarcet();
                        }
                        else
                        {
                            Console.WriteLine("{0} недостаточно денег для этой покупки, сходите на работу", userName);
                            Console.Beep(100, 500);
                        }
                    }
                }

                if (answer.ToLower() == "назад")
                {
                    replayBuy = false;
                }
            } while (replayBuy);
            Console.Clear();
            return Wallet;
        }
    }
}
