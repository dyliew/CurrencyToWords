using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;

namespace CurrencyToWords.Services.Test
{
    [TestClass]
    public class NumberServiceTest
    {
        private INumberService _ns;

        [TestInitialize]
        public void Init()
        {
            _ns = IoC.UnityContainer.Resolve<INumberService>();
        }

        [TestMethod]
        public void ValidInputTest()
        {
            var result = _ns.ConvertPrice("0");
            Assert.AreEqual("ZERO DOLLAR", result.ToUpper());

            result = _ns.ConvertPrice("1");
            Assert.AreEqual("ONE DOLLAR", result.ToUpper());

            result = _ns.ConvertPrice("111");
            Assert.AreEqual("ONE HUNDRED AND ELEVEN DOLLARS", result.ToUpper());

            result = _ns.ConvertPrice(".01");
            Assert.AreEqual("ONE CENT", result.ToUpper());

            result = _ns.ConvertPrice("7.1");
            Assert.AreEqual("SEVEN DOLLARS AND TEN CENTS", result.ToUpper());

            result = _ns.ConvertPrice("7.00");
            Assert.AreEqual("SEVEN DOLLARS", result.ToUpper());

            result = _ns.ConvertPrice(".97");
            Assert.AreEqual("NINETY-SEVEN CENTS", result.ToUpper());

            result = _ns.ConvertPrice("12889211.07");
            Assert.AreEqual("TWELVE MILLION AND EIGHT HUNDRED AND EIGHTY-NINE THOUSAND AND TWO HUNDRED AND ELEVEN DOLLARS AND SEVEN CENTS", result.ToUpper());

            result = _ns.ConvertPrice("100000001.01");
            Assert.AreEqual("ONE HUNDRED MILLION AND ONE DOLLARS AND ONE CENT", result.ToUpper());

            result = _ns.ConvertPrice("9999999999999.99");
            Assert.AreEqual("NINE TRILLION AND NINE HUNDRED AND NINETY-NINE BILLION AND NINE HUNDRED AND NINETY-NINE MILLION AND NINE HUNDRED AND NINETY-NINE THOUSAND AND NINE HUNDRED AND NINETY-NINE DOLLARS AND NINETY-NINE CENTS", result.ToUpper());

            result = _ns.ConvertPrice("123.45");
            Assert.AreEqual("ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS", result.ToUpper());

            result = _ns.ConvertPrice("1.111");
            Assert.AreEqual("ONE DOLLAR AND ELEVEN CENTS", result.ToUpper());

            result = _ns.ConvertPrice("0.117");
            Assert.AreEqual("TWELVE CENTS", result.ToUpper());

            result = _ns.ConvertPrice(".198");
            Assert.AreEqual("TWENTY CENTS", result.ToUpper());
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "invalid input string.")]
        public void InvalidCharInputTest1()
        {
            var result = _ns.ConvertPrice("ABC");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "invalid input string.")]
        public void InvalidCharInputTest2()
        {
            var result = _ns.ConvertPrice("abc");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "invalid input string.")]
        public void InvalidCharInputTest3()
        {
            var result = _ns.ConvertPrice("1ac");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "invalid input string.")]
        public void InvalidCharInputTest4()
        {
            var result = _ns.ConvertPrice("ac2");
        }
    }
}
