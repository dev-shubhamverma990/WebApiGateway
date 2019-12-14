using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApiGateway.Entities
{
    public partial class PrincetonhiveContext : DbContext
    {
        public PrincetonhiveContext()
        {
        }

        public PrincetonhiveContext(DbContextOptions<PrincetonhiveContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCityMaster> TblCityMaster { get; set; }
        public virtual DbSet<Tblclassmap> Tblclassmap { get; set; }
        public virtual DbSet<TblClassMaster> TblClassMaster { get; set; }
        public virtual DbSet<TblCountryMaster> TblCountryMaster { get; set; }
        public virtual DbSet<TblEnquiry> TblEnquiry { get; set; }
        public virtual DbSet<TblExceptionLog> TblExceptionLog { get; set; }
        public virtual DbSet<TblRegistration> TblRegistration { get; set; }
        public virtual DbSet<TblRoleMaster> TblRoleMaster { get; set; }
        public virtual DbSet<TblSchool> TblSchool { get; set; }
        public virtual DbSet<TblSectionMaster> TblSectionMaster { get; set; }
        public virtual DbSet<TblSessionMaster> TblSessionMaster { get; set; }
        public virtual DbSet<TblStateMaster> TblStateMaster { get; set; }
        public virtual DbSet<TblStudentRegistration> TblStudentRegistration { get; set; }
        public virtual DbSet<TblUserAuth> TblUserAuth { get; set; }
        public virtual DbSet<TblUserLoginHst> TblUserLoginHst { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Server=103.118.157.29;Port=5432;Database=Princetonhive;User Id=postgres;Password=princetonhive@123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCityMaster>(entity =>
            {
                entity.HasKey(e => e.Cityid);

                entity.ToTable("tblCityMaster");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasColumnType("character varying(1000)");
            });

            modelBuilder.Entity<Tblclassmap>(entity =>
            {
                entity.HasKey(e => e.ClassmapId);

                entity.ToTable("tblclassmap");

                entity.Property(e => e.ClassIdBelouga).HasColumnType("character varying");

                entity.Property(e => e.ClassName).HasColumnType("character varying");

                entity.Property(e => e.TeacheId)
                    .HasColumnName("TeacheID")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<TblClassMaster>(entity =>
            {
                entity.HasKey(e => e.Classid);

                entity.ToTable("tblClassMaster");

                entity.Property(e => e.ClassIdBelouga).HasColumnType("character varying(100)");

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasColumnType("character varying(1000)");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("character varying(5000)");

                entity.Property(e => e.SchoolId).HasColumnType("character varying(100)");

                entity.Property(e => e.Status).HasColumnType("character varying(1000)");

                entity.Property(e => e.TeacherId).HasColumnType("character varying(100)");

                entity.Property(e => e.UpdatedDate).HasColumnType("date");
            });

            modelBuilder.Entity<TblCountryMaster>(entity =>
            {
                entity.HasKey(e => e.Countryid);

                entity.ToTable("tblCountryMaster");

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasColumnType("character varying(1000)");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasColumnType("character varying(1000)");

                entity.Property(e => e.Phonecode)
                    .IsRequired()
                    .HasColumnType("character varying(1000)");
            });

            modelBuilder.Entity<TblEnquiry>(entity =>
            {
                entity.HasKey(e => e.EnquiryId);

                entity.ToTable("tblEnquiry");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("character varying(50000)");

                entity.Property(e => e.Designation)
                    .IsRequired()
                    .HasColumnType("character varying(100)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("character varying(100)");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnType("character varying(1000)");

                entity.Property(e => e.MobileNo)
                    .IsRequired()
                    .HasColumnType("character varying(50)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying(500)");

                entity.Property(e => e.SchoolName)
                    .IsRequired()
                    .HasColumnType("character varying(1000)");
            });

            modelBuilder.Entity<TblExceptionLog>(entity =>
            {
                entity.ToTable("tblException_Log");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ErrorCode)
                    .IsRequired()
                    .HasColumnType("character varying(100)");

                entity.Property(e => e.ErrorDescription)
                    .IsRequired()
                    .HasColumnType("character varying(50000)");

                entity.Property(e => e.LogDateTime).HasColumnType("date");
            });

            modelBuilder.Entity<TblRegistration>(entity =>
            {
                entity.HasKey(e => e.RegistrationId);

                entity.ToTable("tblRegistration");

                entity.Property(e => e.Address).HasColumnType("character varying(5000)");

                entity.Property(e => e.City).HasColumnType("character varying(50)");

                entity.Property(e => e.Class).HasColumnType("character varying(100)");

                entity.Property(e => e.ClassIdBelouga).HasColumnType("character varying(100)");

                entity.Property(e => e.DisplayName).HasColumnType("character varying(1000)");

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.Email).HasColumnType("character varying(500)");

                entity.Property(e => e.FirstName).HasColumnType("character varying(500)");

                entity.Property(e => e.Gender).HasColumnType("character varying(500)");

                entity.Property(e => e.LastName).HasColumnType("character varying(500)");

                entity.Property(e => e.Mobileno).HasColumnType("character varying(100)");

                entity.Property(e => e.Photo).HasColumnType("character varying(500)");

                entity.Property(e => e.PostalCode).HasColumnType("character varying(100)");

                entity.Property(e => e.SchoolContactPerson).HasColumnType("character varying(50)");

                entity.Property(e => e.SchoolEmail).HasColumnType("character varying(50)");

                entity.Property(e => e.SchoolIdBelouga).HasColumnType("character varying(100)");

                entity.Property(e => e.SchoolLogo).HasColumnType("character varying(100)");

                entity.Property(e => e.SchoolName).HasColumnType("character varying(100)");

                entity.Property(e => e.SchoolPhone).HasColumnType("character varying(20)");

                entity.Property(e => e.Section).HasColumnType("character varying(100)");

                entity.Property(e => e.Session).HasColumnType("character varying(100)");

                entity.Property(e => e.Status).HasColumnType("character varying(500)");

                entity.Property(e => e.TeacherIdBelouga).HasColumnType("character varying(100)");

                entity.Property(e => e.Type).HasColumnType("character varying(50)");

                entity.Property(e => e.Username).HasColumnType("character varying(500)");

                entity.Property(e => e.VendorStatus).HasColumnType("character varying");
            });

            modelBuilder.Entity<TblRoleMaster>(entity =>
            {
                entity.HasKey(e => e.Roleid);

                entity.ToTable("tblRoleMaster");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnType("character varying(100)");

                entity.Property(e => e.RoleType)
                    .IsRequired()
                    .HasColumnType("character varying(2000)");
            });

            modelBuilder.Entity<TblSchool>(entity =>
            {
                entity.HasKey(e => e.Schoolid);

                entity.ToTable("tblSchool");

                entity.Property(e => e.ContactEmail).HasColumnType("character varying(50)");

                entity.Property(e => e.ContactPersonMobile).HasColumnType("character varying(50)");

                entity.Property(e => e.ContactPersonName).HasColumnType("character varying(100)");

                entity.Property(e => e.SchoolAddress).HasColumnType("character varying(5000)");

                entity.Property(e => e.SchoolCode).HasColumnType("character varying(500)");

                entity.Property(e => e.SchoolDescription).HasColumnType("character varying(1000)");

                entity.Property(e => e.SchoolEmail).HasColumnType("character varying(50)");

                entity.Property(e => e.SchoolIdBelouga).HasColumnType("character varying(100)");

                entity.Property(e => e.SchoolLogo).HasColumnType("character varying(100)");

                entity.Property(e => e.SchoolName).HasColumnType("character varying(1000)");

                entity.Property(e => e.SchoolPhone).HasColumnType("character varying(50)");

                entity.Property(e => e.SchoolPostalCode).HasColumnType("character varying(50)");

                entity.Property(e => e.SchoolWebsite).HasColumnType("character varying(50)");
            });

            modelBuilder.Entity<TblSectionMaster>(entity =>
            {
                entity.HasKey(e => e.Sectionid);

                entity.ToTable("tblSectionMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.SectionName)
                    .IsRequired()
                    .HasColumnType("character varying(100)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("character varying(100)");

                entity.Property(e => e.UpdatedDate).HasColumnType("date");
            });

            modelBuilder.Entity<TblSessionMaster>(entity =>
            {
                entity.HasKey(e => e.Sessionid);

                entity.ToTable("tblSessionMaster");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.SessionName)
                    .IsRequired()
                    .HasColumnType("character varying(100)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("character varying(100)");

                entity.Property(e => e.UpdatedDate).HasColumnType("date");
            });

            modelBuilder.Entity<TblStateMaster>(entity =>
            {
                entity.HasKey(e => e.Stateid);

                entity.ToTable("tblStateMaster");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasColumnType("character varying(1000)");
            });

            modelBuilder.Entity<TblStudentRegistration>(entity =>
            {
                entity.HasKey(e => e.StudentRegistrationId);

                entity.ToTable("tblStudentRegistration");

                entity.Property(e => e.Address).HasColumnType("character varying(5000)");

                entity.Property(e => e.ClassIdBelouga).HasColumnType("character varying(100)");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasColumnType("character varying(1000)");

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.Email).HasColumnType("character varying(100)");

                entity.Property(e => e.FatherEducation).HasColumnType("character varying(100)");

                entity.Property(e => e.FatherEmail).HasColumnType("character varying(100)");

                entity.Property(e => e.FatherMobile).HasColumnType("character varying(100)");

                entity.Property(e => e.FatherName).HasColumnType("character varying(500)");

                entity.Property(e => e.FatherOccupation).HasColumnType("character varying(100)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("character varying(500)");

                entity.Property(e => e.Gender).HasColumnType("character varying(100)");

                entity.Property(e => e.LastName).HasColumnType("character varying(500)");

                entity.Property(e => e.Mobileno).HasColumnType("character varying(100)");

                entity.Property(e => e.MotherEducation).HasColumnType("character varying(100)");

                entity.Property(e => e.MotherEmail).HasColumnType("character varying(100)");

                entity.Property(e => e.MotherMobile).HasColumnType("character varying(100)");

                entity.Property(e => e.MotherName).HasColumnType("character varying(500)");

                entity.Property(e => e.MotherOccupation).HasColumnType("character varying(100)");

                entity.Property(e => e.Photo).HasColumnType("character varying(500)");

                entity.Property(e => e.PostalCode).HasColumnType("character varying(100)");

                entity.Property(e => e.SchoolCode).HasColumnType("character varying(100)");

                entity.Property(e => e.Session).HasColumnType("character varying(100)");

                entity.Property(e => e.Status).HasColumnType("character varying(100)");

                entity.Property(e => e.StudentIdBelouga).HasColumnType("character varying(100)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("character varying(500)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("character varying(100)");
            });

            modelBuilder.Entity<TblUserAuth>(entity =>
            {
                entity.HasKey(e => e.UserAuthId);

                entity.ToTable("tblUserAuth");

                entity.Property(e => e.ModifyDate).HasColumnType("date");

                entity.Property(e => e.UserAuthorities)
                    .IsRequired()
                    .HasColumnType("character varying(1000)");

                entity.Property(e => e.UserRole)
                    .IsRequired()
                    .HasColumnType("character varying(500)");

                entity.Property(e => e.UserStatus)
                    .IsRequired()
                    .HasColumnType("character varying(500)");
            });

            modelBuilder.Entity<TblUserLoginHst>(entity =>
            {
                entity.HasKey(e => e.UserLoginHstid);

                entity.ToTable("tblUserLogin_HST");

                entity.Property(e => e.UserLoginHstid).HasColumnName("UserLoginHSTId");

                entity.Property(e => e.LoginDateTime).HasColumnType("date");

                entity.Property(e => e.LogoutDateTime).HasColumnType("date");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.Property(e => e.Captcha).HasColumnType("character varying(500)");

                entity.Property(e => e.City).HasColumnType("character varying(500)");

                entity.Property(e => e.Email).HasColumnType("character varying(500)");

                entity.Property(e => e.Mobile).HasColumnType("character varying(500)");

                entity.Property(e => e.Name).HasColumnType("character varying(500)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("character varying(500)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnType("character varying(500)");

                entity.Property(e => e.UserRole).HasColumnType("character varying(500)");
            });
        }
    }
}
