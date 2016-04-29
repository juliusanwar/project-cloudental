using System.Collections.Generic;

using Omu.AwesomeMvc;

namespace CloudClinic.ViewModels.Input.Settings
{
    public class SettingsInput
    {
        public IEnumerable<KeyContent> Themes { get; set; }
        
        public string SelectedTheme { get; set; }

        public IEnumerable<KeyContent> Popups { get; set; }
        
        public string SelectedPopup { get; set; }
    }
}