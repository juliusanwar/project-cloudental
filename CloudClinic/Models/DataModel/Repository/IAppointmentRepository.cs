using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudClinic.Models.DataModel.Repository
{
    public interface IAppointmentRepository
    {
        void Create(Appointment appointment);
        void Update(Appointment appointment);
        void Delete(int id);
        Appointment FindById(int id);
        IEnumerable<Appointment> FindAll();
    }
}