﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CMS.Data.Models
{
    public partial class DB_CMSContext : DbContext
    {
        public DB_CMSContext()
        {
        }

        public DB_CMSContext(DbContextOptions<DB_CMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCmspageContentMaster> TblCmspageContentMasters { get; set; }
        public virtual DbSet<TblFileDataMaster> TblFileDataMasters { get; set; }
        public virtual DbSet<TblGecategoryMater> TblGecategoryMaters { get; set; }
        public virtual DbSet<TblGeneralEntry> TblGeneralEntries { get; set; }
        public virtual DbSet<TblLookupMaster> TblLookupMasters { get; set; }
        public virtual DbSet<TblLookupTypeMaster> TblLookupTypeMasters { get; set; }
        public virtual DbSet<TblProductImage> TblProductImages { get; set; }
        public virtual DbSet<TblProductMaster> TblProductMasters { get; set; }
        public virtual DbSet<TblProductReview> TblProductReviews { get; set; }
        public virtual DbSet<TblProductStock> TblProductStocks { get; set; }
        public virtual DbSet<TblRoleType> TblRoleTypes { get; set; }
        public virtual DbSet<TblSubLookupMaster> TblSubLookupMasters { get; set; }
        public virtual DbSet<TblUserAddressMaster> TblUserAddressMasters { get; set; }
        public virtual DbSet<TblUserCartList> TblUserCartLists { get; set; }
        public virtual DbSet<TblUserMaster> TblUserMasters { get; set; }
        public virtual DbSet<TblUserMasterLog> TblUserMasterLogs { get; set; }
        public virtual DbSet<TblUserWishList> TblUserWishLists { get; set; }
        public virtual DbSet<VwProductMaster> VwProductMasters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=Sandy-PC;Database=DB_CMS;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<TblCmspageContentMaster>(entity =>
            {
                entity.ToTable("tblCMSPageContentMaster");

                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Heading).HasColumnType("ntext");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblCmspageContentMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("tblCMSPageContentMaster_CreatedBy");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblCmspageContentMasterModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("tblCMSPageContentMaster_ModifiedBy");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.TblCmspageContentMasters)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblCMSPageContentMaster_PageId");
            });

            modelBuilder.Entity<TblFileDataMaster>(entity =>
            {
                entity.ToTable("tblFileDataMaster");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblFileDataMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_tblFileDataMaster_CreatedBy");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblFileDataMasterModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblFileDataMaster_ModifiedBy");
            });

            modelBuilder.Entity<TblGecategoryMater>(entity =>
            {
                entity.ToTable("tblGECategoryMater");

                entity.HasIndex(e => e.EnumValue, "UQ__tblGECat__C06D7C17040A55B7")
                    .IsUnique();

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EnumValue)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ImagePath).HasMaxLength(4000);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsShowUrl).HasColumnName("IsShowURL");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblGecategoryMaterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblGECategoryMater_CreatedBy");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblGecategoryMaterModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblGECategoryMater_ModifiedBy");
            });

            modelBuilder.Entity<TblGeneralEntry>(entity =>
            {
                entity.ToTable("tblGeneralEntry");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.ImagePath).HasMaxLength(1000);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Keyword).HasMaxLength(200);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title).HasMaxLength(4000);

                entity.Property(e => e.Url)
                    .HasMaxLength(4000)
                    .HasColumnName("URL");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TblGeneralEntries)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblGeneralEntry_CategoryId");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblGeneralEntryCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_tblGeneralEntry_CreatedBy");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblGeneralEntryModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblGeneralEntry_ModifiedBy");
            });

            modelBuilder.Entity<TblLookupMaster>(entity =>
            {
                entity.ToTable("tblLookupMaster");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImagePath).HasMaxLength(1000);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LookUpType).HasColumnName("LookUp_Type");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Value).HasMaxLength(50);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblLookupMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLookupMaster_CreatedBy");

                entity.HasOne(d => d.LookUpTypeNavigation)
                    .WithMany(p => p.TblLookupMasters)
                    .HasForeignKey(d => d.LookUpType)
                    .HasConstraintName("FK_tblLookupMaster_LookUp_Type");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblLookupMasterModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLookupMaster_ModifiedBy");
            });

            modelBuilder.Entity<TblLookupTypeMaster>(entity =>
            {
                entity.ToTable("tblLookupTypeMaster");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EnumValue)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblLookupTypeMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLookupTypeMaster_CreatedBy");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblLookupTypeMasterModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLookupTypeMaster_ModifiedBy");
            });

            modelBuilder.Entity<TblProductImage>(entity =>
            {
                entity.ToTable("tblProductImage");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProductId).HasColumnName("Product_Id");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblProductImageCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_tblProductImage_CreatedBy");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblProductImageModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblProductImage_ModifiedBy");
            });

            modelBuilder.Entity<TblProductMaster>(entity =>
            {
                entity.ToTable("tblProductMaster");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Desc).HasColumnType("ntext");

                entity.Property(e => e.ImagePath).HasMaxLength(1000);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Keyword).HasMaxLength(4000);

                entity.Property(e => e.MetaDesc).HasMaxLength(4000);

                entity.Property(e => e.MetaTitle).HasMaxLength(1000);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.PatternId).HasColumnName("PatternID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Summary).HasColumnType("ntext");

                entity.Property(e => e.UniqueId)
                    .HasMaxLength(400)
                    .HasColumnName("UniqueID");

                entity.HasOne(d => d.CaptionTag)
                    .WithMany(p => p.TblProductMasterCaptionTags)
                    .HasForeignKey(d => d.CaptionTagId)
                    .HasConstraintName("FK_tblProductMaster_CaptionTagId");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TblProductMasterCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblProductMaster_CategoryId");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.TblProductMasterColors)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("FK_tblProductMaster_ColorId");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblProductMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblProductMaster_CreatedBy");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.TblProductMasterDiscounts)
                    .HasForeignKey(d => d.DiscountId)
                    .HasConstraintName("FK_tblProductMaster_DiscountId");

                entity.HasOne(d => d.Fabric)
                    .WithMany(p => p.TblProductMasterFabrics)
                    .HasForeignKey(d => d.FabricId)
                    .HasConstraintName("FK_tblProductMaster_FabricId");

                entity.HasOne(d => d.Length)
                    .WithMany(p => p.TblProductMasterLengths)
                    .HasForeignKey(d => d.LengthId)
                    .HasConstraintName("FK_tblProductMaster_LengthId");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblProductMasterModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblProductMaster_ModifiedBy");

                entity.HasOne(d => d.Occasion)
                    .WithMany(p => p.TblProductMasterOccasions)
                    .HasForeignKey(d => d.OccasionId)
                    .HasConstraintName("FK_tblProductMaster_OccasionId");

                entity.HasOne(d => d.Pattern)
                    .WithMany(p => p.TblProductMasterPatterns)
                    .HasForeignKey(d => d.PatternId)
                    .HasConstraintName("FK_tblProductMaster_PatternId");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.TblProductMasters)
                    .HasForeignKey(d => d.SubCategoryId)
                    .HasConstraintName("FK_tblProductMaster_SubCategoryId");

                entity.HasOne(d => d.ViewSection)
                    .WithMany(p => p.TblProductMasterViewSections)
                    .HasForeignKey(d => d.ViewSectionId)
                    .HasConstraintName("FK_tblProductMaster_ViewSectionId");
            });

            modelBuilder.Entity<TblProductReview>(entity =>
            {
                entity.ToTable("tblProductReview");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ShortDescription).HasMaxLength(2000);

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblProductReviews)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("tblProductReview_CreatedBy");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblProductReviews)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblProductReview_ProductId");
            });

            modelBuilder.Entity<TblProductStock>(entity =>
            {
                entity.ToTable("tblProductStocks");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblProductStocks)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblProductStocks_ProductId");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.TblProductStocks)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblProductStocks_SizeId");
            });

            modelBuilder.Entity<TblRoleType>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__tblRoleT__8AFACE1A04105D16");

                entity.ToTable("tblRoleType");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblRoleTypeCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_tblRoleType_CreatedBy");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblRoleTypeModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("FK_tblRoleType_ModifiedBy");
            });

            modelBuilder.Entity<TblSubLookupMaster>(entity =>
            {
                entity.ToTable("tblSubLookupMaster");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImagePath).HasMaxLength(1000);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblSubLookupMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("tblSubLookupMaster_CreatedBy");

                entity.HasOne(d => d.LookUp)
                    .WithMany(p => p.TblSubLookupMasters)
                    .HasForeignKey(d => d.LookUpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblSubLookupMaster_LookUpId");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblSubLookupMasterModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .HasConstraintName("tblSubLookupMaster_ModifiedBy");
            });

            modelBuilder.Entity<TblUserAddressMaster>(entity =>
            {
                entity.ToTable("tblUserAddressMaster");

                entity.Property(e => e.BuildingNumber).HasMaxLength(500);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Landmark).HasMaxLength(2000);

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PinCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.AddressTypeNavigation)
                    .WithMany(p => p.TblUserAddressMasterAddressTypeNavigations)
                    .HasForeignKey(d => d.AddressType)
                    .HasConstraintName("FK_tblUserAddressMaster_AddressType");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblUserAddressMasterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserAddressMaster_CreatedBy");

                entity.HasOne(d => d.ModifiedByNavigation)
                    .WithMany(p => p.TblUserAddressMasterModifiedByNavigations)
                    .HasForeignKey(d => d.ModifiedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserAddressMaster_ModifiedBy");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.TblUserAddressMasterStates)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_tblUserAddressMaster_StateId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblUserAddressMasterUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserAddressMaster_UserId");
            });

            modelBuilder.Entity<TblUserCartList>(entity =>
            {
                entity.ToTable("tblUserCartList");

                entity.Property(e => e.AddedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblUserCartLists)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserCartList_ProductId");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.TblUserCartLists)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserCartList_SizeId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblUserCartLists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserCartList_UserId");
            });

            modelBuilder.Entity<TblUserMaster>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__tmp_ms_x__1788CC4C69C2F205");

                entity.ToTable("tblUserMaster");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.FirstName).HasMaxLength(256);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName).HasMaxLength(256);

                entity.Property(e => e.Mobile).HasMaxLength(15);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.ProfilePhoto).HasMaxLength(1000);

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.TblUserMasters)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("FK__tblUserMa__Gende__1C873BEC");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblUserMasters)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblUserMa__RoleI__5BAD9CC8");
            });

            modelBuilder.Entity<TblUserMasterLog>(entity =>
            {
                entity.ToTable("tblUserMasterLog");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SessionEndTime).HasColumnType("datetime");

                entity.Property(e => e.SessionStartTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblUserMasterLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserMasterLog_UserId");
            });

            modelBuilder.Entity<TblUserWishList>(entity =>
            {
                entity.ToTable("tblUserWishList");

                entity.Property(e => e.AddedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblUserWishLists)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserWishList_ProductId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblUserWishLists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUserWishList_UserId");
            });

            modelBuilder.Entity<VwProductMaster>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_ProductMaster");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Desc).HasColumnType("ntext");

                entity.Property(e => e.ImagePath).HasMaxLength(1000);

                entity.Property(e => e.Keyword).HasMaxLength(4000);

                entity.Property(e => e.MetaDesc).HasMaxLength(4000);

                entity.Property(e => e.MetaTitle).HasMaxLength(1000);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.PatternId).HasColumnName("PatternID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SellingPrice).HasColumnType("decimal(38, 0)");

                entity.Property(e => e.Summary).HasColumnType("ntext");

                entity.Property(e => e.UniqueId)
                    .HasMaxLength(400)
                    .HasColumnName("UniqueID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
