using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market
{
    class JobClass
    {
        public static float Job(float Wallet ,string userName)
        {
            Picture.meJob();
            Console.WriteLine("\t{0} вы пришли на работу", userName);
            Console.Write("Вы сыты на ");
            Satiety.Count();
            Console.WriteLine('%');
            Console.WriteLine("Начать работу?");
            bool replayWork = true;

            do
            {
                string answer = Console.ReadLine();
                switch (answer.ToLower())
                {
                    case "да":
                        Satiety.ChangeCount(5,false);
                        Wallet += 100;
                        Console.Write("Получено 100$. На счету ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("{0}$ ", Wallet);
                        Console.ResetColor();
                        Console.Write("Вы сыты на ");
                        Satiety.Count();
                        Console.WriteLine('%');
                        Console.WriteLine("Продолжить работу?");
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
            return Wallet;
        }
    }
}
