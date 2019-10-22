using HospitalDAL.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalDAL.Interfaces;

namespace HospitalDAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public ApplicationContext Database { get; set; }
        public ClientManager(ApplicationContext db)
        {
            Database = db;
        }
     
        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public List<ClientProfile> GetAll()
        {
            var profiles = Database.ClientProfiles.ToList();

            foreach (var prof in profiles)
            {
                Database.Entry(prof).Reference(p => p.ApplicationUser).Load();
                Database.Entry(prof).Collection(p => p.Complaints).Load();
            }

            return profiles;
        }

        public ClientProfile Get(string idClientProfile)
        {
            var profile = Database.ClientProfiles.
                Where(c=>c.IdClientProfile==idClientProfile).
                FirstOrDefault();
            Database.Entry(profile).Reference(p => p.ApplicationUser).Load();
            Database.Entry(profile).Collection(p => p.Complaints).Load();

            return profile;
        }
    }
}

