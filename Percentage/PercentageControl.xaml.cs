using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Percentage
{
    /// <summary>
    /// Interaction logic for PercentageControl.xaml
    /// </summary>
    public partial class PercentageControl : UserControl
    {
        static readonly Color defaultLowConfidenceBG = Color.FromRgb(255,150,150);
        static readonly Color defaultMedConfidenceBG = Color.FromRgb(255,255,150);
        static readonly Color defaultHighConfidenceBG = Color.FromRgb(150,255,200);

        public PercentageControl()
        {
            InitializeComponent();

            // set the first child control (in this case grdControl) to be the DataContext holder (not the UserControl) 
            // this.DataContext MUST inherit the parent control's datacontext
            grdControl.DataContext = this;
        }

        #region DP LabelTooltip
        public string LabelTooltip
        {
            get { return (string)GetValue(LabelTooltipProperty); }
            set { SetValue(LabelTooltipProperty, value); }
        }
        public static readonly DependencyProperty LabelTooltipProperty =
            DependencyProperty.Register(nameof(LabelTooltip), typeof(string), typeof(PercentageControl), 
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion

        #region DP LabelText
        // TODO: Change this to a content so the user can do a better label
        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }
        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register(nameof(LabelText), typeof(string), typeof(PercentageControl), 
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion

        #region DP Percentage
        public double Percentage
        {
            get => (double)GetValue(PercentageProperty);
            set => SetValue(PercentageProperty, value);
        }
        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register("Percentage", typeof(double), typeof(PercentageControl),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, 
                    new PropertyChangedCallback(PercentPropertyChanged)
                    ));
        public static void PercentPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            // when this property is changed, it's value effects other properties
            o.SetValue(PercentageProperty, args.NewValue);
            // must set the values for all the read only percentage properties when this changes
            SetPercentageLabelValue(o);
            SetPercentageBackground(o); 
            SetPercentageForeground(o);
        }
        #endregion

        #region Confidence Thresholds

        #region DP High Confidence Threshhold
        public double HighConfidenceThreshold
        {
            get => (double)GetValue(HighConfidenceThresholdProperty);
            set => SetValue(HighConfidenceThresholdProperty, value);
        }
        public static readonly DependencyProperty HighConfidenceThresholdProperty =
            DependencyProperty.Register(nameof(HighConfidenceThreshold), typeof(double), typeof(PercentageControl), 
                new PropertyMetadata(.9, new PropertyChangedCallback(HighConfThresholdPropertyChanged))); 
        static void HighConfThresholdPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            o.SetValue(HighConfidenceThresholdProperty, args.NewValue);
            SetPercentageBackground(o);
            SetPercentageForeground(o);
        }

        #endregion

        #region DP Med Confidence Threshhold

        public double MediumConfidenceThreshold
        {
            get => (double)GetValue(MediumConfidenceThresholdProperty);
            set => SetValue(MediumConfidenceThresholdProperty, value);
        }
        public static readonly DependencyProperty MediumConfidenceThresholdProperty =
            DependencyProperty.Register(nameof(MediumConfidenceThreshold), typeof(double), typeof(PercentageControl), 
                new PropertyMetadata(.75, new PropertyChangedCallback(MedConfThresholdPropertyChanged)));
        static void MedConfThresholdPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            o.SetValue(MediumConfidenceThresholdProperty, args.NewValue);
            SetPercentageBackground(o);
            SetPercentageForeground(o);
        }
        #endregion

        #endregion

        #region Confidence Background Colors

        public Brush HighConfidenceBackground
        {
            get => (Brush)GetValue(HighConfidenceBackgroundProperty);
            set
            {
                SetValue(HighConfidenceBackgroundProperty, value);
                SetPercentageBackground(this);
            }
        }
        public static readonly DependencyProperty HighConfidenceBackgroundProperty =
            DependencyProperty.Register(nameof(HighConfidenceBackground), typeof(Brush), typeof(PercentageControl), 
                new PropertyMetadata(new SolidColorBrush(defaultHighConfidenceBG), new PropertyChangedCallback(HighConfidenceBackgroundPropertyChanged)));
        static void HighConfidenceBackgroundPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            o.SetValue(HighConfidenceBackgroundProperty, args.NewValue);
            SetPercentageBackground(o);
        }


        public Brush MediumConfidenceBackground
        {
            get => (Brush)GetValue(MediumConfidenceBackgroundProperty);
            set 
            { 
                SetValue(MediumConfidenceBackgroundProperty, value);
                SetPercentageBackground(this);
            }            
        }
        public static readonly DependencyProperty MediumConfidenceBackgroundProperty =
            DependencyProperty.Register(nameof(MediumConfidenceBackground), typeof(Brush), typeof(PercentageControl), 
                new PropertyMetadata(new SolidColorBrush(defaultMedConfidenceBG), new PropertyChangedCallback(MediumConfidenceBackgroundPropertyChanged)));
        static void MediumConfidenceBackgroundPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            o.SetValue(MediumConfidenceBackgroundProperty, args.NewValue);
            SetPercentageBackground(o);
        }

        public Brush LowConfidenceBackground
        {
            get => (Brush)GetValue(LowConfidenceBackgroundProperty);
            set
            {
                SetValue(LowConfidenceBackgroundProperty, value);
                SetPercentageBackground(this);
            }
        }
        public static readonly DependencyProperty LowConfidenceBackgroundProperty =
            DependencyProperty.Register(nameof(LowConfidenceBackground), typeof(Brush), typeof(PercentageControl), 
                new PropertyMetadata(new SolidColorBrush(defaultLowConfidenceBG), new PropertyChangedCallback(LowConfidenceBackgroundPropertyChanged)));
        static void LowConfidenceBackgroundPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            o.SetValue(LowConfidenceBackgroundProperty, args.NewValue);
            SetPercentageBackground(o);
        }

        #endregion

        #region Confidence Text Colors

        #region DP TextColor High Confidence

        public Brush HighConfidenceForeground
        {
            get => (Brush)GetValue(HighConfidenceForegroundProperty);
            set => SetValue(HighConfidenceForegroundProperty, value);
        }
        public static readonly DependencyProperty HighConfidenceForegroundProperty =
            DependencyProperty.Register(nameof(HighConfidenceForeground), typeof(Brush), typeof(PercentageControl), 
                new PropertyMetadata(Brushes.Black, new PropertyChangedCallback(HighConfidenceForegroundPropertyChanged)));
        static void HighConfidenceForegroundPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            o.SetValue(HighConfidenceForegroundProperty, args.NewValue);
            SetPercentageForeground(o);
        }

        #endregion

        #region DP TextColor Med Confidence

        public Brush MediumConfidenceForeground
        {
            get => (Brush)GetValue(MediumConfidenceForegroundProperty);
            set => SetValue(MediumConfidenceForegroundProperty, value);            
        }
        public static readonly DependencyProperty MediumConfidenceForegroundProperty =
            DependencyProperty.Register(nameof(MediumConfidenceForeground), typeof(Brush), typeof(PercentageControl), 
                new PropertyMetadata(Brushes.Black, new PropertyChangedCallback(MediumConfidenceForegroundPropertyChanged)));
        static void MediumConfidenceForegroundPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            o.SetValue(MediumConfidenceForegroundProperty, args.NewValue);
            SetPercentageForeground(o);
        }

        #endregion

        #region DP TextColor Low Confidence

        public Brush LowConfidenceForeground
        {
            get => (Brush)GetValue(LowConfidenceForegroundProperty);
            set => SetValue(LowConfidenceForegroundProperty, value);
        }
        public static readonly DependencyProperty LowConfidenceForegroundProperty =
            DependencyProperty.Register(nameof(LowConfidenceForeground), typeof(Brush), typeof(PercentageControl), 
                new PropertyMetadata(Brushes.Black, new PropertyChangedCallback(LowConfidenceForegroundPropertyChanged)));
        static void LowConfidenceForegroundPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            o.SetValue(LowConfidenceForegroundProperty, args.NewValue);
            SetPercentageForeground(o);
        }

        #endregion

        #endregion

        #region Read-Only Properties

        /// <summary>
        /// The text for the Percentage textblock
        /// </summary>
        public string PercentageLabel => (string)GetValue(PercentageLabelProperty);
        static void SetPercentageLabelValue(DependencyObject o)
        {
            var val = (double)o.GetValue(PercentageProperty);
            var P = val.ToString("P");
            o.SetValue(PercentageLabelKey, P); // Must use the key to set the value
        }
        static readonly DependencyPropertyKey PercentageLabelKey = DependencyProperty.RegisterReadOnly(
            nameof(PercentageLabel),
            typeof(string), typeof(PercentageControl),
            new UIPropertyMetadata(0.0.ToString("P"))
            );
        public static readonly DependencyProperty PercentageLabelProperty = PercentageLabelKey.DependencyProperty;

        /// <summary>
        /// The Background color for the percentage label
        /// </summary>
        public Brush PercentageBackground => (Brush)GetValue(PercentageBackgroundProperty);        
        static void SetPercentageBackground(DependencyObject o)
        {
            var percentage = (double)o.GetValue(PercentageProperty);

            Brush brush = (Brush)o.GetValue(LowConfidenceBackgroundProperty);
            if (percentage > (double)o.GetValue(HighConfidenceThresholdProperty))
                brush = (Brush)o.GetValue(HighConfidenceBackgroundProperty);
            else if (percentage > (double)o.GetValue(MediumConfidenceThresholdProperty))
                brush = (Brush)o.GetValue(MediumConfidenceBackgroundProperty);

            o.SetValue(PercentageBackgroundKey, brush);
        }
        static readonly DependencyPropertyKey PercentageBackgroundKey = DependencyProperty.RegisterReadOnly(
            nameof(PercentageBackground),
            typeof(Brush), typeof(PercentageControl),
            new PropertyMetadata(new SolidColorBrush(defaultLowConfidenceBG))
            );
        public static readonly DependencyProperty PercentageBackgroundProperty = PercentageBackgroundKey.DependencyProperty;


        /// <summary>
        /// The text color for the percentage label
        /// </summary>
        public Brush PercentageForeground => (Brush)GetValue(PercentageLabelTextColorProperty);
        static void SetPercentageForeground(DependencyObject o)
        {
            var percentage = (double)o.GetValue(PercentageProperty);

            Brush brush = (Brush)o.GetValue(LowConfidenceForegroundProperty);
            if (percentage > (double)o.GetValue(HighConfidenceThresholdProperty))
                brush = (Brush)o.GetValue(HighConfidenceForegroundProperty);
            else if (percentage > (double)o.GetValue(MediumConfidenceThresholdProperty))
                brush = (Brush)o.GetValue(MediumConfidenceForegroundProperty);

            o.SetValue(PercentageLabelForegroundKey, brush);
        }
        internal static readonly DependencyPropertyKey PercentageLabelForegroundKey = DependencyProperty.RegisterReadOnly(
            nameof(PercentageForeground),
            typeof(Brush), typeof(PercentageControl),
            new PropertyMetadata(Brushes.Black)
            );
        public static readonly DependencyProperty PercentageLabelTextColorProperty = PercentageLabelForegroundKey.DependencyProperty;

        #endregion

        
    }
}
