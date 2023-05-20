using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DbModels
{
    public class QuizerineDbContext : DbContext
    {
        public QuizerineDbContext(DbContextOptions options) : base(options) { }
        public QuizerineDbContext() : base() { }

    }
}
