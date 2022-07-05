using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using Cryptocurrency.Model.Data;
using Cryptocurrency.Pages;
using Cryptocurrency.Services.Interfaces;

namespace Cryptocurrency.ViewModel
{
    public class StartViewModel : INotifyPropertyChanged
    {
        private readonly IRetrieveDataService _retrieveDataService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Asset> Assets { get; set; } = new ObservableCollection<Asset>();
        public Page? CurrentPage { get; set; }

        public StartViewModel(IRetrieveDataService retrieveDataService)
        {
            _retrieveDataService = retrieveDataService;
            Task.FromResult(RetrieveAssets());
        }

        private Asset? _selectedAsset;
        public Asset? SelectedAsset 
        {
            get { return _selectedAsset; }
            set 
            { 
                _selectedAsset = value;
                CurrentPage = new AssetPage();
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private async Task RetrieveAssets()
        {
            var assets = await _retrieveDataService.GetAssetsAsync(1);
            assets.ToList().ForEach(asset => Assets.Add(asset));
        }
    }
}
