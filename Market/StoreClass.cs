using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market
{
    class StoreClass
    {
        public static float Store(List<string> productList, List<float> priceList, string userName, float Wallet)
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
                    Console.WriteLine("{0:0.##}", priceList[i]);
                    Console.ResetColor();
                }
                Console.WriteLine("Хотите добавить новый предмет в магазин?");
                Console.Beep();
                var answer = Console.ReadLine().Trim().ToLower();
                Console.Clear();
                switch (answer)
                {
                    case "да":
                        Picture.meMarcet();
                        productList = AddProduct(productList);
                        priceList = AddPrice(priceList);
                        break;
                    case "нет":
                        Picture.meMarcet();
                        Wallet = Buy(productList, priceList, userName, Wallet);
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
            return Wallet;
        }

        static List<string> AddProduct(List<string> productList)
        {
            Console.WriteLine("Введите название товара");
            Console.Beep();
            string productName = Console.ReadLine().Trim().ToLower();
            productList.Add(productName);
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
                result = float.TryParse(Console.ReadLine().Trim(), out price);
                if (price < 0)
                {
                    result = false;
                    Console.WriteLine("Использование отрецательных чисел запрещено");
                    Console.Beep(100, 500);
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

        static float Buy(List<string> productList, List<float> priceList, string userName, float Wallet)
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
                Console.Write("назад");
                Console.ResetColor();
                Console.WriteLine(')');
                Console.Beep();
                answer = Console.ReadLine().Trim().ToLower();
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
                if (answer.ToLower() == "назад")
                {
                    result = false;
                }
            } while (result);
            Console.Clear();
            return Wallet;
        }
    }
}
