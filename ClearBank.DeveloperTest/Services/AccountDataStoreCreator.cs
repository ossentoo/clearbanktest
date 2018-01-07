using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountDataStoreCreator
    {
        private readonly string _dataStoreType;

        public AccountDataStoreCreator(IConfigurationService configurationService)
        {
            _dataStoreType = configurationService.GetDataStoreType();
        }

        public Account Create(string debtorAccountNumber)
        {
            if (_dataStoreType == "Backup")
            {
                var accountDataStore = new BackupAccountDataStore();
                return accountDataStore.GetAccount(debtorAccountNumber);
            }
            else
            {
                var accountDataStore = new AccountDataStore();
                return accountDataStore.GetAccount(debtorAccountNumber);
            }
        }
    }
}