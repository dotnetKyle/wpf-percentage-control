using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Percentage
{
    public class Stock : INotifyPropertyChanged
    {
        public Stock() { }
        public Stock(string code, string desc, double percentage)
        {
            Code = code;
            Description = desc;
            Percentage = percentage;
        }

        double _percentage;
        public double Percentage
        {
            get => _percentage;
            set { _percentage = value; OnPropertyChanged(); }
        }

        string _code;
        public string Code
        {
            get => _code;
            set { _code = value; OnPropertyChanged(); }
        }

        string _description;
        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}
