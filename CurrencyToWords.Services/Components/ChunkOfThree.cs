using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyToWords.Services.Components
{
    public class ChunkOfThree
    {
        private readonly string _chunk;

        public ChunkOfThree(string s)
        {
            _chunk = s;
        }

        public int Length { get { return _chunk.Length; } }

        public string Hundred { get { return (Length == 3 ? _chunk[0].ToString(CultureInfo.InvariantCulture) : ""); } }
        public string Ten { get { return (Length > 1 ? _chunk[_chunk.Length - 2].ToString(CultureInfo.InvariantCulture) : ""); } }
        public string Single { get { return (Length > 0 ? _chunk[_chunk.Length - 1].ToString(CultureInfo.InvariantCulture) : ""); } }
        public int HundredToInt { get { return string.IsNullOrWhiteSpace(Hundred) ? 0 : Convert.ToInt32(Hundred); } }
        public int TenToInt { get { return string.IsNullOrWhiteSpace(Ten) ? 0 : Convert.ToInt32(Ten); } }
        public int SingleToInt { get { return string.IsNullOrWhiteSpace(Single) ? 0 : Convert.ToInt32(Single); } }
        public int Value { get { return Convert.ToInt32(_chunk); } }
        public int HundredTenSingleValue { get{ return Length > 2 ? Convert.ToInt32(Hundred + Ten + Single) : TenSingleValue; } }
        public int TenSingleValue { get { return Length > 1 ? Convert.ToInt32(Ten + Single) : SingleValue; }}
        public int TenValue { get { return Length > 1 ? Convert.ToInt32(Ten + "0") : SingleValue; } }
        public int SingleValue { get { return SingleToInt; } }

        public override string ToString()
        {
            return _chunk;
        }

        public Boolean IsZero()
        {
            return string.IsNullOrWhiteSpace(_chunk) || Value == 0;
        }
    }
}
