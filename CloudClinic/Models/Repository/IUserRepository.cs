using CloudClinic.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CloudClinic.Models.Repository
{
    public interface IUserRepository
    {
        Pasien GetUser(string userName);
    }
}
