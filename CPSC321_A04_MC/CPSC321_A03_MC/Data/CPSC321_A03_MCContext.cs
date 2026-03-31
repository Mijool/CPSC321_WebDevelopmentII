using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CPSC321_A04_MC.Models;

namespace CPSC321_A04_MC.Data
{
    public class CPSC321_A03_MCContext : DbContext
    {
        public CPSC321_A03_MCContext (DbContextOptions<CPSC321_A03_MCContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> UserModel { get; set; } = default!;
    }
}
