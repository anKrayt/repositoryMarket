using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market
{
    class Satiety
    {
        const int death = -75;

        const int fainting = -50;

        const int veryHungry = -25;

        const int hungry = 0;

        static int satietyInt = 100; //сытость

        //возвращение свойства сытости
        public static int SatietyInt
        {
            get { return satietyInt; }
        }

        public static void Count()
        {
            Console.Write(SatietyInt);
        }

        public static void ChangeCount(int nums, bool plusOrMinus)
        {
            if (nums > 0)
            {
                satietyInt = plusOrMinus ? (satietyInt += nums) : (satietyInt -= nums);
            }
            else
            {
                Console.Error.WriteLine("Ошибка!");
            }

        }

        public static void SetCount(int satietyNew)
        {
            if (satietyNew > 0)
            {
                satietyInt = satietyNew;
            }
            else
            {
                Console.Error.WriteLine("Ошибка!");
            }
        }

        public static bool Result()
        {
            bool replayMainMenu = true;

            if (SatietyInt <= death)
            {
                Console.Clear();
                Console.WriteLine("Вы умерли от голода.");
                Console.WriteLine("\tКОНЕЦ ИГРЫ (>_<)");
                replayMainMenu = false;
                Console.ReadKey();
            }
            else if (SatietyInt <= fainting)
            {
                Console.WriteLine("У вас голодный обморок");
            }
            else if (SatietyInt <= veryHungry)
            {
                Console.WriteLine("Вы хотите ЖРАТЬ");
            }
            else if (SatietyInt == hungry)
            {
                Console.Clear();
                Console.WriteLine("Вы голодны. Пора бы поесть");
            }
            return replayMainMenu;
        }
    }
}
