using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace CheckComboBoxSample.MVVM.Controls
{
    [TemplatePart(Name = ElementPanel, Type = typeof(Panel))]
    [TemplatePart(Name = ElementSelectAll, Type = typeof(ListBoxItem))]
    public class CheckComboBox : ListBox
    {
        private const string ElementPanel = "PART_Panel";

        private const string ElementSelectAll = "PART_SelectAll";

        private Panel _panel;

        private ListBoxItem _selectAllItem;

        private bool _isInternalAction;

        public static readonly DependencyProperty MaxDropDownHeightProperty =
            ComboBox.MaxDropDownHeightProperty.AddOwner(typeof(CheckComboBox),
                new FrameworkPropertyMetadata(SystemParameters.PrimaryScreenHeight / 3));

        [Bindable(true), Category("Layout")]
        [TypeConverter(typeof(LengthConverter))]
        public double MaxDropDownHeight
        {
            get => (double)GetValue(MaxDropDownHeightProperty);
            set => SetValue(MaxDropDownHeightProperty, value);
        }


        public static readonly DependencyProperty DropDownScrollViewerHeightProperty = DependencyProperty.Register(
           "DropDownScrollViewerHeight", typeof(double), typeof(CheckComboBox), new PropertyMetadata(0.0));

        public double DropDownScrollViewerHeight
        {
            get => (double)GetValue(DropDownScrollViewerHeightProperty);
            set => SetValue(DropDownScrollViewerHeightProperty, value);
        }

        public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register(
            "IsDropDownOpen", typeof(bool), typeof(CheckComboBox), new PropertyMetadata(false, OnIsDropDownOpenChanged));

        private static void OnIsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckComboBox ctl = (CheckComboBox)d;

            if (!(bool)e.NewValue)
            {
                _ = ctl.Dispatcher.BeginInvoke(new Action(() =>
                {
                    _ = Mouse.Capture(null);
                }), DispatcherPriority.Send);
            }
        }

        public bool IsDropDownOpen
        {
            get => (bool)GetValue(IsDropDownOpenProperty);
            set => SetValue(IsDropDownOpenProperty, value);
        }

        public static readonly DependencyProperty TagStyleProperty = DependencyProperty.Register(
            "TagStyle", typeof(Style), typeof(CheckComboBox), new PropertyMetadata(default(Style)));

        public Style TagStyle
        {
            get => (Style)GetValue(TagStyleProperty);
            set => SetValue(TagStyleProperty, value);
        }

        public static readonly DependencyProperty ShowSelectAllButtonProperty = DependencyProperty.Register(
            "ShowSelectAllButton", typeof(bool), typeof(CheckComboBox), new PropertyMetadata(false));

        public bool ShowSelectAllButton
        {
            get => (bool)GetValue(ShowSelectAllButtonProperty);
            set => SetValue(ShowSelectAllButtonProperty, value);
        }

        public static readonly DependencyProperty PublicSelectedItemsProperty = DependencyProperty.Register(
            "PublicSelectedItems", typeof(IList), typeof(CheckComboBox), new PropertyMetadata(new List<object>()));

        public IList PublicSelectedItems
        {
            get => (IList)GetValue(PublicSelectedItemsProperty);
            set => SetValue(PublicSelectedItemsProperty, value);
        }


        public CheckComboBox()
        {
            AddHandler(Controls.Tag.ClosedEvent, new RoutedEventHandler(Tags_OnClosed));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, (s, e) =>
            {
                SetCurrentValue(SelectedValueProperty, null);
                SetCurrentValue(SelectedItemProperty, null);
                SetCurrentValue(SelectedIndexProperty, -1);
                SelectedItems.Clear();
            }));

            ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
        }

        private void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
            if (ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                UpdateTags();
            }
        }

        public override void OnApplyTemplate()
        {
            if (_selectAllItem != null)
            {
                _selectAllItem.Selected -= SelectAllItem_Selected;
                _selectAllItem.Unselected -= SelectAllItem_Unselected;
            }

            base.OnApplyTemplate();

            _panel = GetTemplateChild(ElementPanel) as Panel;
            _selectAllItem = GetTemplateChild(ElementSelectAll) as ListBoxItem;
            if (_selectAllItem != null)
            {
                _selectAllItem.Selected += SelectAllItem_Selected;
                _selectAllItem.Unselected += SelectAllItem_Unselected;
            }

            DropDownScrollViewerHeight = ShowSelectAllButton ? MaxDropDownHeight - _selectAllItem.MinHeight - 8 : MaxDropDownHeight - 8;

            Dispatcher.BeginInvoke(new Action(() =>
            {
                IsDropDownOpen = true;
                IsDropDownOpen = false;
            }), DispatcherPriority.DataBind);
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            UpdateTags();

            base.OnSelectionChanged(e);
        }

        protected override bool IsItemItsOwnContainerOverride(object item) => item is ListBoxItem;

        protected override DependencyObject GetContainerForItemOverride() => new ListBoxItem();

        protected override void OnDisplayMemberPathChanged(string oldDisplayMemberPath, string newDisplayMemberPath) => UpdateTags();

        private void Tags_OnClosed(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is Tag tag && tag.Tag is ListBoxItem checkComboBoxItem)
            {
                checkComboBoxItem.SetCurrentValue(IsSelectedProperty, false);
            }
        }

        private void SwitchAllItems(bool selected)
        {
            if (_isInternalAction) return;
            _isInternalAction = true;

            foreach (var item in Items)
            {
                if (ItemContainerGenerator.ContainerFromItem(item) is ListBoxItem checkComboBoxItem)
                {
                    checkComboBoxItem.SetCurrentValue(ListBoxItem.IsSelectedProperty, selected);
                }
            }

            _isInternalAction = false;
            UpdateTags();
        }

        private void SelectAllItem_Selected(object sender, RoutedEventArgs e) => SwitchAllItems(true);

        private void SelectAllItem_Unselected(object sender, RoutedEventArgs e) => SwitchAllItems(false);

        private void UpdateTags()
        {
            if (_panel == null || _isInternalAction) return;

            if (_selectAllItem != null)
            {
                _isInternalAction = true;
                _selectAllItem.SetCurrentValue(IsSelectedProperty, SelectedItems.Count == Items.Count);
                _isInternalAction = false;
            }

            _panel.Children.Clear();
            PublicSelectedItems.Clear();

            foreach (var item in SelectedItems)
            {
                if (ItemContainerGenerator.ContainerFromItem(item) is ListBoxItem checkComboBoxItem)
                {
                    var tag = new Tag
                    {
                        Style = TagStyle,
                        Tag = checkComboBoxItem
                    };

                    if (ItemsSource != null)
                    {
                        tag.SetBinding(ContentControl.ContentProperty, new Binding(DisplayMemberPath) { Source = item });
                    }
                    else
                    {
                        tag.Content = checkComboBoxItem.Content;
                    }

                    _panel.Children.Add(tag);
                    PublicSelectedItems.Add(item);
                }
            }
        }
    }
}
