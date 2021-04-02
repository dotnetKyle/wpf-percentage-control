using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Percentage
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel() 
        {
            Stocks = new ObservableCollection<Stock>();
            
            if(WpfDesignerHelper.Designer.Active)
            {
                Stocks = new ObservableCollection<Stock>
                {
                    // Stocks chosen at random
                    new Stock("KLR+", "Kaleyra, Inc.", .0),
                    new Stock("LNTH", "Lantheus Holdings, Inc.", .0),
                    new Stock("RNLC", "Large Cap US Equity Select", .0),
                    new Stock("STM", "STMicroelectronics N.V.", .0),
                    new Stock("CRMT", "America's Car-Mart, Inc.", .0),
                    new Stock("VIHAW", "VPC Impact Acquisition Holdings", .0),
                    new Stock("PHAR", "Pharming Group N.V. - ADS", .0),
                    new Stock("RDWR ", "Radware Ltd.", .0),
                    new Stock("HURC", "Hurco Companies, Inc.", .0)
                };
            }
        }

        public ObservableCollection<Stock> Stocks { get; set; }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}
