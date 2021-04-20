using LestDate_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LestDate_API.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
        RefreshToken GenerateRefreshToken();
    }
}
