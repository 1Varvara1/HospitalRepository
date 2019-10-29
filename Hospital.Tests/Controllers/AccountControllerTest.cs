using Hospital.Controllers;
using Hospital.Models;
using HospitalBLL.Interfaces;
using HospitalBLL.Services;
using HospitalDAL.Entities;
using Microsoft.Owin.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace Hospital.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        private ServiceCreator creator;
        private AccountController controller;
        [TestInitialize]
        public void Setup()
        {
            controller = new AccountController();
            var mock = new Mock<ISpecialityService>();
            creator = new ServiceCreator();
            // result = controller.Index() as ViewResult;
        }
     
        private IDoctorService DoctorService
        {
            get
            {
                return creator.CreateDoctorService();
            }
        }
       
        [TestMethod]
        public void Account()
        {
            //Arrange
            // var appuser= 
          // var  user = new ApplicationUser { Email = userBll.Email, UserName = userBll.Email };
            var email = DoctorService.GetAll().First().ClientProfile.ApplicationUser.Email;
        //    var email = m.GetPatients().First().Email;
            var model = new LoginModel {
                Email = email,
                Password= "333333"
            };

            //Act
            var result = controller.Login(model);

       //     var ar = controller.ModelState.Count;
            //Assert
            Assert.IsFalse(controller.ModelState.IsValid);
        }

    }
}
