using Cryptocurrency.Helper;
using Cryptocurrency.Model.Data;
using Cryptocurrency.Model.Enums;
using Cryptocurrency.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Cryptocurrency.ViewModel
{
    public class AssetViewModel : INotifyPropertyChanged
    {
        private readonly IRetrieveDataService _retrieveDataService;
        public event PropertyChangedEventHandler? PropertyChanged;

        public Asset CurrentAsset { get; set; }
        public ICollectionView RatesView { get; set; }
        public ObservableCollection<Rate> Rates { get; set; } = new ObservableCollection<Rate>();
        public double ConverterValue { get; set; }

        public AssetViewModel(IRetrieveDataService retrieveDataService, StartViewModel startViewModel)
        {
            _retrieveDataService = retrieveDataService;
            Task.FromResult(RetrieveRates());
            CurrentAsset = startViewModel.SelectedAsset!;
            RatesView = CollectionViewSource.GetDefaultView(Rates);
        }

        private Rate? _selectedRate;
        public Rate? SelectedRate 
        {
            get { return _selectedRate; }
            set
            {
                _selectedRate = value;
                OnPropertyChanged(nameof(SelectedRate));
            }
        }

        private string _selectedValueConverter = "1";
        public string SelectedValueConverter 
        {
            get { return _selectedValueConverter; }
            set
            {
                if (value != null && double.TryParse(value, out double doubleValue))
                {
                    ConverterValue = doubleValue;
                    _selectedValueConverter = value;
                }
                OnPropertyChanged(nameof(SelectedValueConverter));
            }
        }

        private string? _convertResult = "0";
        public string? ConvertResult 
        {  
            get { return _convertResult; }
            set 
            {
                _convertResult = value; 
                OnPropertyChanged(nameof(ConvertResult)); 
            }
        }

        public ICommand toHomePage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OpenDefaultBrowser();
                });
            }
        }

        public ICommand ConvertToCurrency
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    ConvertAsset(Converter.toCurrency);
                });
            }
        }

        public ICommand ConvertToCryptocurrency
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    ConvertAsset(Converter.toCryptoCurrency);
                });
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void OpenDefaultBrowser()
        {
            var psi = new ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = CurrentAsset.Explorer;
            Process.Start(psi);
        }

        private async Task RetrieveRates()
        {
            var rates = await _retrieveDataService.GetRatesAsync();
            rates.ToList().ForEach(rate => Rates.Add(rate));
        }

        private void ConvertAsset(Converter toConvert)
        {
            var priceResult = double.TryParse(CurrentAsset.PriceUsd!.Replace('.', ','), out double price);
            var rateResult = double.TryParse(SelectedRate!.RateUsd!.Replace('.', ','), out double rate);
            var value = double.Parse(SelectedValueConverter);
                if (priceResult && rateResult)
                {
                    if (toConvert == Converter.toCurrency)
                    {
                        ConvertResult = (price * value / rate).ToString();
                    }
                    else
                    {
                        ConvertResult = (value * rate / price).ToString();
                    }
                }
        }
    }
}
