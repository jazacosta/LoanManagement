﻿using Core.DTOs.LoanManagement;

namespace Core.Interfaces.Services;

public interface ILoanService
{
    Task ApproveLoan(ApprovedLoanDTO loanApproval, CancellationToken cancellationToken = default);
    Task RejectLoan(RejectedLoanDTO loanRejection);
}
