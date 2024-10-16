using Microsoft.EntityFrameworkCore;
using MasterProjectDAL.DataModel;
using MasterProjectDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterProjectDAL.DataModel
{
    public interface IMasterProjectContext : IMasterProjectDbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserMember> UserMember { get; set; }

        public DbSet<Relation> Relationships { get; set; }

        public DbSet<School> School { get; set; }
    }
}
