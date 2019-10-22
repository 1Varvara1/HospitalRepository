using HospitalDAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDAL.Identity
{
    public class AppRoleManager : RoleManager<ApplicationRole>
    {
        public AppRoleManager(RoleStore<ApplicationRole> store)
                    : base(store)
        { }
    }
   
}
