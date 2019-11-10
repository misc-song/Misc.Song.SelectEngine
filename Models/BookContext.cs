using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Misc.Song.SelectEngine.Models
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options) : base(options)
        {
            //Database.Migrate();
            Database.EnsureCreated();
        }
        public DbSet<B2> b2 { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
    }
}
