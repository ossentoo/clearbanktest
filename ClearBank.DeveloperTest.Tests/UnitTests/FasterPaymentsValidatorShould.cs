using System;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validation;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.UnitTests
{
    [TestFixture]
    public class FasterPaymentsValidatorShould
    {
        private readonly MakePaymentRequest _makePaymentRequest;

        public FasterPaymentsValidatorShould()
        {
            _makePaymentRequest = new MakePaymentRequest
            {
                CreditorAccountNumber = "AAA111",
                DebtorAccountNumber = "AAA112",
                Amount = 100M,
                PaymentDate = new DateTime(2018, 01, 02),
                PaymentScheme = PaymentScheme.Bacs,
            };
        }

        [Test]
        public void ReturnFalseWhenAccountHasInvalidScheme()
        {
            // Access database to retrieve account, code removed for brevity 
            var account =  new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps,
                Location = AccountLocation.Primary
            };

            var validator = new FasterPaymentsSchemeValidator(account, _makePaymentRequest);
            var result = validator.Validate();

            Assert.AreEqual(false, result.Success);
        }

        [Test]
        public void ReturnFalseWhenAccountValidSchemeButAmountsInvalid()
        {
            // Access database to retrieve account, code removed for brevity 
            var account =  new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Location = AccountLocation.Primary
            };

            var validator = new FasterPaymentsSchemeValidator(account, _makePaymentRequest);
            var result = validator.Validate();

            Assert.AreEqual(false, result.Success);
        }
        [Test]
        public void ReturnTrueWhenAccountValidSchemeAndAmountsValid()
        {
            // Access database to retrieve account, code removed for brevity 
            var account =  new Account
            {
                Balance = 200,
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Location = AccountLocation.Primary
            };

            var validator = new FasterPaymentsSchemeValidator(account, _makePaymentRequest);
            var result = validator.Validate();

            Assert.AreEqual(true, result.Success);
        }
    }
}
