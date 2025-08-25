using CashFlowLite.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowLite.Application.Services
{
    public interface IOnboardingService
    {
        Task<int> RegisterAndCreateAccountAsync(RegisterDto dto);
    }
}
