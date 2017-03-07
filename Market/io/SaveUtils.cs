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
                    string line;
                    for (int i = 0; i < countLines - 1; i += 2)
                    {
                        line = streamReaderProduct.ReadLine();
                        productList.Add(line);
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
            int lineСountFile = File.ReadAllLines(Constants.fileNameTxt).Length;

            try
            {
                using (StreamReader streamReaderPrice = new StreamReader(Constants.fileNameTxt, Encoding.Default))
                {
                    for (int i = 0; i < lineСountFile - 1; i += 2)
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

        public static float WalletLoad()
        {
            string line;
            int LineСountFile = File.ReadAllLines(Constants.fileNameTxt).Length;
            using (StreamReader streamReaderWallet = new StreamReader(Constants.fileNameTxt, Encoding.Default))
            {
                line = File.ReadLines(Constants.fileNameTxt).Skip(LineСountFile - 1).First();
            }
            return Convert.ToSingle(line);
        }
    }


}
