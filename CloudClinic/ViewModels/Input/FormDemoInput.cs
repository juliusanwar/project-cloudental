using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CloudClinic.ViewModels.Input
{
    public class FormDemoInput
    {
        [Required]
        [DisplayName("First Name")]
        public string FName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LName { get; set; }
    }
}