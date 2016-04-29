using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudClinic.Models.DataModel
{

    public class ApplicationUser : IdentityUser
    {
        // HomeTown will be stored in the same table as Users
        public string HomeTown { get; set; }
        public virtual ICollection<ToDo> ToDoes { get; set; }

        // FirstName & LastName will be stored in a different table called Pasien
        public virtual Pasien Pasien { get; set; }
    }
}
