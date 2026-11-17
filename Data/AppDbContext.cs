using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanBoardCourse.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KanbanBoardCourse.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users {  get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Board> Boards { get; set; }   
        public DbSet<Models.Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Server=KOMPUTER\SQLEXPRESS;Database=KanbanDBCourse;Trusted_Connection=True;TrustServerCertificate=True;");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Task>()
                .HasOne(t => t.Creator)
                .WithMany(u => u.CreatedTasks)
                .HasForeignKey(t => t.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.Task>()
                .HasOne(t => t.Assignee)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssigneeId)
                .OnDelete(DeleteBehavior.NoAction); 

            base.OnModelCreating(modelBuilder);
        }

    }
}
