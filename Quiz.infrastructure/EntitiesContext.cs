using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

using Microsoft.VisualBasic;
using Quiz.Contracts.Entities;
using Quiz.Contracts.Interfaces;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Net;

namespace Quiz.Infrastructure
{
    public partial class EntitiesContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private IDateTime _datetime { get; set; }
        private IUserIdentity _identity { get; set; }
        private IDbConnection _dbConnection { get; }

        public EntitiesContext(DbContextOptions<EntitiesContext> options,
            IDateTime datetime,
            IUserIdentity identity,
            IConfiguration configuration) : base(options)
        {
            _datetime = datetime;
            _identity = identity;
            _configuration = configuration;
            _dbConnection = new SqlConnection(_configuration.GetConnectionString("ConnectionSQLServer"));

            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conString = _dbConnection.ConnectionString;

            optionsBuilder.UseSqlServer(conString, builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });

            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<BaseVersionModel>().AsEnumerable())
            {
                if (item.State == EntityState.Added)
                {

                    if (item.Entity.CREATE_DATE == new DateTime())
                    {
                        item.Entity.CREATE_DATE = _datetime.Now;
                    }

                    item.Entity.UPDATE_DATE = _datetime.Now;
                }

                if (item.State == EntityState.Modified)
                {
                    item.Entity.UPDATE_DATE = _datetime.Now;
                }

            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public virtual DbSet<Q_USER> USER { get; set; }
        public virtual DbSet<Q_USER_GROUP> USER_GROUP { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Q_USER>(entity =>
            {
                entity.ToTable("Q_USER");
                entity.Property(e => e.ID)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");
                entity.Property(e => e.USERNAME)
                    .HasMaxLength(50)
                    .HasColumnName("USERNAME");
                entity.Property(e => e.CREATE_DATE)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATE_DATE");
                entity.Property(e => e.UPDATE_DATE)
                    .HasColumnType("datetime")
                    .HasColumnName("UPDATE_DATE");
                entity.Property(e => e.USERGROUP)
                    .HasColumnName("USERGROUP");
            });
            
            modelBuilder.Entity<Q_USER_GROUP>(entity =>
            {
                entity.ToTable("Q_USER_GROUP");
                entity.Property(e => e.ID)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");
                entity.Property(e => e.GROUP_NAME)
                    .HasMaxLength(50)
                    .HasColumnName("GROUP_NAME");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}