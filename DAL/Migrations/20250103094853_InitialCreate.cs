using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Genre = table.Column<string>(type: "TEXT", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Members_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReviewerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReviewedMemberId = table.Column<int>(type: "INTEGER", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Comments = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberReviews_Members_ReviewedMemberId",
                        column: x => x.ReviewedMemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberReviews_Members_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReviewerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Comments = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookReviews_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookReviews_Members_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InitiatorId = table.Column<int>(type: "INTEGER", nullable: false),
                    TargetBookId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OfferedBookId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trades_Books_OfferedBookId",
                        column: x => x.OfferedBookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trades_Books_TargetBookId",
                        column: x => x.TargetBookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trades_Members_InitiatorId",
                        column: x => x.InitiatorId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookReviews_BookId",
                table: "BookReviews",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReviews_ReviewerId",
                table: "BookReviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_OwnerId",
                table: "Books",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberReviews_ReviewedMemberId",
                table: "MemberReviews",
                column: "ReviewedMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberReviews_ReviewerId",
                table: "MemberReviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_InitiatorId",
                table: "Trades",
                column: "InitiatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_OfferedBookId",
                table: "Trades",
                column: "OfferedBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_TargetBookId",
                table: "Trades",
                column: "TargetBookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookReviews");

            migrationBuilder.DropTable(
                name: "MemberReviews");

            migrationBuilder.DropTable(
                name: "Trades");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
