using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StockShow.Models
{
    public partial class Stock_dbContext : DbContext
    {
        public Stock_dbContext()
        {
        }

        public Stock_dbContext(DbContextOptions<Stock_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<StockDataTable> StockDataTables { get; set; } = null!;
        public virtual DbSet<StockDatum> StockData { get; set; } = null!;
        public virtual DbSet<StockNo> StockNos { get; set; } = null!;
        public virtual DbSet<StockNoTable> StockNoTables { get; set; } = null!;
        public virtual DbSet<StockType> StockTypes { get; set; } = null!;
        public virtual DbSet<StockTypeTable> StockTypeTables { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                //optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=Stock_db;Trusted_Connection=True;MultipleActiveResultSets=true;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockDataTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("StockDataTable");

                entity.Property(e => e.Data).HasColumnType("date");

                entity.Property(e => e.StockNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.StockNoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.StockNo)
                    .HasConstraintName("FK__StockData__Stock__40F9A68C");
            });

            modelBuilder.Entity<StockDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Stock_Data");

                entity.Property(e => e.ClosingPrice).HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.DealNumber).HasMaxLength(50);

                entity.Property(e => e.HighPriceInDay).HasMaxLength(50);

                entity.Property(e => e.LowPriceInDay).HasMaxLength(50);

                entity.Property(e => e.OpeningPrice).HasMaxLength(50);

                entity.Property(e => e.PriceDiff).HasMaxLength(50);

                entity.Property(e => e.StockNo)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<StockNo>(entity =>
            {
                entity.HasKey(e => e.StockNumber);

                entity.ToTable("Stock_No");

                entity.Property(e => e.StockNumber)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Remarks)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Remarks2)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Remarks3)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.StockName)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.StockNumberName).HasMaxLength(50);

                entity.Property(e => e.StockType)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<StockNoTable>(entity =>
            {
                entity.HasKey(e => e.StockNo)
                    .HasName("PK__StockNoT__2C8517D1324D8D26");

                entity.ToTable("StockNoTable");

                entity.HasIndex(e => e.StockNoName, "UQ__StockNoT__F75D8B0331432942")
                    .IsUnique();

                entity.Property(e => e.StockNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Note1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Note2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Note3)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Note4)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Note5)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Note6)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Note7)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StockName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StockNoName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StockType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                //entity.HasOne(d => d.StockTypeIndexNavigation)
                //    .WithMany(p => p.StockNoTables)
                //    .HasForeignKey(d => d.StockTypeIndex)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK__StockNoTa__Stock__3F115E1A");
            });

            modelBuilder.Entity<StockType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Stock_Type");

                entity.Property(e => e.StockType1)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("StockType");
            });

            modelBuilder.Entity<StockTypeTable>(entity =>
            {
                entity.HasKey(e => e.StockTypeIndex)
                    .HasName("PK__StockTyp__15C8561660240581");

                entity.ToTable("StockTypeTable");

                entity.Property(e => e.StockType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
