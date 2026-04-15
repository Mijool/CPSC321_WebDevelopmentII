using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CPSC321_A06_MC.Models;

namespace CPSC321_A06_MC.Data
{
    public class CPSC321_A06_MCContext : DbContext
    {
        public CPSC321_A06_MCContext (DbContextOptions<CPSC321_A06_MCContext> options)
            : base(options)
        {
        }

        public DbSet<CPSC321_A06_MC.Models.UserModel> UserModel { get; set; } = default!;
    }
}
