using HospitalBLL.Interfaces;
using HospitalDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Services
{
    public class DischargeService : IDischargeService
    {
        IUnitOfWork Database { get; set; }

        public DischargeService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void DischargePatient(int idComplaint)
        {
            throw new NotImplementedException();
        }
    }
}
