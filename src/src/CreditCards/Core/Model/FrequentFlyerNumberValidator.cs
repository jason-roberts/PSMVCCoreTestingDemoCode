using System;
using System.Globalization;
using System.Linq;
using CreditCards.Core.Interfaces;

namespace CreditCards.Core.Model
{
    /// <summary>
    /// A frequent flyer number consists of 2 parts separated by a '-':
    /// [member number]-[scheme identifier]
    /// Member numbers consist of 6 numeric digits
    /// Scheme identifiers are a single uppercase alphabetic character
    /// </summary>
    public class FrequentFlyerNumberValidator : IFrequentFlyerNumberValidator
    {
        private readonly char[] _validSchemeIdentifiers = { 'A', 'Q', 'Y' };
        private const int ExpectedTotalLength = 8;
        private const int ExpectedMemberNumberLength = 6;

        public bool IsValid(string frequentFlyerNumber)
        {
            if (frequentFlyerNumber is null)
            {
                throw new ArgumentNullException(nameof(frequentFlyerNumber));
            }

            if (frequentFlyerNumber.Length != ExpectedTotalLength)
            {
                return false;
            }

            var memberNumberPart = frequentFlyerNumber
                                    .Substring(0, ExpectedMemberNumberLength);

            if (!int.TryParse(memberNumberPart, NumberStyles.None, null, out int _))
            {
                return false;
            }

            var schemeIdentifier = frequentFlyerNumber.Last();
            return _validSchemeIdentifiers.Contains(schemeIdentifier);
        }
    }
}
