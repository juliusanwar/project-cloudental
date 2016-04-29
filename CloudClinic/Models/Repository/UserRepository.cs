using CloudClinic.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudClinic.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ClinicContext _context = new ClinicContext();

        public Pasien GetUser(string userName)
        {
            return _context.Pasien.Where(user => user.UserName == userName).FirstOrDefault();
        }

    }
}
