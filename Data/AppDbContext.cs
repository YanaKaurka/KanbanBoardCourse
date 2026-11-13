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
            // ВНИМАНИЕ: Замени строку подключения на свою!
            // Если используешь LocalDB:
            optionsBuilder.UseSqlServer(@"Server=KOMPUTER\SQLEXPRESS;Database=KanbanDBCourse;Trusted_Connection=True;TrustServerCertificate=True;");
            

            // Если используешь обычный SQL Server: Data Source=KOMPUTER\SQLEXPRESS;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Application Name="SQL Server Management Studio";Command Timeout=0
            // optionsBuilder.UseSqlServer(@"Server=localhost;Database=KanbanDB;Trusted_Connection=True;TrustServerCertificate=True;");

            // Если используешь SQLEXPRESS:
            // optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=KanbanDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Task>()
                .HasOne(t => t.Creator)
                .WithMany(u => u.CreatedTasks)
                .HasForeignKey(t => t.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь: Кому назначена задача (МЕНЯЕМ на NoAction!)
            modelBuilder.Entity<Models.Task>()
                .HasOne(t => t.Assignee)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssigneeId)
                .OnDelete(DeleteBehavior.NoAction); // <-- БЫЛО SetNull, СТАЛО NoAction

            base.OnModelCreating(modelBuilder);
        }

    }
}
