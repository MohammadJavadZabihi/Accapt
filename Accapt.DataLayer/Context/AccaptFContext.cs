using Accapt.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.DataLayer.Context
{
    public class AccaptFContext : DbContext
    {
        public AccaptFContext(DbContextOptions<AccaptFContext> context) : base(context) 
        {
            
        }

        #region User Table

        public DbSet<Users> Users { get; set; }

        #endregion
    }
}
