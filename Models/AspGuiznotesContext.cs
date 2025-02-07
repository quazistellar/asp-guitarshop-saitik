using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace guitarshop.Models;

public partial class AspGuiznotesContext : DbContext
{
    public AspGuiznotesContext()
    {
    }

    public AspGuiznotesContext(DbContextOptions<AspGuiznotesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CatalogProd> CatalogProds { get; set; }

    public virtual DbSet<GuitarForm> GuitarForms { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<PosOrder> PosOrders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TypeGuitar> TypeGuitars { get; set; }

    public virtual DbSet<User> Users { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warningTo protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Data Source=DESKTOP-F9O3L5H\\SQLEXPRESS;Initial Catalog=ASP_Guiznotes;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>()
            .HasOne(c => c.Product)
            .WithMany()
            .HasForeignKey(c => c.ProductId);


        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD7B74AC514B2");

            entity.ToTable("Cart");

            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__ProductId__628FA481");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__UserId__619B8048");
        });

        modelBuilder.Entity<CatalogProd>(entity =>
        {
            entity.HasKey(e => e.IdGuitar).HasName("PK__CatalogP__F1A40FCABB4214B1");

            entity.ToTable("CatalogProd");

            entity.Property(e => e.IdGuitar).HasColumnName("ID_Guitar");
            entity.Property(e => e.GuitarDescription).HasColumnType("text");
            entity.Property(e => e.GuitarFormId).HasColumnName("GuitarForm_ID");
            entity.Property(e => e.GuitarImage)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.GuitarPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.GuitarTypeId).HasColumnName("GuitarType_ID");
            entity.Property(e => e.NameGuitar)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.GuitarForm).WithMany(p => p.CatalogProds)
                .HasForeignKey(d => d.GuitarFormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CatalogPr__Guita__5535A963");

            entity.HasOne(d => d.GuitarType).WithMany(p => p.CatalogProds)
                .HasForeignKey(d => d.GuitarTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CatalogPr__Guita__5629CD9C");
        });

        modelBuilder.Entity<GuitarForm>(entity =>
        {
            entity.HasKey(e => e.IdForm).HasName("PK__GuitarFo__195D4BB496D11909");

            entity.ToTable("GuitarForm");

            entity.Property(e => e.IdForm).HasColumnName("ID_Form");
            entity.Property(e => e.FormName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__Orders__EC9FA9552EACF5FC");

            entity.Property(e => e.IdOrder).HasColumnName("ID_Order");
            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.OrderAddress)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.OrderComment).HasColumnType("text");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderTotalCost).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__ID_User__59FA5E80");
        });

        modelBuilder.Entity<PosOrder>(entity =>
        {
            entity.HasKey(e => e.IdPosOrder).HasName("PK__PosOrder__D77BC1CB8A19833C");

            entity.ToTable("PosOrder");

            entity.Property(e => e.IdPosOrder).HasColumnName("ID_PosOrder");
            entity.Property(e => e.IdCatalogProd).HasColumnName("ID_CatalogProd");
            entity.Property(e => e.IdOrder).HasColumnName("ID_Order");

            entity.HasOne(d => d.IdCatalogProdNavigation).WithMany(p => p.PosOrders)
                .HasForeignKey(d => d.IdCatalogProd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PosOrder__ID_Cat__5DCAEF64");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.PosOrders)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PosOrder__ID_Ord__5CD6CB2B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__Roles__43DCD32D26357A82");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B61605197CAB4").IsUnique();

            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TypeGuitar>(entity =>
        {
            entity.HasKey(e => e.IdType).HasName("PK__TypeGuit__DF519A3874B1572B");

            entity.ToTable("TypeGuitar");

            entity.Property(e => e.IdType).HasColumnName("ID_Type");
            entity.Property(e => e.TypeName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__ED4DE44285399542");

            entity.HasIndex(e => e.LoginUser, "UQ__Users__4156FB3106E49C6E").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.Email)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.LoginUser)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NameClient)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.PasswordUser)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PatronimClient)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.SurnameClient)
                .HasMaxLength(70)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__ID_Role__4D94879B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
