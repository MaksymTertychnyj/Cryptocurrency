using Cryptocurrency.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Cryptocurrency.Services.Implementation
{
    public class ThemeProviderService
    {
        public ResourceDictionary _resources { get; set; } = new ResourceDictionary();
        public Theme CurrentTheme { get; set; }
        public Model.Enums.Localization CurrentLocalization { get; set; }

        public void ChangeTheme(Theme theme, ResourceDictionary resources)
        {
            _resources = resources;
            CurrentTheme = theme;
            resources.MergedDictionaries.Clear();
            if (CurrentTheme == Theme.Light)
            {
                ApplyResources("Style/LightTheme.xaml");
            }
            else
            {
                if (CurrentTheme == Theme.Dark)
                    ApplyResources("Style/DarkTheme.xaml");
            }
        }

        public void ChangeLocalization(Model.Enums.Localization localization, ResourceDictionary resources)
        {
            _resources = resources;
            CurrentLocalization = localization;
            resources.MergedDictionaries.Clear();
            if (CurrentLocalization == Model.Enums.Localization.ENG)
            {
                ApplyResources("Localization/Eng.xaml");
            }
            else
            {
                if (CurrentLocalization == Model.Enums.Localization.UKR)
                    ApplyResources("Localization/Ukr.xaml");
            }
        }

        private void ApplyResources(string scr)
        {
            var dict = new ResourceDictionary() { Source = new Uri(scr, UriKind.Relative)};
            foreach (var mergeDict in dict.MergedDictionaries)
            {
                _resources!.MergedDictionaries.Add(mergeDict);
            }

            foreach (var key in dict.Keys)
            {
                _resources[key] = dict[key];
            }
        }
    }
}
