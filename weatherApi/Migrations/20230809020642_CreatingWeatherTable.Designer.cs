﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using weatherApi.Data;

#nullable disable

namespace weatherApi.Migrations
{
    [DbContext(typeof(WeatherContext))]
    [Migration("20230809020642_CreatingWeatherTable")]
    partial class CreatingWeatherTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("weatherApi.Models.WeatherModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Weater")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("data")
                        .HasColumnType("date");

                    b.Property<float>("max_temperature")
                        .HasColumnType("real");

                    b.Property<float>("min_temperature")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("WeatherModels");
                });
#pragma warning restore 612, 618
        }
    }
}