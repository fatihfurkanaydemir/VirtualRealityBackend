using Microsoft.EntityFrameworkCore;
using VirtualReality.Models;

namespace VirtualReality.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public DbSet<House> Houses { get; set; }   
        public DbSet<Room> Rooms { get; set; }

    }
}
