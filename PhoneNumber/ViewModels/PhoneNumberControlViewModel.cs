using PhoneNumber.Models;
using PhoneNumber.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PhoneNumber.ViewModels
{
    internal class PhoneNumberControlViewModel : ViewModelBase
    {
        private string _searchText = string.Empty;
        private readonly PhoneLocaleService _phoneLocaleService;
        private const string DefaultLocaleName = "United States";
        
        private PhoneNumberLocale _selectedLocale;
        private string _phoneNumberPart;

        public PhoneNumberControlViewModel()
        {
            _phoneLocaleService = new PhoneLocaleService();
        }

        public string SearchText { get => _searchText; set { SetProperty(ref _searchText, value); Filter(); } }
        public string PhoneNumberPart { get => _phoneNumberPart; set => SetProperty(ref _phoneNumberPart, value); }
        public PhoneNumberLocale SelectedLocale { get => _selectedLocale; set => SetProperty(ref _selectedLocale, value); }

        public ObservableCollection<PhoneNumberLocaleViewModel> FilterableLocales { get; set; } = new ObservableCollection<PhoneNumberLocaleViewModel>();

        public string FullPhoneNumber()
        {
            return _selectedLocale != null && !string.IsNullOrEmpty(_phoneNumberPart)
                ? _selectedLocale.Phone.Substring(1) + _phoneNumberPart // exclude "+" from phone number
                : string.Empty;
        }

        public void Filter()
        {
            foreach (var locale in FilterableLocales)
            {
                locale.Hidden = SearchText != null && !locale.Name.StartsWith(SearchText, System.StringComparison.OrdinalIgnoreCase);
            }
        }

        public async Task OnLoaded()
        {
            var locales = await _phoneLocaleService.LoadLocales();
            foreach (var locale in locales)
            {
                FilterableLocales.Add(new PhoneNumberLocaleViewModel(locale));
            }
        }
    }
}
