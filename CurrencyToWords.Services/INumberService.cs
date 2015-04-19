using System.Collections.Generic;

namespace CurrencyToWords.Services
{
    public interface INumberService
    {
        string ConvertPrice(string input);
        string ConvertPrice(double input);

        Dictionary<long, string> NumberWordDict { get; }
    }
}