﻿// <auto-generated />
using System;
using HumansInHarmony.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HumansInHarmony.Migrations
{
    [DbContext(typeof(SongContext))]
    [Migration("20191206015855_Add name to User")]
    partial class AddnametoUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HumansInHarmony.Models.SongInfo", b =>
                {
                    b.Property<string>("TrackId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArtistName");

                    b.Property<string>("ArtworkUrl100");

                    b.Property<string>("CollectionName");

                    b.Property<string>("PreviewUrl");

                    b.Property<string>("TrackName");

                    b.Property<int?>("UserId");

                    b.Property<int?>("UserId1");

                    b.HasKey("TrackId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("SongInfo");
                });

            modelBuilder.Entity("HumansInHarmony.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("HumansInHarmony.Models.SongInfo", b =>
                {
                    b.HasOne("HumansInHarmony.Models.User")
                        .WithMany("Dislikes")
                        .HasForeignKey("UserId");

                    b.HasOne("HumansInHarmony.Models.User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId1");
                });
#pragma warning restore 612, 618
        }
    }
}
