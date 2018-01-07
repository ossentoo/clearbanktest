using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validation
{
    public class ChapsSchemeValidator : BaseValidator, IValidateRequest
    {
        public ChapsSchemeValidator(Account account) : base(account)
        {
            
        }

        public MakePaymentResult Validate()
        {
            var result = new MakePaymentResult();

            if (!ValidateAccount())
                return result;

            if (!Account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
            {
                result.Success = false;
            }
            else if (Account.Status != AccountStatus.Live)
            {
                result.Success = false;
            }
            else
            {
                result.Success = true;
            }

            return result;
        }
    }
}