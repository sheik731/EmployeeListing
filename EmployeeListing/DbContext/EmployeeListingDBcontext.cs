using Microsoft.EntityFrameworkCore;
using EmployeeListing.Models;

public class EmployeeListingDBcontext : DbContext
{
    public EmployeeListingDBcontext(DbContextOptions<EmployeeListingDBcontext> options) : base(options)
    {

    }
    public DbSet<Class> ? Classes {get; set;}
    public DbSet<Employee> ? Employees {get; set;}
    public DbSet<Gender> ? Genders {get; set;}
    public DbSet<Subject> ? Subjects{get; set;}
    public DbSet<Role>? Roles{get; set;}

     protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                        .HasData(
                         new Class { ClassId = 1, className = 6, isActive = true },
                         new Class { ClassId = 2, className = 7, isActive = true },
                         new Class { ClassId = 3, className = 8, isActive = true }
                        );

            modelBuilder.Entity<Gender>()
                        .HasData(
                         new Gender { GenderId = 1, GenderName = "Male", isActive = true },
                         new Gender { GenderId = 2, GenderName = "Female", isActive = true },
                         new Gender { GenderId = 3, GenderName = "Others", isActive = true}
                        );

            modelBuilder.Entity<Subject>()
                        .HasData(
                         new Subject { SubjectId = 1, SubjectName = "Tamil", isActive = true },
                         new Subject { SubjectId = 2, SubjectName = "English", isActive = true },
                         new Subject { SubjectId = 3, SubjectName = "Maths", isActive = true}
                        );
            modelBuilder.Entity<Employee>()
                      .HasData(
                       new Employee { EmployeeId = 1, EmployeeName = "Prithvi", RegisterNumber = "PSNA0001", MailId = "prithvi.palani@aspiresys.com", Password = "Pass@12345", EmployeeClass = 1, Qualification = 1, GenderId = 1 , SubjectId = 1, DateOfBirth=new DateTime(1990,10,7), RoleId=1},
                       new Employee { EmployeeId = 2, EmployeeName = "Vinodhini", RegisterNumber = "PSNA0002", MailId = "vinoth.jayakumar@aspiresys.com", Password = "Pass@12345", EmployeeClass = 2, Qualification = 1, GenderId = 2 , SubjectId = 2, DateOfBirth=new DateTime(1993,10,17),RoleId=2},
                       new Employee { EmployeeId = 3, EmployeeName = "Sheik", RegisterNumber = "PSNA0003", MailId = "sheik.farid@aspiresys.com", Password = "Pass@12345", EmployeeClass = 1, Qualification = 2, GenderId = 1 , SubjectId = 3, DateOfBirth=new DateTime(1992,10,1),RoleId=3}
                      );

            modelBuilder.Entity<Role>()
                        .HasData(
                         new Role { RoleId = 1, RoleName = "Admin", isActive = true },
                         new Role { RoleId = 2, RoleName = "HM", isActive = true },
                         new Role { RoleId = 3, RoleName = "User", isActive = true}
                        );
            modelBuilder.Entity<Qualification>()
                        .HasData(
                            new Qualification{QualificationId = 1, QualificationName = "M.A Tamil", isActive = true},
                            new Qualification{QualificationId = 2, QualificationName = "B.A Tamil", isActive = true},
                            new Qualification{QualificationId = 3, QualificationName = "M.A English", isActive = true},
                            new Qualification{QualificationId = 4, QualificationName = "B.A English", isActive = true},
                            new Qualification{QualificationId = 5, QualificationName = "M.A B.Ed", isActive = true},
                            new Qualification{QualificationId = 6, QualificationName = "B.A B.Ed", isActive = true}
                        );
                      
    }
}