using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CheckComboBoxSample.MVVM.Controls
{
    public class Tag : ContentControl
    {
        public static readonly RoutedEvent SelectedEvent = EventManager.RegisterRoutedEvent(
            "Selected", RoutingStrategy.Bubble, typeof(EventHandler), typeof(Tag));

        public event EventHandler Selected
        {
            add => AddHandler(SelectedEvent, value);
            remove => RemoveHandler(SelectedEvent, value);
        }

        public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent(
            "Closed", RoutingStrategy.Bubble, typeof(EventHandler), typeof(Tag));

        public event EventHandler Closed
        {
            add => AddHandler(ClosedEvent, value);
            remove => RemoveHandler(ClosedEvent, value);
        }

        public static readonly DependencyProperty ShowCloseButtonProperty = DependencyProperty.Register(
            "ShowCloseButton", typeof(bool), typeof(Tag), new PropertyMetadata(true));

        public bool ShowCloseButton
        {
            get => (bool)GetValue(ShowCloseButtonProperty);
            set => SetValue(ShowCloseButtonProperty, value);
        }

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected", typeof(bool), typeof(Tag), new PropertyMetadata(false, (o, args) =>
            {
                var ctl = (Tag)o;
                ctl.RaiseEvent(new RoutedEventArgs(SelectedEvent, ctl));
            }));

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public Tag()
        {
            _ = CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, (s, e) =>
            {
                RaiseEvent(new RoutedEventArgs(ClosedEvent, this));
            }));
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            IsSelected = true;
        }
    }
}
