using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validation;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfigurationService _configurationService;

        public PaymentService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var creator = new AccountDataStoreCreator(_configurationService);
            var account = creator.Create(request.DebtorAccountNumber);

            var validator = SchemeValidatorFactory.GetValidator(account, request);
            var result = validator.Validate();

            if (result.Success)
            {
                account.Balance -= request.Amount;

                if (account.Location == AccountLocation.Backup)
                {
                    var accountDataStore = new BackupAccountDataStore();
                    accountDataStore.UpdateAccount(account);
                }
                else
                {
                    var accountDataStore = new AccountDataStore();
                    accountDataStore.UpdateAccount(account);
                }
            }

            return result;
        }
    }
}
