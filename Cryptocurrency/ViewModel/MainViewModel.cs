using System.Windows.Controls;
using Cryptocurrency.Services.Implementation;
using Cryptocurrency.Pages;
using Cryptocurrency.Services.Interfaces;
using System.ComponentModel;
using System.Windows.Input;
using Cryptocurrency.Helper;

namespace Cryptocurrency.ViewModel
{
    public class MainViewModel
    {
        private readonly PageService _pageService;

        public StartViewModel StartViewModel { get; set; }

        public MainViewModel(StartViewModel startViewModel, PageService pageService)
        {
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
                }, (obj) => nameof(StartViewModel.CurrentPage) != nameof(Start));
            }
        }
    }
}
