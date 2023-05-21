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
        public QuizerineDbContext() { }
        public QuizerineDbContext(DbContextOptions options) : base(options) { 

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
            builder.UseSqlite("Data Source=C:\\Users\\kvvkv\\source\\repos\\Quizerine\\quiz.db");
        }

        public virtual DbSet<DbQuiz> DbQuizzes { get; set; } = null!;
        public virtual DbSet<DbQuestion> DbQuestions { get; set; } = null!;
        public virtual DbSet<DbQuizResult> DbResults { get; set; } = null!;
        public virtual DbSet<DbAnswer> DbAnswers { get; set; } = null!;
    }
}
