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
        private IDbConnection _dbConnection { get; }

        public EntitiesContext(DbContextOptions<EntitiesContext> options,
            IDateTime datetime,
            IConfiguration configuration) : base(options)
        {
            _datetime = datetime;
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
        public virtual DbSet<Q_QUESTION> QUESTION { get; set; }
        public virtual DbSet<Q_CHOICE> CHOICE { get; set; }
        public virtual DbSet<Q_SAVE> SAVE { get; set; }

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
            
            modelBuilder.Entity<Q_QUESTION>(entity =>
            {
                entity.ToTable("Q_QUESTION");
                entity.Property(e => e.ID)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");
                entity.Property(e => e.QUESTION)
                    .HasColumnName("QUESTION");
                entity.Property(e => e.USERGROUP)
                    .HasColumnName("USERGROUP");
            });
            
            modelBuilder.Entity<Q_CHOICE>(entity =>
            {
                entity.ToTable("Q_CHOICE");
                entity.Property(e => e.ID)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");
                entity.Property(e => e.CHOICE)
                    .HasColumnName("CHOICE");
                entity.Property(e => e.QUESTION_ID)
                    .HasColumnName("QUESTION_ID");
                entity.Property(e => e.SCORE)
                    .HasColumnName("SCORE");
            });
            
            modelBuilder.Entity<Q_SAVE>(entity =>
            {
                entity.ToTable("Q_SAVE");
                entity.Property(e => e.ID)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");
                entity.Property(e => e.USER_ID)
                    .HasColumnName("USER_ID");
                entity.Property(e => e.QUESTION_ID)
                    .HasColumnName("QUESTION_ID");
                entity.Property(e => e.CHOICE_ID)
                    .HasColumnName("CHOICE_ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}