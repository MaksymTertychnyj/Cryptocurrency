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
using Cryptocurrency.Services.Interfaces;

namespace Cryptocurrency.ViewModel
{
    public class StartViewModel : INotifyPropertyChanged
    {
        private readonly IRetrieveDataService _retrieveDataService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICollectionView AssetsView { get; set; }
        public ObservableCollection<Asset> Assets { get; set; } = new ObservableCollection<Asset>();
        public Page? CurrentPage { get; set; }

        public StartViewModel(IRetrieveDataService retrieveDataService)
        {
            _retrieveDataService = retrieveDataService;
            Task.FromResult(RetrieveAssets());
            _searchText = string.Empty;
            AssetsView = CollectionViewSource.GetDefaultView(Assets);
        }

        private Asset? _selectedAsset;
        public Asset? SelectedAsset 
        {
            get { return _selectedAsset; }
            set 
            { 
                if (value != null)
                {
                _selectedAsset = value;
                CurrentPage = new AssetPage();
                OnPropertyChanged(nameof(CurrentPage));
                }
            }
        }

        private string _searchText;
        public string SearchText 
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                AssetsView.Filter = (obj) =>
                {
                    if (obj is Asset asset)
                    {
                        return asset.Name?.ToLower().Contains(SearchText.ToLower()) == true;
                    }

                    return false;
                };
            }
        }

        public ICommand Search
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    AssetsView.Refresh();
                });
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
