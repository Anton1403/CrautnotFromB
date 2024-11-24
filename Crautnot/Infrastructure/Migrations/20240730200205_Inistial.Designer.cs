﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20240730200205_Inistial")]
    partial class Inistial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ExchangeTokensNews", b =>
                {
                    b.Property<long>("ExchangeTokensId")
                        .HasColumnType("bigint")
                        .HasColumnName("exchange_tokens_id");

                    b.Property<long>("NewsId")
                        .HasColumnType("bigint")
                        .HasColumnName("news_id");

                    b.HasKey("ExchangeTokensId", "NewsId")
                        .HasName("pk_exchange_tokens_news");

                    b.HasIndex("NewsId")
                        .HasDatabaseName("ix_exchange_tokens_news_news_id");

                    b.ToTable("exchange_tokens_news", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.DealInformation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long>("ExchangeTokenId")
                        .HasColumnType("bigint")
                        .HasColumnName("exchange_token_id");

                    b.Property<bool>("IsLong")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_long");

                    b.Property<int>("MaxLeverage")
                        .HasColumnType("int")
                        .HasColumnName("max_leverage");

                    b.HasKey("Id")
                        .HasName("pk_deal_information");

                    b.HasIndex("ExchangeTokenId")
                        .HasDatabaseName("ix_deal_information_exchange_token_id");

                    b.ToTable("deal_information", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.Exchange", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_exchange");

                    b.ToTable("exchange", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Bybit"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Mexc"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "GateIo"
                        },
                        new
                        {
                            Id = 4L,
                            Name = "Binance"
                        },
                        new
                        {
                            Id = 5L,
                            Name = "Okx"
                        });
                });

            modelBuilder.Entity("Infrastructure.Entities.ExchangeTokens", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long>("ExchangeId")
                        .HasColumnType("bigint")
                        .HasColumnName("exchange_id");

                    b.Property<long>("TokenId")
                        .HasColumnType("bigint")
                        .HasColumnName("token_id");

                    b.HasKey("Id")
                        .HasName("pk_exchange_tokens");

                    b.HasIndex("ExchangeId")
                        .HasDatabaseName("ix_exchange_tokens_exchange_id");

                    b.HasIndex("TokenId")
                        .HasDatabaseName("ix_exchange_tokens_token_id");

                    b.ToTable("exchange_tokens", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.News", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<int>("Category")
                        .HasColumnType("int")
                        .HasColumnName("category");

                    b.Property<DateTime>("ListingDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("listing_date");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("publish_date");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("topic");

                    b.HasKey("Id")
                        .HasName("pk_news");

                    b.ToTable("news", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Entities.Token", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_token");

                    b.ToTable("token", (string)null);

                    b.HasData(
                        new
                        {
                            Id = -1L,
                            Name = "BTC"
                        });
                });

            modelBuilder.Entity("Infrastructure.Entities.TokenData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<decimal>("ClosingPrice")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("closing_price");

                    b.Property<DateTime>("Dtv")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("dtv");

                    b.Property<long>("ExchangeTokensId")
                        .HasColumnType("bigint")
                        .HasColumnName("exchange_tokens_id");

                    b.Property<decimal>("HighestPrice")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("highest_price");

                    b.Property<decimal>("LowestPrice")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("lowest_price");

                    b.Property<decimal>("OpeningPrice")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("opening_price");

                    b.Property<decimal>("TradingVolume")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("trading_volume");

                    b.HasKey("Id")
                        .HasName("pk_token_data");

                    b.HasIndex("ExchangeTokensId")
                        .HasDatabaseName("ix_token_data_exchange_tokens_id");

                    b.ToTable("token_data", (string)null);
                });

            modelBuilder.Entity("ExchangeTokensNews", b =>
                {
                    b.HasOne("Infrastructure.Entities.ExchangeTokens", null)
                        .WithMany()
                        .HasForeignKey("ExchangeTokensId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_exchange_tokens_news_exchange_tokens_exchange_tokens_id");

                    b.HasOne("Infrastructure.Entities.News", null)
                        .WithMany()
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_exchange_tokens_news_news_news_id");
                });

            modelBuilder.Entity("Infrastructure.Entities.DealInformation", b =>
                {
                    b.HasOne("Infrastructure.Entities.ExchangeTokens", "ExchangeToken")
                        .WithMany()
                        .HasForeignKey("ExchangeTokenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_deal_information_exchange_tokens_exchange_token_id");

                    b.Navigation("ExchangeToken");
                });

            modelBuilder.Entity("Infrastructure.Entities.ExchangeTokens", b =>
                {
                    b.HasOne("Infrastructure.Entities.Exchange", "Exchange")
                        .WithMany("ExchangeTokens")
                        .HasForeignKey("ExchangeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_exchange_tokens_exchange_exchange_id");

                    b.HasOne("Infrastructure.Entities.Token", "Token")
                        .WithMany("ExchangeTokens")
                        .HasForeignKey("TokenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_exchange_tokens_token_token_id");

                    b.Navigation("Exchange");

                    b.Navigation("Token");
                });

            modelBuilder.Entity("Infrastructure.Entities.TokenData", b =>
                {
                    b.HasOne("Infrastructure.Entities.ExchangeTokens", "ExchangeTokens")
                        .WithMany("TokenData")
                        .HasForeignKey("ExchangeTokensId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_token_data_exchange_tokens_exchange_tokens_id");

                    b.Navigation("ExchangeTokens");
                });

            modelBuilder.Entity("Infrastructure.Entities.Exchange", b =>
                {
                    b.Navigation("ExchangeTokens");
                });

            modelBuilder.Entity("Infrastructure.Entities.ExchangeTokens", b =>
                {
                    b.Navigation("TokenData");
                });

            modelBuilder.Entity("Infrastructure.Entities.Token", b =>
                {
                    b.Navigation("ExchangeTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
