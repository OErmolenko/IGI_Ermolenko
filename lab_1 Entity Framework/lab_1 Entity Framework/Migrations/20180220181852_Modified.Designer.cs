﻿// <auto-generated />
using lab_1_Entity_Framework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace lab_1EntityFramework.Migrations
{
    [DbContext(typeof(AirportContext))]
    [Migration("20180220181852_Modified")]
    partial class Modified
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("lab_1_Entity_Framework.Models.Aircraft", b =>
                {
                    b.Property<int>("AircraftId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Capasity");

                    b.Property<double>("Carrying");

                    b.Property<string>("Mark");

                    b.Property<int?>("TypeId");

                    b.HasKey("AircraftId");

                    b.HasIndex("TypeId");

                    b.ToTable("Aircrafts");
                });

            modelBuilder.Entity("lab_1_Entity_Framework.Models.Flight", b =>
                {
                    b.Property<int>("FlightId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AircraftId");

                    b.Property<DateTime>("Date");

                    b.Property<double>("FlightTime");

                    b.Property<string>("PlaceArrival");

                    b.Property<string>("PlaceDeparture");

                    b.HasKey("FlightId");

                    b.HasIndex("AircraftId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("lab_1_Entity_Framework.Models.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FlightId");

                    b.Property<string>("FullName");

                    b.Property<string>("NumberPlace");

                    b.Property<string>("PassportData");

                    b.Property<double>("Price");

                    b.HasKey("TicketId");

                    b.HasIndex("FlightId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("lab_1_Entity_Framework.Models.Type", b =>
                {
                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Appointment");

                    b.Property<string>("NameType");

                    b.Property<string>("Restrictions");

                    b.HasKey("TypeId");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("lab_1_Entity_Framework.Models.Aircraft", b =>
                {
                    b.HasOne("lab_1_Entity_Framework.Models.Type", "Type")
                        .WithMany("Aircraft")
                        .HasForeignKey("TypeId");
                });

            modelBuilder.Entity("lab_1_Entity_Framework.Models.Flight", b =>
                {
                    b.HasOne("lab_1_Entity_Framework.Models.Aircraft", "Aircraft")
                        .WithMany("Flights")
                        .HasForeignKey("AircraftId");
                });

            modelBuilder.Entity("lab_1_Entity_Framework.Models.Ticket", b =>
                {
                    b.HasOne("lab_1_Entity_Framework.Models.Flight", "Flight")
                        .WithMany("Tickets")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
