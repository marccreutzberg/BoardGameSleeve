namespace BoardGameSleeveWebsite
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelContext : DbContext
    {
        public ModelContext()
            : base("name=ModelContext")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Game>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Sizes)
                .WithMany(e => e.Games)
                .Map(m => m.ToTable("GameSizeRelation").MapLeftKey("GameID").MapRightKey("SizeID"));

            modelBuilder.Entity<Invoice>()
                .HasMany(e => e.Customers)
                .WithOptional(e => e.Invoice)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Invoice>()
                .HasMany(e => e.Sales)
                .WithRequired(e => e.Invoice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Color)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(6, 2);

            modelBuilder.Entity<Size>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Size>()
                .Property(e => e.SizeDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Size>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Size)
                .WillCascadeOnDelete(false);
        }
    }
}
