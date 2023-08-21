using System.Collections.Generic;
using System.ComponentModel;

namespace PhoneNumberControl.Sample
{
    internal class Vm : INotifyPropertyChanged
    {
        public string FullPhoneNumber
        {
            get => _fullPhoneNumber;
            set
            {
                _fullPhoneNumber = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FullPhoneNumber"));
            }
        }
        private string _outer = "outer";
        private string _fullPhoneNumber;

        public string Outer { get => _outer; set { _outer = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Outer")); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<Item> Items { get; set; } = new List<Item>()
        {
            new Item() {Name = "name1"},
            new Item() {Name = "2"},
        };

        public class Item
        {
            public string Name { get; set; }
        }
    }
}
