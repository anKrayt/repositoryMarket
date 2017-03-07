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
        static float wallet; //деньги

        static List<int> satietyList = new List<int>(); //лист сытости

        static List<string> productList = new List<string>(); //лист продуктов

        static List<float> priceList = new List<float>(); //лист цен

        static List<string> productInFridge = new List<string>(); //лист продуктов в холодильнике

        static List<int> countProductInFridgeList = new List<int>(); //лист количества продуктов в холодильнике

        static string username = Environment.UserName; //имя пользователя

        public static float Wallet //свойство кошелька
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
            satietyList.Add(15);
            satietyList.Add(10);
            satietyList.Add(5);

            productInFridge.Add("хлеб");
            productInFridge.Add("пиво");
            productInFridge.Add("чай");

            countProductInFridgeList.Add(1);
            countProductInFridgeList.Add(4);
            countProductInFridgeList.Add(2);

            productList.Add("хлеб");
            productList.Add("пиво");
            productList.Add("чай");

            priceList.Add(10);
            priceList.Add(30);
            priceList.Add(20);

            Wallet = 0f;

            bool replayMainMenu;

            do
            {
                Picture.drawHouse();
                Console.Write("\t{0} вы дома. На счету ", username);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0:0.##}", wallet);
                Console.ResetColor();

                Console.Write("Вы сыты на ");
                Satiety.Count();
                Console.WriteLine('%');

                Console.Write("Идти ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("работа");
                Console.ResetColor();
                Console.Write("ть или в ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("магазин");
                Console.ResetColor();
                Console.WriteLine("?");

                Console.WriteLine("Чтобы поесть введите (есть)");

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
                string answer = Console.ReadLine().ToLower().Trim();
                Console.Clear();

                switch (answer)
                {
                    case "есть":
                        FridgeUtils.Food(productInFridge, countProductInFridgeList, satietyList);
                        break;
                    case "работа":
                        Satiety.ChangeCount(15, false);
                        Wallet = JobUtils.Job(Wallet, username);
                        break;
                    case "магазин":
                        Satiety.ChangeCount(15, false);
                        Wallet = StoreUtils.Store(productList, priceList, username, Wallet, productInFridge, countProductInFridgeList, satietyList);
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
                        do
                        {
                            answer = Console.ReadLine().ToLower().Trim();
                            switch (answer)
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
                        do
                        {
                            answer = Console.ReadLine().ToLower().Trim();
                            switch (answer)
                            {
                                case "текст":
                                    productList = LoadingUtils.loadProduct();
                                    priceList = LoadingUtils.loadPrice();
                                    Wallet = LoadingUtils.loadWallet();
                                    replayFormatLoad = false;
                                    break;
                                case "бин":
                                    productList.Clear();
                                    priceList.Clear();
                                    productList = LoadingUtils.loadBinaryProduct(productList);
                                    priceList = LoadingUtils.LoadBinaryPrice(priceList);
                                    Wallet = LoadingUtils.loadBinaryWallet(wallet);
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
                replayMainMenu = Satiety.Result();
            } while (replayMainMenu);
        }


    }

    class Picture
    {
        public static void drawHouse()
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

        public static void drawMarcet()
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

        public static void drawJob()
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

        public static void drawFridge()
        {
            Console.WriteLine("          _________");
            Console.WriteLine("         /        /|");
            Console.WriteLine("        /        / |");
            Console.WriteLine("       /_______ /__|___");
            Console.Write("      |        | ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("%");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("  *");
            Console.ResetColor();
            Console.WriteLine("  |");
            Console.Write("      |        |");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" +");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("   @");
            Console.ResetColor();
            Console.WriteLine(" |");
            Console.Write("      |        |  ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("&");
            Console.ResetColor();
            Console.WriteLine("  _ |");
            Console.Write("      |        |");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("#");
            Console.ResetColor();
            Console.WriteLine("      |");
            Console.Write("      |        | ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("?");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("  $");
            Console.ResetColor();
            Console.WriteLine("  |");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Э~~~~~");
            Console.ResetColor();
            Console.WriteLine("|________|_______|");
            Console.WriteLine("      |________|/");
        }
    }
}
