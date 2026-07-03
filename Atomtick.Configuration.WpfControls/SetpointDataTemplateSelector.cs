using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SCADA.Configuration.WpfControls
{
    public class SetpointDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BoolTemplate { get; set; }
        public DataTemplate ColorTemplate { get; set; }
        public DataTemplate DateTimeTemplate { get; set; }
        public DataTemplate FilePathTemplate { get; set; }
        public DataTemplate FolderPathTemplate { get; set; }
        public DataTemplate OptionsTemplate { get; set; }
        public DataTemplate TextTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ObservableConfigItem configItem = item as ObservableConfigItem;
            if (configItem == null)
            {
                return null;
            }
            else if (configItem.Type == ConfigType.@bool)
            {
                return BoolTemplate;
            }
            else if (configItem.Type == ConfigType.file)
            {
                return FilePathTemplate;
            }
            else if (configItem.Type == ConfigType.folder)
            {
                return FolderPathTemplate;
            }
            else if (configItem.Type == ConfigType.datetime)
            {
                return DateTimeTemplate;
            }
            else if (configItem.Type == ConfigType.color)
            {
                return ColorTemplate;
            }
            else if (configItem.Options != null && configItem.Options.Length > 0)
            {
                return OptionsTemplate;
            }
            else
            {
                return TextTemplate;
            }
        }
    }
}