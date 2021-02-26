using System.ComponentModel;
using System.Windows;

namespace Percentage
{
    internal static class Designer
    {
        public static bool Active
        {
            get
            {
#if DEBUG
                try
                {
                    return (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;
                }
                catch { }
#endif

                return false;
            }
        }
    }
}
