using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Percentage
{
    /// <summary>
    /// Interaction logic for PercentageControl.xaml
    /// </summary>
    public partial class PercentageControl : UserControl, INotifyPropertyChanged
    {
        public PercentageControl()
        {
            InitializeComponent();
            grdControl.DataContext = this;
        }

        public string LabelTooltip
        {
            get { return (string)GetValue(LabelTooltipProperty); }
            set { SetValue(LabelTooltipProperty, value); }
        }
        public static readonly DependencyProperty LabelTooltipProperty =
            DependencyProperty.Register("LabelTooltip", typeof(string), typeof(PercentageControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }
        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register("LabelText", typeof(string), typeof(PercentageControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double Percentage
        {
            get 
            { 
                var val = (double)GetValue(PercentageProperty);
                System.Diagnostics.Debug.WriteLine($"val returned:{val}");
                return val;
            }
            set 
            {
                System.Diagnostics.Debug.Write($"PercentageItem updating to {value:P}");
                SetValue(PercentageProperty, value);
                OnPropertyChanged(nameof(PercentageString));
                OnPropertyChanged(nameof(BackgroundColor));
                OnPropertyChanged(nameof(ForegroundColor));
                System.Diagnostics.Debug.WriteLine($"...updated");
            }
        }

        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register("Percentage", typeof(double), typeof(PercentageControl),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string PercentageString
        {
            get
            {
                return Percentage.ToString("P");
            }
        }

        #region Confidence Thresholds

        public double HighConfidenceThreshold
        {
            get { return (double)GetValue(HighConfidenceThresholdProperty); }
            set 
            { 
                SetValue(HighConfidenceThresholdProperty, value);
                OnPropertyChanged(nameof(BackgroundColor));
                OnPropertyChanged(nameof(ForegroundColor));
            }
        }
        public static readonly DependencyProperty HighConfidenceThresholdProperty =
            DependencyProperty.Register("HighConfidenceThreshold", typeof(double), typeof(PercentageControl), new PropertyMetadata(.9));

        public double MediumConfidenceThreshold
        {
            get { return (double)GetValue(MediumConfidenceThresholdProperty); }
            set 
            { 
                SetValue(MediumConfidenceThresholdProperty, value);
                OnPropertyChanged(nameof(BackgroundColor));
                OnPropertyChanged(nameof(ForegroundColor));
            }
        }
        public static readonly DependencyProperty MediumConfidenceThresholdProperty =
            DependencyProperty.Register("MediumConfidenceThreshold", typeof(double), typeof(PercentageControl), new PropertyMetadata(.75));

        #endregion

        #region Confidence Background Colors

        public Brush HighConfidenceColor
        {
            get { return (Brush)GetValue(HighConfidenceColorProperty); }
            set 
            { 
                SetValue(HighConfidenceColorProperty, value);
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }
        public static readonly DependencyProperty HighConfidenceColorProperty =
            DependencyProperty.Register("HighConfidenceColor", typeof(Brush), typeof(PercentageControl), new PropertyMetadata(Brushes.Green));
        public Brush MediumConfidenceColor
        {
            get { return (Brush)GetValue(MediumConfidenceColorProperty); }
            set 
            { 
                SetValue(MediumConfidenceColorProperty, value);
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }
        public static readonly DependencyProperty MediumConfidenceColorProperty =
            DependencyProperty.Register("MediumConfidenceColor", typeof(Brush), typeof(PercentageControl), new PropertyMetadata(Brushes.Yellow));
        public Brush LowConfidenceColor
        {
            get { return (Brush)GetValue(LowConfidenceColorProperty); }
            set 
            { 
                SetValue(LowConfidenceColorProperty, value);
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }
        public static readonly DependencyProperty LowConfidenceColorProperty =
            DependencyProperty.Register("LowConfidenceColor", typeof(Brush), typeof(PercentageControl), new PropertyMetadata(Brushes.Red));
        public Brush BackgroundColor
        {
            get
            {
                if (Percentage >= HighConfidenceThreshold)
                    return HighConfidenceColor;
                else if (Percentage >= MediumConfidenceThreshold)
                    return MediumConfidenceColor;
                else
                    return LowConfidenceColor;
            }
        }

        #endregion

        #region Confidence Text Colors

        public Brush HighConfidenceTextColor
        {
            get { return (Brush)GetValue(HighConfidenceColorTextProperty); }
            set 
            { 
                SetValue(HighConfidenceColorTextProperty, value);
                OnPropertyChanged(nameof(ForegroundColor));
            }
        }
        public static readonly DependencyProperty HighConfidenceColorTextProperty =
            DependencyProperty.Register("HighConfidenceTextColor", typeof(Brush), typeof(PercentageControl), new PropertyMetadata(Brushes.White));
        public Brush MediumConfidenceTextColor
        {
            get { return (Brush)GetValue(MediumConfidenceColorTextProperty); }
            set 
            { 
                SetValue(MediumConfidenceColorTextProperty, value);
                OnPropertyChanged(nameof(ForegroundColor));
            }
        }
        public static readonly DependencyProperty MediumConfidenceColorTextProperty =
            DependencyProperty.Register("MediumConfidenceTextColor", typeof(Brush), typeof(PercentageControl), new PropertyMetadata(Brushes.Black));
        public Brush LowConfidenceTextColor
        {
            get { return (Brush)GetValue(LowConfidenceColorTextProperty); }
            set 
            { 
                SetValue(LowConfidenceColorTextProperty, value);
                OnPropertyChanged(nameof(ForegroundColor));
            }
        }
        public static readonly DependencyProperty LowConfidenceColorTextProperty =
            DependencyProperty.Register("LowConfidenceTextColor", typeof(Brush), typeof(PercentageControl), new PropertyMetadata(Brushes.Black));
        public Brush ForegroundColor
        {
            get
            {
                if (Percentage > HighConfidenceThreshold)
                    return HighConfidenceTextColor;
                else if (Percentage > MediumConfidenceThreshold)
                    return MediumConfidenceTextColor;
                else
                    return LowConfidenceTextColor;
            }
        }

        #endregion


        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
        
    }
}
