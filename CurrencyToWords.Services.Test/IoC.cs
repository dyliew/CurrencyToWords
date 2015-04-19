using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace CurrencyToWords.Services.Test
{
    static class IoC
    {
        static UnityContainer _unityContainer = new UnityContainer();

        static IoC()
        {
            _unityContainer.RegisterType<INumberService, NumberServiceA>();
        }

        public static UnityContainer UnityContainer
        {
            get { return _unityContainer; }
        }
    }
}
