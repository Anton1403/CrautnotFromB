using Infrastructure.Entities;
using Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }
        public DbSet<News> News { get; set; }
        public DbSet<Token> Token { get; set; }
        public DbSet<Exchange> Exchange { get; set; }
        public DbSet<ExchangeTokens> ExchangeTokens { get; set; }
        public DbSet<TokenData> TokenData { get; set; }
        public DbSet<DealInformation> DealInformation { get; set; }
        public DbSet<Logs> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly, t => t.GetInterfaces()
                                                             .Any(i =>
                                                                      i.IsGenericType &&
                                                                      i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) &&
                                                                      typeof(IEntity).IsAssignableFrom(i.GenericTypeArguments[0])));

            modelBuilder.Entity<Exchange>().HasData(
            Enum.GetValues(typeof(ExchangeEnum))
                .Cast<ExchangeEnum>()
                .Select(e => new Exchange
                {
                    Id = (int)e,
                    Name = e.ToString(),
                })
                .ToArray()
        );

            modelBuilder.Entity<Token>().HasData(
                new Token
                {
                    Id = -1,
                    Name = "BTC",
                }
            );
        }

        public class TrainingDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
        {
            //private readonly IConfiguration _configuration; 
            //public TrainingDbContextFactory(IConfiguration configuration)
            //{
            //    _configuration = configuration;
            //}
            public MainDbContext CreateDbContext(string[] args) {
                var optionsBuilder = new DbContextOptionsBuilder<MainDbContext>();
                optionsBuilder
                    .UseMySql("Server=localhost;Port=3306;Database=crautnot;UserId=root;Password=root", ServerVersion.AutoDetect("Server=localhost;Port=3306;Database=crautnot;UserId=root;Password=root"))
                    .UseSnakeCaseNamingConvention()
                    .EnableSensitiveDataLogging();

                return new MainDbContext(optionsBuilder.Options);
            }
        }
    }
}
