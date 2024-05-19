using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

using Microsoft.VisualBasic;
using Quiz.Contracts.Entities;
using Quiz.Contracts.Interfaces;

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
            _dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string companyCode = _identity.GetCompanyCode();
            string conString = _dbConnection.ConnectionString.Replace("#DbName", companyCode);

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
                var identity = _identity.GetUserId() ?? "System";
                if (item.State == EntityState.Added)
                {

                    if (item.Entity.CreateDate == new DateTime())
                    {
                        item.Entity.CreateDate = _datetime.Now;
                    }

                    if (item.Entity.CreateBy == null)
                    {
                        item.Entity.CreateBy = identity;
                        item.Entity.CreateDate = _datetime.Now;
                    }

                    if (identity == null)
                    {
                        item.Entity.UpdateBy = item.Entity.CreateBy;
                    }
                    else
                    {
                        item.Entity.UpdateBy = identity;
                    }

                    item.Entity.UpdateDate = _datetime.Now;
                }

                if (item.State == EntityState.Modified)
                {
                    item.Entity.UpdateDate = _datetime.Now;

                    if (identity != null)
                        item.Entity.UpdateBy = identity;
                }

            }

            return base.SaveChangesAsync(cancellationToken);
        }

       
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}