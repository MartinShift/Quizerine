using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbQuizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false),
                    TimeLimit = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbQuizzes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false),
                    QuizId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbQuestions_DbQuizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "DbQuizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DbResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientName = table.Column<string>(type: "TEXT", nullable: false),
                    SecondsSpent = table.Column<int>(type: "INTEGER", nullable: false),
                    Points = table.Column<int>(type: "INTEGER", nullable: false),
                    QuizId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbResults_DbQuizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "DbQuizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DbAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSelected = table.Column<bool>(type: "INTEGER", nullable: false),
                    QuestionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbAnswers_DbQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "DbQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbAnswers_QuestionId",
                table: "DbAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_DbQuestions_QuizId",
                table: "DbQuestions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_DbResults_QuizId",
                table: "DbResults",
                column: "QuizId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbAnswers");

            migrationBuilder.DropTable(
                name: "DbResults");

            migrationBuilder.DropTable(
                name: "DbQuestions");

            migrationBuilder.DropTable(
                name: "DbQuizzes");
        }
    }
}