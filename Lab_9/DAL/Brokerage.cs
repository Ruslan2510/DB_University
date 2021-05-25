using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public partial class Brokerage : DbContext
    {
        public Brokerage()
            : base("name=Brokerage")
        {
        }

        public virtual DbSet<BIDDING> BIDDINGs { get; set; }
        public virtual DbSet<BROKER> BROKERs { get; set; }
        public virtual DbSet<CLIENT> CLIENTs { get; set; }
        public virtual DbSet<DEAL> DEALs { get; set; }
        public virtual DbSet<ORDERR> ORDERRs { get; set; }
        public virtual DbSet<PROFIT> PROFITs { get; set; }
        public virtual DbSet<SECURITy> SECURITIES { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BROKER>()
                .Property(e => e.broker_surname)
                .IsUnicode(false);

            modelBuilder.Entity<BROKER>()
                .Property(e => e.broker_name)
                .IsUnicode(false);

            modelBuilder.Entity<BROKER>()
                .Property(e => e.broker_patronymic)
                .IsUnicode(false);

            modelBuilder.Entity<BROKER>()
                .Property(e => e.broker_tel)
                .IsUnicode(false);

            modelBuilder.Entity<BROKER>()
                .Property(e => e.broker_email)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENT>()
                .Property(e => e.client_surname)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENT>()
                .Property(e => e.client_name)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENT>()
                .Property(e => e.client_patronymic)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENT>()
                .Property(e => e.client_tel)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENT>()
                .Property(e => e.client_email)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENT>()
                .Property(e => e.client_fortune)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DEAL>()
                .Property(e => e.deal_customer)
                .IsUnicode(false);

            modelBuilder.Entity<DEAL>()
                .Property(e => e.deal_type)
                .IsUnicode(false);

            modelBuilder.Entity<ORDERR>()
                .Property(e => e.order_comment)
                .IsUnicode(false);

            modelBuilder.Entity<ORDERR>()
                .Property(e => e.order_status)
                .IsUnicode(false);

            modelBuilder.Entity<PROFIT>()
                .Property(e => e.profit_amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SECURITy>()
                .Property(e => e.securities_name)
                .IsUnicode(false);

            modelBuilder.Entity<SECURITy>()
                .Property(e => e.securities_purchase_rate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SECURITy>()
                .Property(e => e.securities_sales_rate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SECURITy>()
                .Property(e => e.securities_owner)
                .IsUnicode(false);
        }
    }
}
