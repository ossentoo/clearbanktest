using System;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.UnitTests
{
    [TestFixture]
    public class PaymentServiceShould
    {
        private readonly Mock<IConfigurationService> _configurationService;

        public PaymentServiceShould()
        {
            _configurationService = new Mock<IConfigurationService>();
        }

        [Test]
        public void MakeAPaymentWhereAccountContainsBacsSchemeBackup()
        {
            var request = new MakePaymentRequest
            {
                CreditorAccountNumber = "AAA111",
                DebtorAccountNumber = "AAA112",
                Amount = 100M,
                PaymentDate = new DateTime(2018,01,02),
                PaymentScheme = PaymentScheme.Bacs,
            };
            _configurationService.Setup(x => x.GetDataStoreType()).Returns("Backup");


            var service = new PaymentService(_configurationService.Object);
            var result = service.MakePayment(request);

            Assert.AreEqual(true, result.Success);
            
        }

        [Test]
        public void MakeAPaymentWhereAccountContainsBacsSchemePrimary()
        {
            var request = new MakePaymentRequest
            {
                CreditorAccountNumber = "AAA111",
                DebtorAccountNumber = "AAA112",
                Amount = 100M,
                PaymentDate = new DateTime(2018,01,02),
                PaymentScheme = PaymentScheme.Bacs,
            };
            _configurationService.Setup(x => x.GetDataStoreType()).Returns("Primary");


            var service = new PaymentService(_configurationService.Object);
            var result = service.MakePayment(request);

            Assert.AreEqual(false, result.Success);
            
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

            _configurationService.Setup(x => x.GetDataStoreType()).Returns("Backup");

            var service = new PaymentService(_configurationService.Object);
            var result = service.MakePayment(request);

            Assert.AreEqual(false, result.Success);
            
        }
    }
}
