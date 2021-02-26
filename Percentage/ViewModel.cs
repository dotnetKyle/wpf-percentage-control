using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace Percentage
{
    public class ViewModel : INotifyPropertyChanged
    {
        System.Timers.Timer timer;
        Random rand;
        public ViewModel()
        {
            rand = new Random();
            timer = new System.Timers.Timer(5000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("Timer..");
            var i = rand.Next(0, Items.Count);
            Items[i].Percentage = rand.NextDouble();
        }

        public List<PercentageItem> Items { get; set; }
            = new List<PercentageItem>
            {
                new PercentageItem("ENG", "English", .358489537),
                new PercentageItem("SPA", "Spanish", .752389478923),
                new PercentageItem("RUS", "Russian", .984345345),
                new PercentageItem("ENG", "English", .358489537),
                new PercentageItem("ENG", "English", .358489537),
                new PercentageItem("ENG", "English", .358489537),
                new PercentageItem("ENG", "English", .358489537),
                new PercentageItem("ENG", "English", .358489537),
                new PercentageItem("ENG", "English", .358489537),
                new PercentageItem("ENG", "English", .358489537),
                new PercentageItem("ENG", "English", .358489537),
                new PercentageItem("ENG", "English", .358489537)
            };

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }

    public class PercentageItem : INotifyPropertyChanged
    {
        public PercentageItem(string code, string desc, double percentage)
        {
            Code = code;
            Description = desc;
            Percentage = percentage;
        }

        double _percentage;
        public double Percentage
        {
            get => _percentage;
            set 
            {
                System.Diagnostics.Debug.Write($"PercentageItem updating to {value:P}");
                _percentage = value;
                OnPropertyChanged();
                System.Diagnostics.Debug.WriteLine($"...updated");

            }
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
