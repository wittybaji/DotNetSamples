using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CheckComboBoxSample.MVVM.Controls
{
    public class ToggleBlock : Control
    {
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
            "IsChecked", typeof(bool?), typeof(ToggleBlock), new FrameworkPropertyMetadata(false,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal));

        [Category("Appearance")]
        [TypeConverter(typeof(NullableBoolConverter))]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public bool? IsChecked
        {
            get
            {
                var value = GetValue(IsCheckedProperty);
                return value == null ? new bool?() : new bool?((bool)value);
            }
            set => SetValue(IsCheckedProperty, value.Value);
        }

        public static readonly DependencyProperty CheckedContentProperty = DependencyProperty.Register(
            "CheckedContent", typeof(object), typeof(ToggleBlock), new PropertyMetadata(default(object)));

        public object CheckedContent
        {
            get => GetValue(CheckedContentProperty);
            set => SetValue(CheckedContentProperty, value);
        }

        public static readonly DependencyProperty UnCheckedContentProperty = DependencyProperty.Register(
            "UnCheckedContent", typeof(object), typeof(ToggleBlock), new PropertyMetadata(default(object)));

        public object UnCheckedContent
        {
            get => GetValue(UnCheckedContentProperty);
            set => SetValue(UnCheckedContentProperty, value);
        }

        public static readonly DependencyProperty IndeterminateContentProperty = DependencyProperty.Register(
            "IndeterminateContent", typeof(object), typeof(ToggleBlock), new PropertyMetadata(default(object)));

        public object IndeterminateContent
        {
            get => GetValue(IndeterminateContentProperty);
            set => SetValue(IndeterminateContentProperty, value);
        }
    }
}
