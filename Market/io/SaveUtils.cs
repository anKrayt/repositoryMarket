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
                for (int i = 0; i < productList.Count; i++)
                {
                    streamWriterProduct.WriteLine(productList[i]);
                    streamWriterProduct.WriteLine(priceList[i]);
                }
                streamWriterProduct.WriteLine(Program.Wallet);
                Console.WriteLine("Сохранение завершено");
            }
            catch (Exception x)
            {
                Console.Error.WriteLine(x.Message);
            }
            finally
            {
                streamWriterProduct.Close();
            }
        }
    }

    class LoadingUtils
    {
        public static List<string> ProductLoad()
        {
            List<string> product = new List<string>();
            int num = File.ReadAllLines(Constants.fileNameTxt).Length;
            try
            {
                using (StreamReader streamReaderProduct = new StreamReader(Constants.fileNameTxt, System.Text.Encoding.Default))
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
                using (StreamReader streamReaderPrice = new StreamReader(Constants.fileNameTxt, System.Text.Encoding.Default))
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
            float wallet;
            int LineСountFile = File.ReadAllLines(Constants.fileNameTxt).Length;
            using (StreamReader streamReaderWallet = new StreamReader(Constants.fileNameTxt, System.Text.Encoding.Default))
            {
                string line = File.ReadLines(Constants.fileNameTxt).Skip(LineСountFile - 1).First();
                wallet = Convert.ToSingle(line);
            }
            return wallet;
        }
    }


}
