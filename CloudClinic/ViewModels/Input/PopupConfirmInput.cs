using System.ComponentModel.DataAnnotations;

namespace CloudClinic.ViewModels.Input
{
    public class PopupConfirmInput
    {
        [Required]
        public string Name { get; set; }
    }
}