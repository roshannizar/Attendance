using Attendance.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance.Api.DbContexts
{
    public class AttendanceDBContext : DbContext
    {
        public AttendanceDBContext(DbContextOptions<AttendanceDBContext> options) : base(options) { }
        
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Delivery> Delivery { get; set; }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<Employees>().HasQueryFilter(e => e.Active == Enums.RecordState.Active);
            modelBuilder.Entity<Delivery>().HasQueryFilter(d => d.Active == Enums.RecordState.Active);
        }
    }
}
