# PhoneNumberControl
A WPF UserControl library for phone number input with country locales. There are 2 ways to interact with a control. You can use bindings if you follow MVVM principles. The other way is to use event handlers in the code behind.
# Examples
Here is an example of using bindings.
Xaml:
```
<TextBlock><Run>Full number is:</Run><Run Text="{Binding FullPhoneNumber}"/></TextBlock>
<TextBlock><Run>Phone number without locale is:</Run><Run Text="{Binding PhoneNumber}"/></TextBlock>
<TextBox Text="{Binding PhoneNumber, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"/>
<TextBlock><Run>Locale part name is:</Run><Run Text="{Binding PhoneLocale.Name}"/></TextBlock>
<TextBlock><Run>Locale part phone is:</Run><Run Text="{Binding PhoneLocale.Phone}"/></TextBlock>
        
<control:PhoneNumberControl
  FullPhoneNumber="{Binding FullPhoneNumber, Mode=OneWayToSource}"
  PhoneLocale="{Binding PhoneLocale, Mode=OneWayToSource}"
  PhoneNumber="{Binding PhoneNumber, Mode=TwoWay}"/>
```
ViewModel:
```
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
```
# Properties
| Name  | Type | Description |
| ------------- | ------------ | ------------ |
| FullPhoneNumber  | string  | A full phone number with locale without plus |
| PhoneNumber  | string  | A phone number without locale  |
| PhoneLocale  | PhoneNumberLocale  | A phone number locale. A type is located at PhoneNumber.Models namespace. It has a country name, number, image  |
# Samples:
1. [PhoneNumberControl.Sample](https://github.com/bkovalevych/PhoneNumberControl/tree/master/PhoneNumberControl.Sample)
