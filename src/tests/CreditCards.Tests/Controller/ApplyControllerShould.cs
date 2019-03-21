using System.Threading.Tasks;
using CreditCards.Controllers;
using CreditCards.Core.Interfaces;
using CreditCards.Core.Model;
using CreditCards.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CreditCards.Tests.Controller
{
    public class ApplyControllerShould
    {
        private readonly Mock<ICreditCardApplicationRepository> _mockRepository;
        private readonly ApplyController _sut;

        public ApplyControllerShould()
        {
            _mockRepository = new Mock<ICreditCardApplicationRepository>();
            _sut = new ApplyController(_mockRepository.Object);
        }

        [Fact]
        public void ReturnViewForIndex()
        {           
            IActionResult result = _sut.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task ReturnViewWhenInvalidModelState()
        {
            _sut.ModelState.AddModelError("x","Test Error");

            var application = new NewCreditCardApplicationDetails
            {
                FirstName = "Sarah"
            };

            IActionResult result = await _sut.Index(application);

            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsType<NewCreditCardApplicationDetails>(viewResult.Model);

            Assert.Equal(application.FirstName, model.FirstName);
        }

        [Fact]
        public async Task NotSaveApplicationWhenModelError()
        {
            _sut.ModelState.AddModelError("x", "Test Error");

            var application = new NewCreditCardApplicationDetails();

            await _sut.Index(application);

            _mockRepository.Verify(
                x => x.AddAsync(It.IsAny<CreditCardApplication>()), Times.Never);
        }

        [Fact]
        public async Task SaveApplicationWhenValidModel()
        {
            CreditCardApplication savedApplication = null;

            _mockRepository.Setup(x => x.AddAsync(It.IsAny<CreditCardApplication>()))
                .Returns(Task.CompletedTask)
                .Callback<CreditCardApplication>(x => savedApplication = x);

            var application = new NewCreditCardApplicationDetails
            {
                FirstName = "Sarah",
                LastName = "Smith",
                Age = 18,
                FrequentFlyerNumber = "012345-A",
                GrossAnnualIncome = 100_000
            };

            await _sut.Index(application);

            _mockRepository.Verify(
                x => x.AddAsync(It.IsAny<CreditCardApplication>()), Times.Once);

            Assert.Equal(application.FirstName, savedApplication.FirstName);
            Assert.Equal(application.LastName, savedApplication.LastName);
            Assert.Equal(application.Age, savedApplication.Age);
            Assert.Equal(application.FrequentFlyerNumber, savedApplication.FrequentFlyerNumber);
            Assert.Equal(application.GrossAnnualIncome, savedApplication.GrossAnnualIncome);
        }


        [Fact]
        public async Task ReturnApplicationCompleteViewWhenValidModel()
        {
            var application = new NewCreditCardApplicationDetails
            {
                FirstName = "Sarah",
                LastName = "Smith",
                Age = 18,
                FrequentFlyerNumber = "012345-A",
                GrossAnnualIncome = 100_000
            };

            var result = await _sut.Index(application);

            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.Equal("ApplicationComplete", viewResult.ViewName);
        }
    }
}
