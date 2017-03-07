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

    class SaveUtils
    {
        public static void saveProduct(List<string> productList, List<float> priceList)
        {
            StreamWriter streamWriterProduct = new StreamWriter(Constants.fileNameTxt, false, System.Text.Encoding.Default);
            try
            {
                using (var nameProduct = productList.GetEnumerator())
                using (var priceProduct = priceList.GetEnumerator())
                    while (nameProduct.MoveNext() && priceProduct.MoveNext())
                    {
                        streamWriterProduct.WriteLine(nameProduct.Current);
                        streamWriterProduct.WriteLine(priceProduct.Current);
                    }
                streamWriterProduct.WriteLine(Program.Wallet);
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
                    binaryWriteProduct.Write(Program.Wallet);

                    using (var nameProduct = productList.GetEnumerator())
                    using (var priceProduct = priceList.GetEnumerator())
                        while (nameProduct.MoveNext() && priceProduct.MoveNext())
                        {
                            binaryWriteProduct.Write(nameProduct.Current);
                            binaryWriteProduct.Write(priceProduct.Current);
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

    class LoadingUtils
    {
        public static List<string> LoadProduct()
        {
            List<string> productList = new List<string>();
            int countLines = File.ReadAllLines(Constants.fileNameTxt).Length;
            try
            {
                using (StreamReader streamReaderProduct = new StreamReader(Constants.fileNameTxt, System.Text.Encoding.Default))
                {
                    for (int i = 0; i < countLines - 1; i += 2)
                    {
                        productList.Add(streamReaderProduct.ReadLine());
                        streamReaderProduct.ReadLine();
                    }
                }
            }
            catch (FormatException e)
            {
                Console.Error.WriteLine(e.Message);
            }
            return productList;
        }

        public static List<float> LoadPrice()
        {
            List<float> priceList = new List<float>();
            int LineСountFile = File.ReadAllLines(Constants.fileNameTxt).Length;

            try
            {
                using (StreamReader streamReaderPrice = new StreamReader(Constants.fileNameTxt, System.Text.Encoding.Default))
                {
                    for (int i = 0; i < LineСountFile - 1; i += 2)
                    {
                        streamReaderPrice.ReadLine();
                        priceList.Add(Convert.ToSingle(streamReaderPrice.ReadLine()));
                    }
                    Console.WriteLine("Закгрузка прошла успешно");
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
            return priceList;
        }

        public static float LoadWallet()
        {
            string line;
            int LineСountFile = File.ReadAllLines(Constants.fileNameTxt).Length;
            using (StreamReader streamReaderWallet = new StreamReader(Constants.fileNameTxt, System.Text.Encoding.Default))
            {
                line = File.ReadLines(Constants.fileNameTxt).Skip(LineСountFile - 1).First();
            }
            return Convert.ToSingle(line);
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
