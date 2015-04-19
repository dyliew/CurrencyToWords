using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CurrencyToWords.Services;
using Microsoft.Practices.Unity;

namespace CurrencyToWords.Components
{
    public class UnityConfig
    {
        private UnityContainer _container;

        public UnityConfig(UnityContainer container)
        {
            _container = container;
        }

        public void config()
        {
            _container.RegisterType<INumberService, NumberServiceA>(new HierarchicalLifetimeManager());
        }
    }
}