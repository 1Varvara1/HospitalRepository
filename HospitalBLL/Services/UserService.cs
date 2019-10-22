﻿using HospitalBLL.Infrastructure;
using HospitalBLL.Interfaces;
using HospitalBLL.Models;
using HospitalDAL;
using HospitalDAL.Entities;
using HospitalDAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserBLL userBll)
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
                ClientProfile clientProfile = new ClientProfile {
                    IdClientProfile = user.Id,
                    Address = userBll.Address,
                    Name = userBll.Name,
                    Surname=userBll.Surname,
                    SecondName=userBll.SecondName,
                    Birth=userBll.Birth
                };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserBLL userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserBLL adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public  List<UserBLL>GetPatients()
        {

            var patients = new List<UserBLL>();
            var users = Database.ClientManager.GetAll();
            foreach (var u in users)
            {
                var role = Database.UserManager.GetRoles(u.IdClientProfile);
                if (role.Contains("user"))
                {
                    var uBll = new UserBLL(u.IdClientProfile, u.Name, u.Surname, u.SecondName,
                        u.Birth, u.Address, u.ApplicationUser.Email,"user");
                    if (IsNeedDoctor(u))
                    {
                        uBll.NeedDoctor = true;
                    }
                   patients.Add(uBll);
                }
            }


            return patients;
        }
        public bool IsNeedDoctor(ClientProfile user)
        {
            //int count = Database.ClientManager.GetAll().Where(cl => cl.IdClientProfile == user.IdClientProfile).
            //    Where(cl => cl.Complaints.Any(c => c.IsProccesed == false)).Count();

            //int count = Database.ClientManager.GetAll().Where(cl => cl.IdClientProfile == user.IdClientProfile).
            //    Where(cl => cl.Complaints.Any(c => c.IsProccesed == false)).Count();
            var count = Database.ComplaintRepository.GetAll().
                Where(c => c.IsProccesed == false && c.ClientProfileIdClientProfile==user.IdClientProfile).
                Count();
            if (count >= 1) return true;
            return false;
        }
    }
}
