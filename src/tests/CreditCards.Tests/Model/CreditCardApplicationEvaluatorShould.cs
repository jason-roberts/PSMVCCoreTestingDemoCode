using CreditCards.Core.Model;
using CreditCards.Core.Interfaces;
using Xunit;
using Moq;

namespace CreditCards.Tests.Model
{
    public class CreditCardApplicationEvaluatorShould
    {
        private const int ExpectedLowIncomeThreshhold = 20_000;
        private const int ExpectedHighIncomeThreshhold = 100_000;

        private readonly Mock<IFrequentFlyerNumberValidator> _mockValidator;
        private readonly CreditCardApplicationEvaluator _sut;

        public CreditCardApplicationEvaluatorShould()
        {
            _mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            _mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            _sut = new CreditCardApplicationEvaluator(_mockValidator.Object);
        }
            

        [Theory]
        [InlineData(ExpectedHighIncomeThreshhold)]
        [InlineData(ExpectedHighIncomeThreshhold + 1)]
        [InlineData(int.MaxValue)]
        public void AcceptAllHighIncomeApplicants(int income)
        {
            var application = new CreditCardApplication
            {
                GrossAnnualIncome = income
            };

            Assert.Equal(CreditCardApplicationDecision.AutoAccepted,
                _sut.Evaluate(application));
        }




        [Theory]
        [InlineData(20)]
        [InlineData(19)]
        [InlineData(0)]
        [InlineData(int.MinValue)]
        public void ReferYoungApplicantsWhoAreNotHighIncome(int age)
        {            
            var application = new CreditCardApplication
            {
                GrossAnnualIncome = ExpectedHighIncomeThreshhold - 1,
                Age = age
            };

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman,
                _sut.Evaluate(application));
        }


        [Theory]
        [InlineData(ExpectedLowIncomeThreshhold)]
        [InlineData(ExpectedLowIncomeThreshhold + 1)]
        [InlineData(ExpectedHighIncomeThreshhold - 1)]
        public void ReferNonYoungApplicantsWhoAreMiddleIncome(int income)
        {
            var application = new CreditCardApplication
            {
                GrossAnnualIncome = income,
                Age = 21
            };

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman,
                _sut.Evaluate(application));
        }


        [Theory]
        [InlineData(ExpectedLowIncomeThreshhold - 1)]
        [InlineData(0)]
        [InlineData(int.MinValue)]
        public void DeclineAllApplicantsWhoAreLowIncome(int income)
        {
            var application = new CreditCardApplication
            {
                GrossAnnualIncome = income,
                Age = 21
            };

            Assert.Equal(CreditCardApplicationDecision.AutoDeclined,
                _sut.Evaluate(application));
        }


        [Fact]
        public void ReferInvalidFrequentFlyerNumbers()
        {            
            _mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);
            
            var application = new CreditCardApplication();

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman,
                _sut.Evaluate(application));

            _mockValidator.Verify(x => x.IsValid(It.IsAny<string>()),Times.Once);
        }
    }
}
