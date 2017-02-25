using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market
    {
        class Satiety
        {
            static int satietyInt = 100; //сытость

            public static int SatietyInt
            {
                get { return satietyInt; }
            } //свойство сытости

            public static void Count()
            {
                Console.Write(SatietyInt);
            }

            public static void ChangeCount(int nums, bool plusOrMinus)
            {
                if (plusOrMinus)
                {
                    satietyInt += nums;
                }
                else
                {
                    satietyInt -= nums;
                }
            }

            public static void SetCount(int satietyNew)
            {
                satietyInt = satietyNew;
            }

            public static bool Result()
            {
                bool replayMainMenu = true;

                if (SatietyInt <= -75)
                {
                    Console.Clear();
                    Console.WriteLine("Вы умерли от голода.");
                    Console.WriteLine("\tКОНЕЦ ИГРЫ (>_<)");
                    replayMainMenu = false;
                    Console.ReadKey();
                }
                else if (SatietyInt <= -50)
                {
                    Console.Clear();
                    Console.WriteLine("У вас голодный обморок");
                }
                else if (SatietyInt <= -25)
                {
                    Console.Clear();
                    Console.WriteLine("Вы хотите ЖРАТЬ");
                }
                else if (SatietyInt == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Вы голодны. Пора бы поесть");
                }
                return replayMainMenu;
            }
        }
    }
