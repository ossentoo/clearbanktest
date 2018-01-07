using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.UnitTests
{

    [TestFixture]
    public class AccountDataStoreCreatorShould
    {
        private readonly Mock<IConfigurationService> _configurationService;
        private const string AccountNumber = "AAA111";
        public AccountDataStoreCreatorShould()
        {
            _configurationService = new Mock<IConfigurationService>();
        }

        [Test]
        public void CreateAnAccountUsingBackupDataStore()
        {
            _configurationService.Setup(x => x.GetDataStoreType()).Returns("Backup");
            var creator = new AccountDataStoreCreator(_configurationService.Object);
            var account = creator.Create(AccountNumber);

            Assert.AreEqual(AccountLocation.Backup, account.Location);
            Assert.IsTrue(account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs));
            Assert.IsFalse(account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps));
            Assert.IsFalse(account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments));
        }

        [Test]
        public void CreateAnAccountUsingPrimaryDataStore()
        {
            _configurationService.Setup(x => x.GetDataStoreType()).Returns("Primary");
            var creator = new AccountDataStoreCreator(_configurationService.Object);
            var account = creator.Create(AccountNumber);

            Assert.AreEqual(AccountLocation.Primary, account.Location);
            Assert.IsTrue(account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps));
            Assert.IsTrue(account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments));
            Assert.IsFalse(account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs));
        }

    }
}
