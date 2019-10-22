using HospitalBLL.Interfaces;
using HospitalBLL.Models;
using HospitalDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Services
{
    public class SpecialityService : ISpecialityService
    {
        IUnitOfWork Database { get; set; }
        public SpecialityService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public List<SpecialityBLL> GetAllSpecialities()
        {
            var specialities= Database.SpecialityRepository.GetAll();
            List<SpecialityBLL> specialitiesBll = new List<SpecialityBLL>();
            foreach (var s in specialities)
            {
                var item = new SpecialityBLL(s.IdSpeciality,s.NameSpeciality, s.PathPh);
                specialitiesBll.Add(item);
            }
            return specialitiesBll;
        }
    }
}
