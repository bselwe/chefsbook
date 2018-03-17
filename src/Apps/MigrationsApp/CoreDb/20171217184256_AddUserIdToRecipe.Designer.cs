﻿// <auto-generated />
using ChefsBook.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ChefsBook.MigrationsApp.Migrations.CoreDb
{
    [DbContext(typeof(CoreDbContext))]
    [Migration("20171217184256_AddUserIdToRecipe")]
    partial class AddUserIdToRecipe
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("core")
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ChefsBook.Core.Models.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Quantity");

                    b.Property<Guid?>("RecipeId");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("ChefsBook.Core.Models.Recipe", b =>
                {
                    b.Property<Guid>("RecipeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(4000);

                    b.Property<TimeSpan?>("Duration");

                    b.Property<string>("Image")
                        .HasMaxLength(2000);

                    b.Property<string>("Notes")
                        .HasMaxLength(2000);

                    b.Property<int?>("Servings");

                    b.Property<string>("Title")
                        .HasMaxLength(200);

                    b.Property<Guid>("UserId");

                    b.HasKey("RecipeId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("ChefsBook.Core.Models.RecipeTag", b =>
                {
                    b.Property<Guid>("TagId");

                    b.Property<Guid>("RecipeId");

                    b.HasKey("TagId", "RecipeId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeTags");
                });

            modelBuilder.Entity("ChefsBook.Core.Models.Step", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<TimeSpan?>("Duration");

                    b.Property<Guid?>("RecipeId");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("Steps");
                });

            modelBuilder.Entity("ChefsBook.Core.Models.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("ChefsBook.Core.Models.Ingredient", b =>
                {
                    b.HasOne("ChefsBook.Core.Models.Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId");
                });

            modelBuilder.Entity("ChefsBook.Core.Models.RecipeTag", b =>
                {
                    b.HasOne("ChefsBook.Core.Models.Recipe")
                        .WithMany("Tags")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ChefsBook.Core.Models.Tag", "Tag")
                        .WithMany("Recipes")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ChefsBook.Core.Models.Step", b =>
                {
                    b.HasOne("ChefsBook.Core.Models.Recipe")
                        .WithMany("Steps")
                        .HasForeignKey("RecipeId");
                });
#pragma warning restore 612, 618
        }
    }
}
