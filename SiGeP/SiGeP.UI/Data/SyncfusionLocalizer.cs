using BaseUI.Resources;
using Syncfusion.Blazor;

namespace SiGeP.UI.Data
{
    public class SyncfusionLocalizer : ISyncfusionStringLocalizer
    {
        public string GetText(string key)
        {
            return this.ResourceManager.GetString(key);
        }

        public System.Resources.ResourceManager ResourceManager
        {
            get
            {
                return SfResources.ResourceManager;
            }
        }
    }
}
