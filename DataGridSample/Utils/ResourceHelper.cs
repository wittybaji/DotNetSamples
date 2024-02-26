using System.Windows;

namespace DataGridSample
{
    public class ResourceHelper
    {
        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetResource<T>(string key)
        {
            return Application.Current.TryFindResource(key) is T resource ? resource : default;
        }

        /// <summary>
        /// 设置资源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool SetResource<T>(string key, T value)
        {
            for (int i = 0; i < Application.Current.Resources.MergedDictionaries.Count; i++)
            {
                if (Application.Current.Resources.MergedDictionaries[i].Contains(key))
                {
                    Application.Current.Resources.MergedDictionaries[i][key] = value;
                    return true;
                }
            }
            return false;
        }

    }
}
