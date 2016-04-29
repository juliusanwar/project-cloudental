using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CloudClinic.Models.DataModel.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ClinicContext _context = new ClinicContext();

        public void Create(Appointment appointment)
        {
            //var exists = (from c in _context.Appointment
            //              where c.PasienId == c);

            _context.Appointment.Add(appointment);
            //_context.Appointment.Any(app => app.PasienId == c);
            _context.SaveChanges();
        }

        public void Update(Appointment appointment)
        {
            _context.Entry(appointment).State = EntityState.Modified;
            _context.Entry(appointment).Property(model => model.CreatedAt).IsModified = false;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var appointment = FindById(id);
            _context.Appointment.Remove(appointment);
            _context.SaveChanges();
        }

        public Appointment FindById(int id)
        {
            return _context.Appointment.Find(id);
        }

        public IEnumerable<Appointment> FindAll()
        {

            return _context.Appointment.ToList();
        }
    }
}