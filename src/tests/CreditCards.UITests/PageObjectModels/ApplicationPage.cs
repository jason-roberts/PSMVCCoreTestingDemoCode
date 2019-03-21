using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace CreditCards.UITests.PageObjectModels
{
    class ApplicationPage
    {
        public IWebDriver Driver { get; }

        private const string PagePath = "apply";

        public ApplicationPage(IWebDriver driver)
        {
            Driver = driver;

            PageFactory.InitElements(driver, this);
        }

        public void NavigateTo()
        {
            var root = new Uri(Driver.Url).GetLeftPart(UriPartial.Authority);

            var url = $"{root}/{PagePath}";

            Driver.Navigate().GoToUrl(url);
        }

        [FindsBy(How = How.Name, Using = "FirstName")]
        private IWebElement FirstName { get; set; }


        [FindsBy(How = How.Name, Using = "LastName")]
        private IWebElement LastName { get; set; }


        [FindsBy(How = How.Id, Using = "FrequentFlyerNumber")]
        private IWebElement FrequentFlyerNumber { get; set; }


        [FindsBy(How = How.Id, Using = "Age")]
        private IWebElement Age { get; set; }


        [FindsBy(How = How.Id, Using = "GrossAnnualIncome")]
        private IWebElement GrossAnnualIncome { get; set; }


        [FindsBy(How = How.Id, Using = "submitApplication")]
        private IWebElement ApplyButton { get; set; }


        [FindsBy(How = How.CssSelector, Using = ".validation-summary-errors ul > li")]
        private IWebElement FirstError { get; set; }

        public string FirstErrorMessage => FirstError.Text;


        public void EnterName(string firstName, string lastName)
        {
            FirstName.SendKeys(firstName);
            LastName.SendKeys(lastName);
        }

        public void EnterFrequentFlyerNumber(string frequentFLyerNumber)
        {
            FrequentFlyerNumber.SendKeys(frequentFLyerNumber);
        }

        public void EnterAge(string age)
        {
            Age.SendKeys(age);
        }

        public void EnterGrossAnnualIncome(string income)
        {
            GrossAnnualIncome.SendKeys(income);
        }

        public ApplicationCompletePage SubmitApplication()
        {
            ApplyButton.Click();

            return new ApplicationCompletePage(Driver);
        }
    }
}

