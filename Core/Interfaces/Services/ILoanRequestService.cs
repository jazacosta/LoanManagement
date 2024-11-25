using Core.DTOs.Request;

namespace Core.Interfaces.Services;

public interface ILoanRequestService
{
   Task<LoanRequestResponseDTO> CreateLoanRequest(LoanRequestDTO loanRequestDTO);
}
