using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DbModels
{
    public class QuizerineDbContext : DbContext
    {
        public QuizerineDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DbQuiz>().HasKey(p => p.Id);
            modelBuilder.Entity<DbQuiz>().Property(p => p.Id);
            modelBuilder.Entity<DbQuiz>().Property(p => p.Image).IsRequired();
            modelBuilder.Entity<DbQuiz>().Property(p => p.TimeLimit).IsRequired();
            modelBuilder.Entity<DbQuiz>().Property(p => p.Title).IsRequired();
            modelBuilder.Entity<DbQuiz>().Property(p => p.Questions);

            modelBuilder.Entity<DbQuiz>().HasMany(p => p.Questions).WithOne(p => p.Quiz).HasForeignKey(p => p.QuizId);

            modelBuilder.Entity<DbQuestion>().HasMany(p => p.Answers).WithOne(p => p.Question).HasForeignKey(p => p.QuestionId);
        }
    }
}
