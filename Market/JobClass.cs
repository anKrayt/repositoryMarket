using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market
{
    class JobClass
    {
        public static float Job(float wallet,string userName)
        {
            Picture.meJob();
            Console.WriteLine("\t{0} вы пришли на работу", userName);
            Console.WriteLine("Начать работу?");
            Console.Beep();
            bool replayWork = true;
            do
            {
                string answer = Console.ReadLine();
                switch (answer.ToLower())
                {
                    case "да":
                        wallet += 100;
                        Console.Write("Получено 100 $. на счету ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("{0} $", wallet);
                        Console.ResetColor();
                        Console.WriteLine(". Продолжыть работу?");
                        Console.Beep(500, 150);
                        Console.Beep(900, 600);
                        break;
                    case "нет":
                        replayWork = false;
                        break;
                    default:
                        wallet -= 100;
                        Console.WriteLine(
                            "{0} вам засунули лопату в жопу и заставили закопать 100 $. НА ВАШЕМ СЧЕТУ " + wallet +
                            " $. Вытащить лопату из задницы и продолжить работу?", userName);
                        Console.Beep(100, 500);
                        break;
                }
            } while (replayWork);
            Console.Clear();
            return wallet;
        }
    }
}
