using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonalDiaryApp.Controllers;
using Xunit;

namespace PersonalDiaryTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            PersonalDiaryLogin();
            PersonalDiaryEvent();

            PersonalDiaryUITest personalDiaryUITest = new PersonalDiaryUITest();
            personalDiaryUITest.RegistrationView();
            personalDiaryUITest.LoginView();
            personalDiaryUITest.ListEventView();
            personalDiaryUITest.CreateEventView();
            personalDiaryUITest.EditEventView();
        }
        [Fact]
        public void PersonalDiaryLogin()
        {
            LoginController homeController = new LoginController();
            homeController.Index();

            homeController.Index();
            homeController.Login();
            homeController.Registration();

            Assert.IsTrue(true);
        }
        [Fact]
        public void PersonalDiaryEvent()
        {
            HomeController homeController = new HomeController();
            homeController.Index();

            homeController.Index();
            homeController.EventDetail(1);
            homeController.DeleteEvent(1);

            Assert.IsTrue(true);
        }
    }
}
