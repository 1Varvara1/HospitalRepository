using HospitalBLL.Infrastructure;
using HospitalBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserBLL userBLL);
        Task<ClaimsIdentity> Authenticate(UserBLL userBLL);
        Task SetInitialData(UserBLL adminBLL, List<string> roles);
        List<UserBLL> GetPatients();

        string GetRole(string id);
        string GetId(string UserName);

        ClientProfileBLL GetProfile(string UserName);
        List<UserBLL> GetPatientsAreBeingTreated();
        //  Task<List<UserBLL>> GetPatientsByYear();
    }
}
