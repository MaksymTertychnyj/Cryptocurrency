using System.Windows.Controls;
using Cryptocurrency.Services.Implementation;
using Cryptocurrency.Pages;
using Cryptocurrency.Services.Interfaces;

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
    }
}
