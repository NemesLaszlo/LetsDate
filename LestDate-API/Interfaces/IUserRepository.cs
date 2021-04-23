﻿using LestDate_API.DTOs;
using LestDate_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LestDate_API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<IEnumerable<MemberDto>> GetUsersAsync();
        Task<MemberDto> GetUserByIdAsync(int id);
        Task<MemberDto> GetUserByUsernameAsync(string username);
        Task<string> GetUserGender(string username);
        Task<bool> SaveAllAsync();
    }
}
