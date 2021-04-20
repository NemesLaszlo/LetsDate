using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LestDate_API.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public AppUser AppUser { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; } = DateTime.UtcNow.AddDays(7);
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime? Revoked { get; set; } // optional
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
