using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Market;

namespace Market.io
{
    class Constants
    {
        public const string fileNameTxt = "TxtSave.txt";

        public const string fileNameBinary = "BinarySave.dat";
    } 

    class Save
    {
        public static void saveProduct(List<string> productList, List<float> priceList)
        {
                StreamWriter streamWriterProduct = new StreamWriter(Constants.fileNameTxt, false, Encoding.Default);

            try
            {
                for (int i = 0; i < productList.Count; i++)
                {
                    streamWriterProduct.WriteLine(productList[i]);
                    streamWriterProduct.WriteLine(priceList[i]);
                }
                streamWriterProduct.WriteLine(HouseClass.Wallet);
                Console.WriteLine("Сохранение завершено");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
            finally
            {
                streamWriterProduct.Close();
            }
        }

        public static void saveBinary(List<string> productList, List<float> priceList)
        {
            try
            {
                using (BinaryWriter binaryWriteProduct = new BinaryWriter(File.Open(Constants.fileNameBinary, FileMode.OpenOrCreate)))
                {
                    binaryWriteProduct.Write(HouseClass.Wallet);
                    for (int i = 0; i < productList.Count; i++)
                    {
                        binaryWriteProduct.Write(productList[i]);
                        binaryWriteProduct.Write(priceList[i]);
                    }
                    Console.WriteLine("Сохранение завершено");
                }

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }
    }

    class Loading
    {
        public static List<string> ProductLoad()
        {
            List<string> product = new List<string>();

            int num = File.ReadAllLines(Constants.fileNameTxt).Length;

            try
            {
                using (StreamReader streamReaderProduct = new StreamReader(Constants.fileNameTxt, Encoding.Default))
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
                Console.Error.WriteLine(e.Message);
            }
            return product;
        }

        public static List<float> PriceLoad()
        {
            List<float> price = new List<float>();

            int LineСountFile = File.ReadAllLines(Constants.fileNameTxt).Length;

            try
            {
                using (StreamReader streamReaderPrice = new StreamReader(Constants.fileNameTxt, Encoding.Default))
                {
                    float line;
                    for (int i = 0; i < LineСountFile - 1; i += 2)
                    {
                        streamReaderPrice.ReadLine();
                        line = Convert.ToSingle(streamReaderPrice.ReadLine());
                        price.Add(line);
                    }
                    Console.WriteLine("Закгрузка прошла успешно");
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
            return price;
        }

        public static float WalletLoad()
        {
            float Wallet = 0;

            int LineСountFile = File.ReadAllLines(Constants.fileNameTxt).Length;

            try
            {
                using (StreamReader streamReaderWallet = new StreamReader(Constants.fileNameTxt, Encoding.Default))
                {
                    string line = File.ReadLines(Constants.fileNameTxt).Skip(LineСountFile - 1).First();
                    Wallet = Convert.ToSingle(line);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
            return Wallet;
        }

        public static List<string> binaryProductLoad(List<string> productList)
        {
            try
            {
                using (BinaryReader binaryReaderProduct = new BinaryReader(File.Open(Constants.fileNameBinary, FileMode.Open)))
                {
                    binaryReaderProduct.ReadSingle();
                    while (binaryReaderProduct.PeekChar() > -1)
                    {
                        productList.Add(binaryReaderProduct.ReadString());
                        binaryReaderProduct.ReadSingle();
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
            return productList;
        }

        public static List<float> binaryPriceLoad(List<float> priceList)
        {
            try
            {
                using (BinaryReader binaryReaderPrice = new BinaryReader(File.Open(Constants.fileNameBinary, FileMode.Open)))
                {
                    binaryReaderPrice.ReadSingle();
                    while (binaryReaderPrice.PeekChar() > -1)
                    {
                        binaryReaderPrice.ReadString();
                        priceList.Add(binaryReaderPrice.ReadSingle());
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
            return priceList;
        }

        public static float binaryWalletLoad(float wallet)
        {
            try
            {
                using (BinaryReader binaryReaderPrice = new BinaryReader(File.Open(Constants.fileNameBinary, FileMode.Open)))
                    wallet = binaryReaderPrice.ReadSingle();

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
            return wallet;
        }
    }


}
