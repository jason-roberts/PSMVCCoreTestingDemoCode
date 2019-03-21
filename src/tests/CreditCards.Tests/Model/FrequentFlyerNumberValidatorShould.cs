using System;
using CreditCards.Core.Model;
using Xunit;

namespace CreditCards.Tests.Model
{
    public class FrequentFlyerNumberValidatorShould
    {
        [Theory]
        [InlineData("012345-A")]
        [InlineData("012345-Q")]
        [InlineData("012345-Y")]
        public void AcceptValidSchemes(string number)
        {
            var sut = new FrequentFlyerNumberValidator();

            Assert.True(sut.IsValid(number));
        }

        [Theory]
        [InlineData("012345-1")]
        [InlineData("012345-B")]
        [InlineData("012345-P")]
        [InlineData("012345-R")]
        [InlineData("012345-X")]
        [InlineData("012345-Z")]
        [InlineData("012345- ")]
        [InlineData("012345-a")]
        [InlineData("012345-q")]
        [InlineData("012345-y")]
        public void RejectInvalidSchemes(string number)
        {
            var sut = new FrequentFlyerNumberValidator();

            Assert.False(sut.IsValid(number));
        }


        [Theory]
        [InlineData("0012345-A")]
        [InlineData("X12345-A")]
        [InlineData("01234X-A")]
        public void RejectInvalidMemberNumbers(string number)
        {
            var sut = new FrequentFlyerNumberValidator();

            Assert.False(sut.IsValid(number));
        }


        [Theory]
        [InlineData("      -A")]
        [InlineData("  1   -A")]
        [InlineData("1     -A")]
        [InlineData("     1-A")]
        public void RejectEmptyMemberNumberDigits(string number)
        {
            var sut = new FrequentFlyerNumberValidator();

            Assert.False(sut.IsValid(number));
        }

        [Theory]
        [InlineData("        ")]
        [InlineData("")]
        public void RejectEmptyFrequentFlyerNumber(string number)
        {
            var sut = new FrequentFlyerNumberValidator();

            Assert.False(sut.IsValid(number));
        }

        [Fact]
        public void ThrowExceptionWhenNullFrequentFlyerNumber()
        {
            var sut = new FrequentFlyerNumberValidator();

            Assert.Throws<ArgumentNullException>(() => sut.IsValid(null));
        }
    }
}
