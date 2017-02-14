using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.io
{
    public class Constsnts
    {
        public const string writeAndRead = "streamSave.txt";
        public const string saveBinary = "saveBinary.dat";
    } 

    class Save
    {
        public static void saveProduct(Program.ShopKucha shop)
        {
                StreamWriter streamWriterProduct = new StreamWriter(Constsnts.writeAndRead, false, System.Text.Encoding.Default);
            try
            {
                for (int i = 0; i < shop.productList.Count; i++)
                {
                    streamWriterProduct.WriteLine(shop.productList[i]);
                    streamWriterProduct.WriteLine(shop.priceList[i]);
                }
                streamWriterProduct.WriteLine(shop.wallet);
                Console.WriteLine("Сохранение завершено");
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
            finally
            {
                streamWriterProduct.Close();
            }
        }

        public static void saveBinary(Program.ShopKucha shop)
        {
            try
            {
                using (BinaryWriter binaryWriteProduct = new BinaryWriter(File.Open(Constsnts.saveBinary, FileMode.OpenOrCreate)))
                {
                    binaryWriteProduct.Write(shop.wallet);
                    for (int i = 0; i < shop.productList.Count; i++)
                    {
                        binaryWriteProduct.Write(shop.productList[i]);
                        binaryWriteProduct.Write(shop.priceList[i]);
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

    static class Loading
    {
        public static List<string> productLoad()
        {
            List<string> product = new List<string>();
            int num = File.ReadAllLines(Constsnts.writeAndRead).Length;
            try
            {
                using (StreamReader streamReaderProduct = new StreamReader(Constsnts.writeAndRead, System.Text.Encoding.Default))
                {
                    string line;
                    for (int i = 0; i < num - 1; i += 2)
                    {
                        line = streamReaderProduct.ReadLine();
                        product.Add(line);
                        streamReaderProduct.ReadLine();
                    }
                }

            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            return product;
        }

        public static List<int> priceLoad()
        {
            List<int> price = new List<int>();
            int LineСountFile = File.ReadAllLines(Constsnts.writeAndRead).Length;

            try
            {
                using (StreamReader streamReaderPrice = new StreamReader(Constsnts.writeAndRead, System.Text.Encoding.Default))
                {
                    int line;
                    for (int i = 0; i < LineСountFile - 1; i += 2)
                    {
                        streamReaderPrice.ReadLine();
                        line = Convert.ToInt32(streamReaderPrice.ReadLine());
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

        public static int wallteLoad()
        {
            int wallet;
            int LineСountFile = File.ReadAllLines(Constsnts.writeAndRead).Length;
            using (StreamReader streamReaderWallet = new StreamReader(Constsnts.writeAndRead, System.Text.Encoding.Default))
            {
                string line = File.ReadLines(Constsnts.writeAndRead).Skip(LineСountFile - 1).First();
                wallet = Convert.ToInt32(line);

            }
            return wallet;
        }

        public static Program.ShopKucha binaryLoad(Program.ShopKucha shop)
        {
            try
            {
                using (BinaryReader binaryReaderProduct = new BinaryReader(File.Open(Constsnts.saveBinary, FileMode.Open)))
                {
                    shop.wallet = binaryReaderProduct.ReadInt32();
                    while (binaryReaderProduct.PeekChar() > -1)
                    {
                        shop.productList.Add(binaryReaderProduct.ReadString());
                        shop.priceList.Add(binaryReaderProduct.ReadInt32());
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
