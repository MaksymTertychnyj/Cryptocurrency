using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Cryptocurrency.Helper;
using Cryptocurrency.Model.Data;
using Cryptocurrency.Pages;
using Cryptocurrency.Services.Implementation;
using Cryptocurrency.Services.Interfaces;

namespace Cryptocurrency.ViewModel
{
    public class StartViewModel : INotifyPropertyChanged
    {
        private readonly IRetrieveDataService _retrieveDataService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Asset> Assets { get; set; } = new ObservableCollection<Asset>();
        public Asset? SelectedAsset { get; set; }
        public Page? CurrentPage { get; set; }

        public StartViewModel(IRetrieveDataService retrieveDataService)
        {
            _retrieveDataService = retrieveDataService;
            Task.FromResult(RetrieveAssets());
        }

        private async Task RetrieveAssets()
        {
            var assets = await _retrieveDataService.GetAssetsAsync(1);
            assets.ToList().ForEach(asset => Assets.Add(asset));
        }
    }
}
