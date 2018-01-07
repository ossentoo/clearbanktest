using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validation
{
    public class BacsSchemeValidator :BaseValidator, IValidateRequest
    {
        public BacsSchemeValidator(Account account):base(account)
        {
            
        }
        public MakePaymentResult Validate()
        {
            var result = new MakePaymentResult();

            if (!ValidateAccount())
                return result;

            result.Success = Account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);

            return result;
        }
    }
}
