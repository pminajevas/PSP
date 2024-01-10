using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Infrastructure.Context
{
    public class PoSDBContextFactory : IDesignTimeDbContextFactory<PoSDBContext>
    {
        public PoSDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PoSDBContext>();
            optionsBuilder.UseSqlServer("DefaultConnection");

            return new PoSDBContext(optionsBuilder.Options);
        }
    }
}
