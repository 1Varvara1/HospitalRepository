using Hospital.Controllers;
using Hospital.Models;
using HospitalBLL.Interfaces;
using HospitalBLL.Models;
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
            creator = new ServiceCreator();
            
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

            var mock = new Mock<IServiceCreator>();
            mock.Setup(m => m.CreateDoctorService().GetAll()).Returns(new List<DoctorBLL>());
            var doc = mock.Object.CreateDoctorService();
            var email = doc.GetAll();
           // var email = DoctorService.GetAll().First().ClientProfile.ApplicationUser.Email;
           //    var email = m.GetPatients().First().Email;
            //var model = new LoginModel {
            //    Email = email,
            //    Password= "333333"
            //};
          
            //Act
          //  var result = controller.Login(model);

       //     var ar = controller.ModelState.Count;
            //Assert
           // Assert.IsFalse(controller.ModelState.IsValid);
        }

    }
}
