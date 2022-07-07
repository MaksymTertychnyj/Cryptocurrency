using System.Windows.Controls;
using Cryptocurrency.Services.Implementation;
using Cryptocurrency.Pages;
using Cryptocurrency.Services.Interfaces;
using System.ComponentModel;
using System.Windows.Input;
using Cryptocurrency.Helper;
using System.Runtime.CompilerServices;
using Cryptocurrency.Model.Enums;

namespace Cryptocurrency.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly PageService _pageService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public StartViewModel StartViewModel { get; set; }
        public ThemeProviderService ThemeProvider { get; set; }

        public MainViewModel(StartViewModel startViewModel, PageService pageService, ThemeProviderService themeProvider)
        {
            ThemeProvider = themeProvider;
            StartViewModel = startViewModel;

            _pageService = pageService;
            _pageService.OnPageChanged += (page) => StartViewModel.CurrentPage = page;
            _pageService.ChangePage(new Start());
        }

        public ICommand ClickHome
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    _pageService.ChangePage(new Start());
                    StartViewModel.OnPropertyChanged(nameof(StartViewModel.CurrentPage));
                }, (obj) => StartViewModel.CurrentPage!.GetType() != typeof(Start));
            }
        }

        public ICommand ChangeTheme
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (ThemeProvider.CurrentTheme == Theme.Light)
                    {
                        ThemeProvider.ChangeTheme(Theme.Dark, ThemeProvider._resources);
                    }
                    else
                    {
                        ThemeProvider.ChangeTheme(Theme.Light, ThemeProvider._resources);
                    }

                    OnPropertyChanged(nameof(ThemeProvider.CurrentTheme));
                });
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
