using AutoMapper;
using AutoMapper.QueryableExtensions;
using LestDate_API.Database;
using LestDate_API.DTOs;
using LestDate_API.Entities;
using LestDate_API.Helpers;
using LestDate_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LestDate_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            var query = _context.Users.AsQueryable();

            query = query.Where(u => u.UserName != userParams.CurrentUsername); // except current user
            query = query.Where(u => u.Gender == userParams.Gender); // gender

            var minDateOfBirth = DateTime.Today.AddYears(-userParams.MaxAge - 1); // -1 today havent had birthbay yet
            var maxDateOfBirth = DateTime.Today.AddYears(-userParams.MinAge);

            query = query.Where(u => u.DateOfBirth >= minDateOfBirth && u.DateOfBirth <= maxDateOfBirth); // age filters

            query = userParams.OrderBy switch
            {
                "created" => query.OrderByDescending(u => u.Created),
                _ => query.OrderByDescending(u => u.LastActive)
            }; // sorting operation, default "lastActive"

            var members = query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking();

            return await PagedList<MemberDto>.CreateAsync(
                members,
                userParams.PageNumber, 
                userParams.PageSize
            ); 
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
                .Where(x => x.UserName == username)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<string> GetUserGender(string username)
        {
            return await _context.Users
                .Where(x => x.UserName == username)
                .Select(x => x.Gender).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified; // Mark it has been modified
        }
    }
}
