using System;
using System.Threading;
using CreditCards.UITests.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace CreditCards.UITests
{
    public class CreditCardApplicationTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly ApplicationPage _applicationPage;

        public CreditCardApplicationTests()
        {
            _driver = new ChromeDriver();

            _driver.Navigate().GoToUrl("http://localhost:44108/");

            _applicationPage = new ApplicationPage(_driver);
            _applicationPage.NavigateTo();
        }

        [Fact]
        public void ShouldLoadApplicationPage_SmokeTest()
        {          
            Assert.Equal("Credit Card Application - CreditCards", _applicationPage.Driver.Title);
        }

        [Fact]
        public void ShouldValidateApplicationDetails()
        {
            // Don't enter a first name
            _applicationPage.EnterName("", "Smith");
            DelayForDemoVideo();

            _applicationPage.EnterFrequentFlyerNumber("012345-A");
            DelayForDemoVideo();

            _applicationPage.EnterAge("20");
            DelayForDemoVideo();

            _applicationPage.EnterGrossAnnualIncome("100000");
            DelayForDemoVideo();

            _applicationPage.SubmitApplication();

            Assert.Equal("Credit Card Application - CreditCards", _applicationPage.Driver.Title);

            Assert.Equal("Please provide a first name", _applicationPage.FirstErrorMessage);
        }

        [Fact]
        public void ShouldDeclineLowIncomes()
        {
            _applicationPage.EnterName("Sarah", "Smith");
            DelayForDemoVideo();

            _applicationPage.EnterFrequentFlyerNumber("012345-A");
            DelayForDemoVideo();

            _applicationPage.EnterAge("35");
            DelayForDemoVideo();

            _applicationPage.EnterGrossAnnualIncome("10000");
            DelayForDemoVideo();

            ApplicationCompletePage applicationCompletePage =
                _applicationPage.SubmitApplication();

            Assert.Equal("Application Complete - CreditCards", applicationCompletePage.Driver.Title);
            Assert.Equal("AutoDeclined", applicationCompletePage.ApplicationDecision);
        }

        /// <summary>
        /// Brief delay to slow down browser interactions for
        /// demo video recording purposes
        /// </summary>
        private static void DelayForDemoVideo()
        {
            Thread.Sleep(1000);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
