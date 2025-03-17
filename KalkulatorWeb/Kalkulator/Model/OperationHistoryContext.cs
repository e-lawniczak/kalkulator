using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Model
{
    class OperationHistoryContext : DbContext
    {

        public string DbPath { get; }

        public OperationHistoryContext() : base()
        {
            System.IO.Directory.CreateDirectory("./database");
            DbPath = System.IO.Path.Join("./database","operations.db");
            Database.EnsureCreated();
        }
        public DbSet<HistoryEntry> Operations { get; set; }
        public DbSet<Operator> Operators { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlite( $"Data Source={DbPath}");
    }
}
