using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"D:\Learning\PDFEditor\Script\Sample.txt";
            //var tableName = "IPs";
            var numberOfData = 200;
            //var columns = "Name,Password";

            // Check if file already exists. If yes, delete it.     
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using (StreamWriter sw = File.CreateText(fileName))
            {
                //sw.WriteLine($"INSERT INTO {tableName} ({columns}) VALUES");
                sw.WriteLine($"Id    Namn    Epost    Rapportägare    Immediate manager e-mail    Org1    Org2    Org3    Språk");
                string managerEmail = null;
                for (int i = 0; i < numberOfData; i++)
                {
                    string userEmail = $"htestuser{i}@test.com";
                    //sw.WriteLine($"(TestUser{1},{RandomString(8)})");
                    sw.WriteLine($"{i}   TestUser{i}  {userEmail}    {managerEmail}  Company total  UnitA  Subunit1  1");
                    managerEmail = userEmail;
                    //if(i != (numberOfData - 1)) 
                    //    sw.Write(",");
                }
            }
            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
