using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Task AddPayment(Payment payment);
        Task UpdatePayment(Payment payment);
    }
}
