using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Shared.Data.Models
{
    public partial class dmsdevdbContext : DbContext
    {
        public dmsdevdbContext()
        {
        }

        public dmsdevdbContext(DbContextOptions<dmsdevdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountType> AccountTypes { get; set; } = null!;
        public virtual DbSet<AdminUser> AdminUsers { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<DeletionRequest> DeletionRequests { get; set; } = null!;
        public virtual DbSet<DistributorSapAccount> DistributorSapAccounts { get; set; } = null!;
        public virtual DbSet<Otp> Otps { get; set; } = null!;
        public virtual DbSet<OtpStatus> OtpStatuses { get; set; } = null!;
        public virtual DbSet<Registration> Registrations { get; set; } = null!;
        public virtual DbSet<RegistrationStatus> RegistrationStatuses { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserChangeLog> UserChangeLogs { get; set; } = null!;
        public virtual DbSet<UserLogin> UserLogins { get; set; } = null!;
        public virtual DbSet<UserPasswordHistory> UserPasswordHistories { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<UserStatus> UserStatuses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.ToTable("AccountTypes", "Accounts");

                entity.HasIndex(e => e.Name, "IX_AccountTypes_UniqueKey")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AdminUser>(entity =>
            {
                entity.ToTable("AdminUsers", "Accounts");

                entity.HasIndex(e => new { e.UserId, e.CompanyId }, "IX_AdminUsers_UniqueKey")
                    .IsUnique();

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.AdminUsers)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdminUsers_Companies");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AdminUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdminUsers_Users");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Companies", "Accounts");

                entity.HasIndex(e => e.Code, "IX_Companies")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DeletionRequest>(entity =>
            {
                entity.ToTable("DeletionRequests", "Accounts");

                entity.HasIndex(e => e.UserId, "IX_DeletionRequests_Indexer");

                entity.Property(e => e.Reason)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DeletionRequests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeletionRequests_Users");
            });

            modelBuilder.Entity<DistributorSapAccount>(entity =>
            {
                entity.ToTable("DistributorSapAccounts", "Accounts");

                entity.HasIndex(e => e.UserId, "IX_DistributorSapAccounts_Indexer");

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DistributorName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DistributorSapNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FriendlyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.DistributorSapAccounts)
                    .HasForeignKey(d => d.AccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DistributorSapAccounts_AccountTypes");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DistributorSapAccounts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DistributorSapAccounts_Users");
            });

            modelBuilder.Entity<Otp>(entity =>
            {
                entity.ToTable("Otps", "Accounts");

                entity.HasIndex(e => e.OtpStatusId, "IX_Otps");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateExpiry).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.OtpStatus)
                    .WithMany(p => p.Otps)
                    .HasForeignKey(d => d.OtpStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Otps_OtpStatuses");

                entity.HasOne(d => d.Registration)
                    .WithMany(p => p.Otps)
                    .HasForeignKey(d => d.RegistrationId)
                    .HasConstraintName("FK_Otps_Registrations");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Otps)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Otps_Users");
            });

            modelBuilder.Entity<OtpStatus>(entity =>
            {
                entity.ToTable("OtpStatuses", "Accounts");

                entity.HasIndex(e => e.Name, "IX_OtpStatuses_UniqueKey")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.ToTable("Registrations", "Accounts");

                entity.Property(e => e.ChannelCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DeviceId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistributorNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.RegistrationStatus)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(d => d.RegistrationStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Registrations_RegistrationStatuses");
            });

            modelBuilder.Entity<RegistrationStatus>(entity =>
            {
                entity.ToTable("RegistrationStatuses", "Accounts");

                entity.HasIndex(e => e.Name, "IX_RegistrationStatuses_UniqueKey")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles", "Accounts");

                entity.HasIndex(e => e.Name, "IX_Roles_UniqueKey")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "Accounts");

                entity.HasIndex(e => e.UserStatusId, "IX_Users_Indexer");

                entity.HasIndex(e => new { e.UserName, e.IsDeleted }, "IX_Users_UniqueKey")
                    .IsUnique();

                entity.Property(e => e.DeviceId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProfilePhotoCloudPath)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ProfilePhotoPublicUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ResetToken)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserStatus)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_UserStatuses");
            });

            modelBuilder.Entity<UserChangeLog>(entity =>
            {
                entity.ToTable("UserChangeLogs", "Accounts");

                entity.Property(e => e.ChangeType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NewAccountStatusCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("New_AccountStatusCode");

                entity.Property(e => e.NewDateDeleted).HasColumnName("New_DateDeleted");

                entity.Property(e => e.NewDateModified).HasColumnName("New_DateModified");

                entity.Property(e => e.NewDeletedByUserId).HasColumnName("New_DeletedByUserId");

                entity.Property(e => e.NewDeviceId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("New_DeviceId");

                entity.Property(e => e.NewEmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("New_EmailAddress");

                entity.Property(e => e.NewFirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("New_FirstName");

                entity.Property(e => e.NewIsDeleted).HasColumnName("New_IsDeleted");

                entity.Property(e => e.NewLastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("New_LastName");

                entity.Property(e => e.NewLockedOutDate).HasColumnName("New_LockedOutDate");

                entity.Property(e => e.NewModifiedByUserId).HasColumnName("New_ModifiedByUserId");

                entity.Property(e => e.NewPasswordExpiryDate).HasColumnName("New_PasswordExpiryDate");

                entity.Property(e => e.NewUserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("New_UserName");

                entity.Property(e => e.OldAccountStatusCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Old_AccountStatusCode");

                entity.Property(e => e.OldDateDeleted).HasColumnName("Old_DateDeleted");

                entity.Property(e => e.OldDateModified).HasColumnName("Old_DateModified");

                entity.Property(e => e.OldDeletedByUserId).HasColumnName("Old_DeletedByUserId");

                entity.Property(e => e.OldDeviceId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Old_DeviceId");

                entity.Property(e => e.OldEmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Old_EmailAddress");

                entity.Property(e => e.OldFirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Old_FirstName");

                entity.Property(e => e.OldIsDeleted).HasColumnName("Old_IsDeleted");

                entity.Property(e => e.OldLastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Old_LastName");

                entity.Property(e => e.OldLockedOutDate).HasColumnName("Old_LockedOutDate");

                entity.Property(e => e.OldModifiedByUserId).HasColumnName("Old_ModifiedByUserId");

                entity.Property(e => e.OldPasswordExpiryDate).HasColumnName("Old_PasswordExpiryDate");

                entity.Property(e => e.OldUserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Old_UserName");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserChangeLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accounts.UserChangeLogs_Accounts.Users");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.ToTable("UserLogins", "Accounts");

                entity.HasIndex(e => e.UserId, "IX_UserLogins_Indexer");

                entity.Property(e => e.ChannelCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeviceId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserLogins_Users");
            });

            modelBuilder.Entity<UserPasswordHistory>(entity =>
            {
                entity.ToTable("UserPasswordHistories", "Accounts");

                entity.Property(e => e.Password)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPasswordHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_UserPasswordHistories");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRoles", "Accounts");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_Roles");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_Users");
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.ToTable("UserStatuses", "Accounts");

                entity.HasIndex(e => e.Name, "IX_UserStatuses_UniqueKey")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
