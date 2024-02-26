using System.Windows;
using System.Windows.Controls;

namespace DataGridSample
{
    public class DataGridCellTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate
        SelectTemplate(object item, DependencyObject container)
        {
            if (item is SystemParam model)
            {
                return ResourceHelper.GetResource<DataTemplate>($"DataGridCell{model.ParamType}Template");
            }

            return null;
        }
    }
}
