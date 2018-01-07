using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validation
{
    public abstract class BaseValidator
    {
        protected Account Account { get; }

        protected BaseValidator(Account account)
        {
            Account = account;
        }

        protected bool ValidateAccount()
        {
            return Account != null;
        }
    }
}