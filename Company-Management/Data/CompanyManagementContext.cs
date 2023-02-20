using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Company_Management.Data
{
    public partial class CompanyManagementContext : DbContext
    {
        public CompanyManagementContext()
        {
        }

        public CompanyManagementContext(DbContextOptions<CompanyManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CompanyTable> CompanyTables { get; set; }
        public virtual DbSet<DepartmentTable> DepartmentTables { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeePersonalDetail> EmployeePersonalDetails { get; set; }
        public virtual DbSet<MemberTable> MemberTables { get; set; }
        public virtual DbSet<Otp> Otps { get; set; }
        public virtual DbSet<Qualification> Qualifications { get; set; }
        public virtual DbSet<ReportingManager> ReportingManagers { get; set; }
        public virtual DbSet<UserTable> UserTables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=CompanyManagement;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyTable>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("PK__CompanyT__2D971CACD36DC7D3");

                entity.ToTable("CompanyTable");

                entity.Property(e => e.CompanyAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Company_Address");

                entity.Property(e => e.CompanyCity)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Company_City");

                entity.Property(e => e.CompanyCountry)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Company_Country");

                entity.Property(e => e.CompanyMobieNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Company_MobieNo");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Company_Name");

                entity.Property(e => e.CompanyState)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Company_State");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Dstatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.CompanyTables)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK__CompanyTable__Id__31EC6D26");
            });

            modelBuilder.Entity<DepartmentTable>(entity =>
            {
                entity.HasKey(e => e.DepartmentId)
                    .HasName("PK__Departme__B2079BED33FB9CBF");

                entity.ToTable("DepartmentTable");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Dstatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.DepartmentTables)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("fk_Mem");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.DepartmentTables)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK__Departmen__Manag__412EB0B6");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__Employee__781134A199BF6F24");

                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("Employee_Id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateofJoining).HasColumnType("datetime");

                entity.Property(e => e.DepartmentId).HasColumnName("Department_Id");

                entity.Property(e => e.Dstatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeFullName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Employee_FullName");

                entity.Property(e => e.EmployementType)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.WorkingDays).HasColumnName("Working_Days");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_DeptId");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK__Employee__Id__440B1D61");
            });

            modelBuilder.Entity<EmployeePersonalDetail>(entity =>
            {
                entity.Property(e => e.AlterNateEmail)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AlterNatePhoneNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CurrentAddressLine)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentCity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentHouseNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentLocality)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentPinCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentState)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dstatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Mid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("MId");

                entity.Property(e => e.PermanentAddressLine)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.PermanentCity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PermanentHouseNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PermanentLocality)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PermanentPinCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PermanentState)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.EmployeePersonalDetails)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK__EmployeeP__EmpId__5070F446");

                entity.HasOne(d => d.MidNavigation)
                    .WithMany(p => p.EmployeePersonalDetails)
                    .HasForeignKey(d => d.Mid)
                    .HasConstraintName("FK__EmployeePer__MId__5165187F");
            });

            modelBuilder.Entity<MemberTable>(entity =>
            {
                entity.ToTable("MemberTable");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Dstatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Otp>(entity =>
            {
                entity.ToTable("OTP");

                entity.Property(e => e.Otpid).HasColumnName("OTPid");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Otp1)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("OTP");

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Qualification>(entity =>
            {
                entity.ToTable("Qualification");

                entity.Property(e => e.QualificationId).HasColumnName("QualificationID");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Dstatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.InstituteName)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.MemberId)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.QualificationEndYear)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.QualificationName)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.QualificationStartYear)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Score)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Qualifications)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK__Qualifica__EmpID__4CA06362");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Qualifications)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK__Qualifica__Membe__4D94879B");
            });

            modelBuilder.Entity<ReportingManager>(entity =>
            {
                entity.HasKey(e => e.ManagerId)
                    .HasName("PK__Reportin__3BA2AAE152C03A20");

                entity.ToTable("ReportingManager");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Dstatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Specialization)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.ReportingManagers)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK__ReportingMan__Id__3D5E1FD2");
            });

            modelBuilder.Entity<UserTable>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserTabl__CB9A1CFFD1E7AF39");

                entity.ToTable("UserTable");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Dstatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.UserTables)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK__UserTable__Id__2F10007B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
