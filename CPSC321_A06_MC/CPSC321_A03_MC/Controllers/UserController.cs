using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CPSC321_A06_MC.Data;
using CPSC321_A06_MC.Models;

namespace CPSC321_A06_MC.Controllers
{
    public class UserController : Controller
    {
        private readonly CPSC321_A06_MCContext _context;

        public UserController(CPSC321_A06_MCContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            View(await _context.UserModel.ToListAsync());
        }

        
    }
}
