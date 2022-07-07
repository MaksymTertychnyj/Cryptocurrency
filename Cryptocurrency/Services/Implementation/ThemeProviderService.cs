﻿using Cryptocurrency.Model.Enums;
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
