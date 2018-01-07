using System;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validation
{
    public class FasterPaymentsSchemeValidator : BaseValidator, IValidateRequest
    {
        private readonly MakePaymentRequest _request;

        public FasterPaymentsSchemeValidator(Account account, MakePaymentRequest request)
            :base(account)
        {
            _request = request;
        }
        public MakePaymentResult Validate()
        {
            var result = new MakePaymentResult();

            if (!ValidateAccount())
                return result;

            if (!Account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
            {
                result.Success = false;
            }
            else if (Account.Balance < _request.Amount)
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