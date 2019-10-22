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
    public class DrugService : IDrugsService
    {
        IUnitOfWork Database { get; set; }

        public DrugService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public List<DrugBLL> GetAllDrags()
        {
            var drugs = Database.DrugsRepository.GetAll();
            List<DrugBLL> dr = new List<DrugBLL>(); 
            foreach(var d in drugs)
            {
                dr.Add(new DrugBLL(d.IdDrugs, d.DrugsName));
            }
            return dr;
        }
    }
}
