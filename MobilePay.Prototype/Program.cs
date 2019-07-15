using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MobilePay.Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            UserStory2();
            Console.WriteLine("-----------");
            UserStory3();
            Console.WriteLine("-----------");
            UserStory4();
            Console.WriteLine("-----------");
            UserStory5();
            Console.WriteLine("-----------");

            //Console.ReadKey();
        }

        private static void UserStory2()
        {
            
            var lines = File.ReadAllLines("transactions-2.txt");
            foreach (var line in lines)
            {
                var values = line.Split(' ', '\t');
                var transactionDate = values[0];
                var companyName = values[1];
                var transactionAmount = decimal.Parse(values[2]);
                var transactionFee = transactionAmount * 0.01m;
                
                Console.WriteLine($"{transactionDate} {companyName} {transactionFee:N2}");
            }
        }

        private static void UserStory3()
        {
            var lines = File.ReadAllLines("transactions-3.txt");
            foreach (var line in lines)
            {
                var values = line.Split(' ', '\t');
                var transactionDate = values[0];
                var companyName = values[1];
                var transactionAmount = decimal.Parse(values[2]);
                var transactionFee = transactionAmount * (companyName == "TELIA" ? 0.009m : 0.01m);

                Console.WriteLine($"{transactionDate} {companyName} {transactionFee:N2}");
            }
        }

        private static void UserStory4()
        {
            var lines = File.ReadAllLines("transactions-4.txt");
            foreach (var line in lines)
            {
                var values = line.Split(' ', '\t');
                var transactionDate = values[0];
                var companyName = values[1];
                var transactionAmount = decimal.Parse(values[2]);
                var transactionFee = transactionAmount * (companyName == "CIRCLE_K" ? 0.008m : 0.01m);

                Console.WriteLine($"{transactionDate} {companyName} {transactionFee:N2}");
            }
        }

        private static void UserStory5()
        {
            var companyTaxed = new HashSet<string>();
            var lines = File.ReadAllLines("transactions-5.txt");
            foreach (var line in lines)
            {
                var values = line.Split(' ', '\t');
                var transactionDate = values[0];
                var companyName = values[1];
                var transactionAmount = decimal.Parse(values[2]);
                var transactionFee = transactionAmount * (companyName == "TELIA" ? 0.009m : 0.01m);
                var companyMonthKey = $"{transactionDate.Substring(5, 2)}|{companyName}";
                if (!companyTaxed.Contains(companyMonthKey))
                {
                    transactionFee += 29;
                    companyTaxed.Add(companyMonthKey);
                }
                Console.WriteLine($"{transactionDate} {companyName} {transactionFee:N2}");
            }
        }
    }
}
