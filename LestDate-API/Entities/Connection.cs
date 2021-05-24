using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LestDate_API.Entities
{
    public class Connection
    {
        public string ConnectionId { get; set; } // Entity framework because of the property name knows it is a primary key
        public string Username { get; set; }

        public Connection() // Entity Framework needs this because of it is an Entity (Default constructor with no parameters)
        {
        }

        public Connection(string connectionId, string username)
        {
            ConnectionId = connectionId;
            Username = username;
        }
    }
}
