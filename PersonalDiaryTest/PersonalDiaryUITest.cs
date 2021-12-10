using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PersonalDiaryTest
{
    public class PersonalDiaryUITest: IDisposable
    {
        private readonly IWebDriver driver = new ChromeDriver();
        public PersonalDiaryUITest()
        {

        }
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void RegistrationView()
        {
            driver.Navigate().GoToUrl("https://localhost:44304/Login/Registration");

            driver.FindElement(By.Id("FirstName")).SendKeys("Test");

            driver.FindElement(By.Id("LastName")).SendKeys("User");

            driver.FindElement(By.Id("Email")).SendKeys("testuser@gmail.com");

            driver.FindElement(By.Id("Password")).SendKeys("Test@123");

            driver.FindElement(By.Id("RegisterUser")).Click();

            Assert.Equals("Registartion", driver.Title);
        }

        [Fact]
        public void LoginView()
        {
            driver.Navigate().GoToUrl("https://localhost:44304/Login/Login");

            driver.FindElement(By.Id("Username")).SendKeys("testuser@gmail.com");

            driver.FindElement(By.Id("Password")).SendKeys("Test@123");

            driver.FindElement(By.Id("LoginButton")).Click();

            Assert.Equals("Login", driver.Title);
        }
        [Fact]
        public void ListEventView()
        {
            driver.Navigate().GoToUrl("https://localhost:44304/Home/Index");

            Assert.Equals("Event List", driver.Title);
        }
        [Fact]
        public void CreateEventView()
        {
            driver.Navigate().GoToUrl("https://localhost:44304/Login/Login");

            driver.FindElement(By.Id("EventTitle")).SendKeys("Test Event 1");

            driver.FindElement(By.Id("EventDescription")).SendKeys("Test Event Description");

            driver.FindElement(By.Id("EventDate")).SendKeys(DateTime.Now.ToString());

            driver.FindElement(By.Id("EventImage")).SendKeys("https://techcrunch.com/wp-content/uploads/2020/04/Customize-Zoom-Preview-Link-Image.png");

            driver.FindElement(By.Id("CreateEvent")).Click();

            Assert.Equals("Create Event", driver.Title);
        }
        [Fact]
        public void EditEventView()
        {
            driver.Navigate().GoToUrl("https://localhost:44304/Home/EditEvent?eventId=1");

            driver.FindElement(By.Id("EventTitle")).SendKeys("Test Event 2");

            driver.FindElement(By.Id("EventDescription")).SendKeys("Test Event Description 2");

            driver.FindElement(By.Id("EventDate")).SendKeys(DateTime.Now.ToString());

            driver.FindElement(By.Id("EventImage")).SendKeys("https://techcrunch.com/wp-content/uploads/2020/04/Customize-Zoom-Preview-Link-Image.png");

            driver.FindElement(By.Id("UpdateEvent")).Click();

            Assert.Equals("Edit Event", driver.Title);
        }
    }
}
