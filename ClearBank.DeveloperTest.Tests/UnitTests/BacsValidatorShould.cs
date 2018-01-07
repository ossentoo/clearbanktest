using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validation;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.UnitTests
{
    [TestFixture]
    public class BacsValidatorShould
    {
        [Test]
        public void ReturnFalseWhenAccountHasInvalidScheme()
        {
            // Access database to retrieve account, code removed for brevity 
            var account =  new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments | AllowedPaymentSchemes.Chaps,
                Location = AccountLocation.Primary
            };

            var validator = new BacsSchemeValidator(account);
            var result = validator.Validate();

            Assert.AreEqual(false, result.Success);
        }
        [Test]
        public void ReturnTrueWhenAccountHasValidScheme()
        {
            // Access database to retrieve account, code removed for brevity 
            var account =  new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                Location = AccountLocation.Backup
            };

            var validator = new BacsSchemeValidator(account);
            var result = validator.Validate();

            Assert.AreEqual(true, result.Success);
        }
    }
}
