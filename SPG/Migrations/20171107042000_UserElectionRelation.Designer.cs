﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SPG.Data;
using SPG.Models.Db;
using System;

namespace SPG.Migrations
{
    [DbContext(typeof(ElectContext))]
    [Migration("20171107042000_UserElectionRelation")]
    partial class UserElectionRelation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SPG.Models.Db.Candidate", b =>
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

            modelBuilder.Entity("SPG.Models.Db.Election", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Elections");
                });

            modelBuilder.Entity("SPG.Models.Db.ElectionVoter", b =>
                {
                    b.Property<int>("ElectionId");

                    b.Property<int>("VoterId");

                    b.HasKey("ElectionId", "VoterId");

                    b.HasIndex("VoterId");

                    b.ToTable("ElectionVoters");
                });

            modelBuilder.Entity("SPG.Models.Db.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LIK")
                        .IsRequired();

                    b.Property<byte>("Role");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SPG.Models.Db.Candidate", b =>
                {
                    b.HasOne("SPG.Models.Db.Election", "Election")
                        .WithMany("Candidates")
                        .HasForeignKey("ElectionID");
                });

            modelBuilder.Entity("SPG.Models.Db.ElectionVoter", b =>
                {
                    b.HasOne("SPG.Models.Db.Election", "Election")
                        .WithMany("ElectionVoters")
                        .HasForeignKey("ElectionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SPG.Models.Db.User", "Voter")
                        .WithMany("ElectionVoters")
                        .HasForeignKey("VoterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
