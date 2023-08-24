using PhoneNumber.Models;
using PhoneNumber.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PhoneNumber.ViewModels
{
    internal class PhoneNumberControlViewModel : ViewModelBase
    {
        private string _searchText = string.Empty;
        private readonly PhoneLocaleService _phoneLocaleService;
        private const string DefaultLocaleName = "United States";
        private ICommand _clearFilterCommand;

        private PhoneNumberLocale _selectedLocale;
        private string _phoneNumberPart;
        private PhoneNumberLocale _firstLocale;
        private List<PhoneNumberLocale> _locales { get; set; } = new List<PhoneNumberLocale>();

        public PhoneNumberControlViewModel()
        {
            _phoneLocaleService = new PhoneLocaleService();
        }

        public string SearchText { get => _searchText; set { SetProperty(ref _searchText, value); Filter(); } }
        public string PhoneNumberPart { get => _phoneNumberPart; set => SetProperty(ref _phoneNumberPart, value); }
        public PhoneNumberLocale SelectedLocale { get => _selectedLocale; set => SetProperty(ref _selectedLocale, value); }

        public ObservableCollection<PhoneNumberLocale> FilterableLocales { get; set; } = new ObservableCollection<PhoneNumberLocale>();
        
        public ICommand ClearFilterCommand => _clearFilterCommand ??= new Command(() =>
        {
            SearchText = string.Empty;
        });

        public string FullPhoneNumber()
        {
            return _selectedLocale != null && !string.IsNullOrEmpty(_phoneNumberPart)
                ? _selectedLocale.Phone.Substring(1) + _phoneNumberPart // exclude "+" from phone number
                : string.Empty;
        }

        public void Filter()
        {
            for (var i = FilterableLocales.Count - 1; i >= 0; --i)
            {
                if (FilterableLocales[i] != SelectedLocale)
                {
                    FilterableLocales.RemoveAt(i);
                }
            }
            if (SelectedLocale != _firstLocale)
            {
                FilterableLocales.Insert(0, _firstLocale);
            }

            var filtered = _locales.Where(x =>
                x.Name != _firstLocale?.Name
                && x.Name != SelectedLocale?.Name
                && (string.IsNullOrEmpty(SearchText)
                    || x.Name.ToLower().Contains(SearchText.ToLower())
                    || x.Phone.ToLower().Contains(SearchText.ToLower()))
                );

            foreach (var locale in filtered)
            {
                FilterableLocales.Add(locale);
            }
        }

        public async Task OnLoaded()
        {
            _locales = await _phoneLocaleService.LoadLocales();
            _firstLocale = _locales.FirstOrDefault(x => x.Name == DefaultLocaleName);
            FilterableLocales.Add(_firstLocale);

            foreach (var locale in _locales.Where(x => x != _firstLocale))
            {
                FilterableLocales.Add(locale);
            }

            SelectedLocale = _firstLocale;
        }
    }
}
