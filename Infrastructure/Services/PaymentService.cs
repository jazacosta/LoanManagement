using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using System.Linq;

namespace Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentService;
    private readonly IInstallmentRepository _installmentRepository;

    public PaymentService(IPaymentRepository paymentRepository, IInstallmentRepository installmentRepository)
    {
        _paymentService = paymentRepository;
        _installmentRepository = installmentRepository;
    }

    public async Task PayInstallment(int approveLoanId, int installmentsToPay)
    {
        //var installments = await _installmentRepository.GetInstallments(approveLoanId);
        //var
        //if (installments == null || installments.FirstOrDefault(x => x.Status != "Pending"))
        //    throw new InvalidOperationException("The installment(s) is not available for payment");
        throw new NotImplementedException();

    }
}
