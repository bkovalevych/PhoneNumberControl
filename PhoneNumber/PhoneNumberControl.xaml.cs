using PhoneNumber.Models;
using PhoneNumber.Services;
using PhoneNumber.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhoneNumber
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class PhoneNumberControl : UserControl
    {
        public static readonly DependencyProperty FullPhoneNumberPropery = DependencyProperty.Register
            (nameof(FullPhoneNumber), typeof(string), 
            typeof(PhoneNumberControl),
            new PropertyMetadata("hello", new PropertyChangedCallback((d, e) =>
            {

            })));

        public event EventHandler<string> FullPhoneNumberChanged;
        event EventHandler<PhoneNumberLocaleViewModel> PhoneLocaleChanged;
        public event EventHandler<TextChangedEventArgs> PhoneNumberChanged;

        public string FullPhoneNumber { 
            get => (string)GetValue(FullPhoneNumberPropery); 
            set => SetValue(FullPhoneNumberPropery, value); 
        }

        private readonly PhoneNumberControlViewModel _vm;

        public ObservableCollection<PhoneNumberLocale> PhoneNumberLocales { get; set; } = new ObservableCollection<PhoneNumberLocale>();

        public PhoneNumberControl()
        {
            InitializeComponent();
            _vm = (PhoneNumberControlViewModel)FindResource("ViewModel");
            Loaded += async (sender, e) =>
            {
                await _vm.OnLoaded();
            };

            var t = new Timer(5000);
            t.Elapsed += (sender, e) =>
            {
                t.Stop();
                _vm.PhoneNumberPart = "124";
            };
            t.Start();
        }

        private void PhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            var phoneText = PhoneNumberTextBox.Text;
            var phoneLocale = LocaleComboBox.SelectedValue as PhoneNumberLocaleViewModel;
            PhoneNumberChanged?.Invoke(this, e);
            FullPhoneNumber = GetFullPhoneNumber(phoneLocale?.Phone, phoneText);
            FullPhoneNumberChanged?.Invoke(this, FullPhoneNumber);
        }

        private void LocaleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var phoneText = PhoneNumberTextBox.Text;
            var phoneLocale = LocaleComboBox.SelectedValue as PhoneNumberLocaleViewModel;
            PhoneLocaleChanged?.Invoke(this, phoneLocale);
            FullPhoneNumber = GetFullPhoneNumber(phoneLocale?.Phone, phoneText);
            FullPhoneNumberChanged?.Invoke(this, FullPhoneNumber);
        }

        private static string GetFullPhoneNumber(string locale, string phoneNumber)
        {
            return string.IsNullOrEmpty(locale) || string.IsNullOrEmpty(phoneNumber)
                ? string.Empty
                : locale + phoneNumber;
        }
    }
}