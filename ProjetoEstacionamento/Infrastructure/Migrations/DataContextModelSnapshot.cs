﻿// <auto-generated />
using System;
using Estacionamento.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Estacionamento.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Estacionamento.Domain.Entities.Vaga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int")
                        .HasColumnName("quantidade");

                    b.Property<int>("TipoVaga")
                        .HasColumnType("int")
                        .HasColumnName("tipo_vaga");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("TipoVaga")
                        .IsUnique();

                    b.ToTable("VAGA", (string)null);
                });

            modelBuilder.Entity("Estacionamento.Domain.Entities.Veiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("Entrada")
                        .HasColumnType("datetime2")
                        .HasColumnName("entrada");

                    b.Property<int>("IdVaga")
                        .HasColumnType("int")
                        .HasColumnName("id_vaga");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("placa");

                    b.Property<DateTime?>("Saida")
                        .HasColumnType("datetime2")
                        .HasColumnName("saida");

                    b.Property<int>("TipoVeiculo")
                        .HasColumnType("int")
                        .HasColumnName("tipo_veiculo");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("IdVaga");

                    b.ToTable("VEICULO", (string)null);
                });

            modelBuilder.Entity("Estacionamento.Domain.Entities.Veiculo", b =>
                {
                    b.HasOne("Estacionamento.Domain.Entities.Vaga", "Vaga")
                        .WithMany("Veiculos")
                        .HasForeignKey("IdVaga")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Vaga");
                });

            modelBuilder.Entity("Estacionamento.Domain.Entities.Vaga", b =>
                {
                    b.Navigation("Veiculos");
                });
#pragma warning restore 612, 618
        }
    }
}
