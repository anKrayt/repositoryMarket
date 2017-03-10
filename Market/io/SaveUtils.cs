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
            StreamWriter streamWriterProduct = new StreamWriter(Constants.fileNameTxt, false, Encoding.Default);

            try
            {
                using (var nameProductList = productList.GetEnumerator())
                using (var countProceList = priceList.GetEnumerator())
                    while (nameProductList.MoveNext() && countProceList.MoveNext())
                    {
                        streamWriterProduct.WriteLine(nameProductList.Current);
                        streamWriterProduct.WriteLine(countProceList.Current);
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

                    using (var nameProductList = productList.GetEnumerator())
                    using (var countProceList = priceList.GetEnumerator())
                        while (nameProductList.MoveNext() && countProceList.MoveNext())
                        {
                        binaryWriteProduct.Write(nameProductList.Current);
                        binaryWriteProduct.Write(countProceList.Current);
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
        public static List<string> loadProduct()
        {
            List<string> product = new List<string>();

            int countLines = File.ReadAllLines(Constants.fileNameTxt).Length;

            try
            {
                using (StreamReader streamReaderProduct = new StreamReader(Constants.fileNameTxt, Encoding.Default))
                {
                    string line;
                    for (int i = 0; i < countLines - 1; i += 2)
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

        public static List<float> loadPrice()
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

        public static float loadWallet()
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

        public static List<string> loadBinaryProduct(List<string> productList)
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

        public static List<float> LoadBinaryPrice(List<float> priceList)
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

        public static float loadBinaryWallet(float wallet)
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
