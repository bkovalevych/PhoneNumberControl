using PhoneNumber.WPF.Models;

namespace PhoneNumber.WPF.ViewModels
{
    internal class PhoneNumberLocaleViewModel : ViewModelBase
    {
        private bool _hidden;

        public PhoneNumberLocaleViewModel()
        {
                
        }

        public PhoneNumberLocaleViewModel(PhoneNumberLocale phoneNumber)
        {
            IconSrc = phoneNumber.IconSrc;
            Name = phoneNumber.Name;
            Phone = phoneNumber.Phone;
        }

        public string IconSrc { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool Hidden { get => _hidden; set => SetProperty(ref _hidden, value); }
    }
}
