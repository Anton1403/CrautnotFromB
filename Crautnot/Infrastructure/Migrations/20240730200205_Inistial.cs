using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inistial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "exchange",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exchange", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "news",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    topic = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    listing_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    publish_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_news", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "token",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_token", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "exchange_tokens",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    token_id = table.Column<long>(type: "bigint", nullable: false),
                    exchange_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exchange_tokens", x => x.id);
                    table.ForeignKey(
                        name: "fk_exchange_tokens_exchange_exchange_id",
                        column: x => x.exchange_id,
                        principalTable: "exchange",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_exchange_tokens_token_token_id",
                        column: x => x.token_id,
                        principalTable: "token",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "deal_information",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    exchange_token_id = table.Column<long>(type: "bigint", nullable: false),
                    max_leverage = table.Column<int>(type: "int", nullable: false),
                    is_long = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_deal_information", x => x.id);
                    table.ForeignKey(
                        name: "fk_deal_information_exchange_tokens_exchange_token_id",
                        column: x => x.exchange_token_id,
                        principalTable: "exchange_tokens",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "exchange_tokens_news",
                columns: table => new
                {
                    exchange_tokens_id = table.Column<long>(type: "bigint", nullable: false),
                    news_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exchange_tokens_news", x => new { x.exchange_tokens_id, x.news_id });
                    table.ForeignKey(
                        name: "fk_exchange_tokens_news_exchange_tokens_exchange_tokens_id",
                        column: x => x.exchange_tokens_id,
                        principalTable: "exchange_tokens",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_exchange_tokens_news_news_news_id",
                        column: x => x.news_id,
                        principalTable: "news",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "token_data",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    exchange_tokens_id = table.Column<long>(type: "bigint", nullable: false),
                    dtv = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    trading_volume = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    closing_price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    highest_price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    lowest_price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    opening_price = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_token_data", x => x.id);
                    table.ForeignKey(
                        name: "fk_token_data_exchange_tokens_exchange_tokens_id",
                        column: x => x.exchange_tokens_id,
                        principalTable: "exchange_tokens",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "exchange",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1L, "Bybit" },
                    { 2L, "Mexc" },
                    { 3L, "GateIo" },
                    { 4L, "Binance" },
                    { 5L, "Okx" }
                });

            migrationBuilder.InsertData(
                table: "token",
                columns: new[] { "id", "name" },
                values: new object[] { -1L, "BTC" });

            migrationBuilder.CreateIndex(
                name: "ix_deal_information_exchange_token_id",
                table: "deal_information",
                column: "exchange_token_id");

            migrationBuilder.CreateIndex(
                name: "ix_exchange_tokens_exchange_id",
                table: "exchange_tokens",
                column: "exchange_id");

            migrationBuilder.CreateIndex(
                name: "ix_exchange_tokens_token_id",
                table: "exchange_tokens",
                column: "token_id");

            migrationBuilder.CreateIndex(
                name: "ix_exchange_tokens_news_news_id",
                table: "exchange_tokens_news",
                column: "news_id");

            migrationBuilder.CreateIndex(
                name: "ix_token_data_exchange_tokens_id",
                table: "token_data",
                column: "exchange_tokens_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deal_information");

            migrationBuilder.DropTable(
                name: "exchange_tokens_news");

            migrationBuilder.DropTable(
                name: "token_data");

            migrationBuilder.DropTable(
                name: "news");

            migrationBuilder.DropTable(
                name: "exchange_tokens");

            migrationBuilder.DropTable(
                name: "exchange");

            migrationBuilder.DropTable(
                name: "token");
        }
    }
}
