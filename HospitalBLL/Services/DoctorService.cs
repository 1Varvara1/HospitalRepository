using HospitalBLL.Infrastructure;
using HospitalBLL.Interfaces;
using HospitalBLL.Models;
using HospitalDAL;
using HospitalDAL.Entities;
using HospitalDAL.HelpModels;
using HospitalDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Services
{
    public class DoctorService : IDoctorService
    {
        IUnitOfWork Database { get; set; }

        public DoctorService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserBLL userBll, SpecialityBLL speciality)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userBll.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userBll.Email, UserName = userBll.Email };
                var result = await Database.UserManager.CreateAsync(user, userBll.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userBll.Role);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile
                {
                    IdClientProfile = user.Id,
                    Address = userBll.Address,
                    Name = userBll.Name,
                    Surname = userBll.Surname,
                    SecondName = userBll.SecondName,
                    Birth = userBll.Birth

                };
                Database.ClientManager.Create(clientProfile);

                var doc = new Doctor
                {
                    ClientProfileIdClientProfile=user.Id,
                    PathPhoto="",
                    SpecialityIdSpeciality = speciality.IdSpeciality
                 
                };
                Database.DoctorRepository.Create(doc);

                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public List<DoctorBLL> GetDoctorsByIdSpeciality(int Idspeciality)
        {
            // Search for doctors with given speciality
            var doctors = Database.DoctorRepository.GetAll().Where(d => d.SpecialityIdSpeciality == Idspeciality);
            var DoctorsBLL = new List<DoctorBLL>();
            foreach(var d in doctors)
            {
                var item = new DoctorBLL(d.ClientProfile,d.Speciality,d.Complaint_Doctors.ToList());
                DoctorsBLL.Add(item);
            }

            return DoctorsBLL;
        }

        public List<DoctorBLL> GetAll()
        {
            var doctors = Database.DoctorRepository.GetAll();
            var DoctorsBLL = new List<DoctorBLL>();

            // Figuring out how many patients doctors have
            foreach (var d in doctors)
            {
                var item = new DoctorBLL(d.ClientProfile, d.Speciality, d.Complaint_Doctors.ToList());
               
                //var complaints_doctor = Database.Complaint_DoctorRepository.
                //    GetAll().
                //    Where(c=>c.DoctorIdDoctor==d.ClientProfileIdClientProfile);

                //List<Complaint> complaints=new List<Complaint>();

                //foreach(var cd in complaints_doctor)
                //{
                //    complaints.Add(cd.Complaint);
                //}

                //var patients = complaints.Distinct(new ComplaintsEqualityComparer()).Count();

                //item.AmountPatients = patients;
                DoctorsBLL.Add(item);
            }

            return DoctorsBLL;
        }

      
    }
}
