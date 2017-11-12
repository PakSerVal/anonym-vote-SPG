﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SPG.Data;
using SPG.Models.Entities;
using System;

namespace SPG.Migrations
{
    [DbContext(typeof(ElectContext))]
    partial class ElectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SPG.Models.Enities.Candidate", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ElectionID");

                    b.Property<string>("FIO")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("ElectionID");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("SPG.Models.Enities.Election", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Elections");
                });

            modelBuilder.Entity("SPG.Models.Enities.ElectionVoter", b =>
                {
                    b.Property<int>("ElectionId");

                    b.Property<int>("VoterId");

                    b.HasKey("ElectionId", "VoterId");

                    b.HasIndex("VoterId");

                    b.ToTable("ElectionVoters");
                });

            modelBuilder.Entity("SPG.Models.Enities.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LIK")
                        .IsRequired();

                    b.Property<string>("Password");

                    b.Property<byte>("Role");

                    b.Property<string>("SignatureModulus");

                    b.Property<string>("SignaturePubExponent");

                    b.Property<string>("UserName");

                    b.Property<bool>("isRegistred");

                    b.Property<string>("salt");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SPG.Models.Enities.Candidate", b =>
                {
                    b.HasOne("SPG.Models.Enities.Election", "Election")
                        .WithMany("Candidates")
                        .HasForeignKey("ElectionID");
                });

            modelBuilder.Entity("SPG.Models.Enities.ElectionVoter", b =>
                {
                    b.HasOne("SPG.Models.Enities.Election", "Election")
                        .WithMany("ElectionVoters")
                        .HasForeignKey("ElectionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SPG.Models.Enities.User", "Voter")
                        .WithMany("ElectionVoters")
                        .HasForeignKey("VoterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
