using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CPSC321_A05_MC.Models;

namespace CPSC321_A05_MC.Data
{
    public class CPSC321_A05_MCContext : DbContext
    {
        public CPSC321_A05_MCContext (DbContextOptions<CPSC321_A05_MCContext> options)
            : base(options)
        {
        }

        public DbSet<CPSC321_A05_MC.Models.CarModel> Cars { get; set; } = default!;
    }
}
