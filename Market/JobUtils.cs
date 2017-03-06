using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market
{
    class JobUtils
    {
        public static void Job(float Wallet, string userName)
        {
            PictureUtils.DrawJob();
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
                        Wallet += 100;
                        Console.Write("Получено 100 $. на счету ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("{0} $", Wallet);
                        Console.ResetColor();
                        Console.WriteLine(". Продолжыть работу?");
                        Console.Beep(500, 150);
                        Console.Beep(900, 600);
                        break;
                    case "нет":
                        replayWork = false;
                        break;
                    default:
                        Wallet -= 100;
                        Console.WriteLine(
                            "{0} вам засунули лопату в жопу и заставили закопать 100 $. НА ВАШЕМ СЧЕТУ " + Wallet +
                            " $. Вытащить лопату из задницы и продолжить работу?", userName);
                        Console.Beep(100, 500);
                        break;
                }
            } while (replayWork);
            Console.Clear();
        }
    }
}
