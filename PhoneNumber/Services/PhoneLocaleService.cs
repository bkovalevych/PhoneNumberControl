using PhoneNumber.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace PhoneNumber.Services
{
    internal class PhoneLocaleService
    {
        private readonly Uri _configPath;

        public PhoneLocaleService()
        {
            _configPath = new Uri("/PhoneNumber;component/Resources/Locales/phoneNumberLocales.json", UriKind.Relative);
        }

        public async Task<List<PhoneNumberLocale>> LoadLocales()
        {
            var info = Application.GetResourceStream(_configPath);
            var configs = await JsonSerializer.DeserializeAsync<List<PhoneNumberLocale>>(info.Stream);
            info.Stream.Dispose();

            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

            var regex = new Regex(@"/(\w+)\.png");
            foreach ( var c in configs) 
            {
                var gr = regex.Match(c.IconSrc).Groups;
                var localeName = gr[1].Value;
                var matched = cultures.Where(x => x.Name.EndsWith(localeName, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return configs.OrderBy(x => x.Name).ToList();
        }
    }
}
