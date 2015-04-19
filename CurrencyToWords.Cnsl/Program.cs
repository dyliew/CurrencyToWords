using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyToWords.Services;

namespace CurrencyToWords.Cnsl
{
    public class Program
    {
        static void Main(string[] args)
        {
            var ns = new NumberServiceA();

            var input = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(ns.ConvertPrice(input));
                input = Console.ReadLine();
            }
        }
    }
}
