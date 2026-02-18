using System.Data.Entity;
using CPSC321_A03_MC.Models;

namespace CPSC321_A03_MC.wwwroot.Context;

public class AppDbContext: DbContext
{

    // This tells the scaffolding engine about your UserModel
    public DbSet<UserModel> Users { get; set; }
}