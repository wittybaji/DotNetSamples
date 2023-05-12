using System;
using System.Windows;

namespace GrowlSample.MVVM.Utils
{
    /// <summary>
    /// 资源帮助类
    /// </summary>
    public class ResourceHelper
    {
        private static ResourceDictionary _theme;

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetResource<T>(string key)
        {
            if (Application.Current.TryFindResource(key) is T resource)
            {
                return resource;
            }

            return default;
        }

        internal static T GetResourceInternal<T>(string key)
        {
            if (GetTheme()[key] is T resource)
            {
                return resource;
            }

            return default;
        }

        public static ResourceDictionary GetTheme()
        {
            if (_theme == null)
            {
                _theme = new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/GrowlSample;component/Themes/Theme.xaml")
                };
            }
            return _theme;
        }
    }
}
