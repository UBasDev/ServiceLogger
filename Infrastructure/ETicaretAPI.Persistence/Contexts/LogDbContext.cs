using ETicaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Contexts
{
    public class LogDbContext:DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> options):base(options)
        {

        }
        public DbSet<RequestLog> RequestLogs { get; set; }
    }
}
