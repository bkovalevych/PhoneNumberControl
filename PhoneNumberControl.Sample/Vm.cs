using PhoneNumber.WPF.Models;
using System.ComponentModel;

namespace PhoneNumberControl.SampleWithBinding
{
    internal class Vm : INotifyPropertyChanged
    {
        private string _fullPhoneNumber;
        private string _phoneNumber;
        private PhoneNumberLocale _phoneLocale;
        public string FullPhoneNumber
        {
            get => _fullPhoneNumber;
            set
            {
                _fullPhoneNumber = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FullPhoneNumber"));
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PhoneNumber"));
            }
        }

        public PhoneNumberLocale PhoneLocale
        {
            get => _phoneLocale;
            set
            {
                _phoneLocale = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PhoneLocale"));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
