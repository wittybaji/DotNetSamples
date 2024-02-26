using System.Windows;

namespace TabControlSample.Utils
{
    /// <summary>
    /// 资源帮助类
    /// </summary>
    public class ResourceHelper
    {
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

    }
}
