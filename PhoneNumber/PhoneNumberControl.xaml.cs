using PhoneNumber.Models;
using PhoneNumber.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PhoneNumber
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class PhoneNumberControl : UserControl
    {
        public static readonly DependencyProperty FullPhoneNumberPropery = DependencyProperty.Register
            (nameof(FullPhoneNumber), typeof(string),
            typeof(PhoneNumberControl));

        public static readonly DependencyProperty PhoneLocalePropery = DependencyProperty.Register
            (nameof(PhoneLocale), typeof(PhoneNumberLocale),
            typeof(PhoneNumberControl), new PropertyMetadata((sender, e) =>
            {
                var vm = GetVM(sender);
                vm.SelectedLocale = (PhoneNumberLocale)e.NewValue;
            }));

        public static readonly DependencyProperty PhoneNumberPropery = DependencyProperty.Register
            (nameof(PhoneNumber), typeof(string),
            typeof(PhoneNumberControl), new PropertyMetadata((sender, e) =>
            {
                var vm = GetVM(sender);
                vm.PhoneNumberPart = (string)e.NewValue;
            }));

        public string FullPhoneNumber
        {
            get => (string)GetValue(FullPhoneNumberPropery);
            set => SetValue(FullPhoneNumberPropery, value);
        }

        public string PhoneNumber
        {
            get => (string)GetValue(PhoneNumberPropery);
            set => SetValue(PhoneNumberPropery, value);
        }

        public PhoneNumberLocale PhoneLocale
        {
            get => (PhoneNumberLocale)GetValue(PhoneLocalePropery);
            set => SetValue(PhoneLocalePropery, value);
        }

        private readonly PhoneNumberControlViewModel _vm;

        public ObservableCollection<PhoneNumberLocale> PhoneNumberLocales { get; set; } = new ObservableCollection<PhoneNumberLocale>();

        public PhoneNumberControl()
        {
            InitializeComponent();
            _vm = GetVM(this);
            Loaded += async (sender, e) =>
            {
                await _vm.OnLoaded();
            };
        }

        private static PhoneNumberControlViewModel GetVM(DependencyObject d)
        {
            var control = (d as PhoneNumberControl);
            var vm = (PhoneNumberControlViewModel)control.FindResource("ViewModel");

            return vm;
        }

        private void PhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            InvokeChanged();
        }

        private void LocaleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InvokeChanged();
        }

        private void InvokeChanged()
        {
            PhoneNumber = PhoneNumberTextBox.Text;
            PhoneLocale = LocaleComboBox.SelectedValue as PhoneNumberLocale;
            PhoneLocaleChanged?.Invoke(this, PhoneLocale);
            FullPhoneNumber = GetFullPhoneNumber(PhoneLocale?.Phone, PhoneNumber);
            FullPhoneNumberChanged?.Invoke(this, FullPhoneNumber);
        }

        private static string GetFullPhoneNumber(string locale, string phoneNumber)
        {
            return string.IsNullOrEmpty(locale) || string.IsNullOrEmpty(phoneNumber)
                ? string.Empty
                : locale + phoneNumber;
        }

        public event EventHandler<string> FullPhoneNumberChanged;
        public event EventHandler<PhoneNumberLocale> PhoneLocaleChanged;
        public event EventHandler<TextChangedEventArgs> PhoneNumberChanged;
    }
}