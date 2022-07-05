﻿using Cryptocurrency.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cryptocurrency.Helper
{
    public class ViewModelLocator
    {
        private static IServiceProvider? _provider;

        protected ViewModelLocator()
        {
        }

        public static void Init(IServiceProvider provider)
        {
            _provider = provider;
        }

        public static MainViewModel MainViewModel => _provider!.GetRequiredService<MainViewModel>();
        public static StartViewModel StartViewModel => _provider!.GetRequiredService<StartViewModel>();

    }
}
