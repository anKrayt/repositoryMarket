using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.io
{
    class Save
    {
        public static void saveProduct(Program.ShopKucha shop)
        {
            string writeProduct = "allSave.txt";


            StreamWriter spt = new StreamWriter(writeProduct, false, System.Text.Encoding.Default);
            try
            {
                for (int i = 0; i < shop.product.Count; i++)
                {
                    spt.WriteLine(shop.product[i]);
                    spt.WriteLine(shop.price[i]);
                }
                spt.WriteLine(shop.wallet);
                Console.WriteLine("Сохранение завершено");
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
            finally
            {
                spt.Close();
            }
        }

        public static void saveB(Program.ShopKucha shop)
        {
            string save = "saveBinary.dat";

            try
            {
                using (BinaryWriter bpt = new BinaryWriter(File.Open(save, FileMode.OpenOrCreate)))
                {
                    bpt.Write(shop.wallet);
                    for (int i = 0; i < shop.product.Count; i++)
                    {
                        bpt.Write(shop.product[i]);
                        bpt.Write(shop.price[i]);
                    }
                    Console.WriteLine("Сохранение завершено");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public static class loading
    {
        public static List<string> productSave()
        {
            List<string> product = new List<string>();
            string writeProduct = "allSave.txt";
            int num = File.ReadAllLines("allSave.txt").Length;

            try
            {
                using (StreamReader spt = new StreamReader(writeProduct, System.Text.Encoding.Default))
                {
                    string line;
                    for (int i = 0; i < num - 1; i += 2)
                    {
                        line = spt.ReadLine();
                        product.Add(line);
                        spt.ReadLine();
                    }
                }

            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            return product;
        }

        public static List<int> priceSave()
        {
            List<int> price = new List<int>();
            string writePrice = "allSave.txt";
            int num = File.ReadAllLines("allSave.txt").Length;

            try
            {
                using (StreamReader spe = new StreamReader(writePrice, System.Text.Encoding.Default))
                {
                    int line;
                    for (int i = 0; i < num - 1; i += 2)
                    {
                        spe.ReadLine();
                        line = Convert.ToInt32(spe.ReadLine());
                        price.Add(line);
                    }
                    Console.WriteLine("Закгрузка прошла успешно");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return price;
        }

        public static int wallte()
        {
            int wallet;
            string readWallet = "allSave.txt";
            int num = File.ReadAllLines("allSave.txt").Length;
            using (StreamReader sw = new StreamReader(readWallet, System.Text.Encoding.Default))
            {
                string line = File.ReadLines("allSave.txt").Skip(num - 1).First();
                wallet = Convert.ToInt32(line);

            }
            return wallet;
        }

        public static Program.ShopKucha binarySave(Program.ShopKucha shop)
        {
            string save = "saveBinary.dat";
            try
            {
                using (BinaryReader bpt = new BinaryReader(File.Open(save, FileMode.Open)))
                {
                    shop.wallet = bpt.ReadInt32();
                    while (bpt.PeekChar() > -1)
                    {
                        shop.product.Add(bpt.ReadString());
                        shop.price.Add(bpt.ReadInt32());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return shop;
        }
    }


}
