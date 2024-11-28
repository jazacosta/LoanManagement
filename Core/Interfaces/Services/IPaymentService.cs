using Core.DTOs.InstallmentPayment;

namespace Core.Interfaces.Services
{
    public interface IPaymentService
    {
        Task PayInstallment(int approveLoanId, int installmentsToPay);
    }
}
