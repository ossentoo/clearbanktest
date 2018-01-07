using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validation
{
    public interface IValidateRequest
    {
        MakePaymentResult Validate();
    }
}