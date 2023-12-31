﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessGame.Models
{
    public partial class BusinessGameContext : DbContext
    {
        public BusinessGameContext()
        {
        }

        public BusinessGameContext(DbContextOptions<BusinessGameContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CartProduct> CartProduct { get; set; }
        public virtual DbSet<Carts> Carts { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Shops> Shops { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartProduct>(entity =>
            {
                entity.ToTable("cart_product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCart).HasColumnName("id_cart");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.IdCartNavigation)
                    .WithMany(p => p.CartProduct)
                    .HasForeignKey(d => d.IdCart)
                    .HasConstraintName("FK__cart_prod__id_ca__403A8C7D");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.CartProduct)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("FK__cart_prod__id_pr__412EB0B6");
            });

            modelBuilder.Entity<Carts>(entity =>
            {
                entity.ToTable("carts");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK__carts__id_user__3A81B327");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.ToTable("products");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nome)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("nome");

                entity.Property(e => e.Prezzo)
                    .HasColumnType("money")
                    .HasColumnName("prezzo");

                entity.Property(e => e.Quantita).HasColumnName("quantita");
            });

            modelBuilder.Entity<Shops>(entity =>
            {
                entity.ToTable("shops");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nome");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Shops)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("FK__shops__id_produc__3D5E1FD2");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nome");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}