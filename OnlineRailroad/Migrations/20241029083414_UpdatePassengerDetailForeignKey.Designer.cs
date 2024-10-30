﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineRailroad.Data;

#nullable disable

namespace OnlineRailroad.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241029083414_UpdatePassengerDetailForeignKey")]
    partial class UpdatePassengerDetailForeignKey
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("OnlineRailroad.Models.Distance", b =>
                {
                    b.Property<int>("DistanceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("DistanceID"));

                    b.Property<decimal>("DistanceKm")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("StationAId")
                        .HasColumnType("int");

                    b.Property<int>("StationBId")
                        .HasColumnType("int");

                    b.HasKey("DistanceID");

                    b.HasIndex("StationAId");

                    b.HasIndex("StationBId");

                    b.ToTable("Distances");
                });

            modelBuilder.Entity("OnlineRailroad.Models.FareRule", b =>
                {
                    b.Property<int>("FareRuleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("FareRuleID"));

                    b.Property<decimal>("BasePricePerKm")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<int>("RouteID")
                        .HasColumnType("int");

                    b.HasKey("FareRuleID");

                    b.HasIndex("RouteID");

                    b.ToTable("FareRules");
                });

            modelBuilder.Entity("OnlineRailroad.Models.PassengerDetail", b =>
                {
                    b.Property<long>("PNRNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("PNRNo"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfTravel")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("int");

                    b.Property<string>("TrainNo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("PNRNo");

                    b.HasIndex("UserID");

                    b.ToTable("PassengerDetails");
                });

            modelBuilder.Entity("OnlineRailroad.Models.Route", b =>
                {
                    b.Property<int>("RouteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("RouteID"));

                    b.Property<string>("RouteName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RouteID");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("OnlineRailroad.Models.Schedule", b =>
                {
                    b.Property<int>("ScheduleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ScheduleID"));

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("RouteID")
                        .HasColumnType("int");

                    b.Property<int>("StationID")
                        .HasColumnType("int");

                    b.Property<int>("StationOrder")
                        .HasColumnType("int");

                    b.Property<string>("TrainNo")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("ScheduleID");

                    b.HasIndex("RouteID");

                    b.HasIndex("StationID");

                    b.HasIndex("TrainNo");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("OnlineRailroad.Models.Station", b =>
                {
                    b.Property<int>("StationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("StationID"));

                    b.Property<string>("StationCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("StationName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("StationID");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("OnlineRailroad.Models.Train", b =>
                {
                    b.Property<string>("TrainNo")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("AC1Seats")
                        .HasColumnType("int");

                    b.Property<int>("AC2Seats")
                        .HasColumnType("int");

                    b.Property<int>("AC3Seats")
                        .HasColumnType("int");

                    b.Property<int>("GeneralSeats")
                        .HasColumnType("int");

                    b.Property<int>("SleeperSeats")
                        .HasColumnType("int");

                    b.Property<string>("TrainName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("TrainNo");

                    b.ToTable("Trains");
                });

            modelBuilder.Entity("OnlineRailroad.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FullName")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("ProfilePicture")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("RegisteredDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OnlineRailroad.Models.Distance", b =>
                {
                    b.HasOne("OnlineRailroad.Models.Station", "StationA")
                        .WithMany()
                        .HasForeignKey("StationAId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OnlineRailroad.Models.Station", "StationB")
                        .WithMany()
                        .HasForeignKey("StationBId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("StationA");

                    b.Navigation("StationB");
                });

            modelBuilder.Entity("OnlineRailroad.Models.FareRule", b =>
                {
                    b.HasOne("OnlineRailroad.Models.Route", "Route")
                        .WithMany("FareRules")
                        .HasForeignKey("RouteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");
                });

            modelBuilder.Entity("OnlineRailroad.Models.PassengerDetail", b =>
                {
                    b.HasOne("OnlineRailroad.Models.User", "User")
                        .WithMany("PassengerDetails")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineRailroad.Models.Schedule", b =>
                {
                    b.HasOne("OnlineRailroad.Models.Route", "Route")
                        .WithMany("Schedules")
                        .HasForeignKey("RouteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineRailroad.Models.Station", "Station")
                        .WithMany("Schedules")
                        .HasForeignKey("StationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineRailroad.Models.Train", "Train")
                        .WithMany("Schedules")
                        .HasForeignKey("TrainNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");

                    b.Navigation("Station");

                    b.Navigation("Train");
                });

            modelBuilder.Entity("OnlineRailroad.Models.Route", b =>
                {
                    b.Navigation("FareRules");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("OnlineRailroad.Models.Station", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("OnlineRailroad.Models.Train", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("OnlineRailroad.Models.User", b =>
                {
                    b.Navigation("PassengerDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
