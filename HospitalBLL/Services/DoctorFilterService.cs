using HospitalBLL.Comparers;
using HospitalBLL.Interfaces;
using HospitalDAL;
using HospitalDAL.Entities;
using HospitalDAL.HelpModels;
using HospitalDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Models
{
    public class DoctorFilterService 
    {
        private IUnitOfWork Database { get; set; }

        public DoctorFilterService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public List<DoctorBLL> ApplyFilter(DoctorSortModel docSort)
        {
            var doctors = Database.DoctorRepository.GetAll();
            var doctorsBLL = new List<DoctorBLL>();
            foreach (var doc in doctors)
            {

                // doctorsBLL.Add(DoctortoDoctorBLLTransform(doc));
                var item = DoctortoDoctorBLLTransform(doc);

                var complaints_doctor = doc.Complaint_Doctors.ToList();
                List<Complaint> complaints = new List<Complaint>();

                foreach (var cd in complaints_doctor)
                {
                    complaints.Add(cd.Complaint);
                }

                // Find all the patients of Doctor
                var patients = complaints.Distinct(new ComplaintsEqualityComparer()).ToList();

                // Find all the patients of Doctor who being treated now
                var patientsTreated = patients.Where(c => IsBeingTreated(c.ClientProfile) == true).ToList();

                item.AmountPatients = patients.Count();
                item.AmountBeingTreated = patientsTreated.Count();


                doctorsBLL.Add(item);
            }

            if (docSort.IdSpeciality!=0 )
            {
                doctorsBLL = doctorsBLL.Where(d => d.Speciality.IdSpeciality==
                docSort.IdSpeciality).ToList();
            }
            if (docSort.PatientField != "none")
            {
                if(docSort.PatientField == "IncreaseAll")
                {
                    doctorsBLL.Sort(new DoctorAmountPatientsComparer());
                }
                else if (docSort.PatientField == "DecreaseAll")
                {
                    doctorsBLL.Sort(new DoctorAmountPatientsComparer());
                    doctorsBLL.Reverse();
                }
                else if (docSort.PatientField == "IncreaseTreated")
                {
                    doctorsBLL.Sort(new DoctorAmountPatientsBeingTreatedComparer());
                }
                else if (docSort.PatientField == "DecreaseTreated")
                {
                    doctorsBLL.Sort(new DoctorAmountPatientsBeingTreatedComparer());
                    doctorsBLL.Reverse();
                }
             
            }
            if(docSort.SurnameField != "none")
            {
                if (docSort.SurnameField == "A-Z")
                {
                    doctorsBLL.Sort(new SurnameDoctorComparer());
                }
                else if (docSort.SurnameField == "Z-A")
                {
                    doctorsBLL.Sort(new SurnameDoctorComparer());
                    doctorsBLL.Reverse();
                }
            }
            return doctorsBLL;

        }

        public bool IsBeingTreated(ClientProfile user)
        {
            if (user.Complaints.Count == 0)
            {
                return false;
            }
            var complaints = user.Complaints.ToList();
            var disch = Database.DischargeRepository.GetAll();
            var discharges = Database.DischargeRepository.GetAll().
                Where(d => d.Complaint.ClientProfile.IdClientProfile == user.IdClientProfile).ToList();

            // Check if any patient wasn`t discharged
            foreach (var item in complaints)
            {
                if (!discharges.Any(d => d.Complaint.IdComplaint == item.IdComplaint))
                {
                    return true;
                }
            }

            return false;
        }
        public List<DoctorBLL> GetBySpeciality(List<DoctorBLL> doctors)
        {
            throw new NotImplementedException();
        }

        public List<DoctorBLL> SortByAmountPatientsDecrease(List<DoctorBLL> doctors)
        {
            throw new NotImplementedException();
        }

        public List<DoctorBLL> SortByAmountPatientsIncrease(List<DoctorBLL> doctors)
        {
            throw new NotImplementedException();
        }

        public List<DoctorBLL> SortBySurnameA_Z(List<DoctorBLL> doctors)
        {
            throw new NotImplementedException();
        }

        public List<DoctorBLL> SortBySurnameZ_A(List<DoctorBLL> doctors)
        {
            throw new NotImplementedException();
        }

        public DoctorBLL DoctortoDoctorBLLTransform(Doctor doctor)
        {
            DoctorBLL doc = new DoctorBLL {
                ClientProfile = doctor.ClientProfile,
                Speciality = doctor.Speciality,
                Complaint_Doctors = doctor.Complaint_Doctors.ToList(),
                PathPhoto = doctor.PathPhoto
            };

            return doc;
        }
    }
}
