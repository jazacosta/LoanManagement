using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IInstallmentRepository
{
    Task AddInstallment(Installment installment);
}
