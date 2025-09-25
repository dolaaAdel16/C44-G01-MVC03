using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Data.Contexts;
using Company.G01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {

        private readonly CompanyDbContext _context;

        // ASK CLR Create Object From CompanyDbContext
        public DepartmentRepository(CompanyDbContext context)
        {
        }

    }
}
