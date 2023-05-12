using GrowlSample.MVVM.Extensions;
using GrowlSample.MVVM.Interactivity;
using GrowlSample.MVVM.Models;
using GrowlSample.MVVM.Utils;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace GrowlSample.MVVM.Controls
{
    /// <summary>
    /// 消息提醒
    /// </summary>
    [TemplatePart(Name = ElementGridMain, Type = typeof(Grid))]
    [TemplatePart(Name = ElementPanelMore, Type = typeof(Panel))]
    [TemplatePart(Name = ElementButtonClose, Type = typeof(Button))]
    public class Growl : Control
    {

        #region 样式名称

        private const string ElementPanelMore = "PART_PanelMore";

        private const string ElementGridMain = "PART_GridMain";

        private const string ElementButtonClose = "PART_ButtonClose";

        #endregion

        #region 内部字段

        /// <summary>
        /// 桌面提示容器窗口
        /// </summary>
        public static GrowlWindow GrowlWindow;

        /// <summary>
        /// 单个消息内下方的操作框
        /// </summary>
        private Panel _panelMore;

        /// <summary>
        /// 消息框
        /// </summary>
        private Grid _gridMain;

        /// <summary>
        /// 单个消息框的关闭按钮
        /// </summary>
        private Button _buttonClose;

        /// <summary>
        /// 显示关闭按钮
        /// </summary>
        private bool _showCloseButton;

        /// <summary>
        /// 保持打开 不自动关闭
        /// </summary>
        private bool _staysOpen;

        /// <summary>
        /// 消息显示时间
        /// </summary>
        private int _waitTime = 6;

        /// <summary>
        /// 关闭计数
        /// </summary>
        private int _tickCount;

        /// <summary>
        /// 关闭计时器
        /// </summary>
        private DispatcherTimer _timerClose;

        #endregion

        #region 公共字段

        internal string CancelStr
        {
            get => (string)GetValue(CancelStrProperty);
            set => SetValue(CancelStrProperty, value);
        }

        internal static readonly DependencyProperty CancelStrProperty = DependencyProperty.Register(
            "CancelStr", typeof(string), typeof(Growl), new PropertyMetadata(default(string)));

        internal string ConfirmStr
        {
            get => (string)GetValue(ConfirmStrProperty);
            set => SetValue(ConfirmStrProperty, value);
        }

        internal static readonly DependencyProperty ConfirmStrProperty = DependencyProperty.Register(
            "ConfirmStr", typeof(string), typeof(Growl), new PropertyMetadata(default(string)));

        public bool ShowDateTime
        {
            get => (bool)GetValue(ShowDateTimeProperty);
            set => SetValue(ShowDateTimeProperty, value);
        }

        public static readonly DependencyProperty ShowDateTimeProperty = DependencyProperty.Register(
            "ShowDateTime", typeof(bool), typeof(Growl), new PropertyMetadata(true));

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof(string), typeof(Growl), new PropertyMetadata(default(string)));

        public DateTime Time
        {
            get => (DateTime)GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(
            "Time", typeof(DateTime), typeof(Growl), new PropertyMetadata(default(DateTime)));

        public Geometry Icon
        {
            get => (Geometry)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon", typeof(Geometry), typeof(Growl), new PropertyMetadata(default(Geometry)));

        public Brush IconBrush
        {
            get => (Brush)GetValue(IconBrushProperty);
            set => SetValue(IconBrushProperty, value);
        }

        public static readonly DependencyProperty IconBrushProperty = DependencyProperty.Register(
            "IconBrush", typeof(Brush), typeof(Growl), new PropertyMetadata(default(Brush)));

        public InfoType Type
        {
            get => (InfoType)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
            "Type", typeof(InfoType), typeof(Growl), new PropertyMetadata(default(InfoType)));

        public static readonly DependencyProperty GrowlParentProperty = DependencyProperty.RegisterAttached(
            "GrowlParent", typeof(bool), typeof(Growl), new PropertyMetadata(false, (o, args) =>
            {
                if ((bool)args.NewValue && o is Panel panel)
                {
                    SetGrowlPanel(panel);
                }
            }));

        private static readonly DependencyProperty IsCreatedAutomaticallyProperty = DependencyProperty.RegisterAttached(
            "IsCreatedAutomatically", typeof(bool), typeof(Growl), new PropertyMetadata(false));


        public static void SetGrowlParent(DependencyObject element, bool value) => element.SetValue(GrowlParentProperty, value);

        public static bool GetGrowlParent(DependencyObject element) => (bool)element.GetValue(GrowlParentProperty);

        private static void SetIsCreatedAutomatically(DependencyObject element, bool value)
            => element.SetValue(IsCreatedAutomaticallyProperty, value);

        private static bool GetIsCreatedAutomatically(DependencyObject element)
            => (bool)element.GetValue(IsCreatedAutomaticallyProperty);

        /// <summary>
        /// 消息容器
        /// </summary>
        public static Panel GrowlPanel { get; set; }

        /// <summary>
        /// 关闭之前的操作
        /// </summary>
        private Func<bool, bool> ActionBeforeClose { get; set; }

        #endregion

        public Growl()
        {
            _ = CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, ButtonClose_OnClick));
            _ = CommandBindings.Add(new CommandBinding(ApplicationCommands.Stop, ButtonCancel_OnClick));
            _ = CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, ButtonOk_OnClick));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            Console.WriteLine($"----------------OnRender   {ActualWidth} {ActualHeight}");
        }

        #region 消息面板管理

        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close(false);
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close(false);
        }

        private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            Close(true);
        }


        /// <summary>
        /// 鼠标进入
        /// 显示关闭按钮
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            _buttonClose.Show(_showCloseButton);
        }

        /// <summary>
        /// 鼠标离开
        /// 隐藏关闭按钮
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            _buttonClose.Collapse();
        }

        /// <summary>
        /// 应用样式时调用
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _panelMore = GetTemplateChild(ElementPanelMore) as Panel;
            _gridMain = GetTemplateChild(ElementGridMain) as Grid;
            _buttonClose = GetTemplateChild(ElementButtonClose) as Button;

            CheckNull();
            Update();
        }

        /// <summary>
        /// 检查消息所需元素是否正常
        /// </summary>
        private void CheckNull()
        {
            if (_panelMore == null || _gridMain == null || _buttonClose == null)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// 开始计时器
        /// </summary>
        private void StartTimer()
        {
            _timerClose = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timerClose.Tick += delegate
            {
                if (IsMouseOver)
                {
                    _tickCount = 0;
                    return;
                }

                _tickCount++;
                if (_tickCount >= _waitTime)
                {
                    Close(true);
                }
            };
            _timerClose.Start();
        }

        /// <summary>
        /// 消息容器
        /// </summary>
        /// <param name="panel"></param>
        private static void SetGrowlPanel(Panel panel)
        {
            GrowlPanel = panel;
            InitGrowlPanel(panel);
        }

        /// <summary>
        /// 初始化消息面板
        /// </summary>
        /// <param name="panel"></param>
        private static void InitGrowlPanel(Panel panel)
        {
            if (panel == null)
            {
                return;
            }

            var menuItem = new MenuItem() { Header = "清除" };
            menuItem.Click += (s, e) =>
            {
                foreach (var item in panel.Children.OfType<Growl>())
                {
                    item.Close(false);
                }
            };
            panel.ContextMenu = new ContextMenu
            {
                Items =
                {
                    menuItem
                }
            };

        }

        /// <summary>
        /// 更新显示
        /// </summary>
        private void Update()
        {
            if (Type == InfoType.Ask)
            {
                _panelMore.IsEnabled = true;
                _panelMore.Show();
            }

            var transform = new TranslateTransform
            {
                X = FlowDirection == FlowDirection.LeftToRight ? MaxWidth : -MaxWidth
            };
            _gridMain.RenderTransform = transform;
            transform.BeginAnimation(TranslateTransform.XProperty, AnimationHelper.CreateAnimation(0));
            if (!_staysOpen)
            {
                StartTimer();
            }
        }

        /// <summary>
        /// 显示全局消息
        /// </summary>
        /// <param name="growlInfo"></param>
        private static void ShowGlobal(GrowlInfo growlInfo)
        {
            Application.Current.Dispatcher?.Invoke(
                () =>
                {
                    if (GrowlWindow == null)
                    {
                        GrowlWindow = new GrowlWindow();
                        GrowlWindow.Show();
                        InitGrowlPanel(GrowlWindow.GrowlPanel);
                        GrowlWindow.Init();
                    }

                    GrowlWindow.Show(true);

                    var ctl = new Growl
                    {
                        Message = growlInfo.Message,
                        Time = DateTime.Now,
                        Icon = ResourceHelper.GetResource<Geometry>(growlInfo.IconKey) ?? growlInfo.Icon,
                        IconBrush = ResourceHelper.GetResource<Brush>(growlInfo.IconBrushKey) ?? growlInfo.IconBrush,
                        _showCloseButton = growlInfo.ShowCloseButton,
                        ActionBeforeClose = growlInfo.ActionBeforeClose,
                        _staysOpen = growlInfo.StaysOpen,
                        ShowDateTime = growlInfo.ShowDateTime,
                        ConfirmStr = growlInfo.ConfirmStr,
                        CancelStr = growlInfo.CancelStr,
                        Type = growlInfo.Type,
                        _waitTime = Math.Max(growlInfo.WaitTime, 2),
                        FlowDirection = growlInfo.FlowDirection
                    };
                    GrowlWindow.GrowlPanel.Children.Insert(0, ctl);
                }
            );
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="growlInfo"></param>
        private static void Show(GrowlInfo growlInfo)
        {
            Application.Current.Dispatcher?.Invoke(
                () =>
                {
                    var ctl = new Growl
                    {
                        Message = growlInfo.Message,
                        Time = DateTime.Now,
                        Icon = ResourceHelper.GetResource<Geometry>(growlInfo.IconKey) ?? growlInfo.Icon,
                        IconBrush = ResourceHelper.GetResource<Brush>(growlInfo.IconBrushKey) ?? growlInfo.IconBrush,
                        _showCloseButton = growlInfo.ShowCloseButton,
                        ActionBeforeClose = growlInfo.ActionBeforeClose,
                        _staysOpen = growlInfo.StaysOpen,
                        ShowDateTime = growlInfo.ShowDateTime,
                        ConfirmStr = growlInfo.ConfirmStr,
                        CancelStr = growlInfo.CancelStr,
                        Type = growlInfo.Type,
                        _waitTime = Math.Max(growlInfo.WaitTime, 2)
                    };
                    if (GrowlPanel == null)
                    {
                        GrowlPanel = CreateDefaultPanel();
                    }
                    GrowlPanel?.Children.Insert(0, ctl);
                }
            );
        }

        /// <summary>
        /// 创建默认面板
        /// </summary>
        /// <returns></returns>
        private static Panel CreateDefaultPanel()
        {
            FrameworkElement element = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var decorator = GetChild<AdornerDecorator>(element);

            if (decorator != null)
            {
                var layer = decorator.AdornerLayer;
                if (layer != null)
                {
                    var panel = new StackPanel
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                    };

                    InitGrowlPanel(panel);
                    SetIsCreatedAutomatically(panel, true);

                    var scrollViewer = new ScrollViewer
                    {
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                        IsInertiaEnabled = true,
                        IsPenetrating = true,
                        Content = panel
                    };

                    var container = new AdornerContainer(layer)
                    {
                        Child = scrollViewer
                    };

                    layer.Add(container);

                    return panel;
                }
            }

            return null;
        }

        /// <summary>
        /// 移除默认面板
        /// </summary>
        /// <param name="panel"></param>
        private static void RemoveDefaultPanel(Panel panel)
        {
            FrameworkElement element = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var decorator = GetChild<AdornerDecorator>(element);

            if (decorator != null)
            {
                var layer = decorator.AdornerLayer;
                var adorner = GetParent<Adorner>(panel);

                if (adorner != null)
                {
                    layer?.Remove(adorner);
                }
            }
        }


        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="token"></param>
        public static void Clear(string token = "")
        {
            Clear(GrowlPanel);
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="panel"></param>
        private static void Clear(Panel panel) => panel?.Children.Clear();

        /// <summary>
        /// 清除
        /// </summary>
        public static void ClearGlobal()
        {
            if (GrowlWindow == null) return;
            Clear(GrowlWindow.GrowlPanel);
            GrowlWindow.Close();
            GrowlWindow = null;
        }


        /// <summary>
        /// 关闭
        /// </summary>
        private void Close(bool invokeParam)
        {
            if (ActionBeforeClose?.Invoke(invokeParam) == false)
            {
                return;
            }

            _timerClose?.Stop();
            var transform = new TranslateTransform();
            _gridMain.RenderTransform = transform;
            var animation = AnimationHelper.CreateAnimation(FlowDirection == FlowDirection.LeftToRight ? ActualWidth : -ActualWidth);
            animation.Completed += (s, e) =>
            {
                if (Parent is Panel panel)
                {
                    panel.Children.Remove(this);

                    if (GrowlWindow != null)
                    {
                        if (GrowlWindow.GrowlPanel != null && GrowlWindow.GrowlPanel.Children.Count == 0)
                        {
                            GrowlWindow.Close();
                            GrowlWindow = null;
                        }
                    }
                    else
                    {
                        if (GrowlPanel != null && GrowlPanel.Children.Count == 0 && GetIsCreatedAutomatically(GrowlPanel))
                        {
                            //剩余消息为0时 删除自动生成的消息面板
                            RemoveDefaultPanel(GrowlPanel);
                            GrowlPanel = null;
                        }
                    }
                }
            };
            transform.BeginAnimation(TranslateTransform.XProperty, animation);
        }

        /// <summary>
        /// 填充消息参数
        /// </summary>
        /// <param name="growlInfo"></param>
        /// <param name="infoType"></param>
        private static void InitGrowlInfo(ref GrowlInfo growlInfo, InfoType infoType)
        {
            if (growlInfo == null) throw new ArgumentNullException(nameof(growlInfo));
            growlInfo.Type = infoType;

            switch (infoType)
            {
                case InfoType.Success:
                    if (!growlInfo.IsCustom)
                    {
                        growlInfo.IconKey = ResourceToken.SuccessGeometry;
                        growlInfo.IconBrushKey = ResourceToken.SuccessBrush;
                    }
                    else
                    {
                        if (growlInfo.IconKey == null)
                        {
                            growlInfo.IconKey = ResourceToken.SuccessGeometry;
                        }
                        if (growlInfo.IconBrushKey == null)
                        {
                            growlInfo.IconBrushKey = ResourceToken.SuccessBrush;
                        }
                    }
                    break;
                case InfoType.Info:
                    if (!growlInfo.IsCustom)
                    {
                        growlInfo.IconKey = ResourceToken.InfoGeometry;
                        growlInfo.IconBrushKey = ResourceToken.InfoBrush;
                    }
                    else
                    {
                        if (growlInfo.IconKey == null)
                        {
                            growlInfo.IconKey = ResourceToken.InfoGeometry;
                        }
                        if (growlInfo.IconBrushKey == null)
                        {
                            growlInfo.IconBrushKey = ResourceToken.InfoBrush;
                        }
                    }
                    break;
                case InfoType.Warning:
                    if (!growlInfo.IsCustom)
                    {
                        growlInfo.IconKey = ResourceToken.WarningGeometry;
                        growlInfo.IconBrushKey = ResourceToken.WarningBrush;
                    }
                    else
                    {
                        if (growlInfo.IconKey == null)
                        {
                            growlInfo.IconKey = ResourceToken.WarningGeometry;
                        }
                        if (growlInfo.IconBrushKey == null)
                        {
                            growlInfo.IconBrushKey = ResourceToken.WarningBrush;
                        }
                    }
                    break;
                case InfoType.Error:
                    if (!growlInfo.IsCustom)
                    {
                        growlInfo.IconKey = ResourceToken.ErrorGeometry;
                        growlInfo.IconBrushKey = ResourceToken.DangerBrush;
                        growlInfo.StaysOpen = true;
                    }
                    else
                    {
                        if (growlInfo.IconKey == null)
                        {
                            growlInfo.IconKey = ResourceToken.ErrorGeometry;
                        }
                        if (growlInfo.IconBrushKey == null)
                        {
                            growlInfo.IconBrushKey = ResourceToken.DangerBrush;
                        }
                    }
                    break;
                case InfoType.Fatal:
                    if (!growlInfo.IsCustom)
                    {
                        growlInfo.IconKey = ResourceToken.FatalGeometry;
                        growlInfo.IconBrushKey = ResourceToken.PrimaryTextBrush;
                        growlInfo.StaysOpen = true;
                        growlInfo.ShowCloseButton = false;
                    }
                    else
                    {
                        if (growlInfo.IconKey == null)
                        {
                            growlInfo.IconKey = ResourceToken.FatalGeometry;
                        }
                        if (growlInfo.IconBrushKey == null)
                        {
                            growlInfo.IconBrushKey = ResourceToken.PrimaryTextBrush;
                        }
                    }
                    break;
                case InfoType.Ask:
                    growlInfo.StaysOpen = true;
                    growlInfo.ShowCloseButton = false;
                    if (!growlInfo.IsCustom)
                    {
                        growlInfo.IconKey = ResourceToken.AskGeometry;
                        growlInfo.IconBrushKey = ResourceToken.AccentBrush;
                    }
                    else
                    {
                        if (growlInfo.IconKey == null)
                        {
                            growlInfo.IconKey = ResourceToken.AskGeometry;
                        }
                        if (growlInfo.IconBrushKey == null)
                        {
                            growlInfo.IconBrushKey = ResourceToken.AccentBrush;
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(infoType), infoType, null);
            }
        }

        private static T GetChild<T>(DependencyObject d) where T : DependencyObject
        {
            if (d == null) return default;
            if (d is T t) return t;

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
            {
                var child = VisualTreeHelper.GetChild(d, i);

                var result = GetChild<T>(child);
                if (result != null) return result;
            }

            return default;
        }

        private static T GetParent<T>(DependencyObject d) where T : DependencyObject
        {
            switch (d)
            {
                case null:
                    return default;
                case T t:
                    return t;
                default:
                    return GetParent<T>(VisualTreeHelper.GetParent(d));
            }
        }


        #endregion

        #region 产生消息

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message"></param>
        /// <param name="token"></param>
        public static void Success(string message) => Success(new GrowlInfo { Message = message });

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void Success(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, InfoType.Success);
            Show(growlInfo);
        }

        /// <summary>
        /// 成功 全局
        /// </summary>
        /// <param name="message"></param>
        public static void SuccessGlobal(string message) => SuccessGlobal(new GrowlInfo { Message = message });

        /// <summary>
        /// 成功 全局
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void SuccessGlobal(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, InfoType.Success);
            ShowGlobal(growlInfo);
        }

        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="token"></param>
        public static void Info(string message) => Info(new GrowlInfo { Message = message });

        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void Info(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, InfoType.Info);
            Show(growlInfo);
        }

        /// <summary>
        /// 消息 全局
        /// </summary>
        /// <param name="message"></param>
        public static void InfoGlobal(string message) => InfoGlobal(new GrowlInfo { Message = message });

        /// <summary>
        /// 消息 全局
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void InfoGlobal(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, InfoType.Info);
            ShowGlobal(growlInfo);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message"></param>
        /// <param name="token"></param>
        public static void Warning(string message) => Warning(new GrowlInfo { Message = message });

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void Warning(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, InfoType.Warning);
            Show(growlInfo);
        }

        /// <summary>
        /// 警告 全局
        /// </summary>
        /// <param name="message"></param>
        public static void WarningGlobal(string message) => WarningGlobal(new GrowlInfo { Message = message });

        /// <summary>
        /// 警告 全局
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void WarningGlobal(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, InfoType.Warning);
            ShowGlobal(growlInfo);
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="token"></param>
        public static void Error(string message) => Error(new GrowlInfo { Message = message });

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void Error(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, InfoType.Error);
            Show(growlInfo);
        }

        /// <summary>
        /// 错误 全局
        /// </summary>
        /// <param name="message"></param>
        public static void ErrorGlobal(string message) => ErrorGlobal(new GrowlInfo { Message = message });

        /// <summary>
        /// 错误 全局
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void ErrorGlobal(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, InfoType.Error);
            ShowGlobal(growlInfo);
        }

        /// <summary>
        /// 严重
        /// </summary>
        /// <param name="message"></param>
        /// <param name="token"></param>
        public static void Fatal(string message) => Fatal(new GrowlInfo { Message = message });

        /// <summary>
        /// 严重
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void Fatal(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, InfoType.Fatal);
            Show(growlInfo);
        }

        /// <summary>
        /// 严重 全局
        /// </summary>
        /// <param name="message"></param>
        public static void FatalGlobal(string message) => FatalGlobal(new GrowlInfo { Message = message });

        /// <summary>
        /// 严重 全局
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void FatalGlobal(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, InfoType.Fatal);
            ShowGlobal(growlInfo);
        }

        /// <summary>
        /// 询问
        /// </summary>
        /// <param name="message"></param>
        /// <param name="actionBeforeClose"></param>
        /// <param name="token"></param>
        public static void Ask(string message, Func<bool, bool> actionBeforeClose) => Ask(new GrowlInfo
        {
            Message = message,
            ActionBeforeClose = actionBeforeClose
        });

        /// <summary>
        /// 询问
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void Ask(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, InfoType.Ask);
            Show(growlInfo);
        }

        /// <summary>
        /// 询问 全局
        /// </summary>
        /// <param name="message"></param>
        /// <param name="actionBeforeClose"></param>
        public static void AskGlobal(string message, Func<bool, bool> actionBeforeClose) => AskGlobal(new GrowlInfo
        {
            Message = message,
            ActionBeforeClose = actionBeforeClose
        });

        /// <summary>
        /// 询问 全局
        /// </summary>
        /// <param name="growlInfo"></param>
        public static void AskGlobal(GrowlInfo growlInfo)
        {
            InitGrowlInfo(ref growlInfo, InfoType.Ask);
            ShowGlobal(growlInfo);
        }
        #endregion
    }
}
