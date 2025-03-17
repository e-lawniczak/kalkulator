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
        public DbSet<HistoryEntry> Operations { get; set; }

        public string DbPath { get; }

        public OperationHistoryContext()
        {
            DbPath = System.IO.Path.Join("/datbase","operations.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlite($"Data Source={DbPath}");
    }
}
