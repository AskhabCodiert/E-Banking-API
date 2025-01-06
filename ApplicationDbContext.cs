using E_Banking_API.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Accounts
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountID);
            entity.HasOne<Customer>()
                  .WithMany()
                  .HasForeignKey(e => e.CustomerID)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.AccountType).HasConversion<string>();
            entity.Property(e => e.Status).HasConversion<string>();
        });

        // Branches
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchID);
        });

        // Cards
        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardID);
            entity.HasOne<Account>()
                  .WithMany()
                  .HasForeignKey(e => e.AccountID)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.CardType).HasConversion<string>();
            entity.Property(e => e.Status).HasConversion<string>();
        });

        // Customers
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerID);
        });

        // Employees
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeID);
        });

        // Loans
        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.LoanID);
            entity.HasOne<Customer>()
                  .WithMany()
                  .HasForeignKey(e => e.CustomerID)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.Status).HasConversion<string>();
        });

        // Settings
        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.SettingID);
            entity.HasOne<Customer>()
                  .WithMany()
                  .HasForeignKey(e => e.CustomerID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Subscriptions
        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionID);
            entity.HasOne<Customer>()
                  .WithMany()
                  .HasForeignKey(e => e.CustomerID)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne<Account>()
                  .WithMany()
                  .HasForeignKey(e => e.AccountID)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Transactions
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionID);
            entity.HasOne<Account>()
                  .WithMany()
                  .HasForeignKey(e => e.AccountID)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.TransactionType).HasConversion<string>();
        });
    }
}

// Entity classes
public class Account
{
    [Key]
    public int AccountID { get; set; }
    public int CustomerID { get; set; }
    [Required]
    [StringLength(25)]
    public string AccountIBAN { get; set; }
    [Required]
    [StringLength(20)]
    public string AccountBIC { get; set; }
    public string AccountType { get; set; }
    public decimal Balance { get; set; }
    [StringLength(3)]
    public string Currency { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class Branch
{
    [Key]
    public int BranchID { get; set; }
    [Required]
    [StringLength(100)]
    public string BranchName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
}

public class Card
{
    [Key]
    public int CardID { get; set; }
    public int AccountID { get; set; }
    [Required]
    [StringLength(16)]
    public string CardNumber { get; set; }
    public string CardType { get; set; }
    public DateTime ExpiryDate { get; set; }
    [Required]
    [StringLength(3)]
    public string CVV { get; set; }
    [Required]
    [StringLength(100)]
    public string CardHolderName { get; set; }
    public decimal MonthlyLimit { get; set; }
    public string Status { get; set; }
}



public class Employee
{
    [Key]
    public int EmployeeID { get; set; }
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }
    [Required]
    [StringLength(50)]
    public string LastName { get; set; }
    [Required]
    [StringLength(100)]
    public string Email { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    [Required]
    [StringLength(50)]
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
}

public class Loan
{
    [Key]
    public int LoanID { get; set; }
    public int CustomerID { get; set; }
    public decimal LoanAmount { get; set; }
    public decimal InterestRate { get; set; }
    public int LoanTermMonths { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
}

public class Setting
{
    [Key]
    public int SettingID { get; set; }
    public int CustomerID { get; set; }
    public string NotificationPreferences { get; set; }
    public string PrivacySettings { get; set; }
}

public class Subscription
{
    [Key]
    public int SubscriptionID { get; set; }
    public int CustomerID { get; set; }
    public int AccountID { get; set; }
    [Required]
    [StringLength(100)]
    public string ServiceName { get; set; }
    public decimal MonthlyFee { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool Active { get; set; }
}

public class Transaction
{
    [Key]
    public int TransactionID { get; set; }
    public int AccountID { get; set; }
    public int TargetAccountID { get; set; }
    public string TransactionType { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; }
}
