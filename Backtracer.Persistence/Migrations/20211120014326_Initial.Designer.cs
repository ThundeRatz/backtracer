﻿// <auto-generated />
using System;
using Backtracer.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backtracer.Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211120014326_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Backtracer.Persistence.Model.ConstantEntity", b =>
                {
                    b.Property<int>("ConstantTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("ConstantGroupId")
                        .HasColumnType("integer");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("ConstantTypeId", "ConstantGroupId");

                    b.HasIndex("ConstantGroupId");

                    b.ToTable("Constants", "tracer");
                });

            modelBuilder.Entity("Backtracer.Persistence.Model.ConstantGroupEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("ConstantGroups", "tracer");
                });

            modelBuilder.Entity("Backtracer.Persistence.Model.ConstantTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ConstantTypes", "tracer");
                });

            modelBuilder.Entity("Backtracer.Persistence.Model.ConstantEntity", b =>
                {
                    b.HasOne("Backtracer.Persistence.Model.ConstantGroupEntity", "ConstantGroup")
                        .WithMany("Constants")
                        .HasForeignKey("ConstantGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backtracer.Persistence.Model.ConstantTypeEntity", "ConstantType")
                        .WithMany()
                        .HasForeignKey("ConstantTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ConstantGroup");

                    b.Navigation("ConstantType");
                });

            modelBuilder.Entity("Backtracer.Persistence.Model.ConstantGroupEntity", b =>
                {
                    b.Navigation("Constants");
                });
#pragma warning restore 612, 618
        }
    }
}