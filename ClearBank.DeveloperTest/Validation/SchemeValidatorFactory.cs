using System;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validation
{
    public static class SchemeValidatorFactory
    {
        public static IValidateRequest GetValidator(Account account, MakePaymentRequest request)
        {
            if (request.PaymentScheme == PaymentScheme.FasterPayments)
            {
                return new FasterPaymentsSchemeValidator(account,request);
            }
            if (request.PaymentScheme == PaymentScheme.Bacs)
            {
                return new BacsSchemeValidator(account);
            }
            if (request.PaymentScheme == PaymentScheme.Chaps)
            {
                return new ChapsSchemeValidator(account);
            }

            throw new NotImplementedException();
        }
    }
}
