using System;
using System.Windows;
using System.Windows.Media;

namespace GrowlSample.MVVM.Models
{
    /// <summary>
    /// 弹窗消息配置
    /// </summary>
    public class GrowlInfo
    {
        public string Message { get; set; }

        public bool ShowDateTime { get; set; } = true;

        public int WaitTime { get; set; } = 6;

        public string CancelStr { get; set; } = "取消";

        public string ConfirmStr { get; set; } = "确认";

        public Func<bool, bool> ActionBeforeClose { get; set; }

        public bool StaysOpen { get; set; } = false;

        public bool IsCustom { get; set; } = false;

        public InfoType Type { get; set; }

        public Geometry Icon { get; set; }

        public string IconKey { get; set; }

        public Brush IconBrush { get; set; }

        public string IconBrushKey { get; set; }

        public bool ShowCloseButton { get; set; } = true;

        public FlowDirection FlowDirection { get; set; }
    }

    public enum InfoType
    {
        Success = 0,
        Info,
        Warning,
        Error,
        Fatal,
        Ask
    }
}
