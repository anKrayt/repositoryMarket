using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Market
{
    static class FridgeClass //холодильник
    {
        public static void Food(List<string> productInFridge, List<int> countProductInFridgeList, List<int> satietyList)
        {
            string answer;
            bool replayFood = true;
            Console.WriteLine("Вы заглянули в холодильник.");

            do
            {
                Picture.meFridge();
                Console.WriteLine();
                Console.Write("Вы сыты на ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("{0}%", Satiety.SatietyInt);
                Console.ResetColor();
                Console.WriteLine("Чтобы закрыть холодильник введите (назад)");

                for (int i = 0; i < productInFridge.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("{0} ", productInFridge[i]);
                    Console.ResetColor();
                    Console.Write('-');
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" {0} шт.", countProductInFridgeList[i]);
                    Console.ResetColor();
                    Console.Write(" - восполняет");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(" {0}%", satietyList[i]);
                    Console.ResetColor();
                    Console.WriteLine(" сытости");
                }
                Console.WriteLine("Что вы хотите съесть? Введите название продукта");

                answer = Console.ReadLine().ToLower().Trim();

                for (int i = 0; i < productInFridge.Count; i++)
                {
                    if (answer == productInFridge[i]) //поедание продуктов
                    {
                        if (countProductInFridgeList[i] == 1)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Вы съели {0}. Сытость восполнина на {1}%", productInFridge[i], satietyList[i]);
                            Console.ResetColor();
                            productInFridge.RemoveAt(i);
                            countProductInFridgeList.RemoveAt(i);
                            Satiety.ChangeCount(satietyList[i], true);
                            
                            if (Satiety.SatietyInt >= 110 && Satiety.SatietyInt <= 125)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("Вы переели. Вам начало подташнивать");
                                Console.ResetColor();
                            }
                            else if (Satiety.SatietyInt >= 125)
                            {
                                Satiety.SetCount(75);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Вас вырвало. Сытость {0}", Satiety.SatietyInt);
                                Console.ResetColor();
                            }
                            break;
                        }

                        if (countProductInFridgeList[i] > 1)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Вы съели {0}. Сытость восполнина на {1}%", productInFridge[i], satietyList[i]);
                            Console.ResetColor();
                            countProductInFridgeList[i]--;
                            Satiety.ChangeCount(satietyList[i], true);

                            if (Satiety.SatietyInt >= 110 && Satiety.SatietyInt <= 125)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("Вы переели. Вам начало подташнивать");
                                Console.ResetColor();
                            }
                            else if (Satiety.SatietyInt >= 125)
                            {
                                Satiety.SetCount(75);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Вас вырвало. Сытость {0}", Satiety.SatietyInt);
                                Console.ResetColor();
                            }
                            break;
                        }
                    }
                    else if (answer == "назад")
                    {
                        replayFood = false;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("ОШИБКА! Проверьте правильность набора.");
                    }
                }
            } while (replayFood);
        }
    }
}
