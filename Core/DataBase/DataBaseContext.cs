using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.DataBase
{
    class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("name=DatabaseContext")
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
