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
            PersonalDiary();
        }
        [Fact]
        public void PersonalDiary()
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
