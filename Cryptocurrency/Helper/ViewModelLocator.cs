using Cryptocurrency.Services.Implementation;
using Cryptocurrency.Services.Interfaces;
using Cryptocurrency.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cryptocurrency.Helper
{
    public class ViewModelLocator
    {
        private static IServiceProvider _provider;

        public static void Init(IServiceProvider provider)
        {
            _provider = provider;
        }

        public MainViewModel MainViewModel => _provider.GetRequiredService<MainViewModel>();
        public StartViewModel StartViewModel => _provider.GetRequiredService<StartViewModel>();

    }
}
