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
        public QuizerineDbContext(DbContextOptions options) : base(options) { Database.EnsureCreated(); }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DbQuiz>().HasKey(p => p.Id);
            modelBuilder.Entity<DbQuiz>().Property(p => p.Id);
            modelBuilder.Entity<DbQuiz>().Property(p => p.Image).IsRequired();
            modelBuilder.Entity<DbQuiz>().Property(p => p.TimeLimit).IsRequired();
            modelBuilder.Entity<DbQuiz>().Property(p => p.Title).IsRequired();

            modelBuilder.Entity<DbQuizResult>().HasKey(p => p.Id);
            modelBuilder.Entity<DbQuizResult>().Property(p => p.Id);
            modelBuilder.Entity<DbQuizResult>().Property(p => p.Points).IsRequired();
            modelBuilder.Entity<DbQuizResult>().Property(p => p.SecondsSpent).IsRequired();
            modelBuilder.Entity<DbQuizResult>().Property(p => p.ClientName).IsRequired();
            modelBuilder.Entity<DbQuizResult>().Property(p => p.QuizId).IsRequired();

            modelBuilder.Entity<DbQuiz>().HasMany(p => p.Questions).WithOne(p => p.Quiz).HasForeignKey(p => p.QuizId);

            modelBuilder.Entity<DbQuestion>().HasMany(p => p.Answers).WithOne(p => p.Question).HasForeignKey(p => p.QuestionId);

            modelBuilder.Entity<DbQuizResult>().HasOne(p => p.Quiz).WithMany(p=>p.Results).HasForeignKey(p => p.QuizId);

            modelBuilder.Entity<DbQuestion>().HasMany(p => p.Answers).WithOne(p => p.Question).HasForeignKey(p => p.QuestionId);
        }
        public DbSet<DbQuiz> DbQuizzes { get; set; }
        public DbSet<DbQuestion> DbQuestions { get; set; }
        public  DbSet<DbQuizResult> DbResults { get; set; }
        public DbSet<DbAnswer> DbAnswers { get; set; }
    }
}
