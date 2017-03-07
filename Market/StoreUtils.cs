using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market
{
    class StoreUtils
    {
        public static float Store(List<string> productList, List<float> priceList, string userName, float Wallet)
        {
            bool replayStore = true;

            do
            {
                PictureUtils.DrawMarcet();
                Console.Write("\t{0} вы в магазине. На счету {1} Для выхода введите (", userName, Wallet);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("домой");
                Console.ResetColor();
                Console.WriteLine(")");

                Console.WriteLine("На данный момент в магазине есть:");

                using (var nameProduct = productList.GetEnumerator())
                using (var priceProduct = priceList.GetEnumerator())
                    while (nameProduct.MoveNext() && priceProduct.MoveNext())
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(nameProduct.Current);
                        Console.ResetColor();
                        Console.Write('-');
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("{0:0.##}", priceProduct.Current);
                        Console.ResetColor();
                    }

                Console.WriteLine("Хотите добавить новый предмет в магазин?");
                var answer = Console.ReadLine().Trim().ToLower();
                Console.Clear();

                switch (answer)
                {
                    case "да":
                        PictureUtils.DrawMarcet();
                        productList = AddProduct(productList);
                        priceList = AddPrice(priceList);
                        break;
                    case "нет":
                        PictureUtils.DrawMarcet();
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
            productList.Add(Console.ReadLine().Trim().ToLower());
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
                using (var nameProduct = productList.GetEnumerator())
                using (var priceProduct = priceList.GetEnumerator())
                    while (nameProduct.MoveNext() && priceProduct.MoveNext())
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(nameProduct.Current);
                        Console.ResetColor();
                        Console.Write('-');
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("{0:0.##}", priceProduct.Current);
                        Console.ResetColor();
                    }
                Console.Write("{0} что вы хотите купить? для выхода из торговой зоны введите (", userName);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("назад");
                Console.ResetColor();
                Console.WriteLine(')');
                Console.Beep();
                answer = Console.ReadLine().Trim().ToLower();

                using (var nameProduct = productList.GetEnumerator())
                using (var priceProduct = priceList.GetEnumerator())
                    while (nameProduct.MoveNext() && priceProduct.MoveNext())
                    {
                        if (answer.ToLower() == nameProduct.Current)
                        {
                            if (Wallet >= priceProduct.Current)
                            {
                                Wallet -= priceProduct.Current;
                                Console.WriteLine("Вы купили {0}, остаток на счету {1}$", nameProduct.Current, Wallet);
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
