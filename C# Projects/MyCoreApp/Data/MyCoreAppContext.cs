using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCoreApp.Models;

namespace MyCoreApp.Data
{
    public class MyCoreAppContext : DbContext
    {
        public MyCoreAppContext (DbContextOptions<MyCoreAppContext> options)
            : base(options)
        {
        }

        public DbSet<MyCoreApp.Models.Movie> Movie { get; set; } = default!;
    }
}
