using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using Market.io;

namespace Market
{
    static class Program
    {
        static float wallet;

        static List<string> productList = new List<string>();

        static List<float> priceList = new List<float>();

        static string username = Environment.UserName;

        public static float Wallet
        {
            set
            {
                if (value >= 0)
                {
                    wallet = value;
                }
                else
                {
                    Console.WriteLine("Ошибка! отрецательные значения недопустимы");
                }
            }
            get { return wallet; }
        }

        static void Main(string[] args)
        {
            productList.Add("хлеб");
            productList.Add("пиво");
            productList.Add("чай");

            priceList.Add(10);
            priceList.Add(30);
            priceList.Add(20);
            Wallet = 0f;
            bool replayMainMenu = true;
            while (replayMainMenu)
            {
                PictureUtils.DrawHouse();
                Console.Write("\t{0} вы дома. На счету ", username);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0:0.##}", wallet);
                Console.ResetColor();

                Console.Write("Идти ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("работа");
                Console.ResetColor();
                Console.Write("ть или в ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("магазин");
                Console.ResetColor();
                Console.WriteLine("?");

                Console.Write("Если хотите выйти из игры? Для выхода введите (");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("выйти");
                Console.ResetColor();
                Console.WriteLine(")");
                
                Console.Write("Для сохранения игры введите (");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("сохранить");
                Console.ResetColor();
                Console.WriteLine(")");
                
                Console.WriteLine("для загрузки игры введите (загрузить)");
                Console.Beep();
                string answer = Console.ReadLine().ToLower().Trim();

                Console.Clear();
                switch (answer)
                {
                    case "работа":
                        JobUtils.Job(Wallet, username);
                        break;
                    case "магазин":
                        Wallet = StoreUtils.Store(productList, priceList, username, Wallet);
                        break;
                    case "выйти":
                        replayMainMenu = false;
                        break;
                    case "сохранить":
                        bool replayFormatSave = false;
                        Console.Write("{0} какой тип сохранения хотите выбрать? ", username);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("текст");
                        Console.ResetColor();
                        Console.Write("овый или ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("бин");
                        Console.ResetColor();
                        Console.WriteLine("арный?");
                        Console.Beep();
                        do
                        {
                            answer = Console.ReadLine();
                            switch (answer.ToLower())
                            {
                                case "текст":
                                    SaveUtils.saveProduct(productList, priceList);
                                    replayFormatSave = true;
                                    break;
                                case "бин":
                                    SaveUtils.saveBinary(productList, priceList);
                                    replayFormatSave = true;
                                    break;
                                default:
                                    Console.WriteLine(" ОШИБКА! {0} Введите 'текст' или 'бин'.", username);
                                    Console.Beep(100, 500);
                                    break;
                            }
                        } while (!replayFormatSave);
                            break;
                    case "загрузить":
                        bool replayFormatLoad = true;
                        Console.Write("{0} какой тип загрузки хотите выбрать? ", username);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("текст");
                        Console.ResetColor();
                        Console.Write("овый или ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("бин");
                        Console.ResetColor();
                        Console.WriteLine("арный?");
                        Console.Beep();
                        do
                        {
                            answer = Console.ReadLine();
                            switch (answer.ToLower())
                            {
                                case "текст":
                                    productList = LoadingUtils.ProductLoad();
                                    priceList = LoadingUtils.PriceLoad();
                                    Wallet = LoadingUtils.WalletLoad();
                                    replayFormatLoad = false;
                                    break;
                                case "бин":
                                    productList.Clear();
                                    priceList.Clear();
                                    productList = LoadingUtils.binaryProductLoad(productList);
                                    priceList = LoadingUtils.binaryPriceLoad(priceList);
                                    Wallet = LoadingUtils.binaryWalletLoad(wallet);
                                    replayFormatLoad = false;
                                    break;
                                default:
                                    Console.WriteLine(" ОШИБКА!{0} введите 'текст' или 'бин'.", username);
                                    Console.Beep(100, 500);
                                    break;
                            }
                        } while (replayFormatLoad);

                        break;
                    default:
                        Console.WriteLine("ОШИБКА. {0} проверьте правельность набора", username);
                        Console.Beep(100, 500);
                        break;

                }
            }
        }

        
    }

    class PictureUtils
    {
        public static void DrawHouse()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t    /\\\\\\\\\\\\\\\\\\\\\\\\");
            Console.WriteLine("\t   /  \\\\\\\\\\\\\\\\\\\\\\\\");
            Console.WriteLine("\t  /    \\\\\\\\\\\\\\\\\\\\\\\\");
            Console.WriteLine("\t / (__) \\\\\\\\\\\\\\\\\\\\\\\\");
            Console.WriteLine("\t/________\\\\\\\\___\\\\\\\\\\");
            Console.WriteLine("\t| ___    |  | | |    |");
            Console.WriteLine("\t||   |   |  |-+-|    |");
            Console.WriteLine("\t||=  |   |  |_|_|    |");
            Console.WriteLine("\t||___|___|___________|");
            Console.ResetColor();

        }

        public static void DrawMarcet()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\t _________________________________");
            Console.WriteLine("\t|         ________________        |");
            Console.WriteLine("\t|        |_____СИЛЬПО_____|       |");
            Console.WriteLine("\t|        ___________________      |");
            Console.WriteLine("\t|       /  ДОБРО ПОЖАЛОВАТЬ \\\\    |");
            Console.WriteLine("\t|      /_____________________\\\\   |");
            Console.WriteLine("\t|        | ____       ____ |      |");
            Console.WriteLine("\t|        ||    |     |    ||      |");
            Console.WriteLine("\t|        || =  |     |  = ||      |");
            Console.WriteLine("\t|________||____|_____|____||______|");
            Console.ResetColor();
        }

        public static void DrawJob()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t       _____        / /|");
            Console.WriteLine("\t     /_____/|      /_/ |");
            Console.WriteLine("\t   __|     ||_ /|_|  |_|_______");
            Console.WriteLine("\t /   |_____|/ | | |  | /      /");
            Console.WriteLine("\t/____________#| |_|__|/______/|");
            Console.WriteLine("\t||||        __| |          ||||");
            Console.WriteLine("\t||||      /___|/|          ||||");
            Console.WriteLine("\t||        |    ||          ||");
            Console.WriteLine("\t||        |    |           ||");
            Console.ResetColor();
        }
    }
}
