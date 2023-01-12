using Microsoft.EntityFrameworkCore;
using RepositoryPaternUOW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryPaternUOW.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        public virtual DbSet<User> Users { get; set; }
    }
}
