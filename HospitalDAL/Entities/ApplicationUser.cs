using Microsoft.AspNet.Identity.EntityFramework;

namespace HospitalDAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
       
    }
}
