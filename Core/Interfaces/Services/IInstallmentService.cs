﻿using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces.Services;

public interface IInstallmentService
{
    Task<List<InstallmentDTO>> GenerateInstallment(ApprovedLoan approvedLoan);
}
