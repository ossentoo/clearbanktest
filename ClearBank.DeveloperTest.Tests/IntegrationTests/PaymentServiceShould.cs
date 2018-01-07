using System;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.IntegrationTests
{
    [TestFixture]
    public class PaymentServiceShould
    {
        private readonly IConfigurationService _configurationService;
        public PaymentServiceShould()
        {
            _configurationService = new ConfigurationService();
        }

        [Test]
        public void MakeAPaymentWhereAccountContainsBacsScheme()
        {
            var request = new MakePaymentRequest
            {
                CreditorAccountNumber = "AAA111",
                DebtorAccountNumber = "AAA112",
                Amount = 100M,
                PaymentDate = new DateTime(2018,01,02),
                PaymentScheme = PaymentScheme.Bacs,
            };

            var service = new PaymentService(_configurationService);
            var result = service.MakePayment(request);

            Assert.AreEqual(true, result.Success);
            
        }
        [Test]
        public void NotMakeAPaymentWhereAccountDoesnotContainBacsScheme()
        {
            var request = new MakePaymentRequest
            {
                CreditorAccountNumber = "AAA111",
                DebtorAccountNumber = "AAA112",
                Amount = 100M,
                PaymentDate = new DateTime(2018,01,02),
                PaymentScheme = PaymentScheme.FasterPayments,
            };

            var service = new PaymentService(_configurationService);
            var result = service.MakePayment(request);

            Assert.AreEqual(false, result.Success);
            
        }
    }
}
