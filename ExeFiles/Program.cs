using System;
using System.IO;
using System.Globalization;
using ExeFiles.Entities;

namespace ExeFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the full file path: ");
            string sourceFilePath = Console.ReadLine();
            
            try
            {
                string[] lines = File.ReadAllLines(sourceFilePath);

                string sourceFolderPath = Path.GetDirectoryName(sourceFilePath);
                string targetFolderPath = sourceFolderPath + @"\out";
                string targetFilePath = targetFolderPath + @"\summary.csv";

                if (!Directory.Exists(targetFolderPath))
                {
                    Directory.CreateDirectory(targetFolderPath);
                }
                
                using (StreamWriter sw = File.AppendText(targetFilePath))
                {
                    foreach (string l in lines)
                    {
                        string[] fields = l.Split(",");
                        string name = fields[0];
                        double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                        int quantity = int.Parse(fields[2]);

                        Product product = new Product(name, price, quantity);
                        sw.WriteLine(name + product.Total());
                        //Console.WriteLine(name + product.Total());
                    }   
                }

                using(StreamReader sr = File.OpenText(targetFilePath))
                {
                    while (!sr.EndOfStream)
                    {                        
                        Console.WriteLine("Product: " + sr.ReadLine());
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("\r\nAn error occured: " + e.Message);
            }
        }
    }
}
