using HospitalBLL.Interfaces;
using HospitalBLL.Models;
using HospitalDAL.Entities;
using HospitalDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Services
{
    public class ComplaintService: IComplaintService
    {
        IUnitOfWork Database { get; set; }

        public ComplaintService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public List<ComplaintBLL> GetAll()
        {
            var complaints=Database.ComplaintRepository.GetAll().ToList();
            var complaintsBLL = new List<ComplaintBLL>();
            foreach (var item in complaints)
            {
                var comp = new ComplaintBLL(item.ClientProfile, item.Speciality,
                    item.ComplaintInformation, item.Date, item.IsProccesed);
                comp.IdComplaint = item.IdComplaint;
                complaintsBLL.Add(comp);
                
            }
            return complaintsBLL;
        }
        

        public async Task<int> Create(string IdClientProfle, int idSpeciality)
        {
            // Create new complaint 
            var compl = new Complaint
            {
                ClientProfileIdClientProfile = IdClientProfle,
                SpecialityIdSpeciality = idSpeciality,
                Date = DateTime.Now,
                IsProccesed = false
            };

            Database.ComplaintRepository.Create(compl);
            await Database.SaveAsync();
            // Find id of created complaint in order to return 
            int id = Database.ComplaintRepository.GetAll().
                Where(c => c.ClientProfileIdClientProfile == IdClientProfle &&
                c.IsProccesed == false).FirstOrDefault().IdComplaint;

            return id;
        }

        public void MatchComplaintDoctor(int idComplaint, string idDoctor)
        {
            // Get id of the doctor 

            var docId = Database.DoctorRepository.Get(idDoctor).IdDoctor;
            var complaint_doc = new Complaint_Doctor
            {
                ComplaintIdComplaint = idComplaint,
                DoctorIdDoctor = docId
            };

           // Match doctor and complaint 
            Database.Complaint_DoctorRepository.Create(complaint_doc);

            // Change status of compplaint
            Database.ComplaintRepository.Get(idComplaint).IsProccesed = true;

            //Save changes
            Database.ComplaintRepository.Save();
        }
    }
}
